using System.Security.Cryptography;
using System.Text;

namespace api.Repositories;
public class RegisterPlayerRepository : IRegisterPlayerRepository
{
    private const string _collectionName = "players";
    private readonly IMongoCollection<Player>? _collection;
    private readonly ITokenService _tokenService; // save user credential as a token

    public RegisterPlayerRepository(IMongoClient client, IMongoDbSettings dbSettings, ITokenService tokenService)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Player>(_collectionName);
        _tokenService = tokenService;
    }

    public async Task<LoggedInDto?> CreateAsync(RegisterPlayerDto playerInput, CancellationToken cancellationToken)
    {
        bool doesPlayerExist = await _collection.Find<Player>(player =>
        player.Email == playerInput.Email.ToLower().Trim()).AnyAsync(cancellationToken);

        if (doesPlayerExist)
            return null;

        using var hmac = new HMACSHA512();

        Player player = new Player(
            Id: null,
            Email: playerInput.Email.ToLower().Trim(),
            Name: playerInput.Name,
            LastName: playerInput.LastName,
            NationalCode: playerInput.NationalCode,
            Height: playerInput.Height,
            Age: playerInput.Age,
            PasswordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(playerInput.Password)),
            PasswordSalt: hmac.Key
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(player, null, cancellationToken);

        if (player.Id is not null)
        {
            LoggedInDto loggedInDto = new LoggedInDto(
                Id: player.Id,
                Email: player.Email,
                Name: player.Name,
                LastName: player.LastName,
                NationalCode: player.NationalCode,
                Height: player.Height,
                Age: player.Age,
                Token: _tokenService.CreateToken(player)
            );

            return loggedInDto;
        }

        return null;
    }

    public async Task<LoggedInDto?> LoginAsync(LoginDto playerInput, CancellationToken cancellationToken)
    {
        Player player = await _collection.Find<Player>(player =>
        player.Email == playerInput.Email.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

        if (player is null)
            return null;

        using var hmac = new HMACSHA512(player.PasswordSalt!);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(playerInput.Password));

        if (player.PasswordHash is not null && player.PasswordHash.SequenceEqual(ComputeHash))
        {
            if (player.Id is not null)
            {
                return new LoggedInDto(
                    Id: player.Id,
                    Email: player.Email,
                    Name: player.Name,
                    LastName: player.LastName,
                    NationalCode: player.NationalCode,
                    Height: player.Height,
                    Age: player.Age,
                    Token: _tokenService.CreateToken(player)
                );
            }
        }

        return null;
    }
}

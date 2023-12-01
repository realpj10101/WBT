using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api.Repositories;

public class RegisterCoachRepository : IRegisterCoachRepository
{
    private const string _collectionName = "coaches";
    private readonly IMongoCollection<Coach>? _collection;
    private readonly ITokenService _tokenService; // save user credential as a token

    public RegisterCoachRepository(IMongoClient client, IMongoDbSettings dbSettings, ITokenService tokenService)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Coach>(_collectionName);
        _tokenService = tokenService;
    }

    public async Task<LoggedInCoachDto?> CreateCoachAsync(RegisterCoachDto coachInput, CancellationToken cancellationToken)
    {
        bool doesCoachExist = await _collection.Find<Coach>(coach =>
        coach.Email == coachInput.Email.ToLower().Trim()).AnyAsync(cancellationToken);

        if (doesCoachExist)
            return null;

        using var hmac = new HMACSHA512();

        Coach coach = new Coach(
            Id: null,
            Email: coachInput.Email,
            Name: coachInput.Name,
            LastName: coachInput.LastName,
            NationalCode: coachInput.NationalCode,
            Age: coachInput.Age,
            Record: coachInput.Record,
            PasswordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(coachInput.Password)),
            PasswordSalt: hmac.Key
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(coach, null, cancellationToken);

        if (coach.Id is not null)
        {
            LoggedInCoachDto loggedInCoachDto = new LoggedInCoachDto(
                Id: coach.Id,
                Email: coach.Email,
                Name: coach.Name,
                LastName: coach.LastName,
                NationalCode: coach.NationalCode,
                Age: coach.Age,
                Record: coach.Record,
                Token: _tokenService.CreateToken(coach)
            );

            return loggedInCoachDto;
        }

        return null;
    }

    public async Task<LoggedInCoachDto?> LoginCoachAsync(LoginCoachDto coachInput, CancellationToken cancellationToken)
    {
        Coach coach = await _collection.Find<Coach>(coach =>
        coach.Email == coachInput.Email.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

        if (coach is null)
            return null;

        using var hmac = new HMACSHA512(coach.PasswordSalt!);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(coachInput.Password));

        if (coach.PasswordHash is not null && coach.PasswordHash.SequenceEqual(ComputeHash))
        {
            if (coach.Id is not null)
            {
                return new LoggedInCoachDto(
                    Id: coach.Id,
                    Email: coach.Email,
                    Name: coach.Name,
                    LastName: coach.LastName,
                    NationalCode: coach.NationalCode,
                    Age: coach.Age,
                    Record: coach.Record,
                    Token: _tokenService.CreateToken(coach)
                );
            }
        }

        return null;
    }
}
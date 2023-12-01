namespace api.Repositories;
public class PlayerRepository : IPlayerRepository
{
    #region db and token

    IMongoCollection<Player>? _collection;

    public PlayerRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Player>("players");
        // _tokenService = tokenService;
    }

    #endregion

    public async Task<List<PlayerDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Player> players = await _collection.Find<Player>(new BsonDocument()).ToListAsync(cancellationToken);

        List<PlayerDto> playerDtos = new List<PlayerDto>();

        if (players.Any())
        {
            foreach (Player player in players)
            {
                PlayerDto playerDto = new PlayerDto(
                    Id: player.Id!,
                    Email: player.Email,
                    Name: player.Name,
                    LastName: player.LastName,
                    NationalCode: player.NationalCode,
                    Height: player.Height,
                    Age: player.Age
                );

                playerDtos.Add(playerDto);
            }

            return playerDtos;
        }
        return playerDtos;
    }

    public async Task<PlayerDto?> GetByIdAsync(string playerId, CancellationToken cancellationToken)
    {
        Player player = await _collection.Find<Player>(player => player.Id == playerId).FirstOrDefaultAsync(cancellationToken);

        if (player is not null)
            return new PlayerDto(
                Id: player.Id!,
                Email: player.Email,
                Name: player.Name,
                LastName: player.LastName,
                NationalCode: player.NationalCode,
                Height: player.Height,
                Age: player.Age
            );

        return null;
    }
}
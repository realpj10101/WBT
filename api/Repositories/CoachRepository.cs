using System.IO.Pipelines;

namespace api.Repositories;
// 
public class CoachRepository : ICoachRepository
{
    #region db and token
    IMongoCollection<Coach>? _collection;

    public CoachRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Coach>("coaches");
        // _tokenService = tokenService;
    }

    #endregion

    public async Task<List<CoachDto>> GetAllCoachsAsync(CancellationToken cancellationToken)
    {
        List<Coach> coaches = await _collection.Find<Coach>(new BsonDocument()).ToListAsync(cancellationToken);

        List<CoachDto> coachDtos = new List<CoachDto>();

        if (coaches.Any())
        {
            foreach (Coach coach in coaches)
            {
                CoachDto coachDto = new CoachDto(
                    Id: coach.Id!,
                    Email: coach.Email,
                    Name: coach.Name,
                    LastName: coach.LastName,
                    NationalCode: coach.NationalCode,
                    Age: coach.Age,
                    Record: coach.Record
                );

                coachDtos.Add(coachDto);
            }

            return coachDtos;
        }

        return coachDtos;
    }

    public async Task<CoachDto?> GetByCoachIdAsync(string coachId, CancellationToken cancellationToken)
    {
        Coach coach = await _collection.Find<Coach>(coach => coach.Id == coachId).FirstOrDefaultAsync(cancellationToken);

        if (coach is not null)
            return new CoachDto(
                Id: coach.Id!,
                Email: coach.Email,
                Name: coach.Name,
                LastName: coach.LastName,
                NationalCode: coach.NationalCode,
                Age: coach.Age,
                Record: coach.Record
            );

        return null;
    }
}

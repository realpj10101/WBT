namespace api.Interfaces;
public interface ICoachRepository
{
    public Task<List<CoachDto>> GetAllCoachsAsync(CancellationToken cancellationToken);

    public Task<CoachDto?> GetByCoachIdAsync(string coachId, CancellationToken cancellationToken);
}

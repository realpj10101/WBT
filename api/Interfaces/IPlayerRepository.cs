namespace api.Interfaces;
public interface IPlayerRepository
{
    public Task<List<PlayerDto>> GetAllAsync(CancellationToken cancellationToken);
    public Task<PlayerDto?> GetByIdAsync(string playerId, CancellationToken cancellationToken);
}

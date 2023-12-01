namespace api.Interfaces;
public interface IRegisterPlayerRepository
{
    public Task<LoggedInDto?> CreateAsync(RegisterPlayerDto playerInput, CancellationToken cancellationToken);

    public Task<LoggedInDto?> LoginAsync(LoginDto playerInput, CancellationToken cancellationToken);
}
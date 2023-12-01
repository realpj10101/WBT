namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterPlayerController(IRegisterPlayerRepository _registerPlayerRepository) : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> Register(RegisterPlayerDto playerInput, CancellationToken cancellationToken)
    {
        LoggedInDto? playerDto = await _registerPlayerRepository.CreateAsync(playerInput, cancellationToken);

        if (playerDto is null)
            return BadRequest("Email is taken");

        return playerDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginDto playerInput, CancellationToken cancellationToken)
    {
        LoggedInDto? playerDto = await _registerPlayerRepository.LoginAsync(playerInput, cancellationToken);

        if (playerDto is null)
            return Unauthorized("Wrong usrername or password");

        return playerDto;
    }
}

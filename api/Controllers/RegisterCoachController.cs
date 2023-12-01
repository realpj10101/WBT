using System.Security.Cryptography;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterCoachController(IRegisterCoachRepository _registerCoachRepository) : ControllerBase
{

    [HttpPost("register-coach")]
    public async Task<ActionResult<LoggedInCoachDto>> Register(RegisterCoachDto coachInput, CancellationToken cancellationToken)
    {
        LoggedInCoachDto? coachDto = await _registerCoachRepository.CreateCoachAsync(coachInput, cancellationToken);

        if (coachDto is null)
            return BadRequest("Email is taken");

        return coachDto;
    }

    [HttpPost("login-coach")]
    public async Task<ActionResult<LoggedInCoachDto>> Login(LoginCoachDto coachInput, CancellationToken cancellationToken)
    {
        LoggedInCoachDto? coachDto = await _registerCoachRepository.LoginCoachAsync(coachInput, cancellationToken);

        if (coachDto is null)
            return Unauthorized("Wrong username or password");

        return coachDto;
    }
}

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(IPlayerRepository _playerRepository) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<PlayerDto> playerDtos = await _playerRepository.GetAllAsync(cancellationToken);

        if (!playerDtos.Any())
            return NoContent();

        return playerDtos;
    }

    [HttpGet("get-by-id/{playerId}")]
    public async Task<ActionResult<PlayerDto>> GetById(string playerId, CancellationToken cancellationToken)
    {
        PlayerDto? playerDto = await _playerRepository.GetByIdAsync(playerId, cancellationToken);

        if (playerDto is null)
            return NotFound("No player found");

        return playerDto;
    }
}

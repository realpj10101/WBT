namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoachController(ICoachRepository _coachRepository) : ControllerBase
{

    [HttpGet("get-coaches")]
    public async Task<ActionResult<IEnumerable<CoachDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<CoachDto> coachDtos = await _coachRepository.GetAllCoachsAsync(cancellationToken);

        if  (!coachDtos.Any())
            return NoContent();

        return coachDtos;
    }

    [HttpGet("get-by-coach-id/{coachId}")]
    public async Task<ActionResult<CoachDto>> GetById(string coachId, CancellationToken cancellationToken)
    {
        CoachDto? coachDto = await _coachRepository.GetByCoachIdAsync(coachId, cancellationToken);

        if (coachDto is null)
            return NotFound("No Coach Found");

        return coachDto;
    }
}

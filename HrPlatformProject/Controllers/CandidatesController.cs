using HrPlatformProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly CandidatesService _service;

    public CandidatesController(CandidatesService service)
    {
        _service = service;
    }

    [HttpPost("addCandidate")]
    public async Task<IActionResult> AddCandidate(CandidateDto dto)
    {
        var result = await _service.AddCandidate(dto);

        return Ok(result);
    }

    [HttpDelete("deleteCandidate/{candidateId}")]
    public async Task<IActionResult> RemoveCandidate(int candidateId)
    {
        var success = await _service.DeleteCandidate(candidateId);

        if (!success) return NotFound("Candidate not found");

        return Ok("Deleted");

    }

    [HttpPost("updateSkill/{candidateId}/skills/{skillId}")]
    public async Task<IActionResult> AddSkillToCandidate(int candidateId, int skillId)
    {
        var success = await _service.AddSkill(candidateId, skillId);

        if (!success) return NotFound("Candidate or Skill not found");

        return Ok("Skill added");
    }

    [HttpPost("deleteSkill/{candidateId}/skills/{skillId}")]
    public async Task<IActionResult> DeleteSkillToCandidate(int candidateId, int skillId)
    {
        var success = await _service.RemoveSkill(candidateId, skillId);

        if (!success) return NotFound("Candidate or Skill not found");

        return Ok("Skill removed");
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCandidates(string? name, string? skills)
    {
        var result = await _service.Search(name, skills);
        
        return Ok(result);
    }
}
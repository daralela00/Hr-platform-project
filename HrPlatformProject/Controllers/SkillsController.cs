using HrPlatformProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class SkillsController : ControllerBase
{
    private readonly SkillsService _service;

    public SkillsController(SkillsService service)
    {
        _service = service;
    }

    [HttpPost("addSkill")]
    public async Task<IActionResult> AddSkill(SkillDto dto)
    {
        var result = await _service.AddSkill(dto);

        return Ok(result);
    }

    [HttpDelete("deleteSkill/{skillId}")]
    public async Task<IActionResult> DeleteSkill(int skillId)
    {
        var success = await _service.DeleteSkill(skillId);

        if (!success) return NotFound("Skill not found");

        return Ok("Deleted");
    }
}
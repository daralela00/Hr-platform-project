using HrPlatformProject.Models;
using Microsoft.EntityFrameworkCore;

public class SkillsService
{
    private readonly MydbContext _context;

    public SkillsService(MydbContext context)
    {
        _context = context;
    }

    public async Task<Skill> AddSkill(SkillDto dto)
    {
        var skill = new Skill
        {
            Name = dto.Name
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        return skill;
    }

    public async Task<Skill?> GetSkillById(int id)
    {
        return await _context.Skills
            .FirstOrDefaultAsync(s => s.Idskills == id);
    }

    public async Task<bool> DeleteSkill(int id)
    {
        var skill = await GetSkillById(id);

        if (skill == null) return false;

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return true;
    }
}
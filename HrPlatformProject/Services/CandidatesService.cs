using HrPlatformProject.Models;
using Microsoft.EntityFrameworkCore;

public class CandidatesService
{
    private readonly MydbContext _context;

    public CandidatesService(MydbContext context)
    {
        _context = context;
    }

    public async Task<Candidate> AddCandidate(CandidateDto dto)
    {
        var candidate = new Candidate
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth,
            ContactNumber = dto.ContactNumber,
            Email = dto.Email
        };

        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync();

        return candidate;
    }

    public async Task<Candidate?> GetCandidateById(int id)
    {
        return await _context.Candidates
            .Include(c => c.SkillsIdskills)
            .FirstOrDefaultAsync(c => c.Idcandidates == id);
    }

    public async Task<Skill?> GetSkillById(int id)
    {
        return await _context.Skills
            .FirstOrDefaultAsync(s => s.Idskills == id);
    }

    public async Task<bool> DeleteCandidate(int id)
    {
        var candidate = await GetCandidateById(id);

        if (candidate == null)
            return false;

        candidate.SkillsIdskills.Clear();
        _context.Candidates.Remove(candidate);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddSkill(int candidateId, int skillId)
    {
        var candidate = await GetCandidateById(candidateId);
        if (candidate == null) return false;

        var skill = await GetSkillById(skillId);
        if (skill == null) return false;

        candidate.SkillsIdskills.Add(skill);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveSkill(int candidateId, int skillId)
    {
        var candidate = await GetCandidateById(candidateId);
        if (candidate == null) return false;

        var skill = await GetSkillById(skillId);
        if (skill == null) return false;

        candidate.SkillsIdskills.Remove(skill);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<CandidateDto>> Search(string? name, string? skills)
    {
        var query = _context.Candidates
            .Include(c => c.SkillsIdskills)
            .AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(skills))
        {
            var skillList = skills.Split(',').ToList();

            query = query.Where(c =>
                c.SkillsIdskills.Any(s => skillList.Contains(s.Name)));
        }

        return await query.Select(c => new CandidateDto
            {
                Name = c.Name,
                Email = c.Email,
                DateOfBirth = c.DateOfBirth,
                ContactNumber = c.ContactNumber
            }).ToListAsync();
    }
}
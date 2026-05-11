using Xunit;
using Microsoft.EntityFrameworkCore;
using HrPlatformProject.Models;

public class SkillTests
{
    private MydbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<MydbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        return new MydbContext(options);
    }

    [Fact]
    public async Task AddSkill_ShouldAddSkill()
    {
        var context = GetDbContext();

        var skill = new Skill
        {
            Name = "C#"
        };

        context.Skills.Add(skill);
        await context.SaveChangesAsync();

        Assert.Single(context.Skills);
    }
}
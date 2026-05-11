using Xunit;
using Microsoft.EntityFrameworkCore;
using HrPlatformProject.Models;

public class CandidateTests
{
    private MydbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<MydbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        return new MydbContext(options);
    }

    [Fact]
    public async Task AddCandidate_ShouldAddCandidate()
    {
        var context = GetDbContext();

        var candidate = new Candidate
        {
            Name = "Test User",
            Email = "test@gmail.com",
            ContactNumber = 123456,
            DateOfBirth = new DateOnly(2000,1,1)
        };

        context.Candidates.Add(candidate);
        await context.SaveChangesAsync();

        Assert.Equal(1, context.Candidates.Count());
    }
}
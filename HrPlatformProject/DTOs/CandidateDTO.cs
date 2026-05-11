using System.ComponentModel.DataAnnotations;

public class CandidateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public DateOnly? DateOfBirth { get; set; }
    [Required]
    public int? ContactNumber { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
}
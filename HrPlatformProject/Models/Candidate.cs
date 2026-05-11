using System;
using System.Collections.Generic;

namespace HrPlatformProject.Models;

public partial class Candidate
{
    public int Idcandidates { get; set; }

    public string? Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? ContactNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Skill> SkillsIdskills { get; set; } = new List<Skill>();
}

using System;
using System.Collections.Generic;

namespace HrPlatformProject.Models;

public partial class Skill
{
    public int Idskills { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Candidate> CandidatesIdcandidates { get; set; } = new List<Candidate>();
}

using System;
using System.Collections.Generic;

namespace DotNet_Core_Prep_Samples.Entities;

public partial class Project
{
    public int ProjId { get; set; }

    public string? ProjName { get; set; }

    public int? DeptId { get; set; }

    public virtual Department? Dept { get; set; }
}

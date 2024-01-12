using System;
using System.Collections.Generic;

namespace NewAgeHS.Models;

public partial class Class
{
    public string ClassName { get; set; } = null!;

    public int FkemployeeId { get; set; }

    public virtual Staff Fkemp { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

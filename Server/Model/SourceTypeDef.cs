using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class SourceTypeDef
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}

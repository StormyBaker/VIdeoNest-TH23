using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class Profile
{
    public Guid Accountguid { get; set; }

    public string? Username { get; set; }

    public string? Biography { get; set; }

    public string? Image { get; set; }

    public virtual Account Account { get; set; } = null!;
}

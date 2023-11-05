using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class Account
{
    public Guid Guid { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<PlaylistAccess> PlaylistAccesses { get; set; } = new List<PlaylistAccess>();

    public virtual Profile? Profile { get; set; }
}

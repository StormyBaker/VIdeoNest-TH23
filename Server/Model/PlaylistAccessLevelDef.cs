using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class PlaylistAccessLevelDef
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<PlaylistAccess> PlaylistAccesses { get; set; } = new List<PlaylistAccess>();
}

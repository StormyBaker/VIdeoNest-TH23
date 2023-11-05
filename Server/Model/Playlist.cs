using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class Playlist
{
    public Guid Guid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Type { get; set; }

    public virtual ICollection<PlaylistAccess> PlaylistAccesses { get; set; } = new List<PlaylistAccess>();

    public virtual ICollection<PlaylistVideo> PlaylistVideos { get; set; } = new List<PlaylistVideo>();
}

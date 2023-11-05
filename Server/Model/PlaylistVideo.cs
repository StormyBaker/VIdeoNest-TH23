using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class PlaylistVideo
{
    public Guid Playlistguid { get; set; }

    public Guid Videoguid { get; set; }

    public DateTime? Added { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}

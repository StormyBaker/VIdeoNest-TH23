using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class PlaylistAccess
{
    public Guid Playlistguid { get; set; }

    public Guid Accountguid { get; set; }

    public int? Accesslevel { get; set; }

    public virtual PlaylistAccessLevelDef? AccesslevelNavigation { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Playlist Playlist { get; set; } = null!;
}

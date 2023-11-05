using System;
using System.Collections.Generic;

namespace VideoNestServer.Model;

public partial class Video
{
    public Guid Guid { get; set; }

    public string? Sourcelink { get; set; }

    public int? Sourcetype { get; set; }

    public string? Filename { get; set; }

    public string? Title { get; set; }

    public string? Creator { get; set; }

    public virtual ICollection<PlaylistVideo> PlaylistVideos { get; set; } = new List<PlaylistVideo>();

    public virtual SourceTypeDef? SourcetypeNavigation { get; set; }
}

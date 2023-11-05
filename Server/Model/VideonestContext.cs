using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VideoNestServer.Settings;

namespace VideoNestServer.Model;

public partial class VideonestContext : DbContext
{
    public VideonestContext()
    {
    }

    public VideonestContext(DbContextOptions<VideonestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistAccess> PlaylistAccesses { get; set; }

    public virtual DbSet<PlaylistAccessLevelDef> PlaylistAccessLevelDefs { get; set; }

    public virtual DbSet<PlaylistTypeDef> PlaylistTypeDefs { get; set; }

    public virtual DbSet<PlaylistVideo> PlaylistVideos { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<SourceTypeDef> SourceTypeDefs { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.15-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("'2'")
                .HasColumnType("int(11)");
        });

        modelBuilder.Entity<PlaylistAccess>(entity =>
        {
            entity.HasKey(e => new { e.Playlistguid, e.Accountguid })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("PlaylistAccess");

            entity.HasIndex(e => e.Accesslevel, "accesslevel");

            entity.HasIndex(e => e.Accountguid, "accountguid");

            entity.Property(e => e.Playlistguid).HasColumnName("playlistguid");
            entity.Property(e => e.Accountguid).HasColumnName("accountguid");
            entity.Property(e => e.Accesslevel)
                .HasColumnType("int(11)")
                .HasColumnName("accesslevel");

            entity.HasOne(d => d.AccesslevelNavigation).WithMany(p => p.PlaylistAccesses)
                .HasForeignKey(d => d.Accesslevel)
                .HasConstraintName("PlaylistAccess_ibfk_3");

            entity.HasOne(d => d.Account).WithMany(p => p.PlaylistAccesses)
                .HasForeignKey(d => d.Accountguid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlaylistAccess_ibfk_2");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistAccesses)
                .HasForeignKey(d => d.Playlistguid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlaylistAccess_ibfk_1");
        });

        modelBuilder.Entity<PlaylistAccessLevelDef>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("PlaylistAccessLevelDef");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PlaylistTypeDef>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("PlaylistTypeDef");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PlaylistVideo>(entity =>
        {
            entity.HasKey(e => new { e.Playlistguid, e.Videoguid })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(e => e.Videoguid, "fv2");

            entity.Property(e => e.Playlistguid).HasColumnName("playlistguid");
            entity.Property(e => e.Videoguid).HasColumnName("videoguid");
            entity.Property(e => e.Added)
                .HasColumnType("datetime")
                .HasColumnName("added");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistVideos)
                .HasForeignKey(d => d.Playlistguid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fv1");

            entity.HasOne(d => d.Video).WithMany(p => p.PlaylistVideos)
                .HasForeignKey(d => d.Videoguid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fv2");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Accountguid).HasName("PRIMARY");

            entity.ToTable("Profile");

            entity.Property(e => e.Accountguid)
                .ValueGeneratedOnAdd()
                .HasColumnName("accountguid");
            entity.Property(e => e.Biography)
                .HasColumnType("text")
                .HasColumnName("biography");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");

            entity.HasOne(d => d.Account).WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.Accountguid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Profile_ibfk_1");
        });

        modelBuilder.Entity<SourceTypeDef>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("SourceTypeDef");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.HasIndex(e => e.Sourcetype, "sourcetype");

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Creator)
                .HasMaxLength(255)
                .HasColumnName("creator");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .HasColumnName("filename");
            entity.Property(e => e.Sourcelink)
                .HasMaxLength(255)
                .HasColumnName("sourcelink");
            entity.Property(e => e.Sourcetype)
                .HasColumnType("int(11)")
                .HasColumnName("sourcetype");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.SourcetypeNavigation).WithMany(p => p.Videos)
                .HasForeignKey(d => d.Sourcetype)
                .HasConstraintName("Videos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

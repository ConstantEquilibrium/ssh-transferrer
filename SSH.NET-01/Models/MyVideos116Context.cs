using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SSH.NET_01.Models
{
    public partial class KodiContext : DbContext
    {
        public KodiContext()
        {   
        }

        public KodiContext(DbContextOptions<KodiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Art> Art { get; set; }
        public virtual DbSet<Bookmark> Bookmark { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Episode> Episode { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Musicvideo> Musicvideo { get; set; }
        public virtual DbSet<Path> Path { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Seasons> Seasons { get; set; }
        public virtual DbSet<Sets> Sets { get; set; }
        public virtual DbSet<Studio> Studio { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Tvshow> Tvshow { get; set; }
        public virtual DbSet<Uniqueid> Uniqueid { get; set; }

        // Unable to generate entity type for table 'actor_link'. Please see the warning messages.
        // Unable to generate entity type for table 'country_link'. Please see the warning messages.
        // Unable to generate entity type for table 'director_link'. Please see the warning messages.
        // Unable to generate entity type for table 'genre_link'. Please see the warning messages.
        // Unable to generate entity type for table 'movielinktvshow'. Please see the warning messages.
        // Unable to generate entity type for table 'settings'. Please see the warning messages.
        // Unable to generate entity type for table 'stacktimes'. Please see the warning messages.
        // Unable to generate entity type for table 'streamdetails'. Please see the warning messages.
        // Unable to generate entity type for table 'studio_link'. Please see the warning messages.
        // Unable to generate entity type for table 'tag_link'. Please see the warning messages.
        // Unable to generate entity type for table 'tvshowlinkpath'. Please see the warning messages.
        // Unable to generate entity type for table 'version'. Please see the warning messages.
        // Unable to generate entity type for table 'writer_link'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=192.168.12.24;port=3306;database=MyVideos116;user=kodi;password=kodi;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("actor");

                entity.HasIndex(e => e.Name)
                    .HasName("ix_actor_1")
                    .IsUnique();

                entity.Property(e => e.ActorId)
                    .HasColumnName("actor_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ArtUrls)
                    .HasColumnName("art_urls")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Art>(entity =>
            {
                entity.ToTable("art");

                entity.HasIndex(e => new { e.MediaId, e.MediaType, e.Type })
                    .HasName("ix_art");

                entity.Property(e => e.ArtId)
                    .HasColumnName("art_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MediaId)
                    .HasColumnName("media_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MediaType)
                    .HasColumnName("media_type")
                    .HasColumnType("text");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("text");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Bookmark>(entity =>
            {
                entity.HasKey(e => e.IdBookmark)
                    .HasName("PRIMARY");

                entity.ToTable("bookmark");

                entity.HasIndex(e => new { e.IdFile, e.Type })
                    .HasName("ix_bookmark");

                entity.Property(e => e.IdBookmark)
                    .HasColumnName("idBookmark")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdFile)
                    .HasColumnName("idFile")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Player)
                    .HasColumnName("player")
                    .HasColumnType("text");

                entity.Property(e => e.PlayerState)
                    .HasColumnName("playerState")
                    .HasColumnType("text");

                entity.Property(e => e.ThumbNailImage)
                    .HasColumnName("thumbNailImage")
                    .HasColumnType("text");

                entity.Property(e => e.TimeInSeconds).HasColumnName("timeInSeconds");

                entity.Property(e => e.TotalTimeInSeconds).HasColumnName("totalTimeInSeconds");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.HasIndex(e => e.Name)
                    .HasName("ix_country_1")
                    .IsUnique();

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.HasKey(e => e.IdEpisode)
                    .HasName("PRIMARY");

                entity.ToTable("episode");

                entity.HasIndex(e => e.C17)
                    .HasName("ix_episode_bookmark");

                entity.HasIndex(e => e.C19)
                    .HasName("ixEpisodeBasePath");

                entity.HasIndex(e => new { e.C12, e.C13 })
                    .HasName("ix_episode_season_episode");

                entity.HasIndex(e => new { e.IdEpisode, e.IdFile })
                    .HasName("ix_episode_file_1")
                    .IsUnique();

                entity.HasIndex(e => new { e.IdEpisode, e.IdShow })
                    .HasName("ix_episode_show1");

                entity.HasIndex(e => new { e.IdFile, e.IdEpisode })
                    .HasName("id_episode_file_2")
                    .IsUnique();

                entity.HasIndex(e => new { e.IdShow, e.IdEpisode })
                    .HasName("ix_episode_show2");

                entity.Property(e => e.IdEpisode)
                    .HasColumnName("idEpisode")
                    .HasColumnType("int(11)");

                entity.Property(e => e.C00)
                    .HasColumnName("c00")
                    .HasColumnType("text");

                entity.Property(e => e.C01)
                    .HasColumnName("c01")
                    .HasColumnType("text");

                entity.Property(e => e.C02)
                    .HasColumnName("c02")
                    .HasColumnType("text");

                entity.Property(e => e.C03)
                    .HasColumnName("c03")
                    .HasColumnType("text");

                entity.Property(e => e.C04)
                    .HasColumnName("c04")
                    .HasColumnType("text");

                entity.Property(e => e.C05)
                    .HasColumnName("c05")
                    .HasColumnType("text");

                entity.Property(e => e.C06)
                    .HasColumnName("c06")
                    .HasColumnType("text");

                entity.Property(e => e.C07)
                    .HasColumnName("c07")
                    .HasColumnType("text");

                entity.Property(e => e.C08)
                    .HasColumnName("c08")
                    .HasColumnType("text");

                entity.Property(e => e.C09)
                    .HasColumnName("c09")
                    .HasColumnType("text");

                entity.Property(e => e.C10)
                    .HasColumnName("c10")
                    .HasColumnType("text");

                entity.Property(e => e.C11)
                    .HasColumnName("c11")
                    .HasColumnType("text");

                entity.Property(e => e.C12)
                    .HasColumnName("c12")
                    .HasColumnType("varchar(24)");

                entity.Property(e => e.C13)
                    .HasColumnName("c13")
                    .HasColumnType("varchar(24)");

                entity.Property(e => e.C14)
                    .HasColumnName("c14")
                    .HasColumnType("text");

                entity.Property(e => e.C15)
                    .HasColumnName("c15")
                    .HasColumnType("text");

                entity.Property(e => e.C16)
                    .HasColumnName("c16")
                    .HasColumnType("text");

                entity.Property(e => e.C17)
                    .HasColumnName("c17")
                    .HasColumnType("varchar(24)");

                entity.Property(e => e.C18)
                    .HasColumnName("c18")
                    .HasColumnType("text");

                entity.Property(e => e.C19)
                    .HasColumnName("c19")
                    .HasColumnType("text");

                entity.Property(e => e.C20)
                    .HasColumnName("c20")
                    .HasColumnType("text");

                entity.Property(e => e.C21)
                    .HasColumnName("c21")
                    .HasColumnType("text");

                entity.Property(e => e.C22)
                    .HasColumnName("c22")
                    .HasColumnType("text");

                entity.Property(e => e.C23)
                    .HasColumnName("c23")
                    .HasColumnType("text");

                entity.Property(e => e.IdFile)
                    .HasColumnName("idFile")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdSeason)
                    .HasColumnName("idSeason")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdShow)
                    .HasColumnName("idShow")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Userrating)
                    .HasColumnName("userrating")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.IdFile)
                    .HasName("PRIMARY");

                entity.ToTable("files");

                entity.HasIndex(e => new { e.IdPath, e.StrFilename })
                    .HasName("ix_files");

                entity.Property(e => e.IdFile)
                    .HasColumnName("idFile")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("dateAdded")
                    .HasColumnType("text");

                entity.Property(e => e.IdPath)
                    .HasColumnName("idPath")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LastPlayed)
                    .HasColumnName("lastPlayed")
                    .HasColumnType("text");

                entity.Property(e => e.PlayCount)
                    .HasColumnName("playCount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StrFilename)
                    .HasColumnName("strFilename")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.HasIndex(e => e.Name)
                    .HasName("ix_genre_1")
                    .IsUnique();

                entity.Property(e => e.GenreId)
                    .HasColumnName("genre_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie)
                    .HasName("PRIMARY");

                entity.ToTable("movie");

                entity.HasIndex(e => e.C23)
                    .HasName("ixMovieBasePath");

                entity.HasIndex(e => new { e.IdFile, e.IdMovie })
                    .HasName("ix_movie_file_1")
                    .IsUnique();

                entity.HasIndex(e => new { e.IdMovie, e.IdFile })
                    .HasName("ix_movie_file_2")
                    .IsUnique();

                entity.Property(e => e.IdMovie)
                    .HasColumnName("idMovie")
                    .HasColumnType("int(11)");

                entity.Property(e => e.C00)
                    .HasColumnName("c00")
                    .HasColumnType("text");

                entity.Property(e => e.C01)
                    .HasColumnName("c01")
                    .HasColumnType("text");

                entity.Property(e => e.C02)
                    .HasColumnName("c02")
                    .HasColumnType("text");

                entity.Property(e => e.C03)
                    .HasColumnName("c03")
                    .HasColumnType("text");

                entity.Property(e => e.C04)
                    .HasColumnName("c04")
                    .HasColumnType("text");

                entity.Property(e => e.C05)
                    .HasColumnName("c05")
                    .HasColumnType("text");

                entity.Property(e => e.C06)
                    .HasColumnName("c06")
                    .HasColumnType("text");

                entity.Property(e => e.C07)
                    .HasColumnName("c07")
                    .HasColumnType("text");

                entity.Property(e => e.C08)
                    .HasColumnName("c08")
                    .HasColumnType("text");

                entity.Property(e => e.C09)
                    .HasColumnName("c09")
                    .HasColumnType("text");

                entity.Property(e => e.C10)
                    .HasColumnName("c10")
                    .HasColumnType("text");

                entity.Property(e => e.C11)
                    .HasColumnName("c11")
                    .HasColumnType("text");

                entity.Property(e => e.C12)
                    .HasColumnName("c12")
                    .HasColumnType("text");

                entity.Property(e => e.C13)
                    .HasColumnName("c13")
                    .HasColumnType("text");

                entity.Property(e => e.C14)
                    .HasColumnName("c14")
                    .HasColumnType("text");

                entity.Property(e => e.C15)
                    .HasColumnName("c15")
                    .HasColumnType("text");

                entity.Property(e => e.C16)
                    .HasColumnName("c16")
                    .HasColumnType("text");

                entity.Property(e => e.C17)
                    .HasColumnName("c17")
                    .HasColumnType("text");

                entity.Property(e => e.C18)
                    .HasColumnName("c18")
                    .HasColumnType("text");

                entity.Property(e => e.C19)
                    .HasColumnName("c19")
                    .HasColumnType("text");

                entity.Property(e => e.C20)
                    .HasColumnName("c20")
                    .HasColumnType("text");

                entity.Property(e => e.C21)
                    .HasColumnName("c21")
                    .HasColumnType("text");

                entity.Property(e => e.C22)
                    .HasColumnName("c22")
                    .HasColumnType("text");

                entity.Property(e => e.C23)
                    .HasColumnName("c23")
                    .HasColumnType("text");

                entity.Property(e => e.IdFile)
                    .HasColumnName("idFile")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdSet)
                    .HasColumnName("idSet")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Premiered)
                    .HasColumnName("premiered")
                    .HasColumnType("text");

                entity.Property(e => e.Userrating)
                    .HasColumnName("userrating")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Musicvideo>(entity =>
            {
                entity.HasKey(e => e.IdMvideo)
                    .HasName("PRIMARY");

                entity.ToTable("musicvideo");

                entity.HasIndex(e => e.C14)
                    .HasName("ixMusicVideoBasePath");

                entity.HasIndex(e => new { e.IdFile, e.IdMvideo })
                    .HasName("ix_musicvideo_file_2")
                    .IsUnique();

                entity.HasIndex(e => new { e.IdMvideo, e.IdFile })
                    .HasName("ix_musicvideo_file_1")
                    .IsUnique();

                entity.Property(e => e.IdMvideo)
                    .HasColumnName("idMVideo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.C00)
                    .HasColumnName("c00")
                    .HasColumnType("text");

                entity.Property(e => e.C01)
                    .HasColumnName("c01")
                    .HasColumnType("text");

                entity.Property(e => e.C02)
                    .HasColumnName("c02")
                    .HasColumnType("text");

                entity.Property(e => e.C03)
                    .HasColumnName("c03")
                    .HasColumnType("text");

                entity.Property(e => e.C04)
                    .HasColumnName("c04")
                    .HasColumnType("text");

                entity.Property(e => e.C05)
                    .HasColumnName("c05")
                    .HasColumnType("text");

                entity.Property(e => e.C06)
                    .HasColumnName("c06")
                    .HasColumnType("text");

                entity.Property(e => e.C07)
                    .HasColumnName("c07")
                    .HasColumnType("text");

                entity.Property(e => e.C08)
                    .HasColumnName("c08")
                    .HasColumnType("text");

                entity.Property(e => e.C09)
                    .HasColumnName("c09")
                    .HasColumnType("text");

                entity.Property(e => e.C10)
                    .HasColumnName("c10")
                    .HasColumnType("text");

                entity.Property(e => e.C11)
                    .HasColumnName("c11")
                    .HasColumnType("text");

                entity.Property(e => e.C12)
                    .HasColumnName("c12")
                    .HasColumnType("text");

                entity.Property(e => e.C13)
                    .HasColumnName("c13")
                    .HasColumnType("text");

                entity.Property(e => e.C14)
                    .HasColumnName("c14")
                    .HasColumnType("text");

                entity.Property(e => e.C15)
                    .HasColumnName("c15")
                    .HasColumnType("text");

                entity.Property(e => e.C16)
                    .HasColumnName("c16")
                    .HasColumnType("text");

                entity.Property(e => e.C17)
                    .HasColumnName("c17")
                    .HasColumnType("text");

                entity.Property(e => e.C18)
                    .HasColumnName("c18")
                    .HasColumnType("text");

                entity.Property(e => e.C19)
                    .HasColumnName("c19")
                    .HasColumnType("text");

                entity.Property(e => e.C20)
                    .HasColumnName("c20")
                    .HasColumnType("text");

                entity.Property(e => e.C21)
                    .HasColumnName("c21")
                    .HasColumnType("text");

                entity.Property(e => e.C22)
                    .HasColumnName("c22")
                    .HasColumnType("text");

                entity.Property(e => e.C23)
                    .HasColumnName("c23")
                    .HasColumnType("text");

                entity.Property(e => e.IdFile)
                    .HasColumnName("idFile")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Premiered)
                    .HasColumnName("premiered")
                    .HasColumnType("text");

                entity.Property(e => e.Userrating)
                    .HasColumnName("userrating")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Path>(entity =>
            {
                entity.HasKey(e => e.IdPath)
                    .HasName("PRIMARY");

                entity.ToTable("path");

                entity.HasIndex(e => e.IdParentPath)
                    .HasName("ix_path2");

                entity.HasIndex(e => e.StrPath)
                    .HasName("ix_path");

                entity.Property(e => e.IdPath)
                    .HasColumnName("idPath")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("dateAdded")
                    .HasColumnType("text");

                entity.Property(e => e.Exclude)
                    .HasColumnName("exclude")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IdParentPath)
                    .HasColumnName("idParentPath")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NoUpdate)
                    .HasColumnName("noUpdate")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ScanRecursive)
                    .HasColumnName("scanRecursive")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StrContent)
                    .HasColumnName("strContent")
                    .HasColumnType("text");

                entity.Property(e => e.StrHash)
                    .HasColumnName("strHash")
                    .HasColumnType("text");

                entity.Property(e => e.StrPath)
                    .HasColumnName("strPath")
                    .HasColumnType("text");

                entity.Property(e => e.StrScraper)
                    .HasColumnName("strScraper")
                    .HasColumnType("text");

                entity.Property(e => e.StrSettings)
                    .HasColumnName("strSettings")
                    .HasColumnType("text");

                entity.Property(e => e.UseFolderNames)
                    .HasColumnName("useFolderNames")
                    .HasColumnType("tinyint(1)");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("rating");

                entity.HasIndex(e => new { e.MediaId, e.MediaType })
                    .HasName("ix_rating");

                entity.Property(e => e.RatingId)
                    .HasColumnName("rating_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MediaId)
                    .HasColumnName("media_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MediaType)
                    .HasColumnName("media_type")
                    .HasColumnType("text");

                entity.Property(e => e.Rating1).HasColumnName("rating");

                entity.Property(e => e.RatingType)
                    .HasColumnName("rating_type")
                    .HasColumnType("text");

                entity.Property(e => e.Votes)
                    .HasColumnName("votes")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Seasons>(entity =>
            {
                entity.HasKey(e => e.IdSeason)
                    .HasName("PRIMARY");

                entity.ToTable("seasons");

                entity.HasIndex(e => new { e.IdShow, e.Season })
                    .HasName("ix_seasons");

                entity.Property(e => e.IdSeason)
                    .HasColumnName("idSeason")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdShow)
                    .HasColumnName("idShow")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");

                entity.Property(e => e.Season)
                    .HasColumnName("season")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Userrating)
                    .HasColumnName("userrating")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Sets>(entity =>
            {
                entity.HasKey(e => e.IdSet)
                    .HasName("PRIMARY");

                entity.ToTable("sets");

                entity.Property(e => e.IdSet)
                    .HasColumnName("idSet")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StrOverview)
                    .HasColumnName("strOverview")
                    .HasColumnType("text");

                entity.Property(e => e.StrSet)
                    .HasColumnName("strSet")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Studio>(entity =>
            {
                entity.ToTable("studio");

                entity.HasIndex(e => e.Name)
                    .HasName("ix_studio_1")
                    .IsUnique();

                entity.Property(e => e.StudioId)
                    .HasColumnName("studio_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.HasIndex(e => e.Name)
                    .HasName("ix_tag_1")
                    .IsUnique();

                entity.Property(e => e.TagId)
                    .HasColumnName("tag_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Tvshow>(entity =>
            {
                entity.HasKey(e => e.IdShow)
                    .HasName("PRIMARY");

                entity.ToTable("tvshow");

                entity.Property(e => e.IdShow)
                    .HasColumnName("idShow")
                    .HasColumnType("int(11)");

                entity.Property(e => e.C00)
                    .HasColumnName("c00")
                    .HasColumnType("text");

                entity.Property(e => e.C01)
                    .HasColumnName("c01")
                    .HasColumnType("text");

                entity.Property(e => e.C02)
                    .HasColumnName("c02")
                    .HasColumnType("text");

                entity.Property(e => e.C03)
                    .HasColumnName("c03")
                    .HasColumnType("text");

                entity.Property(e => e.C04)
                    .HasColumnName("c04")
                    .HasColumnType("text");

                entity.Property(e => e.C05)
                    .HasColumnName("c05")
                    .HasColumnType("text");

                entity.Property(e => e.C06)
                    .HasColumnName("c06")
                    .HasColumnType("text");

                entity.Property(e => e.C07)
                    .HasColumnName("c07")
                    .HasColumnType("text");

                entity.Property(e => e.C08)
                    .HasColumnName("c08")
                    .HasColumnType("text");

                entity.Property(e => e.C09)
                    .HasColumnName("c09")
                    .HasColumnType("text");

                entity.Property(e => e.C10)
                    .HasColumnName("c10")
                    .HasColumnType("text");

                entity.Property(e => e.C11)
                    .HasColumnName("c11")
                    .HasColumnType("text");

                entity.Property(e => e.C12)
                    .HasColumnName("c12")
                    .HasColumnType("text");

                entity.Property(e => e.C13)
                    .HasColumnName("c13")
                    .HasColumnType("text");

                entity.Property(e => e.C14)
                    .HasColumnName("c14")
                    .HasColumnType("text");

                entity.Property(e => e.C15)
                    .HasColumnName("c15")
                    .HasColumnType("text");

                entity.Property(e => e.C16)
                    .HasColumnName("c16")
                    .HasColumnType("text");

                entity.Property(e => e.C17)
                    .HasColumnName("c17")
                    .HasColumnType("text");

                entity.Property(e => e.C18)
                    .HasColumnName("c18")
                    .HasColumnType("text");

                entity.Property(e => e.C19)
                    .HasColumnName("c19")
                    .HasColumnType("text");

                entity.Property(e => e.C20)
                    .HasColumnName("c20")
                    .HasColumnType("text");

                entity.Property(e => e.C21)
                    .HasColumnName("c21")
                    .HasColumnType("text");

                entity.Property(e => e.C22)
                    .HasColumnName("c22")
                    .HasColumnType("text");

                entity.Property(e => e.C23)
                    .HasColumnName("c23")
                    .HasColumnType("text");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Userrating)
                    .HasColumnName("userrating")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Uniqueid>(entity =>
            {
                entity.ToTable("uniqueid");

                entity.HasIndex(e => new { e.MediaType, e.Value })
                    .HasName("ix_uniqueid2");

                entity.HasIndex(e => new { e.MediaId, e.MediaType, e.Type })
                    .HasName("ix_uniqueid1");

                entity.Property(e => e.UniqueidId)
                    .HasColumnName("uniqueid_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MediaId)
                    .HasColumnName("media_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MediaType)
                    .HasColumnName("media_type")
                    .HasColumnType("text");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("text");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("text");
            });
        }
    }
}

namespace DavidsList.Data
{
    using DavidsList.Data.DbModels;
    using DavidsList.Data.DbModels.ManyToManyTables;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DavidsListDbContext : IdentityDbContext
    {
        public DavidsListDbContext(DbContextOptions<DavidsListDbContext> options)
            : base(options)
        {
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> ReggedUsers { get; init; }
        public DbSet<Movie> Movies { get; init; }
        public DbSet<Comment> Comments { get; init; }
        public DbSet<GenreUser> GenresUsers { get; init; }
        public DbSet<SeenMovie> SeenMovies { get; init; }
        public DbSet<LikedMovie> LikedMovies { get; init; }
        public DbSet<FavouritedMovie> FavouritedMovies { get; init; }
        public DbSet<DislikedMovie> DislikedMovies { get; init; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GenreUser>(ent =>
            {
                ent.HasKey(e => new { e.UserId, e.GenreId });

                ent.Property(e => e.UserId).HasColumnName("UserId");
                ent.Property(e => e.GenreId).HasColumnName("GenreId");
            });

            builder.Entity<LikedMovie>(ent =>
            {
                ent.HasKey(e => new { e.UserId, e.MovieId });

                ent.Property(e => e.UserId).HasColumnName("UserId");
                ent.Property(e => e.MovieId).HasColumnName("MovieId");
            });

            builder.Entity<DislikedMovie>(ent =>
            {
                ent.HasKey(e => new { e.UserId, e.MovieId });

                ent.Property(e => e.UserId).HasColumnName("UserId");
                ent.Property(e => e.MovieId).HasColumnName("MovieId");
            });

            builder.Entity<FavouritedMovie>(ent =>
            {
                ent.HasKey(e => new { e.UserId, e.MovieId });

                ent.Property(e => e.UserId).HasColumnName("UserId");
                ent.Property(e => e.MovieId).HasColumnName("MovieId");
            });

            builder.Entity<SeenMovie>(ent =>
            {
                ent.HasKey(e => new { e.UserId, e.MovieId });

                ent.Property(e => e.UserId).HasColumnName("UserId");
                ent.Property(e => e.MovieId).HasColumnName("MovieId");
            });

           

            base.OnModelCreating(builder);
        }
    }
}

using AmalgamateLabs.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmalgamateLabs.Base
{
    public class SQLiteDBContext : DbContext
    {
        public virtual DbSet<Blog> Blogs{ get; set; }
        public virtual DbSet<ContactForm> ContactForms { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<WebGLGame> WebGLGames { get; set; }
        public virtual DbSet<StoreApp> StoreApps { get; set; }
        public virtual DbSet<AppException> AppExceptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "AmalgamateLabs.db" };
            var connectionString = connectionStringBuilder.ToString();

            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }

        public async Task<List<Movie>> GetMovies(ILogger logger)
        {
            const int DAYS_TO_PERSIST = 1; // Don't spam Amazon.

            // Check if stored movies are still relevant.
            List<Movie> instanceMovies = await Movies.ToListAsync();
            if (instanceMovies.Count > 0)
            {
                DateTime lastSavedDate = instanceMovies.OrderByDescending(m => m.MovieId).Select(m => m.Timestamp).First();
                if (Math.Ceiling((DateTime.Now - lastSavedDate).TotalDays) <= DAYS_TO_PERSIST)
                {
                    return instanceMovies;
                }
            }

            try
            {
                XDocument responseXML = await Services.GetAmazonItemDataFromKeywords(new List<string>() { "movies" }, SystemConfigs.First());
                List<Movie> newMovies = await Movie.GetMoviesFromXML(responseXML);

                // Delete old movies.
                if (newMovies.Count >= 6)
                {
                    foreach (Movie movie in Movies)
                    {
                        Movies.Remove(movie);
                    }
                    await SaveChangesAsync();
                }

                // Store new movies.
                Movies.AddRange(newMovies);
                await SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.Error($"Failed to retrieve and update Amazon movies: {e.Message}");
            }

            return await Movies.ToListAsync();
        }
    }
}

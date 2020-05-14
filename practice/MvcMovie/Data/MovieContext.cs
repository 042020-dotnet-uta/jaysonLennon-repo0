using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data {
    public class MvcMovieContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options) : base(options)
        {

        }

    }

}

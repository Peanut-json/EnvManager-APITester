using EnvironmentManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentManager.Data
{
    public class EnvironmentDBContext : DbContext
    {
        public EnvironmentDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EnvironmentModel> Environment { get; set; }
    }
}

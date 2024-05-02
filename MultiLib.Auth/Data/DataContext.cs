using Microsoft.EntityFrameworkCore;
using MultiLib.Auth.models;

namespace MultiLib.Auth.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        
        public DbSet<User> Users => Set<User>();
        

    }
}

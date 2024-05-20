using Microsoft.EntityFrameworkCore;

namespace MultiLib.EcommerceAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //public DbSet<movieModel> movieStorage => Set<movieModel>();
       

    }
}

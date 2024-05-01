using MainService.models;
using Microsoft.EntityFrameworkCore;

namespace MainService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Movie> movies => Set<Movie>();
        public DbSet<User> Users => Set<User>();
        public DbSet<post> posts => Set<post>();



        public DbSet<movieModel> movieStorage => Set<movieModel>();
        public DbSet<analysis> analysis => Set<analysis>();
        public DbSet<catagoryModel> Catagory => Set<catagoryModel>();
        public DbSet<itemCatagories> itemCatagories => Set<itemCatagories>();
        public DbSet<languageModel> languages => Set<languageModel>();
        public DbSet<itemLanguages> itemLanguages => Set<itemLanguages>();

        public DbSet<seriesModel> series => Set<seriesModel>();
        public DbSet<seasonsModel> seasons => Set<seasonsModel>();
        public DbSet<episodesModel> episodes => Set<episodesModel>();


    }
}

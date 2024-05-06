
using Microsoft.EntityFrameworkCore;
using MultiLib.TaskManagementAPI.models;

namespace MultiLib.TaskManagementAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TaskModel> task => Set<TaskModel>();
        public DbSet<PriorityModel> priority => Set<PriorityModel>();
        public DbSet<StatusModel> status => Set<StatusModel>();
    }
}

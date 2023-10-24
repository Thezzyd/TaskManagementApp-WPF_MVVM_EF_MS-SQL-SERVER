using Microsoft.EntityFrameworkCore;

namespace TaskManagementApp.Data
{
    internal class AppDbContextFactory
    {
        private readonly string _connectionString;
        public AppDbContextFactory(string connectionString){
            _connectionString = connectionString;
        }

        public AppDbContext CreateDbContext(){
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;
            return new AppDbContext(options);
        }
    }

}

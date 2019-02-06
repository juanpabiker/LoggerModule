using Microsoft.EntityFrameworkCore;

namespace Logger.Db
{
    public class LoggerContext : DbContext
    {
        private string _conection;

        public LoggerContext(string conection)
        {
            _conection = conection;
        }

        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conection);
        }
    }
}

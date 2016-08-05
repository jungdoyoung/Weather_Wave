namespace Weather.Model
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;

    public partial class DbCon : DbContext
    {
        public DbCon()
            : base("name=OCeanWeather")
        {
        }

        //public  DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public  DbSet<ErrorLogs> ErrorLogs { get; set; }
        public  DbSet<OceanFileChecks> OceanFileChecks { get; set; }
        public  DbSet<Oceans> Oceans { get; set; }
        public  DbSet<WaveFileChecks> WaveFileChecks { get; set; }
        public  DbSet<Waves> Waves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
       

        }
    }
}

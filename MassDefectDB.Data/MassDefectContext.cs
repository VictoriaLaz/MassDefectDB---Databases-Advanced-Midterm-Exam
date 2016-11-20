using MassDefectDB.Models;

namespace MassDefectDB.Data
{
    
    using System.Data.Entity;
   

    public class MassDefectContext : DbContext
    {
        
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public DbSet<Anomaly> Anomalies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<SolarSystem> SolarSystems { get; set; }
        public DbSet<Star> Stars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasMany(anomaly => anomaly.Victims)
                .WithMany(person => person.Anomalies)
                .Map(configuration =>
                {
                    configuration.MapLeftKey("AnomalyId");
                    configuration.MapRightKey("PersonId");
                    configuration.ToTable("AnomalyVictims");
                });
            base.OnModelCreating(modelBuilder);
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
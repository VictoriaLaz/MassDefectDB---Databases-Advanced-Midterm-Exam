
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MassDefectDB.Models
{
    public class Planet
    {
        public Planet()
        {
            this.Persons = new HashSet<Person>();
            this.AnomalyDestination = new HashSet<Anomaly>();
            this.AnomalyOrigin = new HashSet<Anomaly>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Star Sun { get; set; }

        [Required]
        public virtual SolarSystem SolarSystem { get; set; }
                       
        public virtual ICollection<Person> Persons { get; set; }
                       
        public virtual ICollection<Anomaly> AnomalyOrigin { get; set; }
                       
        public virtual ICollection<Anomaly> AnomalyDestination { get; set; }
    }
}


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefectDB.Models
{
    public class Anomaly
    {
        public Anomaly()
        {
            this.Victims = new HashSet<Person>();
        }
        public int Id { get; set; }

        [InverseProperty("AnomalyOrigin")]
        public virtual Planet OriginPlanet { get; set; }
             
        [InverseProperty("AnomalyDestination")]
        public virtual Planet TeleportPlanet { get; set; }
               
        public virtual ICollection<Person> Victims { get; set; }
    }
}


using System.IO;
using System.Linq;
using MassDefectDB.Data;
using Newtonsoft.Json;

namespace JSONExport
{
    class JSONExport
    {
        static void Main(string[] args)
        {
            var context = new MassDefectContext();

            ExportPlanetsWhichAreNotAnomalyOrigins(context);
            ExportPeopleWhichHaveNotBeenVictims(context);
            ExportTopAnomaly(context);
        }

        private static void ExportTopAnomaly(MassDefectContext context)
        {
            var extractedAnomaly = context.Anomalies
                .OrderByDescending(anomaly => anomaly.Victims.Count)
                .Take(1)
                .Select(anomaly => new
                {
                    id = anomaly.Id,
                    originPlanet = anomaly.OriginPlanet.Name,
                    teleportPlanet = anomaly.TeleportPlanet.Name,
                    victimsCount = anomaly.Victims.Count,
                });
            var anomalyAsJson = JsonConvert.SerializeObject(extractedAnomaly, Formatting.Indented);
            File.WriteAllText("../../anomaly.json", anomalyAsJson);
        }

        private static void ExportPeopleWhichHaveNotBeenVictims(MassDefectContext context)
        {
            var extractedPersons = context.Persons
                .Where(person => !person.Anomalies.Any())
                .Select(person => new
                {
                    name = person.Name,
                    homePlanet = person.HomePlanet.Name
                });
            var personsAsJson = JsonConvert.SerializeObject(extractedPersons, Formatting.Indented);
            File.WriteAllText("../../people.json", personsAsJson);
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigins(MassDefectContext context)
        {
            var exportedPlanets = context.Planets
                .Where(planet => !planet.AnomalyOrigin.Any())
                .Select(planet => new
                {
                    name = planet.Name
                });
            var planetsAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText("../../planets.json", planetsAsJson);
        }
    }
}

using System;
using System.Linq;
using System.Xml.XPath;
using MassDefectDB.Data;
using MassDefectDB.Models;

namespace XmlImports
{
    using System.Xml.Linq;
    public class XmlImports
    {
        private const string NewAnomaliesPath = "../../../datasets/new-anomalies.xml";
        static void Main(string[] args)
        {
            var xml = XDocument.Load(NewAnomaliesPath);
            var anomalies = xml.XPathSelectElements("anomalies/anomaly");

            var context = new MassDefectContext();
            foreach (var anomaly in anomalies)
            {
                ImportAnomalyAndVictims(anomaly, context);
            }
        }

        private static void ImportAnomalyAndVictims(XElement anomalyNode, MassDefectContext context)
        {
            var originPlanetName = anomalyNode.Attribute("origin-planet");
            var teleportPlanetName = anomalyNode.Attribute("teleport-planet");

            if (originPlanetName != null && teleportPlanetName != null)
            {
                Anomaly anomalyEntity = new Anomaly()
                {
                    OriginPlanet = GetPlanetByName(originPlanetName.Value, context),
                    TeleportPlanet = GetPlanetByName(teleportPlanetName.Value, context)
                };
                if (anomalyEntity.OriginPlanet != null && anomalyEntity.TeleportPlanet != null)
                {
                    context.Anomalies.Add(anomalyEntity);
                    Console.WriteLine("Successfully imported anomaly.");
                }
                else
                {
                    Console.WriteLine("Error: Invalid data.");
                }
                var victims = anomalyNode.XPathSelectElements("victims/victim");
                foreach (var victim in victims)
                {
                    ImportVictim(victim, context, anomalyEntity);
                }
             } 
            else
            {
                Console.WriteLine("Error: Invalid data.");
            }
              
        }

        private static void ImportVictim(XElement victimNode, MassDefectContext context, Anomaly anomalyEntity)
        {
            var name = victimNode.Attribute("name");
            if (name != null)
            {
                Person personEntity = GetPersonByName(name.Value, context);
                if (personEntity != null)
                {
                    anomalyEntity.Victims.Add(personEntity);
                }
                else
                {
                    Console.WriteLine("Error: Invalid data.");
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid data.");
            }
        }

        private static Person GetPersonByName(string nameValue, MassDefectContext context)
        {
            Person person = context.Persons.FirstOrDefault(p => p.Name == nameValue);
            return person;
        }

        private static Planet GetPlanetByName(string value, MassDefectContext context)
        {
            Planet planet = context.Planets.FirstOrDefault(p => p.Name == value);
            return planet;
        }
    }
}

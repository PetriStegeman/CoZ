using CoZ.Models.Locations;
using System.Collections.Generic;
using System.Linq;

namespace CoZ.Utility
{
    public static class MapFactory
    {
        public static ICollection<Location> CreateMap()
        {
            ICollection<Location> map = RandomLandFactory.CreateRandomLand(40, 40, 3); //new List<Location>();
            
             
            //AddMountainsToMap(map);
            //AddAltitudeToMap(map);
            //AddSpecialLocationsToMap(map);
            //AddExtraOceansToMap(map);
            //AddRiversToMap(map);
            
            return map;
        }

        private static ICollection<Location> AddRiversToMap(ICollection<Location> map)
        {
            RiverFactory riverBuilder = new RiverFactory();
            var finalMap = riverBuilder.CreateRiver(10, 10, map);
            map = riverBuilder.CreateRiver(15, 12, finalMap);
            finalMap = riverBuilder.CreateRiver(12, 6, map);
            return finalMap;
        }

        private static ICollection<Location> AddExtraOceansToMap(ICollection<Location> map)
        {
            var newOceans = map.Where(l => l.Altitude < 0).ToList();
            var result = new List<Location>();
            foreach (var location in newOceans)
            {
                result.Add(new Ocean(location.XCoord, location.YCoord));
            }
            UpdateMap(map, result);
            return result;
        }

        private static ICollection<Location> AddOceansToMap(ICollection<Location> map)
        {
            var oceans = map.Where(o => o.XCoord == 1 || o.XCoord == 240 || o.YCoord == 1 || o.YCoord == 40);
            var result = new List<Location>();
            foreach (var ocean in oceans)
            {
                result.Add(new Ocean(ocean.XCoord, ocean.YCoord));
            }
            UpdateMap(map, result);
            return result;
        }

        private static void AddMountainsToMap(ICollection<Location> map)
        {
            var mountainRange = MountainFactory.CreateMountainRange(5, 15, 8, map);
            UpdateMap(map, mountainRange);
            var mountainRangeTwo = MountainFactory.CreateMountainRange(10, 10, 20, map);
            UpdateMap(map, mountainRangeTwo);
            var mountainRangeThree = MountainFactory.CreateMountainRange(15, 5, 8, map);
            UpdateMap(map, mountainRangeThree);
            var mountainRangeFour = MountainFactory.CreateMountainRange(5, 5, 8, map);
            UpdateMap(map, mountainRangeFour);
            var mountainRangeFive = MountainFactory.CreateMountainRange(15, 15, 8, map);
            UpdateMap(map, mountainRangeFive);
        }

        private static void AddAltitudeToMap(ICollection<Location> map)
        {
            foreach (var location in map)
            {
                if (!(location is Ocean))
                {
                    location.AddAltitudeToLocation(map);
                }
            }
        }

        private static ICollection<Location> AddSpecialLocationsToMap(ICollection<Location> map)
        {
            var result = new List<Location>();
            result.Add(new StartingLocation(10, 16));
            result.Add(new Town(10, 13));
            result.Add(new Lair(6, 6));
            UpdateMap(map, result);
            return result;
        }

        private static void AddAltitudeToLocation(this Location location, ICollection<Location> map)
        {
            int numberOfMountainsNearby = MountainsNearby(map, location);
            int numberOfOceansNearby = OceansNearby(map, location);
            location.Altitude = location.Altitude + numberOfMountainsNearby * 5 - numberOfOceansNearby * 5;
        }

        private static int MountainsNearby(ICollection<Location> map, Location location)
        {
            return map.Where(l =>
                    (l.XCoord == location.XCoord + 1 && l.YCoord == location.YCoord && l is Mountain) ||
                    (l.XCoord == location.XCoord + 1 && l.YCoord == location.YCoord + 1 && l is Mountain) ||
                    (l.XCoord == location.XCoord + 2 && l.YCoord == location.YCoord && l is Mountain) ||
                    (l.XCoord == location.XCoord - 1 && l.YCoord == location.YCoord && l is Mountain) ||
                    (l.XCoord == location.XCoord - 1 && l.YCoord == location.YCoord - 1 && l is Mountain) ||
                    (l.XCoord == location.XCoord - 2 && l.YCoord == location.YCoord && l is Mountain) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord + 1 && l is Mountain) ||
                    (l.XCoord == location.XCoord - 1 && l.YCoord == location.YCoord + 1 && l is Mountain) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord + 2 && l is Mountain) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord - 1 && l is Mountain) ||
                    (l.XCoord == location.XCoord + 1 && l.YCoord == location.YCoord - 1 && l is Mountain) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord - 2 && l is Mountain)
                ).Count();
        }

        private static int OceansNearby(ICollection<Location> map, Location location)
        {
            return map.Where(l =>
                    (l.XCoord == location.XCoord + 1 && l.YCoord == location.YCoord && l is Ocean) ||
                    (l.XCoord == location.XCoord + 1 && l.YCoord == location.YCoord + 1 && l is Ocean) ||
                    (l.XCoord == location.XCoord + 2 && l.YCoord == location.YCoord && l is Ocean) ||
                    (l.XCoord == location.XCoord - 1 && l.YCoord == location.YCoord && l is Ocean) ||
                    (l.XCoord == location.XCoord - 1 && l.YCoord == location.YCoord - 1 && l is Ocean) ||
                    (l.XCoord == location.XCoord - 2 && l.YCoord == location.YCoord && l is Ocean) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord + 1 && l is Ocean) ||
                    (l.XCoord == location.XCoord - 1 && l.YCoord == location.YCoord + 1 && l is Ocean) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord + 2 && l is Ocean) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord - 1 && l is Ocean) ||
                    (l.XCoord == location.XCoord + 1 && l.YCoord == location.YCoord - 1 && l is Ocean) ||
                    (l.XCoord == location.XCoord && l.YCoord == location.YCoord - 2 && l is Ocean)
                ).Count();
        }

        private static void UpdateMap(ICollection<Location> map, List<Location> locationsToAdd)
        {
            foreach (var location in locationsToAdd)
            {
                var oldLocation = map.SingleOrDefault(l => l.XCoord == location.XCoord && l.YCoord == location.YCoord);
                map.Remove(oldLocation);
                map.Add(location);
            }
        }

        private static void UpdateMap(ICollection<Location> map, Location locationToAdd)
        {
                var oldLocation = map.SingleOrDefault(l => l.XCoord == locationToAdd.XCoord && l.YCoord == locationToAdd.YCoord);
                map.Remove(oldLocation);
                map.Add(locationToAdd);
        }

        private static Location GetTile(int x, int y)
        {
            Location result = null;
            switch (RngThreadSafe.Next(1,3))
            {
                case 1: result = new Forest(x, y); break;
                case 2: result = new Plains(x, y); break;
                default: result = new Lake(x, y); break;
            }
            return result;
        }
    }
}
using CoZ.Models;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public static class MapFactory
    {
        public static ICollection<Location> CreateMap()
        {
            ICollection<Location> map = new List<Location>();
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    if ((i == 20 && j == 20) | (i == 20 && j == 17) | (i == 6 && j == 6))
                    {
                        continue;
                    }
                    Location location = GetTile(i, j);
                    map.Add(location);
                }
            }
            var mountainRange = MountainFactory.CreateMountainRange(5, 15, 8, map);
            UpdateMap(map, mountainRange);
            var mountainRangeTwo = MountainFactory.CreateMountainRange(10, 10, 8, map);
            UpdateMap(map, mountainRangeTwo);
            AddSpecialLocations(map);
            foreach (var location in map)
            {
                if (!(location is Ocean))
                {
                    location.AddAltitudeToLocation(map);
                }
            }
            return map;
        }

        private static void AddSpecialLocations(ICollection<Location> map)
        {
            var startLocation = new StartingLocation(20, 20);
            var town = new Town(20, 17);
            var lair = new Lair(6, 6);
            map.Add(startLocation);
            map.Add(town);
            map.Add(lair);
        }

        private static void AddAltitudeToLocation(this Location location, ICollection<Location> map)
        {
            int numberOfMountainsNearby = MountainsNearby(map, location);
            int numberOfOceansNearby = OceansNearby(map, location);
            location.Altitude = location.Altitude + numberOfMountainsNearby * 5 - numberOfOceansNearby * 10;
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

        private static Location GetTile(int x, int y)
        {
            Location result = null;
            switch (RngThreadSafe.Next(1,5))
            {
                case 1: result = new Forest(x, y); break;
                case 2: result = new Plains(x, y); break;
                case 3: result = new River(x, y); break;
                default: result = new Lake(x, y); break;
            }
            return result;
        }
    }
}
using CoZ.Models;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public class MapFactory
    {
        public static ICollection<Location> CreateSmallMap(string id)
        {
            ICollection <Location> map = new List<Location>();
            for (int i = 0; i <= 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Location location = GetTile(i, j);
                    map.Add(location);
                }
            }
            return map;
        }

        public static ICollection<Location> CreateMediumMap(string id)
        {
            ICollection<Location> map = new List<Location>();
            for (int i = 0; i <= 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Location location = GetTile(i, j);
                    map.Add(location);
                }
            }
            return map;
        }

        public static ICollection<Location> CreateBigMap(string id)
        {
            ICollection<Location> map = new List<Location>();
            for (int i = 0; i <= 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Location location = GetTile(i, j);
                    map.Add(location);
                }
            }
            return map;
        }

        private static Location GetTile(int x, int y)
        {
            Location result = null;
            switch (RngThreadSafe.Next(1,5))
            {
                case 1: result = new Forest(x, y); break;
                case 2: result = new Plains(x, y); break;
                case 3: result = new Forest(x, y); break;
                case 4: result = new Forest(x, y); break;
                default: result = new Forest(x, y); break;
            }
            return result;
        }
    }
}
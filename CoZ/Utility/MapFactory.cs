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
        public static Map CreateSmallMap(string id)
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Collection<Location>();
            for (int i = 0; i <= 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Location location = GetTile(i, j);
                    worldMap.WorldMap.Add(location);
                }
            }
            return worldMap;
        }

        public static Map CreateMediumMap(string id)
        {
            {
                Map worldMap = new Map();
                worldMap.WorldMap = new Collection<Location>();
                for (int i = 0; i <= 40; i++)
                {
                    for (int j = 0; j < 40; j++)
                    {
                        Location location = GetTile(i, j);
                        worldMap.WorldMap.Add(location);
                    }
                }
                return worldMap;
            }
        }

        public static Map CreateBigMap(string id)
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Collection<Location>();
            for (int i = 0; i <= 60; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    Location location = GetTile(i, j);
                    worldMap.WorldMap.Add(location);
                }
            }
            return worldMap;
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
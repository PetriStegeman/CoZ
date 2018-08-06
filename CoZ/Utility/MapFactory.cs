﻿using CoZ.Models;
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
            ICollection <Location> map = new List<Location>();
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    if ((i == 19 && j == 19) | (i == 19 && j == 16) | (i == 6 && j == 6))
                    {
                        continue;
                    }
                    else if((i == 1 || i == 20) || (j == 1 || j == 20))
                        {
                        Location location = new Ocean(i, j);
                        map.Add(location);
                    }
                    else
                    {
                        Location location = GetTile(i, j);
                        map.Add(location);
                    }
                }
            }
            var startLocation = new StartingLocation(19, 19);
            var town = new Town(19, 16);
            var lair = new Lair(6, 6);
            map.Add(startLocation);
            map.Add(town);
            map.Add(lair);

            RiverFactory riverBuilder = new RiverFactory();
            var finalMap = riverBuilder.CreateRiver(10, 10, map);
            map = riverBuilder.CreateRiver(17, 12, finalMap);
            finalMap = riverBuilder.CreateRiver(12, 4, map);
            return finalMap;
        }

        private static Location GetTile(int x, int y)
        {
            Location result = null;
            switch (RngThreadSafe.Next(1,5))
            {
                case 1: result = new Forest(x, y); break;
                case 2: result = new Plains(x, y); break;
                case 3: result = new Plains(x, y); break;
                case 4: result = new Mountain(x, y); break;
                default: result = new Lake(x, y); break;
            }
            return result;
        }
    }
}
using CoZ.Models.Locations;
using System.Collections.Generic;
using System.Linq;

namespace CoZ.Utility
{
    public static class MountainFactory
    {

        /// <summary>
        /// Create a MountainRange starting at coordinates x and y to the map.
        /// </summary>
        /// <param name="X coordinate"></param>
        /// <param name="Y coordinate"></param>
        /// <param name="size of mountain"></param>
        /// <param name="map"></param>
        public static List<Location> CreateMountainRange(int coordinateX, int coordinateY, int size, ICollection<Location> map)
        {       
            List<Location> mountainRange = new List<Location>();
            mountainRange.Add(new Mountain(coordinateX, coordinateY));
            for (int i = 0; i < size; i++)                
            {
                var tileToExpandFrom = NextTile(mountainRange, map);
                var newMountain = NewMountain(tileToExpandFrom.XCoord, tileToExpandFrom.YCoord, mountainRange, map);
                if (newMountain != null)
                {
                    mountainRange.Add(newMountain);
                }
            }
            return mountainRange;
        }

        //Randomly selects the next tile to expand from within the MountainRange
        private static Location NextTile(List<Location> mountainRange, ICollection<Location> map)
        {
            var randomNumber = RngThreadSafe.Next(0, mountainRange.Count);
            var tileToExpandFrom = mountainRange[randomNumber];
            return tileToExpandFrom;
        }

        //Randomly select a location surrounding the tile you expand from
        private static Location NewMountain(int x, int y, List<Location> mountainRange, ICollection<Location> map)
        {
            var possibleNewMountains = map.Where(l => 
                    (l.XCoord == x+1 && l.YCoord == y && !(l is Mountain)) ||
                    (l.XCoord == x-1 && l.YCoord == y && !(l is Mountain)) ||
                    (l.XCoord == x && l.YCoord == y+1 && !(l is Mountain)) ||
                    (l.XCoord == x && l.YCoord == y-1 && !(l is Mountain))
                ).ToList();
            if (possibleNewMountains.Count() == 0)
            {
                return null;
            }
            var randomIndex = RngThreadSafe.Next(0, possibleNewMountains.Count());
            var newLocation = possibleNewMountains[randomIndex];
            return new Mountain(newLocation.XCoord, newLocation.YCoord);
        }

    }
}

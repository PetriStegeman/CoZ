using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.FactoryItems
{
    public class coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }


        /// <summary>
        /// returns true if there are multiple accounts of the coordinate in the given list
        /// </summary>
        public bool CheckCoordinateDuplicacy(List<coordinate> listOfCoordinates)
        {
            if (listOfCoordinates == null)
            { return false; }

            foreach (coordinate coördinate in listOfCoordinates)
            {
                if (coördinate == null)
                {
                    continue;
                }
                if (listOfCoordinates.Where(r => r.X == this.X && r.Y == this.Y).Count() > 1) // checks if searching for the object from the start and from the back returns the same position
                {
                    return true;
                }
            }
            return false;
        }
    }
}
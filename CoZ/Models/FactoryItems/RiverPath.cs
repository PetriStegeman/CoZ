using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.FactoryItems
{
    public class RiverPath
    {
        public List<coördinate> riverCoördinates { get; set; }

        public int LastX()
        {
            return this.riverCoördinates[(this.riverCoördinates.Count - 1)].X;
        }

        public int LastY()
        {
            return this.riverCoördinates[(this.riverCoördinates.Count - 1)].Y;
        }

        public RiverPath()
        {
            this.riverCoördinates = new List<coördinate>();
        }

        public RiverPath(coördinate coördinate)
        {
            this.riverCoördinates = new List<coördinate>();
            this.riverCoördinates.Add(coördinate);
        }

        public RiverPath(RiverPath riverPath, coördinate newCoördinate)
        {
            this.riverCoördinates = new List<coördinate>();
            foreach (coördinate oldCoördinate in riverPath.riverCoördinates)
            { riverCoördinates.Add(new coördinate(oldCoördinate.X, oldCoördinate.Y)); }

            this.riverCoördinates.Add(newCoördinate);
        }


        public bool MoveCheck(int coördinateX, int coördinateY, int currentRiverAltitude, ICollection<Location> mapHolder)
        {
            if (coördinateX < 1 || coördinateX > 20) { return false; }
            else if (coördinateY < 1 || coördinateY > 20) { return false; }
            else if (mapHolder.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude > currentRiverAltitude) { return false; }
            else { return true; }
        }

        public bool EndCheck(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is River) { return true; }
            else if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is Ocean) { return true; }
            else if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is Lake) { return true; }
            else { return false; }
        }
    }


    public class coördinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public coördinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
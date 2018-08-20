using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.FactoryItems
{
    public class RiverPath
    {
        public List<coordinate> riverCoördinates { get; set; }
        public bool hasAnEndNode { get; set; }

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
            this.riverCoördinates = new List<coordinate>();
        }

        public RiverPath(coordinate coördinate)
        {
            this.riverCoördinates = new List<coordinate>();
            this.riverCoördinates.Add(coördinate);
        }

        public RiverPath(RiverPath riverPath, coordinate newCoördinate)
        {
            this.riverCoördinates = new List<coordinate>();
            foreach (coordinate oldCoördinate in riverPath.riverCoördinates)
            { riverCoördinates.Add(new coordinate(oldCoördinate.X, oldCoördinate.Y)); }

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
            if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is River)
            {
                this.hasAnEndNode = true;
                return true;
            }
            else if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is Ocean)
            {
                this.hasAnEndNode = true;
                return true;
            }
            else if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is Lake)
            {
                this.hasAnEndNode = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /*public endNodeFoundChecker(this RiverPath riverPath)
    {
        bool check;
        check = ;
        foreach (coördinate coördinate in riverPath.riverCoördinates)
        {

        }


        if (check)
            return false;
        else
        {
            return true;
        }
        
    }
    */


}
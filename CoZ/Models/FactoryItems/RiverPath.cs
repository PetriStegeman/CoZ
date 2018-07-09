﻿using CoZ.Models.Locations;
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


        public RiverPath ReturnNewRiverPath(coördinate newCoördinate)
        {
            RiverPath newRiverPath = this;
            newRiverPath.riverCoördinates.Add(newCoördinate);
            return newRiverPath;
        }


        public bool MoveCheck(int coördinateX, int coördinateY, int currentRiverAltitude, ICollection<Location> mapHolder)
        {
            if (coördinateX >= 0 && coördinateX < 20) { return false; }
            else if (coördinateY >= 0 && coördinateY < 20) { return false; }
            else if (mapHolder.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude <= currentRiverAltitude) { return false; }
            else { return true; }
        }

        public bool EndCheck(int coördinateX, int coördinateY, ICollection<Location> mapHolder)
        {
            if (mapHolder.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is River) { return true; }
            else if (mapHolder.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is Ocean) { return true; }
            else if (mapHolder.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY) is Lake) { return true; }
            else { return false; }
        }
    }


    public class coördinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public coördinate(int x, int y)
        {
            int X = x;
            int Y = y;
        }
    }
}
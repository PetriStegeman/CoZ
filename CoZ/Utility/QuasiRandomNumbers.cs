using CoZ.Models.FactoryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CoZ.Utility.RngThreadSafe;

namespace CoZ.Utility
{
    public static class QuasiRandomNumbers
    {
        public static List<coordinate> CreateQuasiRandomNumbers(int maxX, int maxY, int amountOfPoints)
        {
            List<coordinate> QuasiRandomNumbers = new List<coordinate>();
            coordinate point1 = new coordinate(5, 10);
            QuasiRandomNumbers.Add(point1);
            if (amountOfPoints == 1)
            {
                return QuasiRandomNumbers;
            }
            coordinate point2 = new coordinate(30, 8);
            QuasiRandomNumbers.Add(point2);
            if (amountOfPoints == 2)
            {
                return QuasiRandomNumbers;
            }
            for (int i = 2; i < amountOfPoints; i++)
            {
                QuasiRandomNumbers.Add(ReturnNewCoordinate(QuasiRandomNumbers, maxX, maxY));
            }

            return QuasiRandomNumbers;
        }

        private static coordinate ReturnNewCoordinate(List<coordinate> QuasiRandomNumbers, int maxX, int maxY)
        {
            List<double> newXList = new List<double>();
            List<double> newYList = new List<double>();
            foreach (coordinate coordinate in QuasiRandomNumbers)
            {
                newXList.Add(coordinate.X);
                newYList.Add(coordinate.Y);
            }

            int xCoord = Convert.ToInt32(ReturnBestAxisPoint(newXList, maxX));
            int yCoord = Convert.ToInt32(ReturnBestAxisPoint(newYList, maxY));

            return new coordinate(xCoord, yCoord);


        }

        private static double ReturnBestAxisPoint(List<double> newXList, int maxX)
        {
            double meanX = newXList.Sum()/newXList.Count();
            double squaredStandardDeviationX = 0;
            foreach (double X in newXList)
            {
                bool positiveNumber = (X < (maxX / 2)) ? true : false;
                double deviation = X - meanX;
                double squaredDeviation = Math.Pow(deviation, 2);
                double realSquaredDeviation = (positiveNumber) ? squaredDeviation : (squaredDeviation * -1);
                squaredStandardDeviationX = +realSquaredDeviation;
            }

            double meanSquaredStandardDeviationX = squaredStandardDeviationX / newXList.Count();
            double meanStandardDeviationX = Math.Sqrt(meanSquaredStandardDeviationX);
            double newX = meanX + meanStandardDeviationX;

            if (newX < 0)
            { return maxX - newX; }
            else if (newX > maxX)
            { return newX - maxX; }
            else
            { return newX; }
        }
    }
}
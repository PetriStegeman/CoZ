using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public class Rng
    {
        public static bool RandomNumberGenerator(int procent)
        {
            bool result = false;
            Random rndm = new Random();
            if (procent <= rndm.Next(1, 100))
            {
                result = true;
            }
            return result;
        }
    }
}
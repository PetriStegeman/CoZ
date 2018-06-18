using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Ocean : Location
    {
        public override Monster AddMonster()
        {
            Monster result = null;
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    result = new Boar();
                    break;
                case 2:
                    result = new Boar();
                    break;
                case 3:
                    result = new Boar();
                    break;
                case 4:
                    result = new Boar();
                    break;
                default:
                    result = new Boar();
                    break;
            }
            return result;
        }
    }
}
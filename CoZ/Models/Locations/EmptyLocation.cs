using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoZ.Models.Monsters;

namespace CoZ.Models.Locations
{
    public class EmptyLocation : Location
    {
        public override Monster AddMonster()
        {
            return null;
        }
    }
}
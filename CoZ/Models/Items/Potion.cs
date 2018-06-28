using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public abstract class Potion : Item
    {
        public int PortionsRemaining { get; set; }

        public abstract void Consume();
    }
}
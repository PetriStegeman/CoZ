using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public abstract class Armor : Item
    {
        public int Hp { get; set; }
        public bool CanBeArmor { get; set; }
    }
}
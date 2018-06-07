using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public bool IsSellable { get; set; }
    }
}
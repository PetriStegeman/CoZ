using CoZ.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public static class ItemFactory
    {
        public static Item CreateItem()
        {
            if (RngThreadSafe.Next(1, 100) <= 20)
            {
                return GetItem();
            }
            return null;
        }

        private static Item GetItem()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    return new HealingPotion();
                    break;
                case 2:
                    return new WoodenSword();
                    break;
                default:
                    return new LinenArmor();
            }
        }

    }
}
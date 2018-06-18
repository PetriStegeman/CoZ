using CoZ.Models.Items;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Plains : Location
    {
        public Plains(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Items = new List<Item>();
            this.Description = "You find yourself in rolling plains";
            this.ShortDescription = "a plains";
            if (RngThreadSafe.Next(1, 5) == 1)
            {
                this.Monsters = new Monster[] { AddMonster() };
            }
        }
        public Plains()
        {
            this.Monsters = new Monster[3];
            this.Items = new Item[3];
            this.Description = "You find yourself in rolling plains";
            this.ShortDescription = "a plains";
            if (RngThreadSafe.Next(1, 5) == 1)
            {
                this.Monsters = new Monster[] { AddMonster() };
            }
        }

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
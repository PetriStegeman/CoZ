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
        public Plains()
        {
            this.Monsters = new Monster[3];
            this.Items = new Item[3];
            this.Description = "You find yourself in rolling plains";
            this.ShortDescription = "a plains";
            MonsterFactory.CreateMonster(this);
        }
    }
}
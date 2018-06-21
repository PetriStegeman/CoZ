using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public class Boar : Monster
    {
        public Boar()
        {
            this.Name = "Boar";
            this.Level = RngThreadSafe.Next(1, 3);
            this.MaxHp = 5 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = this.Level;
            this.MonsterInit();
        }
    }
}
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
            Random rndm = new Random();
            this.Name = "Boar";
            this.Level = rndm.Next(1, 3);
            this.Hp = 5 + this.Level;
            this.Strength = this.Level;
            this.MonsterInit();
        }
    }
}
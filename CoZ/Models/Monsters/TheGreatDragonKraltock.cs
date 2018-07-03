using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public class TheGreatDragonKraltock : Monster
    {
        public override Monster CloneMonster()
        {
            var output = new TheGreatDragonKraltock();
            output.MonsterId = this.MonsterId;
            output.Name = this.Name;
            output.Level = this.Level;
            output.CurrentHp = this.CurrentHp;
            output.MaxHp = this.MaxHp;
            output.Strength = this.Strength;
            output.Gold = this.Gold;
            return output;
        }

        public TheGreatDragonKraltock(Location location)
        {
            this.Name = "The Geat Dragon Kraltock";
            this.Level = 20;
            this.MaxHp = 50;
            this.CurrentHp = this.MaxHp;
            this.Strength = 15;
            this.Speed = 7;
            this.MonsterInit();
            this.Location = location;
        }

        public TheGreatDragonKraltock()
        {
            this.Name = "Kobold Warrior";
            this.Level = RngThreadSafe.Next(3, 6);
            this.MaxHp = 10 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = 1 + this.Level;
            this.Speed = 2;
            this.MonsterInit();
        }
    }
}
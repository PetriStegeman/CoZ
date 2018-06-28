using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoZ.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public virtual ICollection<Item> Inventory { get; set; }
        public virtual ICollection<Location> Map { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        //Statistics
        public int Experience { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int MaxMp { get; set; }
        public int CurrentMp { get; set; }
        public int Strength { get; set; }
        public int Magic { get; set; }
        public int Speed { get; set; }

        public void Camp()
        {
            this.CurrentHp = this.MaxHp;
        }

        public string Attack(Monster monster)
        {
            int monsterDamage = 0;
            int characterDamage = 0;
            string result = "";
            if (RngThreadSafe.Next(1, 10) >= 4 - (this.Speed-monster.Speed))
            {
                this.CurrentHp -= monster.Strength;
                monsterDamage = monster.Strength;
            }
            if (RngThreadSafe.Next(1, 10) >= 4 - (monster.Speed - this.Speed))
            {
                monster.CurrentHp -= this.Strength;
                characterDamage = this.Strength; 
            }
            result = "You inflicted " + characterDamage + " damage to the boar. The boar inflicted " + monsterDamage + " damage to you.";
            return result;
        }

        public void Victory(Monster monster)
        {
            this.Gold += monster.Gold;
            this.Experience += monster.Level;
            //TODO Add extra rewards logic
            //foreach (Item item in monster.Loot)
            //{
            //    this.Inventory.Add(item);
            //}
        }

        public bool IsLevelUp()
        {
            if (this.Experience > ((this.Level+1) * 5))
            {
                LevelUp();
                return true;
            }
            else return false;
        }

        public void LevelUp()
        {
            this.Experience = 0;
            this.Level += 1;
            this.MaxHp += 1;
            this.Strength += 1;
        }

        public void CopyCharacter(Character desiredResult)
        {
            this.CharacterId = desiredResult.CharacterId;
            this.UserId = desiredResult.UserId;
            this.Name = desiredResult.Name;
            this.Gold = desiredResult.Gold;
            this.XCoord = desiredResult.XCoord;
            this.YCoord = desiredResult.YCoord;
            this.Experience = desiredResult.Experience;
            this.Level = desiredResult.Level;
            this.MaxHp = desiredResult.MaxHp;
            this.CurrentHp = desiredResult.CurrentHp;
            this.MaxMp = desiredResult.MaxMp;
            this.CurrentMp = desiredResult.CurrentMp;
            this.Strength = desiredResult.Strength;
            this.Magic = desiredResult.Magic;
            this.Speed = desiredResult.Speed;
        }

        public Character(string id, string name)
        {
            this.Name = name;
            this.Map = MapFactory.CreateBigMap();
            this.UserId = id;
            this.XCoord = 20;
            this.YCoord = 20;
            this.CurrentHp = 10;
            this.MaxHp = 10;
            this.Strength = 2;
            this.Speed = 1;
        }

        public Character(){}
    }
}
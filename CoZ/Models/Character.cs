using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
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
        public int Insanity { get; set; }

        public void Camp()
        {
            this.CurrentHp = this.MaxHp;
        }

        public void Attack(Monster monster)
        {
            this.CurrentHp -= monster.Strength;
            monster.CurrentHp -= this.Strength; //TODO Weapon modifier, miss chance

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
            this.Insanity = desiredResult.Insanity;
        }

        public Character(string id)
        {
            this.Map = MapFactory.CreateBigMap(id);
            this.UserId = id;
            this.XCoord = 10;
            this.YCoord = 10;
            this.CurrentHp = 10;
            this.MaxHp = 10;
            this.Strength = 3;
        }

        public Character(){}
    }
}
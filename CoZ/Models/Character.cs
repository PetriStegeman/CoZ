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
            if (this.CurrentHp < this.MaxHp/2)
            {
                this.CurrentHp = this.MaxHp/2;
            }
        }

        public string Attack(Monster monster, List<Item> inventory)
        {
            int monsterDamage = MonsterDamage(monster, this, inventory);
            int characterDamage = CharacterDamage(monster, this, inventory);
            return AttackResult(characterDamage, monsterDamage, monster.Name);
        }

        private int CharacterDamage(Monster monster, Character character, List<Item> inventory)
        {
            int characterDamage = 0;
            if (RngThreadSafe.Next(1, 10) >= 4 - (character.Speed - monster.Speed))
            {
                Weapon weapon = (Weapon)inventory.SingleOrDefault(w => w.IsEquiped == true && w is Weapon);
                if (weapon != null)
                {
                    monster.CurrentHp -= weapon.Strength;
                }
                monster.CurrentHp -= character.Strength;
                characterDamage = character.Strength;
            }

            return characterDamage;
        }

        private int MonsterDamage(Monster monster, Character character, List<Item> inventory)
        {
            int monsterDamage = 0;
            if (RngThreadSafe.Next(1, 10) >= 4 - (monster.Speed - character.Speed))
            {
                Armor armor = (Armor)inventory.SingleOrDefault(w => w.IsEquiped == true && w is Armor);
                if (armor != null)
                {
                    character.CurrentHp += armor.Hp;
                }
                character.CurrentHp -= monster.Strength;
                monsterDamage = monster.Strength;
            }

            return monsterDamage;
        }

        public string AttackResult(int characterDamage, int monsterDamage, string monsterName)
        {
            var characterResult = "";
            var monsterResult = "";
            var result = "";
            if (characterDamage == 0)
            {
                characterResult = "Your attack missed!";
            }
            else
            {
                characterResult = "You inflicted " + characterDamage + " damage to the " + monsterName + ".";
            }
            if (monsterDamage == 0)
            {
                monsterResult = "The " + monsterName + "'s attack missed!";
            }
            else
            {
                monsterResult = "The " + monsterName + " inflicted " + monsterDamage + " damage to you.";
            }
            result = characterResult + monsterResult;
            return result;
        }

        public void Victory(Monster monster)
        {
            this.Gold += monster.Gold;
            this.Experience += monster.Level;
        }

        public bool IsLevelUp()
        {
            if (this.Experience > (this.Level * 5))
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
            this.Map = MapFactory.CreateMap();
            this.Gold = 5;
            this.Level = 1;
            this.UserId = id;
            this.XCoord = 10;
            this.YCoord = 16;
            this.CurrentHp = 12;
            this.MaxHp = 12;
            this.Strength = 3;
            this.Speed = 1;
            this.Inventory = new List<Item>();
            this.Inventory.Add(new HealingPotion());
        }

        public Character(){}
    }
}
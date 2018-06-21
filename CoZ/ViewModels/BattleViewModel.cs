using CoZ.Models;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.ViewModels
{
    public class BattleViewModel
    {
        public int CharacterCurrentHp { get; set; }
        public int CharacterMaxHp { get; set; }
        public int CharacterCurrentMp { get; set; }
        public int CharacterMaxMp { get; set; }
        public int CharacterLevel { get; set; }
        public int MonsterCurrentHp { get; set; }
        public int MonsterMaxHp { get; set; }
        public int MonsterLevel { get; set; }
        public string MonsterName { get; set; }

        public BattleViewModel() { }

        public BattleViewModel(Monster monster, Character character)
        {
            this.CharacterCurrentHp = character.CurrentHp;
            this.CharacterMaxHp = character.MaxHp;
            this.CharacterCurrentMp = character.CurrentMp;
            this.CharacterMaxMp = character.MaxMp;
            this.CharacterLevel = character.Level;
            this.MonsterCurrentHp = monster.CurrentHp;
            this.MonsterMaxHp = monster.MaxHp;
            this.MonsterLevel = monster.Level;
            this.MonsterName = monster.Name;
        }

    }
}
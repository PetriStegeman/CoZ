﻿using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class MonsterRepository //: Repository
    {

        public Monster FindMonsterByLocation(Location location)
        {
            Monster output;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var monster = dbContext.Locations.Single(d => d.LocationId == location.LocationId).Monster;
                if (monster == null)
                {
                    output = null;
                }
                else
                {
                    output = monster.CloneMonster();
                }
            }
            return output;
        }

        public void DeleteMonster(Monster monster)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = dbContext.Monsters.Find(monster.MonsterId);
                if (originalMonster.Loot != null)
                {
                    dbContext.Items.Remove(dbContext.Items.Find(originalMonster.Loot.ItemId));
                }
                dbContext.Monsters.Remove(originalMonster);
                dbContext.SaveChanges();
            }
        }

        public void Updatemonster(Monster monster)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = dbContext.Monsters.Find(monster.MonsterId);
                originalMonster.CopyMonster(monster);
                dbContext.SaveChanges();
            }
        }

        internal void AddMonsters(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                ICollection<Location> map = character.Map;
                foreach (var location in map)
                {
                    var monster = MonsterFactory.CreateMonster(location);
                    if (monster != null)
                    {
                        var item = ItemFactory.CreateItem();
                        if (item != null)
                        {
                            monster.Loot = item;
                        }
                        dbContext.Monsters.Add(monster);
                        location.Monster = monster;
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}
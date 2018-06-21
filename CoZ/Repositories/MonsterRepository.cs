using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
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
                var originalLocation = dbContext.Locations.Find(location.LocationId);
                var monster = originalLocation.Monsters.First();
                output = monster.CloneMonster();
            }
            return output;
        }

        public void DeleteMonster(Monster monster)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = dbContext.Monsters.Find(monster.MonsterId);
                foreach (var item in originalMonster.Loot)
                {
                    dbContext.Items.Remove(dbContext.Items.Find(item.ItemId));
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
    }
}
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
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = dbContext.Locations.Find(location.LocationId);
                return originalLocation.Monsters.First();
            }
        }

        public void DeleteMonster(Monster monster)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                if (dbContext.Monsters.Where(c => c.MonsterId == monster.MonsterId).Count() != 0)
                {
                    var originalMonster = dbContext.Monsters.Find(monster.MonsterId);
                    dbContext.Monsters.Remove(originalMonster);
                    dbContext.SaveChanges();
                }
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
using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CoZ.Repositories
{
    public class MonsterRepository //: Repository
    {

        public async Task<Monster> FindMonsterByLocation(Location location)
        {
            Monster output;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var monster = await Task.Run(() => dbContext.Locations.SingleOrDefault(d => d.LocationId == location.LocationId).Monster);
                if (monster == null)
                {
                    output = null;
                }
                else
                {
                    output = await Task.Run(() => monster.CloneMonster());
                }
            }
            return output;
        }

        public async Task DeleteMonster(Monster monster)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = await Task.Run(() => dbContext.Monsters.Find(monster.MonsterId));
                if (originalMonster.Loot != null)
                {
                    await Task.Run(() => dbContext.Items.Remove(dbContext.Items.Find(originalMonster.Loot.ItemId)));
                }
                await Task.Run(() => dbContext.Monsters.Remove(originalMonster));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Updatemonster(Monster monster)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = await Task.Run(() => dbContext.Monsters.Find(monster.MonsterId));
                await Task.Run(() => originalMonster.CopyMonster(monster));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddFinalBoss(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                ICollection<Location> map = character.Map;
                var location = map.Single(l => l.XCoord == 6 && l.YCoord == 6);
                var originalLocation = await Task.Run(() => dbContext.Locations.Find(location.LocationId));
                var newMonster = new TheGreatDragonKraltock(originalLocation);
                await Task.Run(() => dbContext.Monsters.Add(newMonster));
                originalLocation.Monster = newMonster;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddMonsters(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                ICollection<Location> map = character.Map;
                foreach (var location in map)
                {
                    if (location is StartingLocation || location is Town)
                    {
                        continue;
                    }
                    else if (location is Lair)
                    {
                        var monster = new TheGreatDragonKraltock(location);
                        await Task.Run(() => dbContext.Monsters.Add(monster));
                        location.Monster = monster;
                        continue;
                    }
                    else
                    {
                        var monster = MonsterFactory.CreateMonster(location);
                        if (monster != null)
                        {
                            var item = ItemFactory.CreateItem();
                            if (item != null)
                            {
                                monster.Loot = item;
                            }
                            await Task.Run(() => dbContext.Monsters.Add(monster));
                            location.Monster = monster;
                        }
                    } 
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoZ.Repositories
{
    public class MonsterRepository
    {

        public async Task<Monster> FindMonsterByLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var monster = await Task.Run(() => dbContext.Locations.SingleOrDefault(d => d.LocationId == location.LocationId).Monster);
                if (monster == null)
                {
                    return null;
                }
                else
                {
                    return await Task.Run(() => monster.CloneMonster());
                }
            }
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
                var location = await Task.Run(() => dbContext.Locations.SingleOrDefault(l => l.XCoord == 6 && l.YCoord == 6 && l.Character.UserId == id));
                var newMonster = new TheGreatDragonKraltock(location);
                await Task.Run(() => dbContext.Monsters.Add(newMonster));
                location.Monster = newMonster;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddMonsters(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var map = await Task.Run(() => dbContext.Locations.Where(l => l.Character.UserId == id).ToList());
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
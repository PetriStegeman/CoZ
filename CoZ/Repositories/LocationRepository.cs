using CoZ.Models;
using CoZ.Models.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoZ.Repositories
{
    public class LocationRepository
    {

        public async Task<bool> AreThereMonstersAtLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = await Task.Run(() => dbContext.Locations.Find(location.LocationId));
                if (originalLocation.Monster != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Location>> GetMap(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var output = await Task.Run(() => dbContext.Locations.Where(l => l.Character.UserId == id).ToList());
                return output;
            }
        }

        public async Task<Location> FindCurrentLocation(string id)
        {
            Location result;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.SingleOrDefault(c => c.UserId == id));
                var location = await Task.Run(() => character.Map.SingleOrDefault(l => l.XCoord == character.XCoord && l.YCoord == character.YCoord));
                result = await Task.Run(() => location.CopyLocation());
            }
            return result;
        }

        public async Task<Location> FindLocation(string id, int x, int y)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var location = await Task.Run(() => dbContext.Locations.SingleOrDefault(l => l.XCoord == x && l.YCoord == y && l.Character.UserId == id));
                return location;
            }
        }

        public async Task UpdateLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = await Task.Run(() => dbContext.Locations.Find(location.LocationId));
                await Task.Run(() => originalLocation.CloneLocation(location));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var monster = await Task.Run(() => dbContext.Locations.SingleOrDefault(d => d.LocationId == location.LocationId).Monster);
                await Task.Run(() => dbContext.Items.Remove(dbContext.Items.Find(monster.Loot.ItemId)));
                await Task.Run(() => dbContext.Monsters.Remove(dbContext.Monsters.Find(monster.MonsterId)));
                await Task.Run(() => dbContext.Locations.Remove(dbContext.Locations.Find(location.LocationId)));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
using CoZ.Models;
using CoZ.Models.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoZ.Repositories
{
    public class LocationRepository //: Repository
    {

        public async Task<bool> AreThereMonstersAtLocation(Location location)
        {
            bool result = false;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = await Task.Run(() => dbContext.Locations.Find(location.LocationId));
                var monster = originalLocation.Monster;
                if (monster != null)
                {
                    result = true;
                }
            }
            return result;
        }

        public async Task<ICollection<Location>> GetMap(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var output = character.Map;
                return output;
            }
        }

        public async Task<Location> FindCurrentLocation(string id)
        {
            Location result;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var location = await Task.Run(() => character.Map.Single(l => l.XCoord == character.XCoord && l.YCoord == character.YCoord));
                result = location.CopyLocation();
            }
            return result;
        }

        public async Task<Location> FindLocation(string id, int x, int y)
        {
            Location result = null;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var location = await Task.Run(() => character.Map.SingleOrDefault(l => l.XCoord == x && l.YCoord == y));
                if (location != null)
                {
                    result = location.CopyLocation();
                } 
            }
            return result;
        }

        public async Task UpdateLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = await Task.Run(() => dbContext.Locations.Find(location.LocationId));
                originalLocation.CloneLocation(location);
                await dbContext.SaveChangesAsync();
            }
        }

        //Gaat er van uit dat er monster/item/location zijn
            /// <summary>
            /// Hard remove location, monsters and items
            /// </summary>
            /// <param name="location"></param>
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
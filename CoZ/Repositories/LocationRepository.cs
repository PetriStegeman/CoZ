using CoZ.Models;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class LocationRepository //: Repository
    {

        public bool AreThereMonstersAtLocation(Location location)
        {
            bool result = false;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var monster = dbContext.Locations.SingleOrDefault(d => d.LocationId == location.LocationId).Monster;
                if (monster != null)
                {
                    result = true;
                }
            }
            return result;
        }

        public Location FindCurrentLocation(string id)
        {
            Location result;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                var location = character.Map.Single(l => l.XCoord == character.XCoord && l.YCoord == character.YCoord);
                result = location.CopyLocation();
            }
            return result;
        }

        public Location FindLocation(string id, int x, int y)
        {
            Location result;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                var location = character.Map.Single(l => l.XCoord == x && l.YCoord == y);
                result = location.CopyLocation();
            }
            return result;
        }

        public void UpdateLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = dbContext.Locations.Find(location.LocationId);
                originalLocation.CloneLocation(location);
                dbContext.SaveChanges();
            }
        }

        public void DeleteLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var monster = dbContext.Locations.SingleOrDefault(d => d.LocationId == location.LocationId).Monster;
                dbContext.Items.Remove(dbContext.Items.Find(monster.Loot.ItemId));
                dbContext.Monsters.Remove(dbContext.Monsters.Find(monster.MonsterId));
                dbContext.Locations.Remove(dbContext.Locations.Find(location.LocationId));
                dbContext.SaveChanges();
            }
        }
    }
}
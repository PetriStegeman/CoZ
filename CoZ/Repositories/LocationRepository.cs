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

        public int GetAmountOfMonsters(Location location)
        {
            int result = 0;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = dbContext.Locations.Find(location.LocationId);
                result = originalLocation.Monsters.Count();
            }
            return result;
        }

        public Location FindCurrentLocation(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Where(c => c.UserId == id).Single();
                return character.Map.Where(l => l.XCoord == character.XCoord && l.YCoord == character.YCoord).Single();
            }
        }

        public void UpdateLocation(Location location)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalLocation = dbContext.Locations.Find(location.LocationId);
                originalLocation.CopyLocation(location);
                dbContext.SaveChanges();
            }
        }
    }
}
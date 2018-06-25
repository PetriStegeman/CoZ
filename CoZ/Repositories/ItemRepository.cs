using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class ItemRepository
    {
        internal void AddItems(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                ICollection<Location> map = character.Map;
                foreach (var location in map)
                {
                    var item = ItemFactory.CreateItem();
                    if (location.Monster == null && item != null)
                    {
                        location.Item = item;
                        dbContext.Items.Add(item);
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}
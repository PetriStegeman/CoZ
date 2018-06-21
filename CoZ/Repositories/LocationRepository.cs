using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class LocationRepository : Repository
    {

        public void UpdateLocation(Location location)
        {
            var originalLocation = this.DbContext.Locations.Find(location.LocationId);
            originalLocation.CopyLocation(location);
            this.DbContext.SaveChanges();
        }
    }
}
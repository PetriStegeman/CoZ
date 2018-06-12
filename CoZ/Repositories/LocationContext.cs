using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class LocationContext : DbContext
    {
        public IDbSet<Location> Locations { get; set; }
    }
}
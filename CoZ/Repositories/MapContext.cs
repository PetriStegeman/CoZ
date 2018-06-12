using CoZ.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class MapContext : DbContext
    {
        public IDbSet<Map> Maps { get; set; }
    }
}
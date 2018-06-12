using CoZ.Models.Items;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class ItemContext : DbContext
    {
        public IDbSet<Item> Items { get; set; }
    }
}
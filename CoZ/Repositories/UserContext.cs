using CoZ.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class UserContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
    }
}
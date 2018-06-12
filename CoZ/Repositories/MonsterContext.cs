using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class MonsterContext :DbContext
    {
        public IDbSet<Monster> Monsters { get; set; }
    }
}
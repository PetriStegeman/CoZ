using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class MonsterRepository : Repository
    {

        public void DeleteMonster(Monster monster)
        {
            if (this.DbContext.Monsters.Where(c => c.MonsterId == monster.MonsterId).Count() != 0)
            {
                var originalMonster = this.DbContext.Monsters.Find(monster.MonsterId);
                this.DbContext.Monsters.Remove(originalMonster);
            }
        }

        public void Updatemonster(Monster monster)
        {
            var originalMonster = this.DbContext.Monsters.Find(monster.MonsterId);
            originalMonster.CopyMonster(monster);
            this.DbContext.SaveChanges();
        }
    }
}
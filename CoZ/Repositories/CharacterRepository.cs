using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class CharacterRepository //: Repository 
    {
        public void CreateCharacter(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = new Character(id);
                dbContext.Characters.Add(character);
                dbContext.SaveChanges();
            }

        }

        public Character FindByCharacterId(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                return dbContext.Characters.Where(c => c.UserId == id).Single();
            }
        }

        public void DeleteExistingCharacters(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                if (dbContext.Characters.Where(c => c.UserId == id).Count() != 0)
                {
                    var character = dbContext.Characters.Where(c => c.UserId == id).Single();
                    dbContext.Characters.Remove(character);
                    dbContext.SaveChanges();
                }
            }
        }

        public void UpdateCharacter(Character character)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalCharacter = dbContext.Characters.Find(character.CharacterId);
                originalCharacter.CopyCharacter(character);
                dbContext.SaveChanges();
            }
        }
    }
}
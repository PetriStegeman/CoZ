using CoZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class CharacterRepository : Repository
    {
        public void CreateCharacter(string id)
        {
            Character character = new Character(id);
            DbContext.Characters.Add(character);
            DbContext.SaveChanges();
        }

        public Character FindByCharacterId(string id)
        {
            return this.DbContext.Characters.Where(c => c.UserId == id).Single();
        }

        public void DeleteExistingCharacters(string id)
        {
            if (this.DbContext.Characters.Where(c => c.UserId == id).Count() != 0)
            {
                var character = this.DbContext.Characters.Where(c => c.UserId == id).Single();
                this.DbContext.Characters.Remove(character);
            }
        }

        public void UpdateCharacter(Character character)
        {
            var originalCharacter = this.DbContext.Characters.Find(character.CharacterId);
            originalCharacter.CopyCharacter(character);
            this.DbContext.SaveChanges();
        }
    }
}
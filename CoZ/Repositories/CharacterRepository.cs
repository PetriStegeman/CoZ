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
            bool characterExists = true;
            while (characterExists)
            {
                if (this.DbContext.Characters.Where(c => c.UserId == id).Count() != 0)
                {
                    Character character = this.DbContext.Characters.Where(c => c.UserId == id).First();
                    this.DbContext.Characters.Remove(character);
                    continue;
                }
                characterExists = false;
            }
        }

        public void UpdateCharacter(Character character)
        {
            var originalCharacter = this.DbContext.Characters.Where(c => c.CharacterId == character.CharacterId).Single();
            originalCharacter.CopyCharacter(character);
            this.DbContext.SaveChanges();
        }
    }
}
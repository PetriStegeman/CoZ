using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Monsters;
using CoZ.Repositories;
using CoZ.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class BattleController : Controller
    {
        #region Repositories
        #pragma warning disable
        private LocationRepository locationRepository;
        protected LocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null)
                {
                    return new LocationRepository();
                }
                else
                {
                    return locationRepository;
                }
            }
        }

        private CharacterRepository characterRepository;
        protected CharacterRepository CharacterRepository
        {
            get
            {
                if (characterRepository == null)
                {
                    return new CharacterRepository();
                }
                else
                {
                    return characterRepository;
                }
            }
        }

        private MonsterRepository monsterRepository;
        protected MonsterRepository MonsterRepository
        {
            get
            {
                if (monsterRepository == null)
                {
                    return new MonsterRepository();
                }
                else
                {
                    return monsterRepository;
                }
            }
        }

        private ItemRepository itemRepository;
        protected ItemRepository ItemRepository
        {
            get
            {
                if (itemRepository == null)
                {
                    return new ItemRepository();
                }
                else
                {
                    return itemRepository;
                }
            }
        }
        #pragma warning restore
        #endregion

        public async Task<ActionResult> Index()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            await Task.WhenAll(character, location).ConfigureAwait(false);
            var monster = await this.MonsterRepository.FindMonsterByLocation(location.Result);
            var result = CreateBattleViewModel(monster, character.Result);
            if (monster == null)
            {
                return RedirectToAction("Index", "Location");
            }
            return View(result);
        }
        
        //Redirect the user to the Location view after defeating the monster(s)
        public async Task<ActionResult> RunAWay()
        {
            string id = User.Identity.GetUserId();
            var location = await this.LocationRepository.FindCurrentLocation(id);
            var monster = await this.MonsterRepository.FindMonsterByLocation(location);
            await this.MonsterRepository.DeleteMonster(monster);
            await this.LocationRepository.UpdateLocation(location);
            return RedirectToAction("Index", "Location");
        }

        public async Task<ActionResult> Attack()
        {
            string id = User.Identity.GetUserId();
            var character =  this.CharacterRepository.FindByCharacterId(id);
            var inventory = this.ItemRepository.GetInventory(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            await Task.WhenAll(character, inventory, location).ConfigureAwait(false);
            var monster = await this.MonsterRepository.FindMonsterByLocation(location.Result);
            var damage = await Task.Run(() => character.Result.Attack(monster, inventory.Result));
            var result = CreateBattleViewModel(monster, character.Result);
            var updateCharacter = this.CharacterRepository.UpdateCharacter(character.Result);
            var updateMonster = this.MonsterRepository.Updatemonster(monster);
            await Task.WhenAll(updateCharacter, updateMonster).ConfigureAwait(false);
            return DetermineBattleOutcome(result, damage);
        }

        public async Task<ActionResult> Victory()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            await Task.WhenAll(character, location).ConfigureAwait(false);
            var monster = await this.MonsterRepository.FindMonsterByLocation(location.Result);
            var item = await this.ItemRepository.FindLoot(monster);
            if (item != null)
            {
                await this.CharacterRepository.GainItem(id, item);
            }
            await Task.Run(() => character.Result.Victory(monster));
            var result = CreateBattleViewModel(monster, character.Result, item);
            await this.MonsterRepository.DeleteMonster(monster);
            var updateLocation = this.LocationRepository.UpdateLocation(location.Result);
            var updateCharacter = this.CharacterRepository.UpdateCharacter(character.Result);
            await Task.WhenAll(updateLocation, updateCharacter).ConfigureAwait(false);
            return View(result);
        }

        public async Task<ActionResult> GameOver()
        {
            string id = User.Identity.GetUserId();
            if (await this.CharacterRepository.FindByCharacterId(id) != null)
            {
                await this.CharacterRepository.DeleteCharacter(id);
            }
            return View();
        }

        public async Task<ActionResult> LevelUp()
        {
            bool levelUp = false;
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            await Task.WhenAll(character, location).ConfigureAwait(false);
            await Task.Run(() => levelUp = character.Result.IsLevelUp());
            await this.CharacterRepository.UpdateCharacter(character.Result);
            if (levelUp)
            {
                return View();
            }
            else return RedirectToAction("Index", "Location");
        }

        public ActionResult DetermineBattleOutcome(BattleViewModel view, string damage)
        {
            if (view.CharacterCurrentHp <= 0)
            {
                return RedirectToAction("GameOver");
            }
            else if (view.MonsterCurrentHp <= 0)
            {
                return RedirectToAction("Victory");
            }
            else
            {
                ViewBag.Message = damage;
                return View("Index", view);
            }
        }

        public BattleViewModel CreateBattleViewModel(Monster monster, Character character, Item item = null)
        {
            return new BattleViewModel(monster, character, item);
        }

    }
}
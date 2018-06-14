﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CoZ.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //TODO add models voor game
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Character> Characters { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<Monster> Monsters { get; set; }
        public IDbSet<Item> Items { get; set; }
        public IDbSet<Map> Maps { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
using System.Linq;
using System.Collections.Generic;
using BigOferta.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace BigOferta.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            this._context = context;
        }

        public static void SeedUsers(UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/Json/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    System.Console.WriteLine($"  ========> {user.UserName}");
                    userManager.CreateAsync(user, "felinonino").Wait();
                }
            }
        }

        public void SeedOffers()
        {
            if (!_context.Offers.Any())
            {
                var offerData = System.IO.File.ReadAllText("Data/Json/OfferSeedData.json");
                var offers = JsonConvert.DeserializeObject<List<Offer>>(offerData);

                foreach (var offer in offers)
                {
                    _context.Offers.Add(offer);
                }

                _context.SaveChangesAsync().Wait();
            }
        }
    }
}
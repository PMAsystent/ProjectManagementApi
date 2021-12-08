using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Persistance.DatabaseContext;
using Newtonsoft.Json;

namespace ProjectManagementApi.IntegrationTests.Common
{
    public static class Utilities
    {
        private static ApplicationDbContext Context { get; set; }

        public static User User1 { get; set; }

        public static void InitializeDbForTests(ApplicationDbContext context)
        {
            Context = context;
            ResetDb();
            SeedForTests();
        }

        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        private static void ResetDb()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        private static void SeedForTests()
        {
            //TODO: implement seed
            SeedUsers();
        }

        private static void SeedUsers()
        {
            var authUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "integration@tests.pl",
                UserName = "integration.tests"
            };
            Context.Add(authUser);
            Context.SaveChanges();

            var user = new User()
            {
                Email = authUser.Email,
                UserName = authUser.UserName,
                ApplicationUserId = authUser.Id
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            User1 = user;
        }
    }
}
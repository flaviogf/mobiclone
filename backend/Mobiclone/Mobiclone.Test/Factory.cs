using Bogus;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using System.Threading.Tasks;

namespace Mobiclone.Test
{
    public class Factory
    {
        public async static Task<User> User(string name = null, string email = null, string password = null)
        {
            var faker = new Faker();

            var bcrypt = new Bcrypt();

            var user = new User
            {
                Name = name ?? faker.Person.FirstName,
                Email = email ?? faker.Person.Email,
                PasswordHash = password != null ? await bcrypt.Make(password) : await bcrypt.Make(faker.Internet.Password())
            };

            return user;
        }
    }
}

using lab2_web_api.Models;
using lab2_web_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Linq;


namespace Tests
{
    public class UsersServiceTests
    {
        private IOptions<AppSettings> config;

        [SetUp]
        public void Setup()
        {
            config = Options.Create(new AppSettings
            {

                Secret = "qwedsadhasdqweqweasejcghduihdfhdifd8ih"
            });
        }

        [Test]
        public void ValidRegisterShouldCreateNewUser()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                         .UseInMemoryDatabase(databaseName: nameof(ValidRegisterShouldCreateNewUser))// "ValidRegisterShouldCreateANewUser")
                         .Options;

            using (var context = new TasksDbContext(options))
            {
                var usersService = new UsersService(context, config);
                var added = new lab2_web_api.ViewModels.RegisterPostModel
                {
                    FirstName = "firstName1",
                    LastName = "lastName1",
                    Username = "test_userName1",
                    Email = "first@yahoo.com",
                    Password = "111111"
                };

                var result = usersService.Register(added);

                Assert.IsNotNull(result);
                Assert.AreEqual(added.Username, result.UserName);
            }
        }


        [Test]
        public void AuthenticateShouldLogTheRegisteredUser()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(AuthenticateShouldLogTheRegisteredUser))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var usersService = new UsersService(context, config);
                var added = new lab2_web_api.ViewModels.RegisterPostModel
                {
                    FirstName = "firstName1",
                    LastName = "lastName1",
                    Username = "test_userName1",
                    Email = "first@yahoo.com",
                    Password = "111111"
                };
                var result = usersService.Register(added);

                var authenticated = new lab2_web_api.ViewModels.LoginPostModel
                {
                    Username = "test_userName1",
                    Password = "111111"
                };
                var authresult = usersService.Authenticate(added.Username, added.Password);

                Assert.IsNotNull(authresult);
                Assert.AreEqual(1, authresult.Id);
                Assert.AreEqual(authenticated.Username, authresult.UserName);
            }
        }



        [Test]
        public void GetAllShouldReturnAllRegisteredUsers()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetAllShouldReturnAllRegisteredUsers))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var usersService = new UsersService(context, config);
                var added1 = new lab2_web_api.ViewModels.RegisterPostModel
                {
                    FirstName = "firstName1",
                    LastName = "firstName1",
                    Username = "test_userName1",
                    Email = "first@yahoo.com",
                    Password = "111111"
                };
                var added2 = new lab2_web_api.ViewModels.RegisterPostModel
                {
                    FirstName = "secondName2",
                    LastName = "secondName2",
                    Username = "test_userName2",
                    Email = "second@yahoo.com",
                    Password = "111111"
                };
                usersService.Register(added1);
                usersService.Register(added2);

                int numberOfElements = usersService.GetAll().Count();

                Assert.NotZero(numberOfElements);
                Assert.AreEqual(2, numberOfElements);

            }
        }
    }
}
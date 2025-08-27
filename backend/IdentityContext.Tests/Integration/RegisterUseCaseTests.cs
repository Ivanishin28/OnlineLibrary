using IdentityContext.Application.UseCases.Commands;
using IdentityContext.DL;
using IdentityContext.DL.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;
using IdentityContext.Tests.Integration.Helpers;
using IdentityContext.Contracts.Commands.Register;
using IdentityContext.DL.Enums;
using IdentityContext.DL.Concrete;

namespace IdentityContext.Tests.Integration
{
    public class RegisterUseCaseTests
    {
        private ApplicationIdentityDbContext _db = null!;
        private RegisterRequestHandler sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationIdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _db = new ApplicationIdentityDbContext(options);

            var userManager = UserManagerFactory.CreateFor(_db);
            sut = new RegisterRequestHandler(userManager, new IdentityChecker(_db));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Creates_application_user_with_pending_registration_status()
        {
            var request = new RegisterRequest(
                "login",
                "email@gmail.com",
                "password");

            var result = await sut.Handle(request, CancellationToken.None);

            var fromDb = await _db
                .Users
                .Where(x => x.Email == request.Email)
                .FirstAsync();

            Assert.True(result.IsSuccess);
            Assert.That(result.Model, Is.EqualTo(fromDb.Id));
            Assert.That(fromDb.Status, Is.EqualTo(ProfileCreationStatus.Pending));
        }

        [Test]
        public async Task Does_not_create_user_if_login_is_taken()
        {
            var login = "login";
            var oldUser = new ApplicationUser
            {
                Email = "other@gmail.com",
                UserName = login,
                PasswordHash = "passoword_hash"
            };
            _db.Add(oldUser);
            await _db.SaveChangesAsync();

            var request = new RegisterRequest(
                login,
                "email@gmail.com",
                "password");
            var result = await sut.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.That(await _db.Users.CountAsync(), Is.EqualTo(1));
        }

        [Test]
        public async Task Does_not_create_user_if_email_is_taken()
        {
            var email = "email@gmail.com";
            var oldUser = new ApplicationUser
            {
                Email = email,
                UserName = "Oldlogin",
                PasswordHash = "passoword_hash"
            };
            _db.Add(oldUser);
            await _db.SaveChangesAsync();

            var request = new RegisterRequest(
                "Newlogin",
                email,
                "password");
            var result = await sut.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.That(await _db.Users.CountAsync(), Is.EqualTo(1));
        }
    }
}

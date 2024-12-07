using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Client.Services;
using TaskManager.Common.Models;
using System.Net;
using TaskManager.Client.Models;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class UsersRequestServiceTests
    {
        private AuthToken _token;
        private UsersRequestService _service;


        [TestMethod()]
        public void GetTokenTest()
        {
            var token = new UsersRequestService().GetToken("admin", "zhizha1234");
            Console.WriteLine(token.access_token);
            Assert.IsNotNull(token);
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "zhizha1234");

            UserModel userTest = new UserModel("Crower", "Gray", "ouuuWu@gmail.com", "qwerty", UserStatus.User, "9230549230");

            var result = service.CreateUser(token, userTest);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }
        [TestMethod()]
        public void GetAllUsersTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "zhizha1234");

            var result = service.GetAllUsers(token);

            Console.WriteLine(result.Count);

            Assert.AreNotEqual(Array.Empty<UserModel>(), result.ToArray());
        }
        [TestMethod()]
        public void DeleteUserTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "zhizha1234");

            var result = service.DeleteUser(token, 14);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void CreateMultipleUsersTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "zhizha1234");

            UserModel userTest1 = new UserModel("Vlad", "Obichny", "etovlad@mail.com", "neOkey", UserStatus.User, "5346373430");
            UserModel userTest2 = new UserModel("KO", "Vzzz", "zhuzhu@gmail.com", "777okey777", UserStatus.Editor, "9230457230");
            UserModel userTest3 = new UserModel("Sergey", "Mavrody", "milomilo@gmail.com", "vzhuhhhh", UserStatus.User, "6638356886");

            List<UserModel> users = new List<UserModel>() { userTest1, userTest2, userTest3 };

            var result = service.CreateMultipleUsers(token, users);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }
        [TestMethod()]
        public void UpdateUserTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "zhizha1234");

            UserModel userTest = new UserModel("Vladik", "Obichny", "etovlad@mail.com", "neOkey", UserStatus.User, "+5346373430");
            userTest.Id = 24;

            var result = service.UpdateUser(token, userTest);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void GetProjectUserAdminTest()
        {
            var id = _service.GetProjectUserAdmin(_token, 9);
            Assert.AreEqual(4, id);
        }
    }
}
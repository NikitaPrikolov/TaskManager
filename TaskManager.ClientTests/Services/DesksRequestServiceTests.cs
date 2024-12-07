using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Common.Models;
using System.Net;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class DesksRequestServiceTests
    {
        private AuthToken _token;
        private DesksRequestService _service;
        public DesksRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("admin", "zhizha1234");
            _service = new DesksRequestService();
        }
        [TestMethod()]
        public void GetAllDesksTest()
        {
            var desks = _service.GetAllDesks(_token);
            Console.WriteLine(desks.Count);
            Assert.AreNotEqual(Array.Empty<DeskModel>(), desks.ToArray());
        }

        [TestMethod()]
        public void GetDeskByIdTest()
        {
            var desk = _service.GetDeskById(_token, 5);
            Assert.AreNotEqual(null, desk);
        }

        [TestMethod()]
        public void GetDeskByProjectTest()
        {
            var desks = _service.GetDeskByProject(_token, 4);
            Assert.AreEqual(1, desks.Count);
        }

        [TestMethod()]
        public void CreateDeskTest()
        {
            var desk = new DeskModel("Доска пожилая", "desk old", true, new string[] { "new", "ready" });
            desk.ProjectId = 4;
            desk.AdminId = 9;
            var result = _service.CreateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateDeskTest()
        {
            var desk = new DeskModel("Доска molodaya", "desk old", true, new string[] { "new", "eeee", "ready" });
            desk.ProjectId = 4;
            desk.AdminId = 9;
            desk.Id = 7;
            var result = _service.UpdateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteDeskByIdTest()
        {
            var result = _service.DeleteDeskById(_token, 7);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}
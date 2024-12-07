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
    public class TasksRequestServiceTests
    {
        private AuthToken _token;
        private TasksRequestService _service;
        public TasksRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("admin", "zhizha1234");
            _service = new TasksRequestService();
        }
        [TestMethod()]
        public void GetAllTasksTest()
        {
            var tasks = _service.GetAllTasks(_token);
            Console.WriteLine(tasks.Count);
            Assert.AreNotEqual(0, tasks.Count);
        }

        [TestMethod()]
        public void GetTaskByIdTest()
        {
            var task = _service.GetTaskById(_token, 2);
            Assert.AreNotEqual(null, task);
        }

        [TestMethod()]
        public void GetTaskByDeskTest()
        {
            var tasks = _service.GetTaskByDesk(_token, 6);
            Assert.AreNotEqual(0, tasks.Count);
        }

        [TestMethod()]
        public void CreateTaskTest()
        {
            var task = new TaskModel("taskaeasy", "tastastastastaska", DateTime.Now, DateTime.Now, "New");
            task.DeskId = 6;
            task.ExecutorId = 26;
            var result = _service.CreateTask(_token, task);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateTaskTest()
        {
            var task = new TaskModel("version two", "tastastastastaska", DateTime.Now, DateTime.Now, "old");
            task.Id = 4;
            task.ExecutorId = 25;
            var result = _service.UpdateTask(_token, task);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteTaskByIdTest()
        {
            var result = _service.DeleteTaskById(_token, 4);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}
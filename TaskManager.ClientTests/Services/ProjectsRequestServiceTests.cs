using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Common.Models;
using TaskManager.Client.Models;
using System.Net;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class ProjectsRequestServiceTests
    {
        private AuthToken _token;
        private ProjectsRequestService _service;

        public ProjectsRequestServiceTests() 
        {
            _token = new UsersRequestService().GetToken("admin", "zhizha1234");
            _service = new ProjectsRequestService();
        }
        [TestMethod()]
        public void GetAllProjectsTest()
        {
            
            var projects = _service.GetAllProjects(_token);
            Console.WriteLine(projects.Count);
            Assert.AreNotEqual(Array.Empty<ProjectModel>(), projects.ToArray());
        }
        [TestMethod()]
        public void GetProjectByIdTest()
        {
            var project = _service.GetProjectById(_token, 2);
            Assert.AreNotEqual(null, project);
        }

        [TestMethod()]
        public void CreateProjectTest()
        {
            ProjectModel project = new ProjectModel("TestProject", "Vweyy new", ProjectStatus.InProgress);
            project.AdminId = 9;
             var result = _service.CreateProject(_token, project);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
        [TestMethod()]
        public void UpdateProjectTest()
        {
            ProjectModel project = new ProjectModel("TestProject v2", "Vweyy new", ProjectStatus.Suspended);
            project.Id = 6;
            var result = _service.UpdateProject(_token, project);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
        [TestMethod()]
        public void DeleteProjectTest()
        {
            var result = _service.DeleteProject(_token, 3);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
        [TestMethod()]
        public void AddUsersToProjectTest()
        {
            var result = _service.AddUsersToProject(_token, 2, new List<int>() { 25, 26});
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
        [TestMethod()]
        public void RemoveUsersFromProjectTest()
        {
            var result = _service.RemoveUsersFromProject(_token, 2, new List<int>() { 25});
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}
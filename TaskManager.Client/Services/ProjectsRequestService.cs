﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services
{
    public class ProjectsRequestService : CommonRequestService
    {
        private string _projectsControllerUrl = HOST + "projects";
        public List<ProjectModel> GetAllProjects(AuthToken token)
        {
            string response = GetDataByUrl(_projectsControllerUrl, token, HttpMethodType.GET);
            List<ProjectModel> projects = JsonConvert.DeserializeObject<List<ProjectModel>>(response);
            return projects;
        }
        public ProjectModel GetProjectById(AuthToken token, int projectId)
        {
            var response = GetDataByUrl(_projectsControllerUrl + $"/{projectId}", token, HttpMethodType.GET);
            ProjectModel project = JsonConvert.DeserializeObject<ProjectModel>(response);
            return project;
        }
        public HttpStatusCode CreateProject(AuthToken token, ProjectModel project)
        {
            string projectJson = JsonConvert.SerializeObject(project);
            var result = SendDataByUrl(HttpMethod.Post, _projectsControllerUrl, token, projectJson);
            return result;
        }
        public HttpStatusCode UpdateProject(AuthToken token, ProjectModel project)
        {
            string projectJson = JsonConvert.SerializeObject(project);
            var result = SendDataByUrl(HttpMethod.Patch, _projectsControllerUrl + $"/{project.Id}", token, projectJson);
            return result;
        }
        public HttpStatusCode DeleteProject(AuthToken token, int projectId)
        {
            var result = DeleteDataByUrl(_projectsControllerUrl + $"/{projectId}", token);
            return result;
        }
        public HttpStatusCode AddUsersToProject(AuthToken token, int projectId, List<int> usersIds)
        {
            string usersIdsJson = JsonConvert.SerializeObject(usersIds);
            var result = SendDataByUrl(HttpMethod.Patch, _projectsControllerUrl + $"/{projectId}/users", token, usersIdsJson);
            return result;
        }
        public HttpStatusCode RemoveUsersFromProject(AuthToken token, int projectId, List<int> usersIds)
        {
            string usersIdsJson = JsonConvert.SerializeObject(usersIds);
            var result = SendDataByUrl(HttpMethod.Patch, _projectsControllerUrl + $"/{projectId}/users/remove", token, usersIdsJson);
            return result;
        }
    }
}

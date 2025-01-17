﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services
{
    public class TasksRequestService : CommonRequestService
    {
        private string _tasksControllerUrl = HOST + "tasks";

        public List<TaskModel> GetAllTasks(AuthToken token)
        {
            string response = GetDataByUrl(_tasksControllerUrl + "/user", token, HttpMethodType.GET);
            List<TaskModel> tasks = JsonConvert.DeserializeObject<List<TaskModel>>(response);
            return tasks;
        }
        public TaskModel GetTaskById(AuthToken token, int taskId)
        {
            var response = GetDataByUrl(_tasksControllerUrl + $"/{taskId}", token, HttpMethodType.GET);
            TaskModel task = JsonConvert.DeserializeObject<TaskModel>(response);
            return task;
        }
        public List<TaskModel> GetTaskByDesk(AuthToken token, int deskId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("deskId", deskId.ToString());
            string response = GetDataByUrl(_tasksControllerUrl, token, HttpMethodType.GET, null, null, parameters);
            List<TaskModel> tasks = JsonConvert.DeserializeObject<List<TaskModel>>(response);
            return tasks;
        }
        public HttpStatusCode CreateTask(AuthToken token, TaskModel task)
        {
            string taskJson = JsonConvert.SerializeObject(task);
            var result = SendDataByUrl(HttpMethod.Post, _tasksControllerUrl, token, taskJson);
            return result;
        }
        public HttpStatusCode UpdateTask(AuthToken token, TaskModel task)
        {
            string taskJson = JsonConvert.SerializeObject(task);
            var result = SendDataByUrl(HttpMethod.Patch, _tasksControllerUrl + $"/{task.Id}", token, taskJson);
            return result;
        }
        public HttpStatusCode DeleteTask(AuthToken token, int taskId)
        {
            var result = DeleteDataByUrl(_tasksControllerUrl + $"/{taskId}", token);
            return result;
        }
    }
}

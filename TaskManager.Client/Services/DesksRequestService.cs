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
    public class DesksRequestService : CommonRequestService
    {
        private string _desksControllerUrl = HOST + "desks";
        public List<DeskModel> GetAllDesks(AuthToken token)
        {
            string response = GetDataByUrl(_desksControllerUrl, token, HttpMethodType.GET);
            List<DeskModel> desks = JsonConvert.DeserializeObject<List<DeskModel>>(response);
            return desks;
        }
        public DeskModel GetDeskById(AuthToken token, int deskId)
        {
            var response = GetDataByUrl(_desksControllerUrl + $"/{deskId}", token, HttpMethodType.GET);
            DeskModel desk = JsonConvert.DeserializeObject<DeskModel>(response);
            return desk;
        }
        public List<DeskModel> GetDeskByProject(AuthToken token, int projectId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("projectId", projectId.ToString());
            string response = GetDataByUrl(_desksControllerUrl + "/project", token, HttpMethodType.GET, null, null, parameters);
            List<DeskModel> desks = JsonConvert.DeserializeObject<List<DeskModel>>(response);
            return desks;
        }
        public HttpStatusCode CreateDesk(AuthToken token, DeskModel desk)
        {
            string deskJson = JsonConvert.SerializeObject(desk);
            var result = SendDataByUrl(HttpMethod.Post, _desksControllerUrl, token, deskJson);
            return result;
        }
        public HttpStatusCode UpdateDesk(AuthToken token, DeskModel desk)
        {
            string deskJson = JsonConvert.SerializeObject(desk);
            var result = SendDataByUrl(HttpMethod.Patch, _desksControllerUrl + $"/{desk.Id}", token, deskJson);
            return result;
        }
        public HttpStatusCode DeleteDesk(AuthToken token, int deskId)
        {
            var result = DeleteDataByUrl(_desksControllerUrl + $"/{deskId}", token);
            return result;
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services
{
    public enum HttpMethodType
    {
        GET,
        POST
    }
    public class UsersRequestService : CommonRequestService
    {
        private string _usersControllerUrl = HOST + "users";
        
        public HttpStatusCode CreateUser(AuthToken token, UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            var result = SendDataByUrl(HttpMethod.Post, _usersControllerUrl, token, userJson);
            return result;
        }
        public UserModel GetCurrentUser(AuthToken token)
        {
            string response = GetDataByUrl(HOST + "account/info", token, HttpMethodType.GET);
            UserModel users = JsonConvert.DeserializeObject<UserModel>(response);
            return users;
        }

        public UserModel GetUserById(AuthToken token, int? userId)
        {
            string response = GetDataByUrl(_usersControllerUrl + $"/{userId}", token, HttpMethodType.GET);
            UserModel users = JsonConvert.DeserializeObject<UserModel>(response);
            return users;
        }

        public List<UserModel> GetAllUsers(AuthToken token)
        {
            string response = GetDataByUrl(_usersControllerUrl, token, HttpMethodType.GET);
            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(response);
            return users;
        }

        public HttpStatusCode DeleteUser(AuthToken token, int userId)
        {
            var result = DeleteDataByUrl(_usersControllerUrl + $"/{userId}", token);
            return result;
        }
        public HttpStatusCode CreateMultipleUsers(AuthToken token, List<UserModel> users)
        {
            string userJson = JsonConvert.SerializeObject(users);
            var result = SendDataByUrl(HttpMethod.Post, _usersControllerUrl + "/all", token, userJson);
            return result;
        }
        public AuthToken GetToken(string userName, string password)
        {
            string url = HOST + "account/token";
            string resultStr = GetDataByUrl(url, null, HttpMethodType.POST, userName, password);
            AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr);
            return token;
        }
        public HttpStatusCode UpdateUser(AuthToken token, UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            var result = SendDataByUrl(HttpMethod.Patch, _usersControllerUrl + $"/{user.Id}", token, userJson);
            return result;
        }

        public int? GetProjectUserAdmin(AuthToken token, int userId)
        {
            var result = GetDataByUrl(_usersControllerUrl + $"/{userId}/admin", token, HttpMethodType.POST);
            int adminId;

            bool parseResult = int.TryParse(result, out adminId);
            if (parseResult)
                return adminId;
            else
                return null;
        }
    }
}

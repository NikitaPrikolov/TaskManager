using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly ApplicationContext _db;
        public UsersController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
        }
        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("Huwuu");
        }
        [Authorize]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _usersService.Create(userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel) 
        {
            if (userModel != null)
            {
                bool result = _usersService.Update(id, userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            bool result = _usersService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUser(int id)
        {
            var user = _usersService.Get(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _db.Users.Select(u => u.ToDto()).ToListAsync();
        }
        [HttpPost("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMultipleUsers([FromBody] List<UserModel> usersModels)
        {
            if (usersModels != null && usersModels.Count > 0)
            {
                bool result = _usersService.CreateMultipleUsers(usersModels);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }
        [HttpGet("{id}/admin")]
        public ActionResult<int?> GetProjectAdminId(int id)
        {
            var admin = _usersService.GetProjectAdmin(id);
            return admin == null ? NotFound(null) : Ok(admin.Id);
        }
    }
}

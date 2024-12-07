﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _userService;
        public AccountController(ApplicationContext db)
        {
            _db = db;
            _userService = new UsersService(db);
        }
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo() 
        {
            string username = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(u => u.Email == username);

            if (user != null)
                return Ok(user.ToDto());
            return NotFound();
        }
        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var userData = _userService.GetUserLoginPassFromBasicAuth(Request);
            var login = userData.Item1;
            var pass = userData.Item2;
            var identity = _userService.GetIdentity(login, pass);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodeJwt,
                username = identity.Name
            };
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("update")]
        public IActionResult UpdateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                string userName = HttpContext.User.Identity.Name;
                User userforUpdate = _db.Users.FirstOrDefault(u => u.Email == userName);
                if (userforUpdate != null)
                {
                    userforUpdate.FirstName = userModel.FirstName;
                    userforUpdate.LastName = userModel.LastName;
                    userforUpdate.Password = userModel.Password;
                    userforUpdate.Phone = userModel.Phone;
                    userforUpdate.Photo = userModel.Photo;

                    _db.Users.Update(userforUpdate);
                    _db.SaveChanges();
                    return Ok();

                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
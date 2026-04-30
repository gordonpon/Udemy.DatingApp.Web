using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy.DatingApp.Web.Data;
using Udemy.DatingApp.Web.DTOs;
using Udemy.DatingApp.Web.Entity;
using Udemy.DatingApp.Web.Extensions;
using Udemy.DatingApp.Web.Interfaces;

namespace Udemy.DatingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
    {
        
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //檢查Email是否已經存在
            if(await CheckEmailExists(registerDto.Email))
                return BadRequest("Email is already exist");

            //使用HMACSHA512來加密密碼
            using var hmac = new HMACSHA512();
            AppUser user = new AppUser
            {
                    Id = Guid.NewGuid().ToString(),
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user.ToDto(tokenService);
        }

        [HttpPost("login")] // api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //先判斷Email是否存在
            var user = await context.Users.FirstOrDefaultAsync(f => f.Email.Equals(loginDto.Email.ToLower()));
            if(user == null)
                return Unauthorized("Invalid email address.");

            //傳入密碼轉換為Hash512
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(var i =0; i< computeHash.Length; i++)
            {
                if(computeHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password.");
            }

            return user.ToDto(tokenService);
        }




        private async Task<bool> CheckEmailExists(string email)
        {
            return await context.Users.AnyAsync(f => f.Email.Equals(email.ToLower()));
        }




    }
}

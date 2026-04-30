using System;
using Udemy.DatingApp.Web.DTOs;
using Udemy.DatingApp.Web.Entity;
using Udemy.DatingApp.Web.Interfaces;

namespace Udemy.DatingApp.Web.Extensions;

public static class AppUserExtensions
{
    public static UserDto ToDto(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = tokenService.CreateToken(user)
        };
        
    }
}

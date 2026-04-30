using System;
using Udemy.DatingApp.Web.Entity;

namespace Udemy.DatingApp.Web.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy.DatingApp.Web.Data;
using Udemy.DatingApp.Web.Entity;

namespace Udemy.DatingApp.Web.Controllers
{
    [Route("api/[controller]")] //localhost:5001/api/members
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        /*
            IReadOnlyList：當你想表達 API 的意圖是「只提供讀取」，這是更好的設計實踐，能防止 Controller 內部誤改資料
            List：如果你允許修改或無所謂
        */
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return members;
        }

        [HttpGet("{id}")] //localhost:5001/api/members/tsuss-id
        public async  Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id);
            if (member == null) return NotFound();
            return member;
        }


    }
}

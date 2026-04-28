using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IReadOnlyList<AppUser>> GetMembers()
        {
            var members = context.Users.ToList();
            return members;
        }

        [HttpGet("{id}")] //localhost:5001/api/members/tsuss-id
        public ActionResult<AppUser> GetMember(string id)
        {
            var member = context.Users.Find(id);
            if (member == null) return NotFound();
            return member;
        }


    }
}

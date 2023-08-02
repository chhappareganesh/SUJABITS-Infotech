using CRUDTask.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IConfiguration _config;
        UserDB _db;
        public UserController(IConfiguration config)
        {
            _config = config;
            _db = new UserDB(_config["ConnectionStrings:CRUDDemo"]);
        }

        [HttpPost]
        public IActionResult LogIn(UserModel user)
        {
           bool status = _db.LogIn(user);
            if (status)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}

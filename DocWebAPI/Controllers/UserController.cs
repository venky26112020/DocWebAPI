using DocWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDataBaseContext _context;
        public UserController(UserDataBaseContext context)
        {
            _context = context;

        }


       [HttpPost("add")]
        public ActionResult userAdd(User userRequest)
        {
            try
            {
                 _context.Users.Add(userRequest);
                _context.SaveChanges();
                return Ok(userRequest);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost("update")]
        public ActionResult userUpdate(User userRequest)
        {
            try
            {
                _context.Users.Update(userRequest);
                _context.SaveChanges();
                return Ok(userRequest);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getSelectedUser/{id}")]
        public ActionResult getAllUsers(int id)
        {
            try
            {
                var user = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
                return Ok(user);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpDelete("{id}")]
        public ActionResult deleteSelectedUser(int id)
        {
            try
            {
                var user = _context.Users.Where(x => x.UserId == id).First();
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok(user);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}

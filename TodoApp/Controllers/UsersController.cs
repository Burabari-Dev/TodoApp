using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDBContext _context;
        public UsersController(IUserDBContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            var dbUser = await _context.AddUser(user);
            return Ok(dbUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser([FromQuery] int id)
        {
            var dbUser = await _context.GetUser(id);
            return Ok(dbUser);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var results = await _context.GetAll();
            return Ok(results);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            var dbUser = await _context.UpdateUser(user);
            return Ok(dbUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromQuery] int id)
        {
            var deleted = await _context.DeleteUser(id);
            if(deleted)
                return NoContent();
            else
                return BadRequest();
        }
    }
}

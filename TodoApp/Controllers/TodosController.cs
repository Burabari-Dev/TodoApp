using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoDBContext _context;
        public TodosController(ITodoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAll()
        {
            var todos = await _context.GetAll();
            if(todos == null)
                return NotFound();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodo(int id)
        {
            var todo = await _context.GetTodo(id);
            if(todo == null)
                return NotFound();
            return Ok(todo);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Todo>> AddTodo(int id, [FromBody] Todo todo)
        {
            try
            {
                var dbTodo = await _context.AddTodo(id, todo);
                return Created($"api/todos/{dbTodo.Id}", dbTodo);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] Todo todo)
        {
            try
            {
                _context.UpdateTodo(id, todo);
                return NoContent();
            } catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}

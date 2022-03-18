using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoDBContext : DbContext, ITodoDBContext
    {
        private DbSet<Todo> todos { get; set; }

        public async Task<IEnumerable<Todo>> GetAll() => todos.AsEnumerable();
    }
}

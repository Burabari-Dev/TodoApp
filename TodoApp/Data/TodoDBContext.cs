using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoDBContext : DbContext, ITodoDBContext
    {
        private DbSet<Todo> todos { get; set; }
        private DbSet<User> users { get; set; }

        public async Task<IEnumerable<Todo>> GetAll() => todos.AsEnumerable();

        public async Task<Todo> AddTodo(int userId, Todo todo)
        {
            var user = await users.FirstAsync(x => x.Id == userId);
            if (user == null)
                throw new ArgumentException();  //TODO: Use a better custom?? exception
            user.Todos.Add(todo);
            SaveChanges();
            return todo;
        }

        public async Task<Todo> GetTodo(int id) => await todos.FirstAsync(x => x.Id == id);

        public void UpdateTodo(int id, Todo todo)
        {
            var dbTodo = todos.First(x => x.Id == id);
            if (dbTodo == null)
                throw new Exception();  //TODO: Use a better custom?? exception
            todos.Update(todo);
            SaveChanges();
        }

        public void DeleteTodo(int id, int userId)
        {
            if(! users.Any(u => u.Id == userId))
                throw new Exception();  //TODO: Use a better custom?? exception
            if(! users.First(u => u.Id == userId).Todos.Any(t => t.Id == id))
                throw new Exception();  //TODO: Use a better custom?? exception
            var todo = users.First(u => u.Id == userId).Todos.First(t => t.Id == id);
            todos.Remove(todo);
            SaveChanges(true);
        }

    }
}

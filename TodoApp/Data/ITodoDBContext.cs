using TodoApp.Models;

namespace TodoApp.Data
{
    public interface ITodoDBContext
    {
        public Task<IEnumerable<Todo>> GetAll();
    }
}

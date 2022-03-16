using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoDBContext : DbContext, ITodoDBContext
    {
        public Task<IEnumerable<Todo>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

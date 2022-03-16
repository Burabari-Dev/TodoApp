using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data
{
    public class TodoDBContext : DbContext, ITodoDBContext
    {
    }
}

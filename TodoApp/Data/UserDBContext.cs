using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data
{
    public class UserDBContext : DbContext, IUserDBContext
    {
    }
}

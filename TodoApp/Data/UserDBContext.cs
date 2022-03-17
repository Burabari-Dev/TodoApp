using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoApp.Models;
using System.Linq;

namespace TodoApp.Data
{
    public class UserDBContext : DbContext, IUserDBContext
    {
        private DbSet<User> users { get; set; }

        public async Task<User> AddUser(User user)
        {
            var dbUser = await users.AddAsync(user);
            return dbUser.Entity;
        }

        public async Task<User> GetUser(int id) => await users.FirstAsync(x => x.Id == id);

        //public async Task<EntityEntry<User>> UpdateUser(User user)
        //{
        //    if(! await users.AnyAsync(u => u.Id == user.Id))
        //        return null;
        //    return users.Update(user);
        //}

    }
}

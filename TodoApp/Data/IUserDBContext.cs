﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoApp.Models;

namespace TodoApp.Data
{
    public interface IUserDBContext
    {
        public Task<User> AddUser(User user);
        public Task<User> GetUser(int id);
        public Task<User> UpdateUser(User user);
        public Task<bool> DeleteUser(int id) ;
    }
}

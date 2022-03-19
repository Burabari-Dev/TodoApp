﻿using TodoApp.Models;

namespace TodoApp.Data
{
    public interface ITodoDBContext
    {
        public Task<IEnumerable<Todo>> GetAll();
        public Task<Todo> AddTodo(int userId, Todo todo);
        public Task<Todo> GetTodo(int id);
        //public Task<Todo> UpdateTodo(Todo todo);
        //public Task<Todo> DeleteTodo(int id);
    }
}

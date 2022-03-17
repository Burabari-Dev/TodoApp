﻿namespace TodoApp.Models
{
    public class User
    {
        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Todo> Todos { get; set; } = Enumerable.Empty<Todo>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
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

        [Key]
        public int Id { get; set; }
        [Column(name: "name")]
        public string Name { get; set; }
        [Column(name: "email")]
        public string Email { get; set; }
        [Column(name: "password")]
        public string Password { get; set; }
        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}

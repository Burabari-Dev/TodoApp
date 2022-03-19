using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    public class Todo
    {
        public Todo(int id, string description, DateTime dateAdded, DateTime dateDue, bool complete)
        {
            Id = id;
            Description = description;
            DateAdded = dateAdded;
            DateDue = dateDue;
            IsComplete = complete;
        }

        public Todo(string description, DateTime dateAdded, DateTime dateDue, bool complete)
        {
            Description = description;
            DateAdded = dateAdded;
            DateDue = dateDue;
            IsComplete = complete;
        }

        [Key]
        public int Id { get; set; }
        [Column(name: "description")]
        public string Description { get; set; }
        [Column(name: "date_added")]
        public DateTime DateAdded { get; set; }
        [Column(name: "date_due")]
        public DateTime DateDue { get; set; }
        [Column(name: "complete")]
        public bool IsComplete { get; set; }

    }
}

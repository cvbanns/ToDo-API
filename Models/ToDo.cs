using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAuth.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        [Required]
        public string Task { get; set; }
        public string Details { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ToDoUser? User { get; set; }
    }
}
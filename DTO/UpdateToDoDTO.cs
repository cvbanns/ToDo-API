using System.ComponentModel.DataAnnotations;

namespace ToDoAuth.DTO
{
    public class UpdateToDoDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Task { get; set; }
        public string Details { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } 
    }
}
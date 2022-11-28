using System.ComponentModel.DataAnnotations;

namespace ToDoAuth.DTO
{
    public class GetToDoDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Task { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
   }
}
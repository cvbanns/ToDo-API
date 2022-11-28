using ToDoAuth.Models;

namespace ToDoAuth.DTO
{
    public class AddToDoDTO
    {
        public string Task { get; set; } = "Task";
        public string Details { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }  
        
    }
}
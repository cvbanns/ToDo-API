using ToDoAuth.Models;

namespace ToDoAuth.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
        public Roles Role {get; set;}

    }
}
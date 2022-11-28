using System.ComponentModel.DataAnnotations;

namespace ToDoAuth.Models
{
    public class ToDoUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        
        [MaxLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

         public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime CreatedAt {get; set;}
        public List<ToDo>? ToDos {get ; set;}
        public Roles Role { get; set; } 
    }
}
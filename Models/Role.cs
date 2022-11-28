using System.Text.Json.Serialization;

namespace ToDoAuth.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Roles
    {
        Admin = 1,
        User = 2   
    }
}
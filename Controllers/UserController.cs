using Microsoft.AspNetCore.Mvc;
using ToDoAuth.Data;

namespace ToDoAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ToDoDbContext toDoDbContext;

        public UserController(ToDoDbContext toDoDbContext)
        {
            this.toDoDbContext = toDoDbContext;
        }
        
    }
}
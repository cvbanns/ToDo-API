using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAuth.DTO;
using ToDoAuth.Services;

namespace ToDoAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/action")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet("AllTodos")]
        public async Task<ActionResult<ServiceResponse<List<GetToDoDTO>>>> Get()
        {
            var todos = await _toDoService.AllToDos();
            return Ok(todos);
        }

        [HttpGet("Todo/{id}")]
        public async Task<ActionResult<ServiceResponse<GetToDoDTO>>> GetToDO(Guid id)
        {
            var response = await _toDoService.GetToDo(id);
            if(response.Data == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("AddToDo")]
        public async Task<ActionResult<ServiceResponse<List<GetToDoDTO>>>> AddToDo(AddToDoDTO request)
        {
            var todo = await _toDoService.AddToDo(request);
            
            return Ok(todo);
        }

        [HttpPost("UpdateToDo")]
        public async Task<ActionResult<ServiceResponse<GetToDoDTO>>> UpdateToDo(UpdateToDoDTO request)
        {
            var response = await _toDoService.UpdateToDo(request);
            if(response.Data == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("DeleteToDo")]
        public async Task<ActionResult<ServiceResponse<List<GetToDoDTO>>>> Delete(Guid id)
        {
            var response = await _toDoService.DeleteToDo(id);
            if(response.Data == null)
            {
                return NotFound();
            }
            return Ok(response);
        }






        
    }
}
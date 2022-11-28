using System.Transactions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ToDoAuth.Data;
using ToDoAuth.DTO;
using ToDoAuth.Models;


namespace ToDoAuth.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _toDoDbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public ToDoService(ToDoDbContext toDoDbContext, IHttpContextAccessor contextAccessor)
        {
            _toDoDbContext = toDoDbContext;
            _contextAccessor = contextAccessor;
        }

        private Guid GetUserId() => Guid.
                    Parse(_contextAccessor.HttpContext.User
                    .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetToDoDTO>>> AddToDo(AddToDoDTO request)
        {
            var response = new ServiceResponse<List<GetToDoDTO>>();
            var todo = new ToDo();
            todo.Id = Guid.NewGuid();
            todo.Details = request.Details;
            todo.Task = request.Task;
            todo.DueDate = request.DueDate;
            todo.User = await _toDoDbContext.ToDoUsers.Where(x => x.Id == GetUserId()).FirstAsync();
            
            await _toDoDbContext.ToDotbl.AddAsync(todo);
            await _toDoDbContext.SaveChangesAsync();

            var getToDo = await _toDoDbContext.ToDotbl.ToListAsync();
            response.Data = MappedTodo(getToDo);
            return response;
        }

        public async Task<ServiceResponse<List<GetToDoDTO>>> AllToDos()
        {
            var response = new ServiceResponse<List<GetToDoDTO>>();
            var getToDo = await _toDoDbContext.ToDotbl.Where(x => x.User.Id  == GetUserId()).ToListAsync();
            response.Data = MappedTodo(getToDo);
            return response;
        }

        public async Task<ServiceResponse<List<GetToDoDTO>>> DeleteToDo(Guid id)
        {
            var response = new ServiceResponse<List<GetToDoDTO>>();

            try
            {
                var todo = await _toDoDbContext.ToDotbl.Where(x => x.Id == id && x.User.Id == GetUserId()).FirstAsync();
                _toDoDbContext.Remove(todo);
                await _toDoDbContext.SaveChangesAsync(); 
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            var todoList = await _toDoDbContext.ToDotbl.Where(x => x.User.Id == GetUserId()).ToListAsync();
            response.Data = MappedTodo(todoList);

            return response;

        }

        public async Task<ServiceResponse<GetToDoDTO>> GetToDo(Guid id)
        {
            var response = new ServiceResponse<GetToDoDTO>();
            try
            {
                var getToDo = await _toDoDbContext.ToDotbl.Where(x => x.Id == id && x.User.Id == GetUserId()).FirstAsync();
                response.Data = MappedAddTodo(getToDo);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetToDoDTO>> UpdateToDo(UpdateToDoDTO request)
        {
            var response = new ServiceResponse<GetToDoDTO>();
            try
            {
                var getToDo = await _toDoDbContext.ToDotbl.Where(x => x.Id == request.Id && x.User.Id == GetUserId()).FirstAsync();
                getToDo.Task = request.Task;
                getToDo.Details = request.Details;
                getToDo.DueDate = request.DueDate;
                getToDo.IsCompleted = request.IsCompleted;

                await _toDoDbContext.SaveChangesAsync();

                response.Data = MappedAddTodo(getToDo);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        
        private GetToDoDTO MappedAddTodo(ToDo todo)
        {
            var todoMap = new GetToDoDTO
            {
                Id = todo.Id,
                Task = todo.Task,
                Details = todo.Details,
                CreatedAt = todo.CreatedAt,
                DueDate = todo.DueDate, 
            };
        return todoMap;

        }
        private List<GetToDoDTO> MappedTodo(List<ToDo> todo) {
            var toDoList = new List<GetToDoDTO>();
            foreach(var x in todo)
            {
                var newTodo = new GetToDoDTO
                {
                    Id = x.Id,
                    Details = x.Details,
                    DueDate = x.DueDate,
                    Task = x.Task,
                    CreatedAt = x.CreatedAt,
                    IsCompleted = x.IsCompleted
                };
                toDoList.Add(newTodo);
            }
            return toDoList;
        }
        
    }
}

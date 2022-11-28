using ToDoAuth.DTO;

namespace ToDoAuth.Services
{
    public interface IToDoService
    {
        Task<ServiceResponse<List<GetToDoDTO>>> AddToDo(AddToDoDTO request);
        Task<ServiceResponse<List<GetToDoDTO>>> AllToDos();
        Task<ServiceResponse<GetToDoDTO>> GetToDo(Guid id);
        Task<ServiceResponse<GetToDoDTO>>  UpdateToDo(UpdateToDoDTO request);
        Task<ServiceResponse<List<GetToDoDTO>>> DeleteToDo(Guid id);
    }
}
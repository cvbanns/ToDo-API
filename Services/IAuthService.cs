using ToDoAuth.DTO;

namespace ToDoAuth.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(RegisterDTO registerDTO);
        Task<ServiceResponse<string>> Login(LoginDTO loginDTO);

    }
}
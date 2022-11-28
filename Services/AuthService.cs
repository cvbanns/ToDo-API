using System.Security.Claims;
using System.Net.Http.Headers;
using System;
using System.Security.Cryptography;
using ToDoAuth.Data;
using ToDoAuth.DTO;
using ToDoAuth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ToDoAuth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ToDoDbContext _toDoDbContext;
        private readonly IConfiguration _configuration;

        public AuthService(ToDoDbContext toDoDbContext, IConfiguration configuration)
        {
            _toDoDbContext = toDoDbContext;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(LoginDTO loginDTO)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var user = await _toDoDbContext.ToDoUsers.Where(x => x.Email == loginDTO.Email).FirstAsync();
                if(user == null)
                {
                    response.Message = "User not found"; 
                }
                
                if(!VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Incorrect Password";
                    return response;
                }
                response.Data = CreateToken(user);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            response.Message = "Login Successful";
            return response;
        }

        public async Task<ServiceResponse<string>> Register(RegisterDTO registerDTO)
        {
            var response = new ServiceResponse<string>();
            GeneratePasswordHash(registerDTO.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            var newUser = new ToDoUser
            {
                Id = Guid.NewGuid(),
                Email = registerDTO.Email,
                Role = registerDTO.Role,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt, 
            };
            await _toDoDbContext.ToDoUsers.AddAsync(newUser);
            await _toDoDbContext.SaveChangesAsync();
            response.Message = "Registration Successful";
            return response;
        }

        private string CreateToken(ToDoUser toDoUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, toDoUser.Id.ToString()),
                new Claim(ClaimTypes.Role, toDoUser.Role.ToString())
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey
            (
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value)
            );
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = cred
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
        private void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
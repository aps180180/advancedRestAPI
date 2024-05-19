using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Models;

namespace AdvancedRestAPI.Interfaces
{
    public interface IUser
    {
        Task<(bool isSuccess,PagedList<UserDTO> Users,string ErrorMessage)> GetAllUsers(int pageNumber, int pageSize);
        Task<(bool isSuccess, PagedList<UserDTO> Users, string ErrorMessage)> GetUsersByNamePaged(string? name, int pageNumber, int pageSize);
        Task<(bool isSuccess, UserDTO User, string ErrorMessage)> GetUserById( Guid Id);
        Task<(bool isSuccess, string ErrorMessage)> AddUser(UserDTO user);
        Task<(bool isSuccess,  string ErrorMessage)> UpdateUser(Guid id, UserDTO user); 
        Task<(bool isSuccess, string ErrorMessage)> DeleteUser(Guid id);
    }
}

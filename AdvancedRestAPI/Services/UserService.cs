using AdvancedRestAPI.Data;
using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Extensions;
using AdvancedRestAPI.Interfaces;
using AdvancedRestAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedRestAPI.Services
{
    public class UserService : IUser
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public UserService( AppDbContext context, IMapper mapper)
        {
                _context = context;
                _mapper= mapper;
        }

        public async Task<(bool isSuccess, string ErrorMessage)> AddUser(UserDTO userdto)
        {
            if(userdto is not null)
            {
                var user =_mapper.Map<User>(userdto);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            return (false, "Please provide user data");   
        }

        public async Task<(bool isSuccess, string ErrorMessage)> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return(true, null);
            }
            return (false, "User not found");

        }

        public async Task<(bool isSuccess, PagedList<UserDTO> Users, string ErrorMessage)> GetAllUsers(int pageNumber, int pageSize)
        {

            var usersQuery = _context.Users.AsQueryable();
            var pagedUsers = await usersQuery.ToPagedListAsync(pageNumber, pageSize);
            var usersDTO = _mapper.Map<List<UserDTO>>(pagedUsers.Items);

            if (usersDTO.Any())
            {
                var result = new PagedList<UserDTO>(usersDTO, pagedUsers.TotalCount, pageNumber, pageSize);
                return (true, result, null);
            }

            return (false, null, "Users not found");

        }


        public async Task<(bool isSuccess, PagedList<UserDTO> Users, string ErrorMessage)> GetUsersByNamePaged( string? name,int pageNumber, int pageSize)
        {
            
            var usersQuery = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
             usersQuery =  usersQuery.Where(users=> users.Name!.ToLower().StartsWith(name.ToLower()));
            }
            var pagedUsers = await usersQuery.ToPagedListAsync(pageNumber, pageSize);
            var usersDTO = _mapper.Map<List<UserDTO>>(pagedUsers.Items);

            if (usersDTO.Any())
            {
                var result = new PagedList<UserDTO>(usersDTO, pagedUsers.TotalCount, pageNumber, pageSize);
                return (true, result, null);
            }

            return (false, null, "Users not found");

        }


        public async Task<(bool isSuccess, UserDTO User, string ErrorMessage)> GetUserById(Guid Id)
        {
            var user = await _context.Users.FindAsync( Id);
            if (user is not null)
            {
               var result= _mapper.Map<UserDTO>(user);
                return (true, result, null);
            }
            return (false, null, "User not found");
        }

        public async Task<(bool isSuccess, string ErrorMessage)> UpdateUser(Guid id, UserDTO userdto)
        {
            var userObject = await _context.Users.FindAsync(id);
            if(userObject is not null)
            {
               var user = _mapper.Map<User>(userdto);
                userObject.Name = user.Name;
                userObject.Address = user.Address;
                userObject.Phone = user.Phone;
                userObject.BloodGroup = user.BloodGroup;
                await _context.SaveChangesAsync();
                return (true, null);
            }
            return (false, "User not found");
            

        }
    }
}

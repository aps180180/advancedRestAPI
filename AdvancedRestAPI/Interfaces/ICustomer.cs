using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Models;

namespace AdvancedRestAPI.Interfaces
{
    public interface ICustomer
    {
        Task<(bool isSuccess,PagedList<CustomerDTO> Customers,string ErrorMessage)> GetAllCustomers(int pageNumber, int pageSize);
        Task<(bool isSuccess, PagedList<CustomerDTO> Customers, string ErrorMessage)> GetCustomersByNamePaged(string? name, int pageNumber, int pageSize);
        Task<(bool isSuccess, CustomerDTO Customer, string ErrorMessage)> GetCustomerById( Guid Id);
        Task<(bool isSuccess, string ErrorMessage)> AddCustomer(CustomerDTO customer);
        Task<(bool isSuccess,  string ErrorMessage)> UpdateCustomer(Guid id, CustomerDTO customer); 
        Task<(bool isSuccess, string ErrorMessage)> DeleteCustomer(Guid id);
    }
}

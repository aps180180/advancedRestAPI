using AdvancedRestAPI.Data;
using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Extensions;
using AdvancedRestAPI.Interfaces;
using AdvancedRestAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedRestAPI.Services
{
    public class CustomerService : ICustomer
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public CustomerService( AppDbContext context, IMapper mapper)
        {
                _context = context;
                _mapper= mapper;
        }

        public async Task<(bool isSuccess, string ErrorMessage)> AddCustomer(CustomerDTO customerdto)
        {
            if(customerdto is not null)
            {
                var customer =_mapper.Map<Customer>(customerdto);
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            return (false, "Please provide customer data");   
        }

        public async Task<(bool isSuccess, string ErrorMessage)> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer is not null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return(true, null);
            }
            return (false, "Customer not found");

        }

        public async Task<(bool isSuccess, PagedList<CustomerDTO> Customers, string ErrorMessage)> GetAllCustomers(int pageNumber, int pageSize)
        {

            var customersQuery = _context.Customers.AsQueryable();
            var pagedCustomers = await customersQuery.ToPagedListAsync(pageNumber, pageSize);
            var customersDTO = _mapper.Map<List<CustomerDTO>>(pagedCustomers.Items);

            if (customersDTO.Any())
            {
                var result = new PagedList<CustomerDTO>(customersDTO, pagedCustomers.TotalCount, pageNumber, pageSize);
                return (true, result, null);
            }

            return (false, null, "Customers not found");

        }


        public async Task<(bool isSuccess, PagedList<CustomerDTO> Customers, string ErrorMessage)> GetCustomersByNamePaged( string? name,int pageNumber, int pageSize)
        {
            
            var customersQuery = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                customersQuery = customersQuery.Where(c=> c.Name!.ToLower().StartsWith(name.ToLower()));
            }
            var pagedCustomers = await customersQuery.ToPagedListAsync(pageNumber, pageSize);
            var customersDTO = _mapper.Map<List<CustomerDTO>>(pagedCustomers.Items);

            if (customersDTO.Any())
            {
                var result = new PagedList<CustomerDTO>(customersDTO, pagedCustomers.TotalCount, pageNumber, pageSize);
                return (true, result, null);
            }

            return (false, null, "Customers not found");

        }


        public async Task<(bool isSuccess, CustomerDTO Customer, string ErrorMessage)> GetCustomerById(Guid Id)
        {
            var customer = await _context.Customers.FindAsync( Id);
            if (customer is not null)
            {
               var result= _mapper.Map<CustomerDTO>(customer);
                return (true, result, null);
            }
            return (false, null, "Customer not found");
        }

        public async Task<(bool isSuccess, string ErrorMessage)> UpdateCustomer(Guid id, CustomerDTO customerdto)
        {
            var customerObject = await _context.Customers.FindAsync(id);
            if(customerObject is not null)
            {
               var customer = _mapper.Map<Customer>(customerdto);
                customerObject.Name = customer.Name;
                customerObject.Address = customer.Address;
                customerObject.Phone = customer.Phone;
                customerObject.BloodGroup = customer.BloodGroup;
                await _context.SaveChangesAsync();
                return (true, null);
            }
            return (false, "Customer not found");
            

        }
    }
}

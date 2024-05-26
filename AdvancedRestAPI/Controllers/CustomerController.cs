using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Interfaces;
using AdvancedRestAPI.Models;
using AdvancedRestAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;



namespace AdvancedRestAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customerService;
       
        public CustomerController( ICustomer customerService)
        {
                _customerService = customerService;
                
        }
        // GET: api/v1/<CustomerController>
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageNumber ?? 5;
           
            var result = await _customerService.GetAllCustomers(currentPageNumber,currentPageSize);

            if (result.isSuccess)
            {
                var response = new
                {
                    Customers = result.Customers.Items,
                    TotalCount = result.Customers.TotalCount,
                    CurrentPage = result.Customers.CurrentPage,
                    PageSize = result.Customers.PageSize,
                    TotalPages = result.Customers.TotalPages
                };

                return Ok(response);
            }

            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCustomersByName(string name,  int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageNumber ?? 5;

            var result = await _customerService.GetCustomersByNamePaged(name, currentPageNumber, currentPageSize);

            if (result.isSuccess)
            {
                var response = new
                {
                    Customers = result.Customers.Items,
                    TotalCount = result.Customers.TotalCount,
                    CurrentPage = result.Customers.CurrentPage,
                    PageSize = result.Customers.PageSize,
                    TotalPages = result.Customers.TotalPages
                };

                return Ok(response);
            }

            return NotFound(result.ErrorMessage);
        }
        // GET api/v1/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _customerService.GetCustomerById(id);
            if (result.isSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound(result.ErrorMessage);

        }
        // POST api/v1/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result= await _customerService.AddCustomer(customer);
            if (result.isSuccess) 
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(result.ErrorMessage);
        }

        // PUT api/v1/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =  await _customerService.UpdateCustomer(id, customer);
            if (result.isSuccess) 
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }

        // DELETE api/v1/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if(result.isSuccess) 
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}

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
    public class UsersController : ControllerBase
    {
        private IUser _userService;
       
        public UsersController( IUser userService)
        {
                _userService = userService;
                
        }
        // GET: api/<UsersController>
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageNumber ?? 5;
           
            var result = await _userService.GetAllUsers(currentPageNumber,currentPageSize);

            if (result.isSuccess)
            {
                var response = new
                {
                    Users = result.Users.Items,
                    TotalCount = result.Users.TotalCount,
                    CurrentPage = result.Users.CurrentPage,
                    PageSize = result.Users.PageSize,
                    TotalPages = result.Users.TotalPages
                };

                return Ok(response);
            }

            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUsersByName(string name,  int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageNumber ?? 5;

            var result = await _userService.GetUsersByNamePaged(name, currentPageNumber, currentPageSize);

            if (result.isSuccess)
            {
                var response = new
                {
                    Users = result.Users.Items,
                    TotalCount = result.Users.TotalCount,
                    CurrentPage = result.Users.CurrentPage,
                    PageSize = result.Users.PageSize,
                    TotalPages = result.Users.TotalPages
                };

                return Ok(response);
            }

            return NotFound(result.ErrorMessage);
        }
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.GetUserById(id);
            if (result.isSuccess)
            {
                return Ok(result.User);
            }
            return NotFound(result.ErrorMessage);

        }
        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result= await _userService.AddUser(user);
            if (result.isSuccess) 
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(result.ErrorMessage);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =  await _userService.UpdateUser(id, user);
            if (result.isSuccess) 
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteUser(id);
            if(result.isSuccess) 
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}

using CMSMicroService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMSMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly ApiDBContext _context;
       // private UserModel user;
        public UserDetailsController(ApiDBContext context)
        {
            _context = context;          
        }

        // GET: api/<UserDetailsController>
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {  
            return _context.Users;
        }

        // GET api/<UserDetailsController>/5
        [HttpGet("{userId}")]
        public IEnumerable<UserModel> Get(int userId)
        {            
            return _context.Users.Where(p => p.userId == userId);
        }

        //I think this is what they wanted
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                //Check if the user exists
                if (Common.CheckUserExists(_context, user.username))
                {
                    return BadRequest();

                }

                Common.AddUserToDB(_context, user);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }             

        // PUT api/<UserDetailsController>/5
        [HttpPut("{userId}")]
        public IActionResult Put([FromBody] UserModel user)
        {        
            if (!Common.CheckUserExistsById(_context, user.userId))
            {            
                return BadRequest();
            }

            Common.UpdateDBUser(_context, user);

            return Ok();
        }

        // DELETE api/<UserDetailsController>/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            if (!Common.CheckUserExistsById(_context, userId))
            {
                return BadRequest();
            }

            Common.DeleteDBUser(_context, userId);
            return Ok();
        }

  
    }
}

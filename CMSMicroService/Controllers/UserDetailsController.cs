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

        // POST api/<UserDetailsController>
        [HttpPost]
        public string Post([FromBody] string UserName)
        {
            //Check if the user is null or empty
            if (String.IsNullOrEmpty(UserName))
            {
                return "Username cannot be null or empty";
            }
            //Check if the user exists
            if (Common.CheckUserExists(_context, UserName))
            {
                return string.Format("User {0} already Exists", UserName);
            }
           //Call method to add user to DB
            Common.AddUserToDB(_context, UserName);
          
            return "User added successfully";
        }

        // PUT api/<UserDetailsController>/5
        [HttpPut("{userId}")]
        public string Put(int userId, [FromBody] string UserName)
        {        
            if (!Common.CheckUserExistsById(_context, userId))
            {
                return "User not found";
            }

            Common.UpdateDBUser(_context, userId, UserName);           

            return "User Updated";
        }

        // DELETE api/<UserDetailsController>/5
        [HttpDelete("{userId}")]
        public string Delete(int userId)
        {
            if (!Common.CheckUserExistsById(_context, userId))
            {
                return "User not found";
            }

            Common.DeleteDBUser(_context, userId);            
            return "User Deleted";
        }
    }
}

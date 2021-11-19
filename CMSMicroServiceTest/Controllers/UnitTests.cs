using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMSMicroService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMSMicroService.Models;

namespace CMSMicroService.Controllers.Tests
{
    [TestClass()]
    public class UnitTests
    {
        private ApiDBContext _context;
        private UserDetailsController API;
        public UnitTests()
        {        

        }

        [TestMethod()]
        public void T01Post_AddNewUser_ReturnsSuccessMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            //Act
            string result = API.Post("Paul");
            _context.Dispose();
            //Assert
            Assert.AreEqual("User added successfully", result);
        }

        public void T02Post_AddExistingUser_ReturnsUserExistsMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            string existing = API.Post("Paul");
            //Act
            string result = API.Post("Paul");
            _context.Dispose();
            //Assert
            Assert.AreEqual("User Paul already Exists", result);
        }

        [TestMethod()]
        public void T03Get_AllUsers_ReturnsUserObject()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            string user1 = API.Post("Paul");
            string user2 = API.Post("Dave");
            //Act          
            var result = API.Get();
            _context.Dispose();
            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<UserModel>));
        }

        [TestMethod()]
        public void T04Get_UserByID_ReturnsUserObject()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            string user1 = API.Post("Paul");
            string user2 = API.Post("Dave");
            //Act
            var result = API.Get(1);
            _context.Dispose();
            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<UserModel>));
        }

        [TestMethod()]
        public void T05Put_UpdateUserName_ReturnUpdateMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            string user1 = API.Post("Paul");
            string user2 = API.Post("Dave");
            //Act
            var result = API.Put(1, "James");
            _context.Dispose();
            //Assert
            Assert.AreEqual("User Updated", result);            
        }

        [TestMethod()]
        public void T06Put_UdateNoexistingUser_ReturnNotfoundMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context); 
            string user1 = API.Post("Paul");           
            //Act
            var result = API.Put(99, "Dave");
            _context.Dispose();
            //Assert
            Assert.AreEqual("User not found", result);
        }

        [TestMethod()]
        public void T07Delete_UserByID_ReturnDeletedMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            string user1 = API.Post("Paul");
            string user2 = API.Post("Dave");
            //Act
            var result = API.Delete(1);
            _context.Dispose();
            //Assert
            Assert.AreEqual("User Deleted", result);
            
        }

        [TestMethod()]
        public void T08Delete_NoexistingUser_ReturnNotfoundMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            string user1 = API.Post("Paul");
            string user2 = API.Post("Dave");          
            //Act
            var result = API.Delete(99);
            _context.Dispose();
            //Assert
            Assert.AreEqual("User not found", result);

        }
    }
}
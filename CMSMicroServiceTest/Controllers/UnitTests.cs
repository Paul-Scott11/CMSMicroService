using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMSMicroService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMSMicroService.Models;
using Microsoft.AspNetCore.Mvc;

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
            UserModel user = new UserModel();
            user.username = "Paul";         
            var result = API.Post(user) as StatusCodeResult;
            _context.Dispose();
            //Assert
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod()]
        public void T02Post_AddExistingUser_ReturnsUserExistsMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            UserModel user = new UserModel();
            user.username = "Paul";
            var existing = API.Post(user);
            //Act
            UserModel user2 = new UserModel();
            user2.username = "Paul";
            var result = API.Post(user2) as StatusCodeResult; ;
            _context.Dispose();
            //Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod()]
        public void T03Get_AllUsers_ReturnsUserObject()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            UserModel user = new UserModel();
            user.username = "Paul";
            user.username = "dave";
            var user1 = API.Post(user);
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
            UserModel user = new UserModel();
            user.username = "Paul";
            user.username = "dave";
            var user1 = API.Post(user);          
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
             UserModel user = new UserModel();
            user.username = "Paul";
            user.username = "dave";
            var user1 = API.Post(user);
            //Act
            UserModel user2 = new UserModel();
            user2.userId = 1;
            user2.username = "James";
            var result = API.Put(user2) as StatusCodeResult;
            _context.Dispose();
            //Assert
            Assert.AreEqual(200, result.StatusCode);            
        }

        [TestMethod()]
        public void T06Put_UdateNoexistingUser_ReturnNotfoundMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            UserModel user = new UserModel();
            user.username = "Paul";            
            var user1 = API.Post(user);
            //Act
            UserModel user2 = new UserModel();
            user2.userId = 99;
            user2.username = "Dave";
            var result = API.Put(user2) as StatusCodeResult; 
            _context.Dispose();
            //Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod()]
        public void T07Delete_UserByID_ReturnDeletedMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            UserModel user = new UserModel();
            user.username = "Paul";
            user.username = "dave";
            var user1 = API.Post(user);
            //Act
            var result = API.Delete(1) as StatusCodeResult;
            _context.Dispose();
            //Assert
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod()]
        public void T08Delete_NoexistingUser_ReturnNotfoundMessage()
        {
            //Arrange
            _context = new ApiDBContext(new DbContextOptionsBuilder<ApiDBContext>().UseInMemoryDatabase(databaseName: "TestCMSInMemoryDatabase").Options);
            API = new UserDetailsController(_context);
            UserModel user = new UserModel();
            user.username = "Paul";
            user.username = "dave";
            var user1 = API.Post(user);
            //Act
            var result = API.Delete(99) as StatusCodeResult;
            _context.Dispose();
            //Assert
            Assert.AreEqual(400, result.StatusCode);

        }
    }
}
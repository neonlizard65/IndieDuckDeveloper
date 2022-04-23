using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataClasses.Models;
using System.Linq;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public async Task GetUsers_TestAsync()
        {
            //Arrange
            var expected = "kristianbale55";
            //Act
            var list = await User.GetAllUsersAsync();
            var result = list.Where(x => x.UserID == 3).FirstOrDefault().UserName;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetUserByLogin_TestAsync()
        {
            //Arrange
            var expected = (uint)3;
            //Act
            var result = await User.GetUserByLogin("kristianbale55");
            //Assert
            Assert.AreEqual(expected, result.UserID);
        }

        [TestMethod]
        public async Task GetUserByPhone_TestAsync()
        {
            //Arrange
            var expected = (uint)1;
            //Act
            var result = await User.GetUserByPhone("88005553535");
            //Assert
            Assert.AreEqual(expected, result.UserID);
        }

        [TestMethod]
        public async Task GetUserByEmail_TestAsync()
        {
            //Arrange
            var expected = (uint)2;
            //Act
            var result = await User.GetUserByEmail("yusufpalmer22@gmail.com");
            //Assert
            Assert.AreEqual(expected, result.UserID);
        }

        [TestMethod]
        public async Task GetUsersBySearch_TestAsync()
        {
            //Arrange
            var expected = "kristianbale55";
            //Act
            var list = await User.GetUsersBySearchAsync("kris");
            var result = list.FirstOrDefault().UserName;
            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}

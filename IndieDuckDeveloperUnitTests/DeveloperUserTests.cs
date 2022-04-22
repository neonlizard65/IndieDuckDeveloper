using DataClasses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class DeveloperUserTests
    {
        [TestMethod]
        public async Task GetDevelopers_TestAsync()
        {
            //Arrange
            var expected = 2;
            //Act
            var list = await DeveloperUser.GetDevelopersAsync();
            var result = list.Count;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetDeveloperByEmail_Test()
        {
            //Arrange
            var expected = "AntonS";
            //Act
            var devuser = await DeveloperUser.GetDeveloperUserByEmailAsync("antonsirotkin@mail.ru");
            var result = devuser.DeveloperUserName;
            //Assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public async Task GetDeveloperByName_Test()
        {
            //Arrange
            var expected = "kirovsergey@mail.ru";
            //Act
            var devuser = await DeveloperUser.GetDeveloperUserByNameAsync("Kirieshka12");
            var result = devuser.DeveloperUserEmail;
            //Assert
            Assert.AreEqual(expected, result);
        }

        /*   [TestMethod]
           public async Task CreateDevUser_Test()
          {
              //Arrange
              DeveloperUser du = new DeveloperUser(0, "Anikashin24", "a", "anikash444@gmail.com", "", 4, 1);
              //Act
              var result = await DeveloperUser.CreateAsync(du);
              //Assert
              Assert.IsTrue(result);
          }
            [TestMethod]
         public async Task CreateDevUserFalse_Test()
           {
               //Arrange
               DeveloperUser du = new DeveloperUser(0, "", "", "", "", 4, 1);
               //Act
               var result = await DeveloperUser.CreateAsync(du);
               //Assert
               Assert.IsFalse(result);
           } */

        [TestMethod]
        public async Task UpdateDevUser_Test()
        {
            //Arrange
            DeveloperUser du = new DeveloperUser(6, "Anikashin24", "a", "anikash434@gmail.com", "", 4, 1);
            //Act
            var result = await DeveloperUser.EditAsync(du);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateGuardCode_Test()
        {
            //Arrange
            DeveloperUser du = new DeveloperUser(6, "Anikashin24", "a", "anikash434@gmail.com", "", 4, 1);
            //Act
            var result = await du.UpdateGuardCodeAsync();
            //Assert
            Assert.IsTrue(result);
        }
    }
}

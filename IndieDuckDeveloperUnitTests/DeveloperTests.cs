using DataClasses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class DeveloperImportTest
    {

        [TestMethod]
        public async Task GetDevelopersByID_NameTest()
        {
            //Arrange
            var expected = "VesnaGames";
            //Act
            var dev = await Developer.GetDeveloperByIDAsync(4);
            //Assert
            Assert.AreEqual(expected, dev.DeveloperName);
        }

        [TestMethod]
        public async Task GetDevelopersByCountryAsync_CountTest()
        {
            //Arrange
            var expected = 1;
            //Act
            var list = await Developer.GetDevelopersByCountryAsync(199);
            //Assert
            Assert.AreEqual(expected, list.Count);
        }

        /*       Success
         *       
         *       [TestMethod]
               public async Task CreateDeveloper_Test()
               {
                   //Arrange
                   Developer dev = new Developer(0, "WagnerGames", "", "12345551231245", "", "", "", "Great bio btw", 26);
                   //Act
                   var result = await Developer.CreateAsync(dev);
                   //Assert
                   Assert.IsTrue(result);
               }

        [TestMethod]
        public async Task UpdateDeveloper_Test()
        {
            //Arrange
            Developer dev = new Developer(6, "WagnerGames", "", "1234555123124566", "", "", "", "Great bio btw", 26);
            //Act
            var result = await Developer.EditAsync(dev);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteDeveloper_Test()
        {
            //Arrange
            Developer dev = new Developer(2, "", "", "", "", "", "", "", 1);
            //Act
            var result = await Developer.DeleteAsync(dev);
            //Assert
            Assert.IsTrue(result);
        }*/

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataClasses.Models;
using System.Linq;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public async Task GetGroups_TestAsync()
        {
            //Arrange
            var expected = "Indielovers";
            //Act
            var list = await Group.GetAllGroupsAsync();
            var result = list.Where(x => x.GroupID == 3).FirstOrDefault().GroupName;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetGroupByID_TestAsync()
        {
            //Arrange
            var expected = "Indielovers";
            //Act
            var result = await Group.GetGroupByID(3);
            //Assert
            Assert.AreEqual(expected, result.GroupName);
        }

        [TestMethod]
        public async Task GetGroupBySearch_TestAsync()
        {
            //Arrange
            var expected = (uint)3;
            //Act
            var list = await Group.GetGroupsBySearchAsync("indie");
            var result = list.FirstOrDefault().GroupID;
            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}

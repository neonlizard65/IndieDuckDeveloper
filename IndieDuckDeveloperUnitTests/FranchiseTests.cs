using DataClasses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class FranchiseTests
    {
        [TestMethod]
        public async Task GetFranchises_TestAsync()
        {
            //Arrange
            var expected = "Cassadia";
            //Act
            var list = await Franchise.GetFranchisesAsync();
            var result = list.Where(x => x.FranchiseID == 3).FirstOrDefault().FranchiseName;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task CreateFranchise_Test()
        {
            //Arrange
            Franchise fran = new Franchise(0, "Backrooms", "");
            //Act
            var result = await Franchise.CreateAsync(fran);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateDeveloper_Test()
        {
            //Arrange
            Franchise fran = new Franchise(1, "Keeran", "a");
            //Act
            var result = await Franchise.EditAsync(fran);
            //Assert
            Assert.IsTrue(result);
        }
    }
}

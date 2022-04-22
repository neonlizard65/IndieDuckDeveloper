using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataClasses.Models;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class PublisherTests
    {
        [TestMethod]
        public async Task GetPublishers_TestAsync()
        {
            //Arrange
            var expected = 2;
            //Act
            var list = await Publisher.GetPublishersAsync();
            var result = list.Count;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetPublisherByID_TestAsync()
        {
            //Arrange
            var expected = "Garniere Games";
            //Act
            var pub = await Publisher.GetPublisherByIDAsync(1);
            //Assert
            Assert.AreEqual(expected, pub.PublisherName);
        }

        /*  [TestMethod]
          public async Task CreatePublisher_TestAsync()
          {
              //Arrange
              Publisher pub = new Publisher(0, "2P Entertainment", "");
              //Act
              var result =await Publisher.CreateAsync(pub);
              //Assert
              Assert.IsTrue(result);
          }
          */
        [TestMethod]
        public async Task EditPublisher_TestAsync()
        {
            //Arrange
            Publisher pub = new Publisher(5, "3P Entertainment", "");
            //Act
            var result = await Publisher.EditAsync(pub);
            //Assert
            Assert.IsTrue(result);
        }
    }
}

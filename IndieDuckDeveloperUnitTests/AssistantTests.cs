using DataClasses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class AssistantTests
    {
        [TestMethod]
        public async Task GetAssistants_TestAsync()
        {
            //Arrange
            var expected = 2;
            //Act
            var list = await Assistant.GetAssistantsAsync();
            var result = list.Count;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetAssistantByName_TestAsync()
        {
            //Arrange
            var expected = (uint)5;
            //Act
            var ast = await Assistant.GetAssistantByUserNameAsync("vladkail12");
            var result = ast.AssistantID;
            //Assert
            Assert.AreEqual(expected, result);
        }

        /*  [TestMethod]
          public async Task CreateAssistant_TestAsync()
          {
              //Arrange
              Assistant ast = new Assistant(0, "alberto23", "Alberto Moreno", "aa");
              //Act
              var result = await Assistant.CreateAsync(ast);
              //Assert
              Assert.IsTrue(result);
          }*/

        [TestMethod]
        public async Task EditAssistant_TestAsync()
        {
            //Arrange
            Assistant ast = new Assistant(8, "keramik52", "Kirill Zhivoglyadov", "2asd");
            //Act
            var result = await Assistant.EditAsync(ast);
            //Assert
            Assert.IsTrue(result);
        }
    }
}

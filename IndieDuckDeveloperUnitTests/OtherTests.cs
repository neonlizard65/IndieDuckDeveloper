using DataClasses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IndieDuckDeveloperUnitTests
{
    [TestClass]
    public class OtherTests
    {
        [TestMethod]
        public async Task GetStatuses_TestAsync()
        {
            //Arrange
            var expected = 6;
            //Act
            var list = await Status.GetStatusesAsync();
            var result = list.Count;
            //Assert
            Assert.AreEqual(expected, result);
            
        }

        [TestMethod]
        public async Task GetCountries_TestAsync()
        {
            //Arrange
            var expected = 235;
            //Act
            var list = await Country.GetCountriesAsync();
            var result = list.Count;
            //Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public async Task GetTags_TestAsync()
        {
            //Arrange
            var expected = "Gothic";
            //Act
            var list = await Tag.GetTagsAsync();
            var result = list.Where(x => x.ID == 210).FirstOrDefault().Name;
            //Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public async Task GetLevels_TestAsync()
        {
            //Arrange
            var expected1 = (uint)1200;
            var expected2 = "Experienced level discount";
            //Act
            var list = await Level.GetLevelsAsync();
            var lvl13 = list.Where(x => x.LevelID == 13).FirstOrDefault();
            var result1 = lvl13.LevelXP;
            var result2 = lvl13.Coupon.CouponName;

            //Assert
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);


        }

        [TestMethod]
        public async Task GetTicketReasons_TestAsync()
        {
            //Arrange
            var expected = "I want to permanently remove this game from my account";
            //Act
            var list = await TicketReason.GetTicketReasonsAsync();
            var result = list.Where(x => x.TicketReasonID == 8).FirstOrDefault().Name;
            //Assert
            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        public async Task GetReportReasons_TestAsync()
        {
            //Arrange
            var expected = "They are involved in theft, scamming, fraud or other malicious activity";
            //Act
            var list = await ReportReason.GetReportReasonsAsync();
            var result = list.Where(x => x.ReportReasonID == 5).FirstOrDefault().ReasonName;
            //Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public async Task GetRoles_TestAsync()
        {
            //Arrange
            var expected = "Moderator";
            //Act
            var list = await Role.GetRolesAsync();
            var result = list.Where(x => x.RoleID == 2).FirstOrDefault().RoleName;
            //Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public async Task GetPrivacyTypes_TestAsync()
        {
            //Arrange
            var expected = "Friend of friend";
            //Act
            var list = await PrivacyType.GetPrivacyTypesAsync();
            var result = list.Where(x => x.PrivacyTypeID == 3).FirstOrDefault().Name;
            //Assert
            Assert.AreEqual(expected, result);

        }


        [TestMethod]
        public async Task GetPlatforms_TestAsync()
        {
            //Arrange
            var expected = "Linux";
            //Act
            var list = await Platform.GetPlatformsAsync();
            var result = list.Where(x => x.PlatformID == 2).FirstOrDefault().Name;
            //Assert
            Assert.AreEqual(expected, result);

        }
    }
}

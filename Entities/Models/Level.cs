using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataClasses.Models
{
    /// <summary>
    /// Класс уровней
    /// </summary>
    public class Level
    {
        /// <summary>
        /// ID уровня
        /// </summary>
        public uint LevelID { get; set; }

        /// <summary>
        /// Номер уровня
        /// </summary>
        public uint LevelNumber { get; set; }

        /// <summary>
        /// Опыт для достижения уровня
        /// </summary>
        public uint LevelXP { get; set; }

        /// <summary>
        /// ID купона при достижении уровня
        /// </summary>
        public uint CouponId { get; set; }

        /// <summary>
        /// Купон
        /// </summary>
        public Coupon Coupon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelID">ID уровня</param>
        /// <param name="levelNumber">Номер уровня</param>
        /// <param name="levelXP">Опыт для достижения уровня</param>
        /// <param name="couponId">ID купона при достижении уровня</param>
        public Level(uint levelID, uint levelNumber, uint levelXP, uint couponId)
        {
            LevelID = levelID;
            LevelNumber = levelNumber;
            LevelXP = levelXP;
            CouponId = couponId;
            Coupon = Coupon.GetCouponsAsync().Result.Where(x=>x.CouponID == couponId).FirstOrDefault();
        }

        /// <summary>
        /// Асинхронное получение списка уровней
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка уровней</returns>
        public static async Task<List<Level>> GetLevelsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/level/getLevels.php");
            var content = await jsonData;
            var levList = await JsonSerializer.DeserializeAsync<List<Level>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return levList;
        }
    }
}

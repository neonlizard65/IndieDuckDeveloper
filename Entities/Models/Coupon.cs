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
    /// Класс купонов
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// ID купона
        /// </summary>
        public uint CouponID { get; set; }
        /// <summary>
        /// Название купона
        /// </summary>
        public string CouponName { get; set; }
        /// <summary>
        /// Процент скидки
        /// </summary>
        public uint DiscountPercent { get; set; }

        /// <summary>
        /// Конструктор купона
        /// </summary>
        /// <param name="couponID">ID купона</param>
        /// <param name="couponName">Название купона</param>
        /// <param name="discountPercent">Процент скидки</param>
        public Coupon(uint couponID, string couponName, uint discountPercent)
        {
            CouponID = couponID;
            CouponName = couponName;
            DiscountPercent = discountPercent;
        }

        /// <summary>
        /// Асинхронное получение списка купонов
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка купонов</returns>
        public static async Task<List<Coupon>> GetCouponsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/coupon/getCoupons.php");
            var content = await jsonData;
            var coupList = await JsonSerializer.DeserializeAsync<List<Coupon>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return coupList;
        }
    }
}

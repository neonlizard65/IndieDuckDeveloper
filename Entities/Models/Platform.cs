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
    /// Класс платформ
    /// </summary>
    public class Platform
    {
        /// <summary>
        /// ID платформы
        /// </summary>
        public uint PlatformID { get; set; }
        /// <summary>
        /// Название платформы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор платформы
        /// </summary>
        /// <param name="platformID">ID платформы</param>
        /// <param name="name">Название платформы</param>
        public Platform(uint platformID, string name)
        {
            PlatformID = platformID;
            Name = name;
        }

        /// <summary>
        /// Асинхронное получение списка платформ
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка платформ</returns>
        public static async Task<List<Platform>> GetPlatformsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/platform/getPlatforms.php");
            var content = await jsonData;
            var platformList = await JsonSerializer.DeserializeAsync<List<Platform>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return platformList;
        }
    }
}

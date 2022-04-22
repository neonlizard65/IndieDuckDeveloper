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
    /// Класс степеней приватности
    /// </summary>
    public class PrivacyType
    {
        /// <summary>
        /// ID степени приватности
        /// </summary>
        public uint PrivacyTypeID { get; set; }

        /// <summary>
        /// Название степени приватности
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор степеней приватности
        /// </summary>
        /// <param name="privacyTypeID">ID степени приватности</param>
        /// <param name="name">Название степени приватности</param>
        public PrivacyType(uint privacyTypeID, string name)
        {
            PrivacyTypeID = privacyTypeID;
            Name = name;
        }

        /// <summary>
        /// Асинхронное получение списка степеней приватности
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка степеней приватности</returns>
        public static async Task<List<PrivacyType>> GetPrivacyTypesAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/privacytype/getPrivacyTypes.php");
            var content = await jsonData;
            var ptList = await JsonSerializer.DeserializeAsync<List<PrivacyType>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return ptList;
        }
    }
}

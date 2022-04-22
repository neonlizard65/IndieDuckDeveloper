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
    /// Класс для получения тэгов для игр
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// ID тэга
        /// </summary>
        [JsonPropertyName("TagID")]
        public string ID { get; set; }

        /// <summary>
        /// Название тэга
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор объекта тэга
        /// </summary>
        /// <param name="id">ID тэга</param>
        /// <param name="name">Название тэга</param>
        public Tag(string id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        /// Асинхронное получение списка тэгов
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка тэгов</returns>
        public static async Task<List<Country>> GetCountriesAsync()
        {
            HttpClient client = new HttpClient();
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/tag/getTags.php");
            var content = await jsonData;
            var tagList = await JsonSerializer.DeserializeAsync<List<Country>>(new MemoryStream(Encoding.UTF8.GetBytes(content)));
            return tagList;
        }

    }

}

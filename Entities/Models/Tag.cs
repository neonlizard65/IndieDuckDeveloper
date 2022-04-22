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
        public uint ID { get; set; }

        /// <summary>
        /// Название тэга
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор объекта тэга
        /// </summary>
        /// <param name="id">ID тэга</param>
        /// <param name="name">Название тэга</param>
        public Tag(uint id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        /// Асинхронное получение списка тэгов
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка тэгов</returns>
        public static async Task<List<Tag>> GetTagsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/tag/getTags.php");
            var content = await jsonData;
            var tagList = await JsonSerializer.DeserializeAsync<List<Tag>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return tagList;
        }

    }

}

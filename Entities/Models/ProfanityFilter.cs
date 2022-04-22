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
    /// Список запрещенных слов
    /// </summary>
    public class ProfanityFilter
    { 
        /// <summary>
        /// ID запрещенного слова
        /// </summary>
        public uint ProfanityFilterID { get; set; }

        /// <summary>
        /// Запрещенное слово
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Конструктор запрещенных слов
        /// </summary>
        /// <param name="profanityFilterID">ID запрещенного слова</param>
        /// <param name="word">Запрещенное слово</param>
        public ProfanityFilter(uint profanityFilterID, string word)
        {
            ProfanityFilterID = profanityFilterID;
            Word = word;
        }

        /// <summary>
        /// Асинхронное получение списка запрещенных слов
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка запрещенных слов</returns>
        public static async Task<List<ProfanityFilter>> GetProfanityFiltersAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/profanityfilter/getProfanityFilter.php");
            var content = await jsonData;
            var pfList = await JsonSerializer.DeserializeAsync<List<ProfanityFilter>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return pfList;
        }
    }
}

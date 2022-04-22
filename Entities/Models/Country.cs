using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Класс для получения списка стран
    /// </summary>
    public class Country
    {
        /// <summary>
        /// ID страны
        /// </summary>
        [JsonPropertyName("CountryID")]
        public uint ID { get; set; }

        /// <summary>
        /// Название страны
        /// </summary>
        [JsonPropertyName("CountryName")]
        public string Name { get; set; }

        /// <summary>
        /// Код страны (из 2-х букв)
        /// </summary>
        [JsonPropertyName("CountryCode")]
        public string Code { get; set; }

        /// <summary>
        /// Путь к флагу страны
        /// </summary>
        [JsonPropertyName("CountryFlag")]
        public string Flag { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">ID страны</param>
        /// <param name="name">Название страны</param>
        /// <param name="code">Код страны</param>
        /// <param name="flag">Путь к флагу страны</param>
        public Country(uint id, string name, string code, string flag)
        {
            ID = id;
            Name = name;
            Code = code;
            Flag = flag;
        }

        /// <summary>
        /// Асинхронное получение списка стран
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка стран</returns>
        public static async Task<List<Country>> GetCountriesAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/country/getCountries.php");
            var content = await jsonData;
            var countryList = await JsonSerializer.DeserializeAsync<List<Country>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return countryList;
        }
    }
}

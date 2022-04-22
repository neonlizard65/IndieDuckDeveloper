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
    /// Класс компании разработчика
    /// </summary>
    public class Developer
    {
        /// <summary>
        /// ID разработчика
        /// </summary>
        public uint DeveloperID { get; set; }

        /// <summary>
        /// Название разработчика
        /// </summary>
        public string DeveloperName { get; set; }

        /// <summary>
        /// Путь к логотипу разработчика
        /// </summary>
        public string DeveloperLogo { get; set; }

        /// <summary>
        /// Банковская карта разработчика
        /// </summary>
        public string DeveloperCard { get; set; }

        /// <summary>
        /// Ютуб канал разработчика
        /// </summary>
        public string DeveloperYoutube { get; set; }

        /// <summary>
        /// Твич канал разработчика
        /// </summary>
        public string DeveloperTwitch { get; set; }

        /// <summary>
        /// Твиттер разработчика
        /// </summary>
        public string DeveloperTwitter { get; set; }

        /// <summary>
        /// Описание разработчика
        /// </summary>
        public string DeveloperBio { get; set; }

        /// <summary>
        /// ID страны разработчика
        /// </summary>
        public uint CountryId { get; set; }

        /// <summary>
        /// Страна разработчика
        /// </summary>
        [JsonIgnore]
        public Country Country { get; set; }

        /// <summary>
        /// Конструктор разработчика
        /// </summary>
        /// <param name="developerID">ID разработчика</param>
        /// <param name="developerName">Название разработчика</param>
        /// <param name="developerLogo">Путь к логотипу разработчика</param>
        /// <param name="developerCard">Банковская карта разработчика</param>
        /// <param name="developerYoutube">Ютуб канал разработчика</param>
        /// <param name="developerTwitch">Твич канал разработчика</param>
        /// <param name="developerTwitter">Твиттер разработчика</param>
        /// <param name="developerBio">Описание разработчика</param>
        /// <param name="countryId">Страна разработчика</param>
        public Developer(uint developerID, string developerName, string developerLogo, string developerCard, string developerYoutube, string developerTwitch, string developerTwitter, string developerBio, uint countryId)
        {
            DeveloperID = developerID;
            DeveloperName = developerName;
            DeveloperLogo = developerLogo;
            DeveloperCard = developerCard;
            DeveloperYoutube = developerYoutube;
            DeveloperTwitch = developerTwitch;
            DeveloperTwitter = developerTwitter;
            DeveloperBio = developerBio;
            CountryId = countryId;
            Country = Country.GetCountriesAsync().Result.Where(x => x.ID == CountryId).FirstOrDefault();
        }

        /// <summary>
        /// Асинхронное получение списка разработчиков
        /// </summary>
        /// <returns>Task с типом списка разработчиков</returns>
        public static async Task<List<Developer>> GetDevelopersAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/developer/getDevelopers.php");
            var content = await jsonData;
            var devList = await JsonSerializer.DeserializeAsync<List<Developer>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return devList;
        }

        /// <summary>
        /// Асинхронное получение разработчика по ID
        /// </summary>
        /// <returns>Task с типом разработчика</returns>
        public static async Task<Developer> GetDeveloperByIDAsync(uint id)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/developer/getDeveloperByID.php?DeveloperID=" + id.ToString());
            var content = await jsonData;
            var dev = await JsonSerializer.DeserializeAsync<Developer>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return dev;
        }

        /// <summary>
        /// Асинхронное получение списка разработчиков по стране
        /// </summary>
        /// <returns>Task с типом списка разработчиков</returns>
        public static async Task<List<Developer>> GetDevelopersByCountryAsync(uint CountryId)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/developer/getDevelopersByCountry.php?CountryId=" + CountryId.ToString());
            var content = await jsonData;
            var devList = await JsonSerializer.DeserializeAsync<List<Developer>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return devList;
        }

        /// <summary>
        /// Асинхронное добавление разработчика
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> CreateAsync(Developer dev)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Developer>(dev);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/developer/create.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное удаление разработчика
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> DeleteAsync(Developer dev)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Developer>(dev);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/developer/delete.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное редактирование разработчика
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> EditAsync(Developer dev)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Developer>(dev);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/developer/update.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

    }
}

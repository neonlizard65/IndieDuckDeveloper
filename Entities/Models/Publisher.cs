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
    /// Класс издателя
    /// </summary>
    public class Publisher
    {
        /// <summary>
        /// ID издателя
        /// </summary>
        public uint PublisherID { get; set; }
        /// <summary>
        /// Название издателя
        /// </summary>
        public string PublisherName { get; set; }
        /// <summary>
        /// Логотип издателя
        /// </summary>
        public string PublisherLogo { get; set; }

        /// <summary>
        /// Конструктор издателя
        /// </summary>
        /// <param name="publisherID">ID издателя</param>
        /// <param name="publisherName">Название издателя</param>
        /// <param name="publisherLogo">Логотип издателя</param>
        public Publisher(uint publisherID, string publisherName, string publisherLogo)
        {
            PublisherID = publisherID;
            PublisherName = publisherName;
            PublisherLogo = publisherLogo;
        }


        /// <summary>
        /// Асинхронное получение списка издателей
        /// </summary>
        /// <returns>Task с типом списка издателей</returns>
        public static async Task<List<Publisher>> GetPublishersAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/publisher/getPublishers.php");
            var content = await jsonData;
            var pubList = await JsonSerializer.DeserializeAsync<List<Publisher>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return pubList;
        }

        /// <summary>
        /// Асинхронное получение издателя по ID
        /// </summary>
        /// <param name="id">ID издателя</param>
        /// <returns>Task с типом издателя</returns>
        public static async Task<Publisher> GetPublisherByIDAsync(uint id)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/publisher/getPublisherByID.php?PublisherID=" + id.ToString());
            var content = await jsonData;
            var pub = await JsonSerializer.DeserializeAsync<Publisher>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return pub;
        }

        /// <summary>
        /// Асинхронное добавление издателя
        /// </summary>
        /// <param name="pub">Издатель</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> CreateAsync(Publisher pub)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Publisher>(pub);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/publisher/create.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное удаление издателя
        /// </summary>
        /// <param name="pub">Издатель</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> DeleteAsync(Publisher pub)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Publisher>(pub);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/publisher/delete.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное редактирование издателя
        /// </summary>
        /// <param name="pub">Издатель</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> EditAsync(Publisher pub)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Publisher>(pub);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/publisher/update.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }
    }
}

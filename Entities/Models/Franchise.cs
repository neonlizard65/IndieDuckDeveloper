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
    /// Класс франшиз игр
    /// </summary>
    public class Franchise
    {
        /// <summary>
        /// ID франшизы
        /// </summary>
        public uint FranchiseID { get; set; }

        /// <summary>
        /// Название франшизы
        /// </summary>
        public string FranchiseName { get; set; }

        /// <summary>
        /// Картинка франшизы
        /// </summary>
        public string FranchiseImage { get; set; }

        public Franchise(uint franchiseID, string franchiseName, string franchiseImage)
        {
            FranchiseID = franchiseID;
            FranchiseName = franchiseName;
            FranchiseImage = franchiseImage;
        }

        /// <summary>
        /// Асинхронное получение списка франшиз
        /// </summary>
        /// <returns>Task с типом списка франшиз</returns>
        public static async Task<List<Franchise>> GetFranchisesAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/franchise/getFranchises.php");
            var content = await jsonData;
            var franList = await JsonSerializer.DeserializeAsync<List<Franchise>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return franList;
        }

        /// <summary>
        /// Асинхронное добавление франшиз
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> CreateAsync(Franchise franchise)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Franchise>(franchise);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/franchise/create.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное удаление франшиз
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> DeleteAsync(Franchise franchise)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Franchise>(franchise);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/franchise/delete.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное редактирование франшиз
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> EditAsync(Franchise franchise)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Franchise>(franchise);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/franchise/update.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

    }
}

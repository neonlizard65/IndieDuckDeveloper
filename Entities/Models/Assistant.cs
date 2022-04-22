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
    /// Класс технических помощников
    /// </summary>
    public class Assistant
    {
       /// <summary>
       /// ID тех. пом.
       /// </summary>
        public uint AssistantID { get; set; }

        /// <summary>
        /// Логин тех. пом.
        /// </summary>
        public string AssistantUserName { get; set; }

        /// <summary>
        /// Имя тех. спец. для чата
        /// </summary>
        public string AssistantRealName { get; set; }

        /// <summary>
        /// Пароль тех. спец.
        /// </summary>
        public string AssistantPass { get; set; }

        /// <summary>
        /// Конструктор для тех. пом.
        /// </summary>
        /// <param name="assistantID">ID тех. пом.</param>
        /// <param name="assistantUserName">Логин тех. пом.</param>
        /// <param name="assistantRealName">Имя тех. спец. для чата</param>
        /// <param name="assistantPass">Пароль тех. спец.</param>
        public Assistant(uint assistantID, string assistantUserName, string assistantRealName, string assistantPass)
        {
            AssistantID = assistantID;
            AssistantUserName = assistantUserName;
            AssistantRealName = assistantRealName;
            AssistantPass = assistantPass;
        }


        /// <summary>
        /// Асинхронное получение технических помощников
        /// </summary>
        /// <returns>Task с типом списка тех.пом.</returns>
        public static async Task<List<Assistant>> GetAssistantsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/assistant/getAssistants.php");
            var content = await jsonData;
            var astList = await JsonSerializer.DeserializeAsync<List<Assistant>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return astList;
        }


        /// <summary>
        /// Асинхронное получение тех.пом. по ID
        /// </summary>
        /// <returns>Task с типом тех.пом.</returns>
        public static async Task<Assistant> GetAssistantByUserNameAsync(string name)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/assistant/getAssistantByUserName.php?AssistantUserName=" + name);
            var content = await jsonData;
            var ast = await JsonSerializer.DeserializeAsync<Assistant>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return ast;
        }


        /// <summary>
        /// Асинхронное добавление тех.пом.
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> CreateAsync(Assistant ast)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Assistant>(ast);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/assistant/create.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное удаление тех.спец.
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> DeleteAsync(Assistant ast)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Assistant>(ast);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/assistant/delete.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное редактирование тех.пом.
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> EditAsync(Assistant ast)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Assistant>(ast);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/assistant/update.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }


    }

}

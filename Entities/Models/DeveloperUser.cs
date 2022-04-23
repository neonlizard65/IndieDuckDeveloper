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
    /// Класс пользователя разработчика
    /// </summary>
    public class DeveloperUser
    {
        /// <summary>
        /// ID пользователя разработчика
        /// </summary>
        public uint DeveloperUserID { get; set; }

        /// <summary>
        /// Логин пользователя разработчика
        /// </summary>
        public string DeveloperUserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string DeveloperUserPass { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string DeveloperUserEmail { get; set; }

        /// <summary>
        /// Код подтверждения пользователя
        /// </summary>
        public string DeveloperUserGuardCode { get; set; }

        /// <summary>
        /// Внешний ключ разработчика
        /// </summary>
        public uint? DeveloperId { get; set; }

        /// <summary>
        /// Разработчик, к которому принадлежит пользователь
        /// </summary>
        [JsonIgnore]
        public Developer Developer { get; set; }

        /// <summary>
        /// Статус пользователя (0 - не админ, 1 - админ)
        /// </summary>
        public byte? IsAdmin { get; set; }

        /// <summary>
        /// Конструктор пользователя разработчика
        /// </summary>
        /// <param name="developerUserID">ID разработчика</param>
        /// <param name="developerUserName">Логин пользователя разработчика</param>
        /// <param name="developerUserPass">Пароль пользователя</param>
        /// <param name="developerUserEmail">Почта пользователя</param>
        /// <param name="developerUserGuardCode">Код подтверждения пользователя</param>
        /// <param name="developerId">Внешний ключ разработчика</param>
        /// <param name="isAdmin">Статус пользователя (0 - не админ, 1 - админ)</param>
        public DeveloperUser(uint developerUserID, string developerUserName, string developerUserPass, string developerUserEmail, string developerUserGuardCode, uint? developerId, byte? isAdmin)
        {
            DeveloperUserID = developerUserID;
            DeveloperUserName = developerUserName;
            DeveloperUserPass = developerUserPass;
            DeveloperUserEmail = developerUserEmail;
            DeveloperUserGuardCode = developerUserGuardCode;
            DeveloperId = developerId;
            this.Developer = Developer.GetDevelopersAsync().Result.Where(x => x.DeveloperID == DeveloperId).FirstOrDefault();
            IsAdmin = isAdmin;
        }

        /// <summary>
        /// Асинхронное получение всех разработчиков
        /// </summary>
        /// <returns>Task с типом списка пользователей разработчика</returns>
        public static async Task<List<DeveloperUser>> GetDevelopersAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/developeruser/getAllDeveloperUsers.php");
            var content = await jsonData;
            var devList = await JsonSerializer.DeserializeAsync<List<DeveloperUser>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return devList;
        }

        /// <summary>
        /// Асинхронное получение разработчика по почте
        /// </summary>
        /// <returns>Task с типом пользователя разработчика</returns>
        public static async Task<DeveloperUser> GetDeveloperUserByEmailAsync(string Email)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/developeruser/getDeveloperUserByEmail.php?DeveloperUserEmail=" + Email);
            var content = await jsonData;
            var dev = await JsonSerializer.DeserializeAsync<DeveloperUser>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return dev;
        }

        /// <summary>
        /// Асинхронное получение пользователя разработчика по логину пользователя
        /// </summary>
        /// <param name="Name">Логин пользователя разработчика</param>
        /// <returns>Task с типом пользователя разработчика</returns>
        public static async Task<DeveloperUser> GetDeveloperUserByNameAsync(string Name)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/developeruser/getDeveloperUserByName.php?DeveloperUserName=" + Name);
            var content = await jsonData;
            var dev = await JsonSerializer.DeserializeAsync<DeveloperUser>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return dev;
        }


        /// <summary>
        /// Асинхронное получение пользователя разработчика по логину пользователя
        /// </summary>
        /// <param name="DeveloperId">ID разработчика</param>
        /// <returns>Task с типом пользователя разработчика</returns>
        public static async Task<List<DeveloperUser>> GetDeveloperUsersByDeveloper (uint DeveloperId)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/developeruser/getDeveloperUsersByDeveloper.php?DeveloperId=" + DeveloperId.ToString());
            var content = await jsonData;
            var dev = await JsonSerializer.DeserializeAsync<List<DeveloperUser>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return dev;
        }

        /// <summary>
        /// Асинхронное добавление пользователя разработчика
        /// </summary>
        /// <param name="dev">Пользователь разработчика</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> CreateAsync(DeveloperUser dev)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<DeveloperUser>(dev);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/developeruser/create.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное пользователя разработчика
        /// </summary>
        /// <param name="dev">Пользователь разработчика</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> DeleteAsync(DeveloperUser dev)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<DeveloperUser>(dev);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/developeruser/delete.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное редактирование  пользователя разработчика
        /// </summary>
        /// <param name="dev">Пользователь разработчика</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> EditAsync(DeveloperUser dev)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<DeveloperUser>(dev);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/developeruser/update.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное обновление кода подтверждения
        /// </summary>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public async Task<bool> UpdateGuardCodeAsync()
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<DeveloperUser>(this);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/developeruser/updateGuardCode.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        } 

    }

}

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
    /// Класс статусов пользователя
    /// </summary>
    public class Status
    {
        /// <summary>
        /// ID статуса
        /// </summary>
        public uint StatusID { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Иконка статуса
        /// </summary>
        public string StatusIcon { get; set; }

        /// <summary>
        /// Цвет статуса
        /// </summary>
        public string StatusColor { get; set; }

        /// <summary>
        /// Конструктор статуса
        /// </summary>
        /// <param name="statusID">ID статуса</param>
        /// <param name="statusName">Название статуса</param>
        /// <param name="statusIcon">Иконка статуса</param>
        /// <param name="statusColor">Цвет статуса</param>
        public Status(uint statusID, string statusName, string statusIcon, string statusColor)
        {
            StatusID = statusID;
            StatusName = statusName;
            StatusIcon = statusIcon;
            StatusColor = statusColor;
        }


        /// <summary>
        /// Асинхронное получение списка статусов
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка статусов</returns>
        public static async Task<List<Status>> GetStatusesAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/status/getStatus.php");
            var content = await jsonData;
            var statusList = await JsonSerializer.DeserializeAsync<List<Status>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return statusList;
        }
    }
}

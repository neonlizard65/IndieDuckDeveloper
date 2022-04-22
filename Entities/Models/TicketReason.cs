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
    /// Класс причин, по которой был подан билет в тех. поддержку
    /// </summary>
    public class TicketReason
    {
        /// <summary>
        /// ID причины
        /// </summary>
        public uint TicketReasonID { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор причин для подачи билета в тех. поддержку
        /// </summary>
        /// <param name="ticketReasonID">ID причины</param>
        /// <param name="name">Причина</param>
        public TicketReason(uint ticketReasonID, string name)
        {
            TicketReasonID = ticketReasonID;
            Name = name;
        }

        /// <summary>
        /// Асинхронное получение списка причин
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка причин</returns>
        public static async Task<List<TicketReason>> GetTicketReasonsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/ticketreason/getTicketReasons.php");
            var content = await jsonData;
            var reasonList = await JsonSerializer.DeserializeAsync<List<TicketReason>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return reasonList;
        }

    }
}

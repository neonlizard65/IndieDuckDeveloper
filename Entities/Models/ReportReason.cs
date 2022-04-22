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
    /// Класс причин, по которой была подана жалоба
    /// </summary>
    public class ReportReason
    {
        /// <summary>
        /// ID причины
        /// </summary>
        public uint ReportReasonID { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        public string ReasonName { get; set; }

        /// <summary>
        /// Конструктор причин для жалобы
        /// </summary>
        /// <param name="reportReasonID">ID причины</param>
        /// <param name="reasonName">Причина</param>
        public ReportReason(uint reportReasonID, string reasonName)
        {
            ReportReasonID = reportReasonID;
            ReasonName = reasonName;
        }

        /// <summary>
        /// Асинхронное получение списка причин
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка причин</returns>
        public static async Task<List<ReportReason>> GetReportReasonsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/reportreason/getReportReasons.php");
            var content = await jsonData;
            var reasonList = await JsonSerializer.DeserializeAsync<List<ReportReason>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return reasonList;
        }
    }
}

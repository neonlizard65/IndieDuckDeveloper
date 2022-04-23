using DataClasses.ConverterJson;
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
    /// Класс групп
    /// </summary>
    public class Group
    {
        /// <summary>
        /// ID группы
        /// </summary>
        public uint GroupID { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Фото группы
        /// </summary>
        public string GroupImage { get; set; }

        /// <summary>
        /// Описание группы
        /// </summary>
        public string GroupBio { get; set; }

        /// <summary>
        /// Роль, позволяющая выкладывать посты.
        /// Есть 3 типа, пишутся в string:
        /// <list type="bullet">
        ///     <item>
        ///         <term>"Admin"</term>
        ///         <description>Делать посты может только админ группы.</description>
        ///     </item>
        ///     <item>
        ///         <term>"Moderator"</term>
        ///         <description>Посты могут выкладывать модераторы и админы.</description>
        ///     </item>
        ///     <item>
        ///         <term>"Everyone"</term>
        ///         <description>Посты могут выкладывать все участники группы.</description>
        ///     </item>
        /// </list>
        /// </summary>
        public string RolePostPrivelege { get; set; }

        /// <summary>
        /// Конструктор группы
        /// </summary>
        /// <param name="groupID">ID группы</param>
        /// <param name="groupName">Название группы</param>
        /// <param name="groupImage">Фото группы</param>
        /// <param name="groupBio">Описание группы</param>
        /// <param name="rolePostPrivelege">Роль, позволяющая выкладывать посты.</param>
        public Group(uint groupID, string groupName, string groupImage, string groupBio, string rolePostPrivelege)
        {
            GroupID = groupID;
            GroupName = groupName;
            GroupImage = groupImage;
            GroupBio = groupBio;
            RolePostPrivelege = rolePostPrivelege;
        }


        /// <summary>
        /// Асинхронное получение списка группы
        /// </summary>
        /// <returns>Task с типом списка групп</returns>
        public static async Task<List<Group>> GetAllGroupsAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/group/getAllGroups.php");
            var content = await jsonData;
            var groupList = await JsonSerializer.DeserializeAsync<List<Group>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return groupList;
        }

        /// <summary>
        /// Асинхронное получение групп по ID
        /// </summary>
        /// <param name="id">Почта пользователя</param>
        /// <returns>Task с типом групп</returns>
        public static async Task<Group> GetGroupByID(uint id)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/group/getGroupByID.php?GroupID=" + id.ToString());
            var content = await jsonData;
            var group = await JsonSerializer.DeserializeAsync<Group>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return group;
        }


        /// <summary>
        /// Асинхронное получение списка групп
        /// </summary>
        /// <param name="search">Результат со строки поиска, по которому будет искаться группа</param>
        /// <returns>Task с типом списка групп</returns>
        public static async Task<List<Group>> GetGroupsBySearchAsync(string search)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/group/getGroupsLike.php?Search=" + search);
            var content = await jsonData;
            var groupList = await JsonSerializer.DeserializeAsync<List<Group>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return groupList;
        }


        /// <summary>
        /// Асинхронное добавление групп
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> CreateAsync(Group group)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Group>(group);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/group/create.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное удаление групп
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> DeleteAsync(Group group)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Group>(group);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/group/delete.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное редактирование групп
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> EditAsync(Group group)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<Group>(group);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/group/update.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }
    }
}

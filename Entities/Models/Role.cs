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
    /// Класс ролей для админского приложения
    /// </summary>
    public class Role
    { 
        /// <summary>
        /// ID роли
        /// </summary>
        public uint RoleID { get; set; }
        
        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Конструктор ролей
        /// </summary>
        /// <param name="roleID">ID роли</param>
        /// <param name="roleName">Название роли</param>
        public Role(uint roleID, string roleName)
        {
            RoleID = roleID;
            RoleName = roleName;
        }

        /// <summary>
        /// Асинхронное получение списка ролей
        /// </summary>
        /// <returns>Возвращается Task, которая имеет тип списка ролей</returns>
        public static async Task<List<Role>> GetRolesAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/role/getRoles.php");
            var content = await jsonData;
            var roleList = await JsonSerializer.DeserializeAsync<List<Role>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return roleList;
        }
    }
}

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
    /// Класс пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public uint UserID { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Телефон пользователя
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// Код для аутентификации через QR код
        /// </summary>
        public string UserAuthToken { get; set; }

        /// <summary>
        /// Код для двухфакторки
        /// </summary>
        public string UserGuardCode { get; set; }

        /// <summary>
        /// ID уровня пользователя
        /// </summary>
        public uint UserLevelId { get; set; }

        /// <summary>
        /// Уровень пользователя
        /// </summary>
        public Level UserLevel { get; set; }

        /// <summary>
        /// Аватар пользователя
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// Опыт пользователя
        /// </summary>
        public uint UserXP { get; set; }

        /// <summary>
        /// Фон пользователя
        /// </summary>
        public string ProfileBackground { get; set; }

        /// <summary>
        /// Открытый/закрытый профиль
        /// </summary>
        public byte? IsPrivate { get; set; }

        /// <summary>
        /// ID статуса пользователя
        /// </summary>
        public uint? StatusId { get; set; }
        public Status Status { get; set; }

        /// <summary>
        /// Настоящее имя пользователя
        /// </summary>
        public string UserRealName { get; set; }

        /// <summary>
        /// Страна пользователя
        /// </summary>
        public uint? UserCountryId { get; set; }
        public Country Country { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Наличие подписки на новостную рассылку
        /// </summary>
        public byte? EmailSubscription { get; set; }

        /// <summary>
        /// Последний раз заходил в сеть
        /// </summary>
        public DateTime? LastOnline { get; set; }

        /// <summary>
        /// Приватность контента страницы
        /// </summary>
        public uint? ContentPrivacyTypeId { get; set; }
        public PrivacyType PrivacyType { get; set; }

        /// <summary>
        /// Конструктор пользователя
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="userPassword">Пароль пользователя</param>
        /// <param name="userEmail">Почта пользователя</param>
        /// <param name="userPhone">Телефон пользователя</param>
        /// <param name="userAuthToken">Код для аутентификации через QR код</param>
        /// <param name="userGuardCode">Код для двухфакторки</param>
        /// <param name="userLevelId">ID уровня пользователя</param>
        /// <param name="userAvatar">Аватар пользователя</param>
        /// <param name="userXP">Опыт пользователя</param>
        /// <param name="profileBackground">Фон пользователя</param>
        /// <param name="isPrivate">Открытый/закрытый профиль</param>
        /// <param name="statusId">ID статуса пользователя</param>
        /// <param name="userRealName">Настоящее имя пользователя</param>
        /// <param name="userCountryId">Страна пользователя</param>
        /// <param name="bio">Описание пользователя</param>
        /// <param name="emailSubscription">Наличие подписки на новостную рассылку</param>
        /// <param name="lastOnline">Последний раз заходил в сеть</param>
        /// <param name="contentPrivacyTypeId">Приватность контента страницы</param>
        public User(uint userID, string userName, string userPassword, string userEmail, string userPhone, string userAuthToken, string userGuardCode, uint userLevelId, string userAvatar,
            uint userXP, string profileBackground, byte? isPrivate, uint? statusId, string userRealName, uint? userCountryId, string bio, byte? emailSubscription, DateTime? lastOnline, uint? contentPrivacyTypeId)
        {
            UserID = userID;
            UserName = userName;
            UserPassword = userPassword;
            UserEmail = userEmail;
            UserPhone = userPhone;
            UserAuthToken = userAuthToken;
            UserGuardCode = userGuardCode;
            UserLevelId = userLevelId;
            UserAvatar = userAvatar;
            UserXP = userXP;
            ProfileBackground = profileBackground;
            IsPrivate = isPrivate;
            StatusId = statusId;
            UserRealName = userRealName;
            UserCountryId = userCountryId;
            Bio = bio;
            EmailSubscription = emailSubscription;
            LastOnline = lastOnline;
            ContentPrivacyTypeId = contentPrivacyTypeId;
            Task.Run(() => GetLinkedObjects());
        }

        /// <summary>
        /// Асинхоронно находим объекты, с которыми связан пользователю
        /// </summary>
        /// <returns></returns>
        public async Task GetLinkedObjects()
        {
            Task<List<Level>> levelsTask = Level.GetLevelsAsync();
            Task<List<Status>> statusesTask = Status.GetStatusesAsync();
            Task<List<Country>> countriesTask = Country.GetCountriesAsync();
            Task<List<PrivacyType>> privacytypesTask = PrivacyType.GetPrivacyTypesAsync();

            await Task.WhenAll(levelsTask, statusesTask, countriesTask, privacytypesTask);

            UserLevel = levelsTask.Result.Where(x => x.LevelID == UserLevelId).FirstOrDefault();
            Status = statusesTask.Result.Where(x => x.StatusID == StatusId).FirstOrDefault();
            Country = countriesTask.Result.Where(x => x.ID == UserCountryId).FirstOrDefault();
            PrivacyType = privacytypesTask.Result.Where(x => x.PrivacyTypeID == ContentPrivacyTypeId).FirstOrDefault();
        }

        /// <summary>
        /// Асинхронное получение списка пользователей
        /// </summary>
        /// <returns>Task с типом списка пользователей</returns>
        public static async Task<List<User>> GetAllUsersAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
            };
            options.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/user/getAllUsers.php");
            var content = await jsonData;
            var userList = await JsonSerializer.DeserializeAsync<List<User>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return userList;
        }

        /// <summary>
        /// Асинхронное получение пользователя по почте
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        /// <returns>Task с типом пользователя</returns>
        public static async Task<User> GetUserByEmail(string email)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            options.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/user/getUserByEmail.php?UserEmail=" + email);
            var content = await jsonData;
            var user = await JsonSerializer.DeserializeAsync<User>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return user;
        }

        /// <summary>
        /// Асинхронное получение пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Task с типом пользователя</returns>
        public static async Task<User> GetUserByLogin(string login)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            options.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/user/getUserByLogin.php?UserName=" + login);
            var content = await jsonData;
            var user = await JsonSerializer.DeserializeAsync<User>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return user;
        }

        /// <summary>
        /// Асинхронное получение пользователя по телефону
        /// </summary>
        /// <param name="phone">Телефон пользователя</param>
        /// <returns>Task с типом пользователя</returns>
        public static async Task<User> GetUserByPhone(string phone)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            options.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/user/getUserByPhone.php?UserPhone=" + phone);
            var content = await jsonData;
            var user = await JsonSerializer.DeserializeAsync<User>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return user;
        }

        /// <summary>
        /// Асинхронное получение списка пользователей
        /// </summary>
        /// <param name="search">Результат со строки поиска, по которому будет искаться пользователь</param>
        /// <returns>Task с типом списка пользователей</returns>
        public static async Task<List<User>> GetUsersBySearchAsync(string search)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
            };
            options.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
            Task<string> jsonData = client.GetStringAsync("http://192.168.1.75/api/methods/user/getUsersSearch.php?Search=" + search);
            var content = await jsonData;
            var userList = await JsonSerializer.DeserializeAsync<List<User>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), options);
            return userList;
        }


        /// <summary>
        /// Асинхронное добавление пользователей
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> CreateAsync(User user)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<User>(user);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/user/create.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное удаление пользователей
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> DeleteAsync(User user)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<User>(user);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/user/delete.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронное редактирование пользователей
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Task с булевым типом, отражающий статус операции (true - успешно, false - ошибка)</returns>
        public static async Task<bool> EditAsync(User user)
        {
            HttpClient client = new HttpClient();
            string serialized = JsonSerializer.Serialize<User>(user);
            var result = await client.PostAsync("http://192.168.1.75/api/methods/user/update.php", new StringContent(serialized));
            return result.IsSuccessStatusCode;
        }
    }
}

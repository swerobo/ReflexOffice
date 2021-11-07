using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReflexOffice.ApplicationSupport;


namespace ReflexOffice.WinForm_DevExpress.DataService
{
    class UserData
    {
        static string BaseUrl = "";
        static HttpClient client;
        static UserData()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public static async Task<string> GetAll()
        {
            using (HttpClient _client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(BaseUrl + "api/User"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string BeutifyJson(string jsonStr)
        {
            JToken parseJson = JToken.Parse(jsonStr);
            return parseJson.ToString(Formatting.Indented);
            // Console.WriteLine(ListOfUser.ToString());
        }
        public static async Task<IEnumerable<User>> GetAllUser()
        {
            var json = await client.GetStringAsync(BaseUrl + "api/User");
            var _user = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
            return _user;
        }
        public static async Task<IEnumerable<User>> GetById(string id)
        {
            var json = await client.GetStringAsync(BaseUrl + "api/User/" + id);
            //var _items = await UserData.GetAllUser();
            var _user = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
            return _user;
        }

        public static async Task RemoveUser(int id)
        {
            var response = await client.DeleteAsync($"api/User/{id}");
        }

        static Random random = new Random();

        public static async Task AddUser(string name, string email, string password)
        {
            var user = new User
            {
                id = random.Next(0, 10000),
                name = name,
                email = email,
                password = password
            };
            var json = JsonConvert.SerializeObject(user);
            var content =
                            new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BaseUrl + "api/User", content);

        }

        public static async Task<string> Post(string Name, String Email)
        {
            var inputData = new Dictionary<string, string>
            {
                {"Name", Name },
                {"Email", Email }
            };
            var input = new FormUrlEncodedContent(inputData);

            using (HttpClient _client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(BaseUrl + "api/User", input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }
        public static async Task<List<User>> ListOfUser()
        {
            using (HttpClient _client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(BaseUrl + "api/User"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        var json = JArray.Parse(data);

                        if (data != null)
                        {
                            var items = new List<User>();
                            foreach (dynamic item in data)
                            {
                                items.Add(new User() { id = item.id, name = item.name });
                            }
                            return items;
                        }
                    }
                }
            }
            return null;
        }

        public static string GridJson(string jsonStr)
        {
            JToken parseJson = JToken.Parse(jsonStr);
            return parseJson.ToString(Formatting.Indented);
        }

        public static async Task<string> Get(string id)
        {
            using (HttpClient _client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(BaseUrl + "api/User/" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static async Task<string> Put(string id, string Name, String Email)
        {
            var inputData = new Dictionary<string, string>
            {
                {"Name", Name },
                {"Email", Email }
            };
            var input = new FormUrlEncodedContent(inputData);

            using (HttpClient _client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsync(BaseUrl + "api/User/" + id, input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrippinExercise.Model;
using static TrippinExercise.Model.DataTransferObjects;

namespace TrippinExercise
{
    class Program
    {
        private static readonly HttpClient HttpClient
                = new HttpClient() { BaseAddress = new Uri("https://services.odata.org/TripPinRESTierService/(S(wilmgnjk5hrbqetjdtojbmdr))/") };

        static async Task Main(string[] args)
        {
            var people = await GetAsync();
            people.Value.ForEach(p => Console.WriteLine(p.UserName));
            Console.WriteLine(string.Empty);
            await PostAsync();
            people = await GetAsync();
            people.Value.ForEach(p => Console.WriteLine(p.UserName));
        }

        public static async Task<People> GetAsync()
        {
            var response = await HttpClient.GetAsync("People");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<People>(responseBody);
        }

        public static async Task PostAsync()
        {
            var people = await GetAsync();
            var fileContent = await File.ReadAllTextAsync("users.json");
            var newPeople = JsonSerializer.Deserialize<List<User>>(fileContent);
            foreach (User u in newPeople)
            {
                Person p = new Person();
                p.UserName = u.UserName;
                p.FirstName = u.FirstName;
                p.LastName = u.LastName;
                p.Emails = new List<string>();
                p.Emails.Add(u.Email);
                City c = new City();
                c.Name = u.CityName;
                c.CountryRegion = u.Country;
                c.Region = "ID";
                AddressInfo a = new AddressInfo();
                a.Address = u.Address;
                a.City = c;
                p.AddressInfo = new List<AddressInfo>();
                p.AddressInfo.Add(a);
                if (!people.Value.Any(item => item.UserName == u.UserName))
                {
                    var content = new StringContent(JsonSerializer.Serialize(p), Encoding.UTF8, "application/json");
                    var response = await HttpClient.PostAsync("People", content);
                    response.EnsureSuccessStatusCode();
                }
            }
        }
    }
}

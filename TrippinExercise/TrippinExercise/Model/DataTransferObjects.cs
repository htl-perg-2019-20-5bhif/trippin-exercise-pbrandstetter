using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrippinExercise.Model
{
    class DataTransferObjects
    {
        public class People
        {
            [JsonPropertyName("value")]
            public List<Person> Value { get; set; }
        }

        public class Person
        {
            [JsonPropertyName("UserName")]
            public string UserName { get; set; }

            [JsonPropertyName("FirstName")]
            public string FirstName { get; set; }

            [JsonPropertyName("LastName")]
            public string LastName { get; set; }

            [JsonPropertyName("Emails")]
            public List<string> Emails { get; set; }

            [JsonPropertyName("AddressInfo")]
            public List<AddressInfo> AddressInfo { get; set; }
        }
    }

    public class AddressInfo
    {
        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("City")]
        public City City { get; set; }
    }

    public class City
    {
        [JsonPropertyName("CountryRegion")]
        public string CountryRegion { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Region")]
        public string Region { get; set; }
    }
}

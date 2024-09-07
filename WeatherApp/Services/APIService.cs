using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public static class APIService
    {
        public static async Task<Root> GetWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            // store the result int that variable. it contains JSON data
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=3a75f6c38bf1d813b50ac6768e28627a", latitude, longitude));

            // Deserialize into C# classes   
            return JsonConvert.DeserializeObject<Root>(response);
        }

        public static async Task<Root> GetWeatherByCity(string city)
        {
            var httpClient = new HttpClient();
            // store the result int that variable. it contains JSON data
            var response = await httpClient.GetStringAsync(string.Format("api.openweathermap.org/data/2.5/forecast?q={0}&appid=3a75f6c38bf1d813b50ac6768e28627a", city));

            // Deserialize into C# classes   
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}

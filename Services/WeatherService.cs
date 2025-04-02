﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherForecast.Models; 
namespace WeatherForecast.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "ae7cb489f65814bdd0deaf5070e165d7";
        private readonly string _baseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherForecast.Models.WeatherForecast> GetWeatherAsync(string city,string lang="en")
        {
            var url = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric&lang={lang}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("حدث خطأ أثناء جلب بيانات الطقس.");
            }

            var json = await response.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject(json);

            var weatherForecast = new WeatherForecast.Models.WeatherForecast
            {
                City = city,
                Description = (string)data.weather[0].description,
                Temperature = (double)data.main.temp,
                Condition = (string)data.weather[0].main,
                Date = DateTime.Now,
                Humidity = (int)data.main.humidity,
                Wind = (double)data.wind.speed
            };

            return weatherForecast;
        }
        public async Task<WeatherForecast.Models.WeatherForecast> GetWeatherByCoordinatesAsync(double lat, double lon, string lang = "en")
        {
            var url = $"{_baseUrl}?lat={lat}&lon={lon}&appid={_apiKey}&units=metric&lang={lang}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("فشل في جلب بيانات الطقس.");

            var json = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(json);

            return new WeatherForecast.Models.WeatherForecast
            {
                City = (string)data.name, 
                Temperature = (double)data.main.temp,
                Condition = (string)data.weather[0].main,
                Description = (string)data.weather[0].description,
                Date = DateTime.Now,
                Humidity = (int)data.main.humidity,
                Wind = (double)data.wind.speed
            };
        }

    }
}

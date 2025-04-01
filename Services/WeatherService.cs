using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherForecast.Models; // تأكد من أن Namespace مطابق للمودل

namespace WeatherForecast.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        // ضع مفتاح API الخاص بك هنا (مثلاً مفتاح OpenWeatherMap)
        private readonly string _apiKey = "ae7cb489f65814bdd0deaf5070e165d7";
        // عنوان خدمة الطقس الخارجية
        private readonly string _baseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // دالة لجلب بيانات الطقس لمدينة معينة باستخدام نموذج WeatherForecast
        public async Task<WeatherForecast.Models.WeatherForecast> GetWeatherAsync(string city)
        {
            // بناء الرابط المطلوب مع تمرير اسم المدينة ومفتاح API والوحدات المترية
            var url = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric";

            // إرسال طلب GET إلى الخدمة الخارجية
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("حدث خطأ أثناء جلب بيانات الطقس.");
            }

            // قراءة الاستجابة بصيغة JSON
            var json = await response.Content.ReadAsStringAsync();

            // تحويل JSON إلى كائن ديناميكي
            dynamic data = JsonConvert.DeserializeObject(json);

            // تعبئة نموذج WeatherForecast بالبيانات المستخرجة
            var weatherForecast = new WeatherForecast.Models.WeatherForecast
            {
                City = city,
                Temperature = (double)data.main.temp,
                Condition = (string)data.weather[0].main,
                Date = DateTime.Now,
                Humidity = (int)data.main.humidity,
                Wind = (double)data.wind.speed
            };

            return weatherForecast;
        }
    }
}

using System;

namespace WeatherForecast.Models
{
    public class WeatherForecast
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public int Humidity { get; set; }     
        public double Wind { get; set; }      
    }
}

using System.Collections.Generic;

namespace WeatherForecast.Services
{
    public class RecentCityService
    {
        private readonly List<string> _recentCities = new();

        public void AddCity(string city)
        {
            city = city.ToLower(); 
            if (!_recentCities.Contains(city))
            {
                _recentCities.Insert(0, city); 
                if (_recentCities.Count > 5)
                    _recentCities.RemoveAt(_recentCities.Count - 1); 
            }
        }

        public List<string> GetRecentCities()
        {
            return _recentCities;
        }
    }
}

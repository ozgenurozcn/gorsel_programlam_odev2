using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Görsel_Programlama_Ödev2.Views
{
    public partial class SehirlerHavaDurumu : ContentPage
    {
        private const string ApiKey = "0b37cdc2dbb85b2a3997f04e5464c6a5";

        public SehirlerHavaDurumu()
        {
            InitializeComponent();
            BindingContext = new WeatherViewModel();
        }
    }

    public class WeatherViewModel : BindableObject
    {
        private string _cityName;
        private string _resultText;

        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }

        public string ResultText
        {
            get { return _resultText; }
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        public Command GetWeatherCommand { get; }

        public WeatherViewModel()
        {
            GetWeatherCommand = new Command(ExecuteGetWeatherCommand);
        }

        private async void ExecuteGetWeatherCommand()
        {
            if (string.IsNullOrEmpty(CityName))
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Lütfen bir şehir adı girin.", "Tamam");
                return;
            }

            WeatherData weatherData = await GetWeatherData(CityName);

            if (weatherData == null)
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Hava durumu verileri alınamadı.", "Tamam");
                return;
            }

            string weatherDescriptions = string.Empty;

            if (weatherData.Weather != null && weatherData.Weather.Length > 0)
            {
                weatherDescriptions = string.Join(", ", weatherData.Weather[0].Description);
            }

            ResultText = $"Şehir: {weatherData.Name}\n" +
                         $"Sıcaklık: {weatherData.Main.Temp} °C\n" +
                         $"Hava Durumu: {weatherDescriptions}";
        }

        private async Task<WeatherData> GetWeatherData(string cityName)
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=0b37cdc2dbb85b2a3997f04e5464c6a5";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(jsonContent);

                    return weatherData;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                    return null;
                }
            }
        }
    }

    public class WeatherData
    {
        public string Name { get; set; }
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
    }
}

using ForecastApp1.Models;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ForecastApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            languegeCB.SelectedIndex = 0;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = new ForecastQueryBuilder()
                .AddCity(cityTb.Text)
                .Build();

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(query);
            var stream = await response.Content.ReadAsStreamAsync();
            var objResponse = JsonSerializer.Deserialize(stream,
                typeof(ForecastResponse), new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            if(objResponse is ForecastResponse forecastResponse)
            {
                //MessageBox.Show(forecastResponse.List.First().Main.Temp.ToString())
                var dtos = new List<ForecastDTO>();
                foreach (var forecast in forecastResponse.List)
                {
                    if (languegeCB.SelectedIndex == 0)
                    {
                        var forecastDTO = new ForecastDTO()
                        {
                            Icon = $"https://openweathermap.org/img/wn/{forecast.Weather[0].Icon}@2x.png",
                            Temp = $"Temperature: {forecast.Main.Temp} °F",
                            Pressure = $"Pressure: {forecast.Main.Pressure} Pа",
                            Humidity = $"Humidity: {forecast.Main.Humidity} %",
                            Date = $"Date: {forecast.dt_txt}",
                            Speed = $"Wind speed: {forecast.Wind.speed} m/s",
                            Deg = $"Deg: {forecast.Wind.deg}",
                            Gust = $"Gust: {forecast.Wind.gust}"
                        };
                        dtos.Add(forecastDTO);
                    }
                    else
                    {
                        var forecastDTO = new ForecastDTO()
                        {
                            Icon = $"https://openweathermap.org/img/wn/{forecast.Weather[0].Icon}@2x.png",
                            Temp = $"Температура: {forecast.Main.Temp} °F",
                            Pressure = $"Тиск: {forecast.Main.Pressure} Па",
                            Humidity = $"Вологість: {forecast.Main.Humidity} %",
                            Date = $"Дата: {forecast.dt_txt}",
                            Speed = $"Швидкість повітря: {forecast.Wind.speed} m/s",
                            Deg = $"Град: {forecast.Wind.deg}",
                            Gust = $"Порив: {forecast.Wind.gust}"
                        };
                        dtos.Add(forecastDTO);
                    }
                    
                }

                listBox.ItemsSource = dtos;
            }
        }
    }
}
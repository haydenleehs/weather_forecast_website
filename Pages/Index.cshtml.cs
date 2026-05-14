using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WeatherSite.Pages;

public class IndexModel : PageModel
{
    // Your API Key
    private const string API_KEY = "64cc5b6e61051a29e98099147f4b646f"; 

    public WeatherResponse? WeatherData { get; set; }
    public string? ErrorMessage { get; set; }

    // 1. Run this when the page loads (Auto-Location)
    public async Task OnGetAsync(string? lat, string? lon)
    {
        // If we received coordinates from the browser, fetch the weather!
        if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lon))
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={API_KEY}&units=metric&cnt=8";
            await FetchWeather(url);
        }
    }

    // 2. Run this when the user types a city (Manual Search)
    public async Task OnPostAsync(string city)
    {
        string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={API_KEY}&units=metric&cnt=8";
        await FetchWeather(url);
    }

    // 3. Shared Logic (Don't repeat code!)
    private async Task FetchWeather(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    WeatherData = JsonConvert.DeserializeObject<WeatherResponse>(json);
                }
                else
                {
                    ErrorMessage = "Could not find weather for that location.";
                }
            }
        }
        catch
        {
            ErrorMessage = "Connection error. Please try again.";
        }
    }
}
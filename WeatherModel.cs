using System.Collections.Generic;

namespace WeatherSite;

// 1. A single 3-hour block of weather
public class ForecastItem
{
    public long dt { get; set; } // Time in Unix format
    public MainData? main { get; set; }
    public List<WeatherDescription>? weather { get; set; }
    public WindData? wind { get; set; }
    public string? dt_txt { get; set; } // Text date: "2024-02-15 15:00:00"
    public float pop { get; set; }
}

// 2. We reuse these existing classes!
public class MainData
{
    public float temp { get; set; }
    public float feels_like { get; set; }
    public int humidity { get; set; }
}

public class WindData
{
    public float speed { get; set; } // <--- NEW: Wind Speed (m/s)
}

public class WeatherDescription
{
    public string? description { get; set; }
    public string? icon { get; set; }
}

public class SysData
{
    public string? country { get; set; }
}

// 3. The Root Object (The box that holds the list)
public class WeatherResponse
{
    public List<ForecastItem>? list { get; set; } // <--- THE BIG CHANGE
    public CityData? city { get; set; }
}

public class CityData
{
    public string? name { get; set; }
    public string? country { get; set; } // Country is now inside 'city'
    public int timezone { get; set; }
}
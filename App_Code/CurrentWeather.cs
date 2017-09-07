using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CurrentWeather
/// </summary>
public class CurrentWeather
{
    public Coordinates coord { get; set; }
    public List<Weather> weather { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Clouds Clouds { get; set; }
    public long dt { get; set; }
    public Sys sys { get; set; }
    public long id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}

public class Coordinates
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public double humidity { get; set; }
    public double pressure { get; set; }
    public double temp_max { get; set; }
    public double temp_min { get; set; }
    public double sea_level { get; set; }
    public double grnd_level { get; set; }
    public double temp_kf { get; set; }
}

public class Wind
{
    public double speed { get; set; }
    public double deg { get; set; }

}

public class Clouds
{
    public double all { get; set; }

}

public class Sys
{
    public int type { get; set; }
    public long id { get; set; }
    public double message { get; set; }
    public string country { get; set; }
    public long sunrise { get; set; }
    public long sunset { get; set; }
    public string pod { get; set; }

}
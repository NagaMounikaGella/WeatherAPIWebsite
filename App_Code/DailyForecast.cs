using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DailyForecast
/// </summary>
public class DailyForecast
{
    public string cod { get; set; }
    public double message { get; set; }
    public int cnt { get; set; }
    public List<WeatherEachDay> list { get; set; }
    public City city { get; set; }
}

public class WeatherEachDay
{
    public long dt { get; set; }
    public Temp temp { get; set; }
    public List<Weather> weather { get; set; }
    public double pressure { get; set; }
    public double humidity { get; set; }
    public double speed { get; set; }
    public double deg { get; set; }
    public double clouds { get; set; }
}

public class Temp
{
    public double day { get; set; }
    public double min { get; set; }
    public double max { get; set; }
    public double night { get; set; }
    public double eve { get; set; }
    public double morn { get; set; }

}
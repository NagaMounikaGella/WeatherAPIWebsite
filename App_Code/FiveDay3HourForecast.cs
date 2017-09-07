using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FiveDay3HourForecast
/// </summary>
public class FiveDay3HourForecast
{
    public string cod { get; set; }
    public double message { get; set; }
    public int cnt { get; set; }
    public List<Weather3Hour> list { get; set; }
    public City city { get; set; }
}

public class Weather3Hour
{
    public long dt { get; set; }
    public Main main { get; set; }
    public List<Weather> weather { get; set; }
    public Wind wind { get; set; }
    public Clouds Clouds { get; set; }
    public Sys sys { get; set; }
    public string dt_txt { get; set; }

}

public class City
{
    public long id { get; set; }
    public string name { get; set; }
    public Coordinates coord { get; set; }
    public string country { get; set; }
    public long population { get; set; }
}
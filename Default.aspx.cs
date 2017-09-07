using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    CurrentWeather currentWeatherData = null;
    FiveDay3HourForecast fiveDay3HourForecastData = null;
    DailyForecast dailyForecastData = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        myDivValidation.InnerText = " ";
    }
    
    protected void fetchLabel(string labelColor, string labelText, string labelOutputText, HtmlGenericControl controlElement)
    {
        Label label = new Label();
        label.Style.Add("color", labelColor);
        label.Style.Add("text-transform", "Capitalize");
        Label labelOutput = new Label();
        label.Text = "<strong>" + labelText + " </strong>";
        labelOutput.Text = labelOutputText;
        controlElement.Controls.Add(label);
        controlElement.Controls.Add(labelOutput);
    }

    protected void getWeatherData()
    {
        string appId = "0cc82242040abd88f87c7f88a51740ed";
        string weatherApiCall = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&APPID={1}", TextBoxCity.Text,appId);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        using (WebClient webClient = new WebClient())
        {
            string jsonData = webClient.DownloadString(weatherApiCall);
            currentWeatherData = (new JavaScriptSerializer()).Deserialize<CurrentWeather>(jsonData);

            HtmlGenericControl myH2 = new HtmlGenericControl("h2");
            myH2.Style.Add("color", "blue");
            myH2.InnerHtml = currentWeatherData.name + " , " + currentWeatherData.sys.country + " - Current Weather Forecast";
            PlaceHolderCurrentWeather.Controls.Add(myH2);

            HtmlGenericControl myH3 = new HtmlGenericControl("h3");
            myH3.Style.Add("color", "orangered");
            DateTime date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(Convert.ToString(currentWeatherData.dt * 1000)));
            myH3.InnerHtml = Convert.ToString(date);
            PlaceHolderCurrentWeather.Controls.Add(myH3);

            Image img = new Image();
            img.ImageUrl = "http://openweathermap.org/img/w/" + currentWeatherData.weather[0].icon + ".png";
            PlaceHolderCurrentWeather.Controls.Add(img);

            HtmlGenericControl mySpanImgDes = new HtmlGenericControl("span");
            mySpanImgDes.Style.Add("color", "orangered");
            mySpanImgDes.Style.Add("font-weight", "500");
            mySpanImgDes.Style.Add("line-height", "1.1");
            mySpanImgDes.Style.Add("font-size", "24px");
            mySpanImgDes.InnerHtml = currentWeatherData.weather[0].main + "<br />";
            PlaceHolderCurrentWeather.Controls.Add(mySpanImgDes);

            HtmlGenericControl myH4 = new HtmlGenericControl("h4");
            myH4.Style.Add("color", "blue");
            myH4.InnerHtml = currentWeatherData.main.temp + " &#8451; ";
            PlaceHolderCurrentWeather.Controls.Add(myH4);

            HtmlGenericControl myDiv = new HtmlGenericControl("div");
            myDiv.Style.Add("margin","5vh");
            myDiv.Style.Add("margin-left","10vw");
            myDiv.Style.Add("margin-right","10vw");
            myDiv.Style.Add("padding","10px");
            myDiv.Style.Add("border","2px solid orangered");
            myDiv.Style.Add("border-radius","20px");

            HtmlGenericControl mySpan1 = new HtmlGenericControl("span");
            mySpan1.Style.Add("margin", "10px");
            mySpan1.Style.Add("padding", "10px");
            mySpan1.Style.Add("text-align", "left");
            mySpan1.Style.Add("display", "inline-block");

            fetchLabel("orangered", "Weather: ", currentWeatherData.weather[0].description + "<br />", mySpan1);
            fetchLabel("orangered", "Wind Speed: ", currentWeatherData.wind.speed + " m/sec <br />", mySpan1);
            fetchLabel("orangered", "Wind Direction: ", currentWeatherData.wind.deg + "&deg; <br />", mySpan1);
            fetchLabel("orangered", "Clouds: ", currentWeatherData.Clouds.all + " % <br />", mySpan1);

            myDiv.Controls.Add(mySpan1);

            HtmlGenericControl mySpan2 = new HtmlGenericControl("span");
            mySpan2.Style.Add("margin", "10px");
            mySpan2.Style.Add("padding", "10px");
            mySpan2.Style.Add("text-align", "left");
            mySpan2.Style.Add("display", "inline-block");

            fetchLabel("orangered", "Minimum-Temperature: ", currentWeatherData.main.temp_min + " &#8451; <br />", mySpan2);
            fetchLabel("orangered", "Maximum-Temperature: ", currentWeatherData.main.temp_max + " &#8451; <br />", mySpan2);
            fetchLabel("orangered", "Humidity: ", currentWeatherData.main.humidity + " % <br />", mySpan2);
            fetchLabel("orangered", "Pressure: ", currentWeatherData.main.pressure + " % <br />", mySpan2);
            
            myDiv.Controls.Add(mySpan2);
            PlaceHolderCurrentWeather.Controls.Add(myDiv);
        }

    }

    protected void ButtonCurrentweather_Click(object sender, EventArgs e)
    {
        myDivValidation.Visible = true;
        divCurrentWeather.Visible = true;
        div5DayForecast.Visible = false;
        myDivDayButtonsBottom.Visible = false;
        divDailyForecast.Visible = false;
        myDivDailyForecastButtonsBottom.Visible = false;
        try
        {
            getWeatherData();
        }
        catch (Exception exceptionOccured)
        {
            myDivValidation.InnerText = "Error: Enter valid city name or " + exceptionOccured.Message;
            myDivValidation.Style.Add("color", "red");
            divCurrentWeather.Visible = false;
        }
    }

    protected void get5DayForecastData()
    {
        string appId = "0cc82242040abd88f87c7f88a51740ed";
        string fiveDay3HourForecastApiCall = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&APPID={1}", TextBoxCity.Text, appId);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        using (WebClient webClient = new WebClient())
        {
            string jsonData = webClient.DownloadString(fiveDay3HourForecastApiCall);
            fiveDay3HourForecastData = (new JavaScriptSerializer()).Deserialize<FiveDay3HourForecast>(jsonData);
            

            HtmlGenericControl myH2 = new HtmlGenericControl("h2");
            myH2.Style.Add("color", "blueviolet");
            myH2.InnerHtml = fiveDay3HourForecastData.city.name + " , " + fiveDay3HourForecastData.city.country + " - 5 Day / 3 Hour Weather Forecast <br />";
            PlaceHolder5DayForecastStart.Controls.Add(myH2);
            
            string TempString = "";
            
            List<string> fiveDaysOfWeek = new List<string>();
            
            foreach (Weather3Hour listItem in fiveDay3HourForecastData.list)
            {
                if (TempString != listItem.dt_txt.Split(' ')[0])
                {
                    DayOfWeek day = new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(Convert.ToString(listItem.dt * 1000))).DayOfWeek;
                    fiveDaysOfWeek.Add(Convert.ToString(day));
                }
                TempString = listItem.dt_txt.Split(' ')[0];
            }

            ButtonDay1.Text = fiveDaysOfWeek[0];
            ButtonDay2.Text = fiveDaysOfWeek[1];
            ButtonDay3.Text = fiveDaysOfWeek[2];
            ButtonDay4.Text = fiveDaysOfWeek[3];
            ButtonDay5.Text = fiveDaysOfWeek[4];

            ButtonDay1Bottom.Text = fiveDaysOfWeek[0];
            ButtonDay2Bottom.Text = fiveDaysOfWeek[1];
            ButtonDay3Bottom.Text = fiveDaysOfWeek[2];
            ButtonDay4Bottom.Text = fiveDaysOfWeek[3];
            ButtonDay5Bottom.Text = fiveDaysOfWeek[4];

        }

    }

    protected void DayButton_Click(object sender, EventArgs e)
    {
        string DateString = "";
        string TempString = "";

        List<string> dateTimeStrings = new List<string>();

        get5DayForecastData();

        foreach (Weather3Hour listItem in fiveDay3HourForecastData.list)
        {
            if (TempString != listItem.dt_txt.Split(' ')[0])
            {
                dateTimeStrings.Add(listItem.dt_txt);
            }
            TempString = listItem.dt_txt.Split(' ')[0];
        }


        switch(((Button)sender).ID)
        {
            case "ButtonDay1":
                DateString = dateTimeStrings[0].Split(' ')[0];
                break;
            case "ButtonDay2":
                DateString = dateTimeStrings[1].Split(' ')[0];
                break;
            case "ButtonDay3":
                DateString = dateTimeStrings[2].Split(' ')[0];
                break;
            case "ButtonDay4":
                DateString = dateTimeStrings[3].Split(' ')[0];
                break;
            case "ButtonDay5":
                DateString = dateTimeStrings[4].Split(' ')[0];
                break;
            case "ButtonDay1Bottom":
                DateString = dateTimeStrings[0].Split(' ')[0];
                break;
            case "ButtonDay2Bottom":
                DateString = dateTimeStrings[1].Split(' ')[0];
                break;
            case "ButtonDay3Bottom":
                DateString = dateTimeStrings[2].Split(' ')[0];
                break;
            case "ButtonDay4Bottom":
                DateString = dateTimeStrings[3].Split(' ')[0];
                break;
            case "ButtonDay5Bottom":
                DateString = dateTimeStrings[4].Split(' ')[0];
                break;
            default:
                break;
        }

        List<Weather3Hour> dayListItems = new List<Weather3Hour>();

        foreach (Weather3Hour listItem in fiveDay3HourForecastData.list)
        {
            if (listItem.dt_txt.Split(' ')[0] == DateString)
            {
                dayListItems.Add(listItem);
            }
        }

        HtmlGenericControl myDiv = new HtmlGenericControl("div");
        myDiv.Style.Add("margin", "5vh");
        myDiv.Style.Add("margin-left", "5vw");
        myDiv.Style.Add("margin-right", "5vw");
        myDiv.Style.Add("padding", "10px");
        myDiv.Style.Add("border", "2px solid orangered");
        myDiv.Style.Add("border-radius", "20px");
        
        HtmlGenericControl dayHeading = new HtmlGenericControl("h3");
        dayHeading.Style.Add("color", "orangered");
        dayHeading.InnerHtml = ((Button)sender).Text;
        myDiv.Controls.Add(dayHeading);

        foreach (Weather3Hour listItem in dayListItems)
        {
            
            HtmlGenericControl mySpan1 = new HtmlGenericControl("span");
            mySpan1.Style.Add("margin", "10px");
            mySpan1.Style.Add("padding", "10px");
            mySpan1.Style.Add("text-align", "center");
            mySpan1.Style.Add("background-color", "blueviolet");
            mySpan1.Style.Add("color", "white");
            mySpan1.Style.Add("display", "inline-block");
            mySpan1.Style.Add("border", "2px solid blueviolet");
            mySpan1.Style.Add("border-radius", "20px");

            HtmlGenericControl myH3 = new HtmlGenericControl("h3");
            myH3.Style.Add("color", "aquamarine");
            myH3.InnerHtml = listItem.dt_txt;
            mySpan1.Controls.Add(myH3);

            Image img = new Image();
            img.ImageUrl = "http://openweathermap.org/img/w/" + listItem.weather[0].icon + ".png";
            mySpan1.Controls.Add(img);

            HtmlGenericControl mySpanImgDes = new HtmlGenericControl("span");
            mySpanImgDes.Style.Add("color", "gold");
            mySpanImgDes.Style.Add("font-weight", "bold");
            mySpanImgDes.Style.Add("line-height", "1.1");
            mySpanImgDes.Style.Add("font-size", "24px");
            mySpanImgDes.InnerHtml = listItem.weather[0].main + "<br />";
            mySpan1.Controls.Add(mySpanImgDes);

            HtmlGenericControl myH4 = new HtmlGenericControl("h4");
            myH4.Style.Add("color", "aquamarine");
            myH4.InnerHtml = listItem.main.temp + " &#8451; ";
            mySpan1.Controls.Add(myH4);

            HtmlGenericControl mySubSpan1 = new HtmlGenericControl("span");
            mySubSpan1.Style.Add("margin", "10px");
            mySubSpan1.Style.Add("padding", "5px");
            mySubSpan1.Style.Add("text-align", "center");
            mySubSpan1.Style.Add("display", "inline-block");

            fetchLabel("gold", "Weather: ", listItem.weather[0].description + "<br />", mySubSpan1);
            fetchLabel("gold", "Wind Speed: ", listItem.wind.speed + " m/sec <br />", mySubSpan1);
            fetchLabel("gold", "Wind Direction: ", listItem.wind.deg + "&deg; <br />", mySubSpan1);
            fetchLabel("gold", "Clouds: ", listItem.Clouds.all + " % <br />", mySubSpan1);

            HtmlGenericControl mySubSpan2 = new HtmlGenericControl("span");
            mySubSpan2.Style.Add("margin", "10px");
            mySubSpan2.Style.Add("padding", "10px");
            mySubSpan2.Style.Add("text-align", "center");
            mySubSpan2.Style.Add("display", "inline-block");

            fetchLabel("gold", "Minimum-Temperature: ", listItem.main.temp_min + " &#8451; <br />", mySubSpan2);
            fetchLabel("gold", "Maximum-Temperature: ", listItem.main.temp_max + " &#8451; <br />", mySubSpan2);
            fetchLabel("gold", "Humidity: ", listItem.main.humidity + " % <br />", mySubSpan2);
            fetchLabel("gold", "Pressure: ", listItem.main.pressure + " % <br />", mySubSpan2);

            mySpan1.Controls.Add(mySubSpan1);
            mySpan1.Controls.Add(mySubSpan2);

            myDiv.Controls.Add(mySpan1);
            
        }

        PlaceHolder5DayForecast.Controls.Add(myDiv);
        myDivDayButtonsBottom.Visible = true;
    }

    protected void Button5DayForecast_Click(object sender, EventArgs e)
    {
        myDivValidation.Visible = true;
        divCurrentWeather.Visible = false;
        div5DayForecast.Visible = true;
        myDivDayButtonsBottom.Visible = false;
        divDailyForecast.Visible = false;
        myDivDailyForecastButtonsBottom.Visible = false;

        try
        {
            get5DayForecastData();
        }
        catch (Exception exceptionOccured)
        {
            myDivValidation.InnerText = "Error: Enter valid city name or " + exceptionOccured.Message;
            myDivValidation.Style.Add("color", "red");
            div5DayForecast.Visible = false;
        }
    }

    protected void getDailyForecastData(int count)
    {
        string appId = "0cc82242040abd88f87c7f88a51740ed";
        string dailyForecastApiCall = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&APPID={1}&cnt={2}", TextBoxCity.Text, appId, count);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        using (WebClient webClient = new WebClient())
        {
            string jsonData = webClient.DownloadString(dailyForecastApiCall);
            dailyForecastData = (new JavaScriptSerializer()).Deserialize<DailyForecast>(jsonData);

            HtmlGenericControl myH2 = new HtmlGenericControl("h2");
            myH2.Style.Add("color", "green");
            myH2.InnerHtml = dailyForecastData.city.name + " , " + dailyForecastData.city.country + " - Daily Weather Forecast";
            PlaceHolderDailyForecastStart.Controls.Add(myH2);

        }

    }

    protected void ButtonFewDaysForeCast_Click(object sender, EventArgs e)
    {
        switch (((Button)sender).ID)
        {
            case "Button3DaysForeCast":
            case "Button3DaysForeCastBottom":
                getDailyForecastData(3);
                break;
            case "Button5DaysForeCast":
            case "Button5DaysForeCastBottom":
                getDailyForecastData(5);
                break;
            case "Button7DaysForeCast":
            case "Button7DaysForeCastBottom":
                getDailyForecastData(7);
                break;
            case "Button9DaysForeCast":
            case "Button9DaysForeCastBottom":
                getDailyForecastData(9);
                break;
            case "Button12DaysForeCast":
            case "Button12DaysForeCastBottom":
                getDailyForecastData(12);
                break;
            case "Button16DaysForeCast":
            case "Button16DaysForeCastBottom":
                getDailyForecastData(16);
                break;
            default:
                break;
        }

        
        HtmlGenericControl noOfDaysHeading = new HtmlGenericControl("h3");
        noOfDaysHeading.Style.Add("color", "green");
        noOfDaysHeading.InnerHtml = ((Button)sender).Text;
        PlaceHolderDailyForecast.Controls.Add(noOfDaysHeading);
        

        HtmlGenericControl myDiv = new HtmlGenericControl("div");
        myDiv.Style.Add("margin", "5vh");
        myDiv.Style.Add("margin-left", "5vw");
        myDiv.Style.Add("margin-right", "5vw");
        myDiv.Style.Add("padding", "10px");
        myDiv.Style.Add("border", "2px solid yellow");
        myDiv.Style.Add("border-radius", "20px");


        int index = 0;

        foreach (WeatherEachDay listItem in dailyForecastData.list)
        {
            index++;
            HtmlGenericControl dayHeading = new HtmlGenericControl("h3");
            dayHeading.Style.Add("color", "orangered");
            dayHeading.InnerHtml = "Day " + index;
            myDiv.Controls.Add(dayHeading);

            HtmlGenericControl mySpan1 = new HtmlGenericControl("span");
            mySpan1.Style.Add("margin", "10px");
            mySpan1.Style.Add("padding", "10px");
            mySpan1.Style.Add("text-align", "center");
            mySpan1.Style.Add("background-color", "green");
            mySpan1.Style.Add("color", "white");
            mySpan1.Style.Add("display", "inline-block");
            mySpan1.Style.Add("border", "2px solid green");
            mySpan1.Style.Add("border-radius", "20px");

            HtmlGenericControl myH3 = new HtmlGenericControl("h3");
            myH3.Style.Add("color", "mediumblue");
            myH3.Style.Add("font-weight", "bold");
            DateTime date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(Convert.ToString(listItem.dt * 1000)));
            myH3.InnerHtml = Convert.ToString(date.DayOfWeek);
            mySpan1.Controls.Add(myH3);

            HtmlGenericControl myDateHeading = new HtmlGenericControl("h3");
            myDateHeading.Style.Add("color", "yellow");
            myDateHeading.InnerHtml = Convert.ToString(date);
            mySpan1.Controls.Add(myDateHeading);

            Image img = new Image();
            img.ImageUrl = "http://openweathermap.org/img/w/" + listItem.weather[0].icon + ".png";
            mySpan1.Controls.Add(img);

            HtmlGenericControl mySpanImgDes = new HtmlGenericControl("span");
            mySpanImgDes.Style.Add("color", "yellow");
            mySpanImgDes.Style.Add("font-weight", "1500");
            mySpanImgDes.Style.Add("line-height", "1.1");
            mySpanImgDes.Style.Add("font-size", "24px");
            mySpanImgDes.InnerHtml = listItem.weather[0].main + "<br />";
            mySpan1.Controls.Add(mySpanImgDes);

            HtmlGenericControl myHeadTempDiv = new HtmlGenericControl("div");
            myHeadTempDiv.Style.Add("margin", "10px");
            myHeadTempDiv.Style.Add("padding", "10px");

            HtmlGenericControl myH4 = new HtmlGenericControl("span");
            myH4.Style.Add("color", "mediumblue");
            myH4.Style.Add("font-weight", "bold");
            myH4.Style.Add("line-height", "1.1");
            myH4.Style.Add("font-size", "18px");
            myH4.Style.Add("margin", "10px");
            myH4.Style.Add("padding", "10px");
            myH4.InnerHtml = "Day: " + listItem.temp.day + " &#8451; ";
            myHeadTempDiv.Controls.Add(myH4);

            HtmlGenericControl myH5 = new HtmlGenericControl("span");
            myH5.Style.Add("color", "mediumblue");
            myH5.Style.Add("font-weight", "bold");
            myH5.Style.Add("line-height", "1.1");
            myH5.Style.Add("font-size", "18px");
            myH4.Style.Add("margin", "10px");
            myH4.Style.Add("padding", "10px");
            myH5.InnerHtml = "Night: " + listItem.temp.night + " &#8451; ";
            myHeadTempDiv.Controls.Add(myH5);

            mySpan1.Controls.Add(myHeadTempDiv);


            HtmlGenericControl mySubSpan1 = new HtmlGenericControl("span");
            mySubSpan1.Style.Add("margin", "10px");
            mySubSpan1.Style.Add("padding", "5px");
            mySubSpan1.Style.Add("text-align", "left");
            mySubSpan1.Style.Add("display", "inline-block");

            fetchLabel("yellow", "Weather: ", listItem.weather[0].description + "<br />", mySubSpan1);
            fetchLabel("yellow", "Wind Speed: ", listItem.speed + " m/sec <br />", mySubSpan1);
            fetchLabel("yellow", "Wind Direction: ", listItem.deg + "&deg; <br />", mySubSpan1);
            fetchLabel("yellow", "Clouds: ", listItem.clouds + " % <br />", mySubSpan1);
            fetchLabel("yellow", "Pressure: ", listItem.pressure + " % <br />", mySubSpan1);

            HtmlGenericControl mySubSpan2 = new HtmlGenericControl("span");
            mySubSpan2.Style.Add("margin", "10px");
            mySubSpan2.Style.Add("padding", "10px");
            mySubSpan2.Style.Add("text-align", "left");
            mySubSpan2.Style.Add("display", "inline-block");

            fetchLabel("yellow", "Morning: ", listItem.temp.morn + " &#8451; <br />", mySubSpan2);
            fetchLabel("yellow", "Evening: ", listItem.temp.eve + " &#8451; <br />", mySubSpan2);
            fetchLabel("yellow", "Minimum-Temperature: ", listItem.temp.min + " &#8451; <br />", mySubSpan2);
            fetchLabel("yellow", "Maximum-Temperature: ", listItem.temp.max + " &#8451; <br />", mySubSpan2);
            fetchLabel("yellow", "Humidity: ", listItem.humidity + " % <br />", mySubSpan2);

            mySpan1.Controls.Add(mySubSpan1);
            mySpan1.Controls.Add(mySubSpan2);

            myDiv.Controls.Add(mySpan1);
        }

        PlaceHolderDailyForecast.Controls.Add(myDiv);
        myDivDailyForecastButtonsBottom.Visible = true;

    }

    protected void ButtonDailyForecast_Click(object sender, EventArgs e)
    {
        myDivValidation.Visible = true;
        divCurrentWeather.Visible = false;
        div5DayForecast.Visible = false;
        myDivDayButtonsBottom.Visible = false;
        divDailyForecast.Visible = true;
        myDivDailyForecastButtonsBottom.Visible = false;
        try
        {
            getDailyForecastData(1);
        }
        catch (Exception exceptionOccured)
        {
            myDivValidation.InnerText = "Error: Enter valid city name or " + exceptionOccured.Message;
            myDivValidation.Style.Add("color", "red");
            divDailyForecast.Visible = false;
        }

    }
    
}
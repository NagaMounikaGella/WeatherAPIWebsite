<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weather Forecast</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <!--    favicons-->
    <link rel="shortcut icon" href="Content/images/weatherIcon.jpg" />
    <link rel="icon" href="Content/images/weatherIcon.jpg" />
    <style>
        .mainHeading{
            color: deeppink;
        }

        .LabelText{
            color: orangered;
            font-weight: bold;
        }

        .btnCurrentWeather{
            color: white;
            background-color: blue;
            border-color: blue;
        }

        .btn5DayForecast{
            color: white;
            background-color: blueviolet;
            border-color: blueviolet;
        }
        .btn5DayForecastDay{
            color: white;
            background-color: orangered;
            border-color: orangered;
        }

        .btnDailyForecast{
            color: white;
            background-color: green;
            border-color: green;
        }
        .btnDailyForecastDays{
            color: white;
            background-color: orangered;
            border-color: orangered;
        }

        #divCurrentWeather{
            border: 2px solid blue;
            border-radius: 10px;
            margin: 10vh;
            margin-left: 10vw;
            margin-right: 10vw;
        }

        #div5DayForecast{
            border: 2px solid blueviolet;
            border-radius: 10px;
            margin: 10vh;
            margin-left: 10vw;
            margin-right: 10vw;
        }
        #divDailyForecast{
            border: 2px solid green;
            border-radius: 10px;
            margin: 10vh;
            margin-left: 10vw;
            margin-right: 10vw;
        }
        .myDivDayButtons,
        .myDivDailyForecastButtons{
            margin: 10px;
            padding: 10px;
        }
        .myDivValidation
        {
            margin: 10px;
            padding: 10px;
        }
        .textBoxBorder{
            border: 2px solid orangered;
            border-radius: 1px;
            font-weight: bold;
        }
        body {
            background:linear-gradient(rgba(255,255,255,0.65),rgba(255,255,255,0.65)), url(../Content/images/download_1.jpg);
            background-size: 100% 100%;
            background-attachment: fixed;
        }
        
.btn:hover,
.btn:focus,
.btn.focus {
  color: #fff;
  text-decoration: none;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="text-center">
        <h1 class="mainHeading text-uppercase">Weather ForeCast</h1>
        <br />
        <div>
            <asp:Label ID="LabelCity" runat="server" Text="Enter City Name: " CssClass="LabelText"></asp:Label>
            <asp:TextBox ID="TextBoxCity" runat="server" CssClass="textBoxBorder"></asp:TextBox>
            
        </div>
        <br />
        <div id="myDivValidation" runat="server" visible="false">

        </div>
        <br />
        <div>
            <asp:Button ID="ButtonCurrentweather" runat="server" Text="Current Weather Data" CssClass="btn btnCurrentWeather" OnClick="ButtonCurrentweather_Click" />
            <asp:Button ID="Button5DayForecast" runat="server" Text="5 Day / 3 Hour Forecast" CssClass="btn btn5DayForecast" OnClick="Button5DayForecast_Click" />
            <asp:Button ID="ButtonDailyForecast" runat="server" Text="Daily Forecast" CssClass="btn btnDailyForecast" OnClick="ButtonDailyForecast_Click" />
        </div>
        <div id="divCurrentWeather" runat="server" visible="false">
            <asp:PlaceHolder ID="PlaceHolderCurrentWeather" runat="server"></asp:PlaceHolder>
        </div>
        <div id="div5DayForecast" runat="server" visible="false">
            <asp:PlaceHolder ID="PlaceHolder5DayForecastStart" runat="server"></asp:PlaceHolder>
            <div class="myDivDayButtons">
                <asp:Button ID="ButtonDay1" runat="server" Text="Day 1" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay2" runat="server" Text="Day 2" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay3" runat="server" Text="Day 3" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay4" runat="server" Text="Day 4" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay5" runat="server" Text="Day 5" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
            </div>
            <asp:PlaceHolder ID="PlaceHolder5DayForecast" runat="server"></asp:PlaceHolder>
            <div id="myDivDayButtonsBottom" runat="server" class="myDivDayButtons" visible="false">
                <asp:Button ID="ButtonDay1Bottom" runat="server" Text="Day 1" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay2Bottom" runat="server" Text="Day 2" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay3Bottom" runat="server" Text="Day 3" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay4Bottom" runat="server" Text="Day 4" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
                <asp:Button ID="ButtonDay5Bottom" runat="server" Text="Day 5" CssClass="btn btn5DayForecastDay" OnClick="DayButton_Click" />
            </div>

        </div>
        <div id="divDailyForecast" runat="server" visible="false">
            <asp:PlaceHolder ID="PlaceHolderDailyForecastStart" runat="server"></asp:PlaceHolder>
            <div class="myDivDailyForecastButtons">
                <asp:Button ID="Button3DaysForeCast" runat="server" Text="3 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button5DaysForeCast" runat="server" Text="5 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button7DaysForeCast" runat="server" Text="7 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button9DaysForeCast" runat="server" Text="9 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button12DaysForeCast" runat="server" Text="12 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button16DaysForeCast" runat="server" Text="16 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
            </div>
            <asp:PlaceHolder ID="PlaceHolderDailyForecast" runat="server"></asp:PlaceHolder>
            <div id="myDivDailyForecastButtonsBottom" runat="server" class="myDivDayButtons" visible="false">
                <asp:Button ID="Button3DaysForeCastBottom" runat="server" Text="3 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button5DaysForeCastBottom" runat="server" Text="5 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button7DaysForeCastBottom" runat="server" Text="7 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button9DaysForeCastBottom" runat="server" Text="9 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button12DaysForeCastBottom" runat="server" Text="12 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
                <asp:Button ID="Button16DaysForeCastBottom" runat="server" Text="16 Days" CssClass="btn btnDailyForecastDays" OnClick="ButtonFewDaysForeCast_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>

using WeatherApp.Services;
namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var result = await APIService.GetWeather(41.715137, 44.827095);
        LblCity.Text = result.City.Name;
        LblWeatherDescription.Text = result.List[0].Weather[0].Description;
        LblTemperature.Text = result.List[0].Main.Temperature + " °C";
        LblHumidity.Text = result.List[0].Main.Humidity + "%";
        LblWind.Text = result.List[0].Wind.Speed + "km/h";
    }
}
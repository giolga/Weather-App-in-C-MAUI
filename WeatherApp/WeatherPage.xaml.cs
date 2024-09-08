using WeatherApp.Services;
namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> weatherList;
	public WeatherPage()
	{
		InitializeComponent();
        weatherList = new List<Models.List>();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var result = await APIService.GetWeather(41.715137, 44.827095);

        foreach(var item in result.List)
        {
            weatherList.Add(item);
        }

        CvWeather.ItemsSource = weatherList;

        LblCity.Text = result.City.Name;
        LblWeatherDescription.Text = result.List[0].Weather[0].Description;
        LblTemperature.Text = result.List[0].Main.Temperature + " °C";
        LblHumidity.Text = result.List[0].Main.Humidity + "%";
        LblWind.Text = result.List[0].Wind.Speed + "km/h";
        ImgWeatherIcon.Source = result.List[0].Weather[0].Customicon;

        var fullIconUrl = result.List[0].Weather[0].Customicon;
        //ImgWeatherIcon.Source = new Uri(fullIconUrl);

        // Display the FullIconUrl to verify
        await DisplayAlert("Weather Icon", $"FullIconUrl: {fullIconUrl}", "OK");
    }
}
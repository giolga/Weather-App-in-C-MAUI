using WeatherApp.Services;
namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> weatherList;
    private double latitude;
    private double longitude;
	public WeatherPage()
	{
		InitializeComponent();
        weatherList = new List<Models.List>();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();

        var result = await APIService.GetWeather(latitude, longitude);

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

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }
}
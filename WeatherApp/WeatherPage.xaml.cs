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
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }

    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await APIService.GetWeather(latitude, longitude);
        UpdateUI(result);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var respone = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");

        if (respone != null)
        {
            await GetWeatherDataByCity(respone);
        }
    }


    public async Task GetWeatherDataByCity(string city)
    {
        var result = await APIService.GetWeatherByCity(city);
        UpdateUI(result);
    }

    public void UpdateUI(dynamic result)
    {
        foreach (var item in result.List)
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
        DisplayAlert("Weather Icon", $"FullIconUrl: {fullIconUrl}", "OK");
    }

}
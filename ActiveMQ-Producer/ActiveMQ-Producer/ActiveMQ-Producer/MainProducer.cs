namespace ActiveMQ_Producer
{
    class MainProducer
    {
        static void Main(string[] args)
        {
            var weatherData = new WeatherData();
            weatherData.FetchData();
        }
    }
}

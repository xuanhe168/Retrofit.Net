using System.Xml.Serialization;

namespace ExampleConsole.Models
{
    [XmlRoot("ArrayOfWeatherForecast")]
    public class WeatherEntity
    {
        [XmlElement("WeatherForecast")]
        public List<WeatherItemEntity>? Weathers { get; set; }
    }

    public class WeatherItemEntity
    {
        [XmlElement("Date")]
        public string Date { get; set; }

        [XmlElement("TemperatureC")]
        public string TemperatureC { get; set; }
        
        [XmlElement("Summary")]
        public string Summary { get; set; }
    }
}
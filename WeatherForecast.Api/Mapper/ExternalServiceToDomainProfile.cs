using System;
using AutoMapper;
using WeatherForecast.Api.Domain;
using WeatherForecast.ExternalServices.Openweathermap;

namespace WeatherForecast.Api.Mapper {
    public class ExternalServiceToDomainProfile : Profile {

        public ExternalServiceToDomainProfile() {
            CreateMap<OpenweatherForecastResponse, Domain.WeatherForecast>()
                .ForMember(m => m.City, opt => opt.MapFrom(src => src.city.name))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => src.message))
                .ForMember(m => m.Forecasts, opt => opt.MapFrom(src => src.list));
            CreateMap<List, ForecastDetails>()
                .ForMember(m => m.TemperatureInKelvin, opt => opt.MapFrom<TemperatureValueResolver>())
                .ForMember(m => m.WindSpeedInMeterPerSecond, opt => opt.MapFrom<WindspeedValueResolver>())
                .ForMember(m => m.HumidityInPercent, opt => opt.MapFrom<HumidityValueResolver>())
                .ForMember(m => m.ForecastDatetime, opt => opt.MapFrom<DatetimeValueResolver>());

            CreateMap<OpenweatherCurrentForecastResponse, ForecastDetails>()
                .ForMember(m => m.TemperatureInKelvin, opt => opt.MapFrom<CurrentTemperatureValueResolver>())
                .ForMember(m => m.WindSpeedInMeterPerSecond, opt => opt.MapFrom<CurrentWindspeedValueResolver>())
                .ForMember(m => m.HumidityInPercent, opt => opt.MapFrom<CurrentHumidityValueResolver>())
                .ForMember(m => m.ForecastDatetime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }

    public class CurrentTemperatureValueResolver : IValueResolver<OpenweatherCurrentForecastResponse, ForecastDetails, float?> {
        public float? Resolve(OpenweatherCurrentForecastResponse source, ForecastDetails destination, float? destMember, ResolutionContext context) {
            return source?.main?.temp;
        }
    }

    public class CurrentWindspeedValueResolver : IValueResolver<OpenweatherCurrentForecastResponse, ForecastDetails, float?> {
        public float? Resolve(OpenweatherCurrentForecastResponse source, ForecastDetails destination, float? destMember, ResolutionContext context) {
            return source?.wind?.speed;
        }
    }

    public class CurrentHumidityValueResolver : IValueResolver<OpenweatherCurrentForecastResponse, ForecastDetails, float?> {
        public float? Resolve(OpenweatherCurrentForecastResponse source, ForecastDetails destination, float? destMember, ResolutionContext context) {
            return source?.main?.humidity;
        }
    }

    public class TemperatureValueResolver : IValueResolver<List, ForecastDetails, float?> {
        public float? Resolve(List source, ForecastDetails destination, float? destMember, ResolutionContext context) {
            return source?.main?.temp;
        }
    }

    public class WindspeedValueResolver : IValueResolver<List, ForecastDetails, float?> {
        public float? Resolve(List source, ForecastDetails destination, float? destMember, ResolutionContext context) {
            return source?.wind?.speed;
        }
    }

    public class HumidityValueResolver : IValueResolver<List, ForecastDetails, float?> {
        public float? Resolve(List source, ForecastDetails destination, float? destMember, ResolutionContext context) {
            return source?.main?.humidity;
        }
    }

    public class DatetimeValueResolver : IValueResolver<List, ForecastDetails, DateTime> {
        public DateTime Resolve(List source, ForecastDetails destination, DateTime destMember, ResolutionContext context) {
            // According to https://openweathermap.force.com/s/article/time-format-and-zone-2019-10-24-21-47-24 the outputed date needs to be UTC
            if (DateTime.TryParse(source.dt_txt, out var date)) return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
            return DateTime.MinValue;
        }
    }
}

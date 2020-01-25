using System;
using AutoMapper;
using WeatherForecast.Api.Domain;
using WeatherForecast.Api.ExternalServices.Openweathermap;

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
                //.ForMember(m => m.ForecastDatetime, opt => opt.ConvertUsing(new DatetimeValueConverter()));
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

    public class DatetimeValueConverter : IValueConverter<List, DateTime> {
        public DateTime Convert(List sourceMember, ResolutionContext context) {
            // According to https://openweathermap.force.com/s/article/time-format-and-zone-2019-10-24-21-47-24 the outputed date needs to be UTC
            if (DateTime.TryParse(sourceMember.dt_txt, out var date)) return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
            throw new NotImplementedException();
        }
    }
    public class DatetimeValueResolver : IValueResolver<List, ForecastDetails, DateTime> {
        public DateTime Convert(List sourceMember, ResolutionContext context) {
            // According to https://openweathermap.force.com/s/article/time-format-and-zone-2019-10-24-21-47-24 the outputed date needs to be UTC
            if (DateTime.TryParse(sourceMember.dt_txt, out var date)) return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
            throw new NotImplementedException();
        }

        public DateTime Resolve(List source, ForecastDetails destination, DateTime destMember, ResolutionContext context) {
            if (DateTime.TryParse(source.dt_txt, out var date)) return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
            return DateTime.MinValue;
        }
    }
}

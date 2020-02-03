import api from './api'

export default {
    getForecast(search) {        
        let parsed = parseInt(search);        
        return isNaN(parsed) ? this.getForecastByCity(search) : this.getForecastByZipcode(search);
    },
    getForecastByZipcode (zipcode) {
        return api().get('/api/WeatherForecast/GetForecastByZipCode?zipcode=' + zipcode)
    },    
    getForecastByCity (city) {
        return api().get('/api/WeatherForecast/GetForecastByCity?city=' + city)
    }
}
# WeatherForecast

This Application uses the https://openweathermap.org/api to provide weather forecasts to a Vue.js frontend.

On the backend it uses ASP.NET Core with MediatR to make use of the command and query responsibility segregation pattern. 
Since the backend only has get-methods there are just queries.

The frontend makes use of the bootstrap-vue component library and also supports vuex and routing.

# WeatherForecast

This Application uses the https://openweathermap.org/api to provide weather forecasts to a Vue.js frontend.

On the backend it uses ASP.NET Core with MediatR to make use of the command and query responsibility segregation pattern. 
Since the backend only has get-methods there are just queries.

The frontend makes use of the bootstrap-vue component library and also supports vuex and routing.


# try to generate automatic swagger doc and postman collection
- 1. add tool-manifest
> dotnet new tool-manifest

2. Install Swashbuckle.AspNetCore.Cli
> dotnet tool install --version 5.6.2 Swashbuckle.AspNetCore.Cli

3. do stuff like this 
> dotnet swagger tofile --output './WeatherForecast.Api/bin/Debug/netcoreapp3.1/swagger.json' "./WeatherForecast.Api/bin/Debug/netcoreapp3.1/WeatherForecast.Api.dll" v1
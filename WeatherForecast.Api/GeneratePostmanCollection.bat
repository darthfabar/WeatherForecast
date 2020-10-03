call dotnet tool install --version 5.6.2 Swashbuckle.AspNetCore.Cli
call dotnet swagger tofile --output ../ApiDocumentation/swagger.json bin/Release/netcoreapp3.1/WeatherForecast.Api.dll v1
call npm install openapi-to-postmanv2 -g
call openapi2postmanv2 -s ../ApiDocumentation/swagger.json -o ../ApiDocumentation/collection.json -p
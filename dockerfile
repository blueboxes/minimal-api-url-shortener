# Build using docker build . -t shortener
# Run docker run --name shortener -p 8081:80 -d shortener

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
COPY src .
RUN dotnet restore
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/shortener.dll" ]
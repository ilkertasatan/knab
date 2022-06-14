FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/quote-service/Knab.QuoteService.Api/Knab.QuoteService.Api.csproj", "Knab.QuoteService.Api/"]
RUN dotnet restore "src/quote-service/Knab.QuoteService.Api/Knab.QuoteService.Api.csproj"
COPY . .
WORKDIR "/src/Knab.QuoteService.Api"
RUN dotnet build "Knab.QuoteService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Knab.QuoteService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Knab.QuoteService.Api.dll"]

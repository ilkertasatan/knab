FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY /src .
WORKDIR /quote-service/Knab.QuoteService.Api
RUN dotnet restore "Knab.QuoteService.Api.csproj"
RUN dotnet build "Knab.QuoteService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Knab.QuoteService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Knab.QuoteService.Api.dll"]
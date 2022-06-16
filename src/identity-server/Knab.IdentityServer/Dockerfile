FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/identity-server/Knab.IdentityServer/Knab.IdentityServer.csproj", "Knab.IdentityServer/"]
RUN dotnet restore "src/identity-server/Knab.IdentityServer/Knab.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Knab.IdentityServer"
RUN dotnet build "Knab.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Knab.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Knab.IdentityServer.dll"]

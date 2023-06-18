#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Ratings.API/Ratings.API.csproj", "src/Services/Ratings.API/"]
RUN dotnet restore "src/Services/Ratings.API/Ratings.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Ratings.API"
RUN dotnet build "Ratings.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ratings.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ratings.API.dll"]
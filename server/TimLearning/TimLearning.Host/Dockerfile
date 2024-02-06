FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TimLearning.Host/TimLearning.Host.csproj", "TimLearning.Host/"]
RUN dotnet restore "TimLearning.Host/TimLearning.Host.csproj"
COPY . .
WORKDIR "/src/TimLearning.Host"
RUN dotnet build "TimLearning.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimLearning.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimLearning.Host.dll"]

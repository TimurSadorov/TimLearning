FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TimLearning.Host/TimLearning.Host.csproj", "TimLearning.Host/"]
COPY ["TimLearning.Api/TimLearning.Api.csproj", "TimLearning.Api/"]
COPY ["TimLearning.Application/TimLearning.Application.csproj", "TimLearning.Application/"]
COPY ["TimLearning.Infrastructure.Interfaces/TimLearning.Infrastructure.Interfaces.csproj", "TimLearning.Infrastructure.Interfaces/"]
COPY ["TimLearning.Infrastructure.Implementation/TimLearning.Infrastructure.Implementation.csproj", "TimLearning.Infrastructure.Implementation/"]
COPY ["TimLearning.Domain/TimLearning.Domain.csproj", "TimLearning.Domain/"]
COPY ["TimLearning.Shared/TimLearning.Shared.csproj", "TimLearning.Shared/"]
RUN dotnet restore "TimLearning.Host/TimLearning.Host.csproj"
COPY . .
WORKDIR "/src/TimLearning.Host"
RUN dotnet build "TimLearning.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "TimLearning.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimLearning.Host.dll"]

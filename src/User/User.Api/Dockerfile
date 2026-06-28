FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/User/User.Api/User.Api.csproj", "src/User/User.Api/"]
COPY ["src/User/User.Application/User.Application.csproj", "src/User/User.Application/"]
COPY ["src/User/User.Domain/User.Domain.csproj", "src/User/User.Domain/"]
COPY ["src/User/User.Infrastructure/User.Infrastructure.csproj", "src/User/User.Infrastructure/"]

RUN dotnet restore "src/User/User.Api/User.Api.csproj"

COPY . .
WORKDIR /src/src/User/User.Api
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Order/Order.Api/Order.Api.csproj", "src/Order/Order.Api/"]
COPY ["src/Order/Order.Application/Order.Application.csproj", "src/Order/Order.Application/"]
COPY ["src/Order/Order.Domain/Order.Domain.csproj", "src/Order/Order.Domain/"]
COPY ["src/Order/Order.Infrastructure/Order.Infrastructure.csproj", "src/Order/Order.Infrastructure/"]

RUN dotnet restore "src/Order/Order.Api/Order.Api.csproj"

COPY . .
WORKDIR /src/src/Order/Order.Api
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]

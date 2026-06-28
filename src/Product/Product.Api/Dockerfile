FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Product/Product.Api/Product.Api.csproj", "src/Product/Product.Api/"]
COPY ["src/Product/Product.Application/Product.Application.csproj", "src/Product/Product.Application/"]
COPY ["src/Product/Product.Domain/Product.Domain.csproj", "src/Product/Product.Domain/"]
COPY ["src/Product/Product.Infrastructure/Product.Infrastructure.csproj", "src/Product/Product.Infrastructure/"]

RUN dotnet restore "src/Product/Product.Api/Product.Api.csproj"

COPY . .
WORKDIR /src/src/Product/Product.Api
RUN dotnet publish "Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Product.Api.dll"]

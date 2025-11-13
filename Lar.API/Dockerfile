FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY Lar.API/Lar.API.csproj Lar.API/
COPY ../Lar.Application/Lar.Application.csproj ../Lar.Application/
COPY ../Lar.Domain/Lar.Domain.csproj ../Lar.Domain/
COPY ../Lar.Infrastructure/Lar.Infrastructure.csproj ../Lar.Infrastructure/

RUN dotnet restore Lar.API/Lar.API.csproj

COPY . .

RUN dotnet publish Lar.API/Lar.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app 

COPY --from=build /app/publish .

EXPOSE 5026

ENTRYPOINT ["dotnet", "Lar.API.dll"]

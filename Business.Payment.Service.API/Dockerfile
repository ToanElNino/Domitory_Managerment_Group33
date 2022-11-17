#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /payapp
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Business.Payment.Service.API/Business.Payment.Service.API.csproj", "Business.Payment.Service.API/"]
RUN dotnet restore "Business.Payment.Service.API/Business.Payment.Service.API.csproj"
COPY . .
WORKDIR "/src/Business.Payment.Service.API"
RUN dotnet build "Business.Payment.Service.API.csproj" -c Release -o /payapp/build

FROM build AS publish
RUN dotnet publish "Business.Payment.Service.API.csproj" -c Release -o /payapp/publish

FROM base AS final
WORKDIR /payapp
COPY --from=publish /payapp/publish .
ENTRYPOINT ["dotnet", "Business.Payment.Service.API.dll"]
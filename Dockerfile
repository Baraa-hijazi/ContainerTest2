#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1883

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ContainerTest2.csproj", "."]
RUN dotnet restore "./ContainerTest2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ContainerTest2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContainerTest2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContainerTest2.dll"]
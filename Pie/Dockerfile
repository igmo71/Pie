#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install -y libfontconfig1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Pie/Pie.csproj", "Pie/"]
RUN dotnet restore "Pie/Pie.csproj"
COPY . .
WORKDIR "/src/Pie"
RUN dotnet build "Pie.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pie.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pie.dll"]
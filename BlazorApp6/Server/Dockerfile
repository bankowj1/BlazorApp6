#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorApp6/Server/BlazorApp6.Server.csproj", "BlazorApp6/Server/"]
COPY ["BlazorApp6/Client/BlazorApp6.Client.csproj", "BlazorApp6/Client/"]
COPY ["BlazorApp6/Shared/BlazorApp6.Shared.csproj", "BlazorApp6/Shared/"]
RUN dotnet restore "BlazorApp6/Server/BlazorApp6.Server.csproj"
COPY . .
WORKDIR "/src/BlazorApp6/Server"
RUN dotnet build "BlazorApp6.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp6.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorApp6.Server.dll"]
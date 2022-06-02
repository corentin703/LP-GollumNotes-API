FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoverotNimorin.GollumChat.Server/CoverotNimorin.GollumChat.Server.csproj", "CoverotNimorin.GollumChat.Server/"]
RUN dotnet restore "CoverotNimorin.GollumChat.Server/CoverotNimorin.GollumChat.Server.csproj"
COPY . .
WORKDIR "/src/CoverotNimorin.GollumChat.Server"
RUN dotnet build "CoverotNimorin.GollumChat.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoverotNimorin.GollumChat.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS="http://*:$PORT";
ENTRYPOINT ["dotnet", "CoverotNimorin.GollumChat.Server.dll"]

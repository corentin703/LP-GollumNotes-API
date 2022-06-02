FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoverotNimorin.GollumNotes.Server/CoverotNimorin.GollumNotes.Server.csproj", "CoverotNimorin.GollumNotes.Server/"]
RUN dotnet restore "CoverotNimorin.GollumNotes.Server/CoverotNimorin.GollumNotes.Server.csproj"
COPY . .
WORKDIR "/src/CoverotNimorin.GollumNotes.Server"
RUN dotnet build "CoverotNimorin.GollumNotes.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoverotNimorin.GollumNotes.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CoverotNimorin.GollumNotes.Server.dll"]

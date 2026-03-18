# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar csproj y restaurar
COPY ["KPIBackend.csproj", "."]
RUN dotnet restore "KPIBackend.csproj"

# Copiar todo y publicar
COPY . .
WORKDIR "/src"
RUN dotnet publish -c Release -o /app/publish

# Etapa runtime ligera
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
EXPOSE 443
ENTRYPOINT ["dotnet", "KPIBackend.dll"]
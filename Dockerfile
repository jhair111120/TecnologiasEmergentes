# ─── Etapa de build ────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivo de proyecto y restaurar dependencias (aprovecha caché de capas)
COPY ["CRM/CRM.csproj", "CRM/"]
RUN dotnet restore "CRM/CRM.csproj"

# Copiar el resto del código y publicar en modo Release
COPY . .
WORKDIR /src/CRM
RUN dotnet publish "CRM.csproj" -c Release -o /app/publish --no-restore

# ─── Etapa de runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Crear carpeta para la base de datos SQLite y darle permisos
RUN mkdir -p /app/data

# Copiar artefactos publicados
COPY --from=build /app/publish .

# La base de datos se almacenará en /app/data/contactos.db (volumen montable)
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/contactos.db"
ENV ASPNETCORE_URLS="http://+:8080"

EXPOSE 8080

ENTRYPOINT ["dotnet", "CRM.dll"]

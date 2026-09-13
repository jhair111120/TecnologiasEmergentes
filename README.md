# CRM – API de Contactos

API REST construida con **ASP.NET Core 8** y **SQLite** para gestionar contactos. Incluye Swagger para explorar y probar los endpoints.

---

## Requisitos

| Herramienta | Versión mínima |
|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download) | 8.0 |
| [Docker Desktop](https://www.docker.com/products/docker-desktop/) | Cualquier versión reciente |
| Git | Cualquier versión |

---

## Ejecución local (sin Docker)

```bash
# 1. Clonar el repositorio
git clone https://github.com/TU_USUARIO/CRM.git
cd CRM

# 2. Restaurar dependencias y crear la migración inicial
cd CRM
dotnet restore

# 3. Aplicar migraciones (crea el archivo contactos.db automáticamente)
dotnet ef database update

# 4. Ejecutar
dotnet run
```

La API quedará disponible en `http://localhost:5000`.  
Swagger UI: `http://localhost:5000/swagger`

---

## Ejecución con Docker (imagen directa)

```bash
# Desde la raíz del repositorio (donde está el Dockerfile)
docker build -t crm-api .
docker run -d -p 8080:8080 --name crm_contactos -v crm_data:/app/data crm-api
```

API: `http://localhost:8080`  
Swagger UI: `http://localhost:8080/swagger`

---

## Ejecución con Docker Compose (recomendado)

```bash
# Desde la raíz del repositorio (donde está docker-compose.yml)
docker compose up -d
```

Para detener:
```bash
docker compose down
```

La base de datos SQLite se persiste en el volumen `crm_data`. Al hacer `down` los datos se mantienen; para eliminarlos también:
```bash
docker compose down -v
```

---

## Endpoints disponibles

Base URL: `http://localhost:8080/api/contacto`

| Método | Ruta | Descripción |
|---|---|---|
| `GET` | `/api/contacto` | Listar todos los contactos |
| `GET` | `/api/contacto?busqueda=garcia` | Buscar por nombre o apellido |
| `GET` | `/api/contacto/{id}` | Obtener detalle de un contacto |
| `POST` | `/api/contacto` | Registrar un nuevo contacto |
| `PUT` | `/api/contacto/{id}` | Editar un contacto existente |
| `DELETE` | `/api/contacto/{id}` | Eliminar un contacto |

### Ejemplo – Body para POST / PUT

```json
{
  "nombre": "Juan",
  "apellido": "García",
  "telefono": "+56912345678",
  "correoElectronico": "juan.garcia@email.com",
  "empresa": "Mi Empresa S.A."
}
```

---

## Estructura del proyecto

```
CRM/
├── Controllers/
│   └── ContactoController.cs   # Endpoints CRUD + búsqueda
├── Datos/
│   └── ApplicationDbContext.cs # DbContext con SQLite
├── DTO/
│   ├── ContactoDTO.cs          # Respuesta
│   └── ContactoCreaDTO.cs      # Creación / edición
├── Entidades/
│   └── Contacto.cs             # Modelo de base de datos
├── Utils/
│   └── AutoMapperProfile.cs    # Mapeos DTO ↔ Entidad
├── Program.cs                  # Configuración y startup
└── appsettings.json
Dockerfile
docker-compose.yml
README.md
```

---

## Migración manual (solo si no usas Docker)

```bash
cd CRM
dotnet ef migrations add Inicial
dotnet ef database update
```

> Con Docker las migraciones se aplican automáticamente al iniciar el contenedor.

---

## Publicar en GitHub

```bash
git init
git add .
git commit -m "feat: API CRUD de contactos con SQLite"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/CRM.git
git push -u origin main
```

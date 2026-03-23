# API de Gestión de Indicadores Académicos

CRUD Indicadores La Salle Bajío – Backend

Este proyecto implementa una **API RESTful en .NET** para la gestión de indicadores académicos dentro de un entorno universitario. Permite administrar entidades como usuarios, facultades, indicadores, estrategias y actividades, manteniendo integridad relacional en **PostgreSQL**.

El sistema está diseñado con una **arquitectura monolítica modular**, utilizando **Repositories, DTOs y Entity Framework Core**, y preparado para ejecución en **contenedores Docker** y despliegue en **Azure con CI/CD mediante GitHub Actions**.

---

# Tabla de Contenidos

* Características
* Tecnologías utilizadas
* Arquitectura del proyecto
* Prerrequisitos
* Configuración y ejecución
* Variables de entorno
* Ejecución con Docker
* Pruebas de la API
* CI/CD con GitHub Actions

---

# Características

### Operaciones CRUD completas

El sistema permite la gestión de las siguientes entidades:

* Usuarios
* Roles
* Facultades
* Carreras
* Indicadores
* Estrategias
* Actividades
* Comentarios
* Directrices
* Periodos escolares
* Grupos de indicadores

### Modelo relacional robusto

Las entidades están interconectadas mediante claves foráneas, permitiendo:

* Jerarquía: Indicador → Estrategia → Actividad
* Dependencias: Usuario → Rol, Facultad, Carrera
* Contexto temporal: Periodos escolares
* Organización: Facultades y grupos de indicadores

### Arquitectura modular

Separación clara de responsabilidades:

* **Controllers** → Endpoints HTTP
* **Repositories** → Acceso a datos desacoplado
* **Models** → Representación de tablas
* **DTOs** → Transferencia de datos
* **DbContext** → Configuración de Entity Framework

### Preparado para contenedores

* Backend dockerizable
* Compatible con despliegue en Azure App Service

---

# Tecnologías utilizadas

* **.NET  / ASP.NET Core**
* **Entity Framework Core**
* **PostgreSQL**
* **Docker**
* **Swagger (OpenAPI)**
* **GitHub Actions**

---

# Arquitectura del proyecto

Estructura general:

```
KPIBackend/
├── Controllers/
├── Models/
├── DTOs/
├── Repositories/
├── Data/
│   └── AppDbContext.cs
├── Program.cs
├── appsettings.json
```

### Descripción

* **Controllers/**: Exponen endpoints REST (ej. `/api/usuarios`, `/api/indicadores`)
* **Models/**: Representan la estructura de la base de datos
* **DTOs/**: Controlan entrada y salida de datos
* **Repositories/**: Implementan lógica de acceso a datos con patrón genérico
* **Data/AppDbContext**: Configuración de conexión y mapeo ORM
* **Program.cs**: Configuración de servicios, inyección de dependencias y middlewares

---

# Prerrequisitos

### Básicos

* .NET 6 SDK
* PostgreSQL
* Docker Desktop

### Complementarios

* Postman
* pgAdmin

---

# Configuración y ejecución

## Paso 1: Configurar conexión a base de datos

En `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=salle;Username=postgres;Password=tu_password"
}
```

O mediante variable de entorno:

```
DB_PASSWORD=tu_password
```

El proyecto reemplaza dinámicamente `${DB_PASSWORD}` en el connection string.

---

## Paso 2: Ejecutar la aplicación

```bash
dotnet build
dotnet run
```

---

# Variables de entorno

* `DB_PASSWORD` → contraseña de PostgreSQL
* `ASPNETCORE_ENVIRONMENT` → Development / Production

---

# Ejecución con Docker

## Build

```bash
docker build -t kpi-backend:latest .
```

## Run

```bash
docker run -p 5000:80 kpi-backend:latest
```

## docker-compose (opcional)

Incluye backend + PostgreSQL con persistencia:

```yaml
services:
  backend:
    build: .
    ports:
      - "5000:80"
    environment:
      - ConnectionStrings__DefaultConnection=Host=db;Database=salle;Username=postgres;Password=postgres

  db:
    image: postgres:15
    environment:
      POSTGRES_DB: salle
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    volumes:
      - pgdata:/var/lib/postgresql/data

volumes:
  pgdata:
```

---

# Pruebas de la API

La API expone documentación interactiva en:

```
https://localhost:{puerto}/swagger
```

Ejemplo de endpoints:

```
GET     /api/usuarios
POST    /api/usuarios
GET     /api/indicadores
POST    /api/indicadores
```

Se recomienda usar **Postman** o Swagger para pruebas.

---

# CI/CD con GitHub Actions

El repositorio incluye configuración para automatizar despliegue:

### Flujo general

1. Push a rama principal
2. Build del proyecto .NET
3. Construcción de imagen Docker
4. Publicación en registry (Docker Hub o Azure Container Registry)
5. Despliegue a Azure App Service

### Requisitos

Configurar secrets en GitHub:

* `DOCKER_USERNAME`
* `DOCKER_PAT`
* Variables de entorno necesarias

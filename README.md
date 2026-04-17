[README.md](https://github.com/user-attachments/files/26808221/README.md)
# Veterinaria API REST

Sistema de gestión para clínica veterinaria desarrollado con arquitectura multicapas en .NET 8, Web API con autenticación JWT y MVC como interfaz de usuario.

## Tecnologías

- .NET 8
- ASP.NET Core Web API
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- JWT (JSON Web Tokens)
- Bootstrap 5
- C#

## Arquitectura

El proyecto está organizado en capas:

```
Veterinaria_Equipo5/
├── VET_WebAPI/         # Web API con JWT y controladores REST
├── VET_MVC/            # Interfaz de usuario MVC
├── AccesoDatos/        # Acceso a datos con EF Core y ADO.NET
├── Entidades/          # Modelos y entidades del dominio
└── LogicaNegocio/      # Lógica de negocio
```

## Funcionalidades

- Autenticación con JWT (ASP.NET Identity)
- CRUD completo de Clientes
- CRUD completo de Mascotas
- Gestión de Citas
- Gestión de Diagnósticos
- Detalle completo por mascota (cliente + mascota + cita + diagnóstico)

## Base de datos

SQL Server con las siguientes tablas:
- `tvet_clientes` — datos del dueño de la mascota
- `tvet_mascotas` — datos de la mascota
- `tvet_detallemascota` — raza, color, peso
- `tvet_citas` — fecha y hora de la cita
- `tvet_diagnostico` — descripción del diagnóstico

## Configuración

### Requisitos
- Visual Studio 2022
- SQL Server
- .NET 8 SDK

### Pasos para ejecutar

1. Clonar el repositorio
```bash
git clone https://github.com/jarethdx/Veterinaria-API-REST.git
```

2. Ejecutar el script SQL en SQL Server para crear la base de datos `DB_Veterinaria`

3. Cambiar el nombre del servidor en `VET_WebAPI/appsettings.json`:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;..."
}
```

4. Configurar proyectos de inicio múltiple en Visual Studio:
   - `VET_WebAPI` → Iniciar
   - `VET_MVC` → Iniciar

5. Presionar F5

### Credenciales por defecto
- Usuario: `admin@vet.com`
- Contraseña: `Admin123!`

## Endpoints principales

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | /api/Autenticacion/login | Obtener token JWT |
| GET | /api/Veterinaria/obtenerRegistroCompleto/{id} | Obtener registro por ID |
| POST | /api/Veterinaria/insRegistroCompleto | Insertar registro |
| PUT | /api/Veterinaria/modRegistroCompleto | Modificar registro |
| DELETE | /api/Veterinaria/delRegistroCompleto/{idCliente}/{idMascota}/{idCita} | Eliminar registro |

## Autor

Dixon Martinez Jarquin  
Estudiante de Ingeniería en Sistemas — UTC Costa Rica  
[GitHub](https://github.com/jarethdx)

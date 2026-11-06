# Lab 12-Chumbirayco

API REST para administrar dueños y sus mascotas en una veterinaria. Un dueño puede tener varias mascotas; cada mascota pertenece a un dueño. Las reglas del dominio impiden registrar dos dueños con el mismo correo y eliminar a un dueño mientras tenga mascotas.

## Proyecto

Abra `Lab12Chumbirayco/Lab12Chumbirayco.csproj` en Visual Studio 2026 y ejecute el proyecto `Lab 12-Chumbirayco`. Está dirigido a .NET 10 y usa controladores. Swagger UI se abre en `/swagger` y lleva una personalización visual morada clara.

## Base de datos y Scaffold Database First

El script PostgreSQL `database/vetcare.sql` crea las tablas, restricciones y datos de ejemplo. Se eligió PostgreSQL para usar un servicio gratuito compatible con una API ASP.NET Core en contenedor. La cadena se configura fuera del repositorio mediante `ConnectionStrings__VetCareDb`.

Después de crear la base PostgreSQL y ejecutar el script, en una terminal de Visual Studio, con `Lab12Chumbirayco` como proyecto de inicio, genere el modelo desde la base existente:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.0
dotnet add Lab12Chumbirayco package Microsoft.EntityFrameworkCore.Design --version 10.0.0
dotnet add Lab12Chumbirayco package Npgsql.EntityFrameworkCore.PostgreSQL --version 10.0.0
$env:ConnectionStrings__VetCareDb = '<cadena de conexión de PostgreSQL>'
dotnet ef dbcontext scaffold "$env:ConnectionStrings__VetCareDb" Npgsql.EntityFrameworkCore.PostgreSQL --project Lab12Chumbirayco --startup-project Lab12Chumbirayco --context VetCareDbContext --context-dir Data --output-dir Models --data-annotations --force
```

La cadena indicada en el comando se guarda solo en la sesión de PowerShell. Tras Scaffold, complete las anotaciones de validación requeridas (`[Range(0, 100)]` en `Pet.Age` y `[EmailAddress]` en `Owner.Email`), haga nullable la navegación `Pet.Owner` (`Owner?`) y agregue `[JsonIgnore]` a la colección `Owner.Pets`. El proveedor puede expresar el `CHECK` como configuración Fluent en el contexto, pero la anotación `[Range]` es necesaria para el 400 automático de `[ApiController]`. No use migraciones: la estructura se origina en el script SQL.

## Configuración local y CORS

Configure estos valores en el perfil de depuración de Visual Studio, en variables de entorno del sistema o en User Secrets. No los agregue a `appsettings.json` ni al repositorio.

```text
ConnectionStrings__VetCareDb=<cadena PostgreSQL>
Cors__AllowedOrigin=https://localhost:7001
```

`Cors__AllowedOrigin` es un único origen concreto. Para el despliegue, configúrelo con el origen del cliente web autorizado. Swagger, servido desde la propia API, es same-origin.

## API

Todas las operaciones usan controladores, DbContext y consultas LINQ asíncronas.

| Método | Ruta | Uso y respuestas principales |
|---|---|---|
| GET | `/api/owners?name=&email=` | Lista y filtra dueños: 200 |
| GET | `/api/owners/{id}` | Dueño: 200, 404 |
| GET | `/api/owners/{id}/pets` | Mascotas de un dueño: 200, 404 |
| POST | `/api/owners` | Crea dueño: 201, 400, 409 por correo duplicado |
| PUT | `/api/owners/{id}` | Actualiza dueño: 204, 400, 404, 409 por correo duplicado |
| DELETE | `/api/owners/{id}` | Elimina dueño: 204, 404, 409 si tiene mascotas |
| GET | `/api/pets?ownerId=&species=&name=` | Lista y filtra mascotas: 200 |
| GET | `/api/pets/{id}` | Mascota: 200, 404 |
| POST | `/api/pets` | Crea mascota: 201, 400 por dueño inexistente |
| PUT | `/api/pets/{id}` | Actualiza mascota: 204, 400, 404 |
| DELETE | `/api/pets/{id}` | Elimina mascota: 204, 404 |

Los modelos incluyen Data Annotations. La navegación `Pet.Owner` admite nulo para que el cliente envíe `ownerId`; `Owner.Pets` se excluye del JSON para evitar ciclos sin DTOs.

## Publicación gratuita propuesta

- **API: Render Free Web Service con Dockerfile.** Render documenta despliegues Docker; el servicio gratuito se suspende tras 15 minutos sin tráfico y el primer arranque puede tardar aproximadamente un minuto.
- **Base: Supabase Free PostgreSQL.** Incluye 500 MB y puede pausar proyectos con poca actividad en una semana. Es adecuado para esta demostración académica, con esas limitaciones.

Cree un proyecto PostgreSQL en Supabase y ejecute `database/vetcare.sql` en su SQL Editor. En Connect, seleccione el pooler compartido en modo sesión y convierta sus parámetros a formato Npgsql (`Host=...;Port=5432;Database=postgres;Username=postgres.<project-ref>;Password=...;SSL Mode=Require`). El pooler compartido admite IPv4, útil para el servicio Render. Publique este repositorio como Web Service Docker en Render y configure esa cadena como `ConnectionStrings__VetCareDb` y el origen cliente como `Cors__AllowedOrigin` en Environment, sin guardarlos en Git. La API publicada expondrá `/swagger`. El usuario debe completar la autenticación de sus propias cuentas de proveedor y conectar el repositorio para obtener URL reales.

## Verificación pendiente

Las pruebas exigidas por el laboratorio deben ejecutarse contra la URL pública, incluyendo cada operación y los estados 200, 201, 204, 400, 404 y 409. Guarde capturas reales en `captures/` y registre los enlaces publicados aquí. Las capturas, URL de API, enlace de repositorio remoto y resultados no se incluyen hasta que exista un despliegue accesible.

## Tema, observaciones y conclusiones

El sistema organiza los datos básicos de clientes de una veterinaria y las mascotas a su cargo. El modelo de dueños y mascotas mantiene la relación uno a muchos en PostgreSQL. Las respuestas 409 protegen la unicidad del correo y evitan dejar referencias incompletas. La elección de PostgreSQL gratuito y una API en contenedor permite publicar ambos componentes sin alojar la base en el equipo local; sus pausas por inactividad y límites gratuitos deben considerarse al usar la demostración.

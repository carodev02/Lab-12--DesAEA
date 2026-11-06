# Lab 12-Chumbirayco

Este proyecto permite registrar y consultar dueños y mascotas de una veterinaria. Cada dueño puede tener varias mascotas. La API incluye operaciones para crear, buscar, modificar y eliminar esos registros.

## Ejecutar el proyecto

1. Abre `Lab 12-Chumbirayco.sln` en Visual Studio 2026.
2. Configura la conexión PostgreSQL en las variables de entorno de Visual Studio:

   - `ConnectionStrings__VetCareDb`
   - `Cors__AllowedOrigin`

3. Ejecuta el proyecto `Lab12Chumbirayco`.
4. Abre Swagger en `/swagger`.

La contraseña de la base de datos no debe guardarse en el repositorio.

## Base de datos

El archivo [database/vetcare.sql](database/vetcare.sql) crea las tablas `owners` y `pets` y agrega algunos registros de ejemplo. La API publicada usa Supabase para PostgreSQL.

## Publicación

La API está publicada en Render: [abrir Swagger](https://lab12-chumbirayco.onrender.com/swagger). El archivo `render.yaml` contiene la configuración del servicio.

## Documentos

- [Informe del laboratorio](docs/informe-laboratorio.md)
- [Resultados de las pruebas publicadas](docs/verificacion-publicacion.md)

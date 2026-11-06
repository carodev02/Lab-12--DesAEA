# Informe del laboratorio

## Proyecto

El proyecto consiste en una API para registrar dueños y mascotas de una veterinaria. La relación es de uno a muchos: un dueño puede tener varias mascotas y cada mascota pertenece a un dueño. El sistema se limita a esos dos tipos de registro.

## Desarrollo

La base de datos se creó con un script SQL que está dentro del repositorio. El script define las tablas, la relación entre ellas y los datos de ejemplo. En Supabase se comprobó que quedaron dos dueños y tres mascotas.

La API está hecha con ASP.NET Core y .NET 10. Tiene operaciones para consultar, agregar, modificar y eliminar dueños y mascotas. También permite filtrar los resultados y consultar las mascotas de un dueño. Se agregaron validaciones y reglas para evitar correos repetidos y para no eliminar a un dueño que todavía tenga mascotas.

La interfaz de prueba es Swagger, con una apariencia morada clara. La API está publicada en Render y se conecta a la base de Supabase. Se puede abrir en [Swagger](https://lab12-chumbirayco.onrender.com/swagger).

## Pruebas y resultados

Se hicieron 30 solicitudes a la API publicada. Se probaron los filtros, las operaciones de ambas entidades, la ruta de mascotas por dueño, las validaciones, CORS y los códigos de respuesta requeridos. Las 30 solicitudes devolvieron el resultado esperado. Después de las pruebas se eliminaron los registros temporales y se confirmó que permanecían los datos de ejemplo.

El detalle de cada solicitud está en [Resultados de las pruebas](verificacion-publicacion.md).

## Observaciones

Render y Supabase se usaron en sus planes gratuitos. Cuando la API pasa tiempo sin recibir solicitudes, Render puede detenerla temporalmente; por eso, el primer acceso después de una pausa puede tardar más. Las credenciales de conexión se guardan en la configuración del servicio y no en los archivos del repositorio.

## Conclusión

La API permite administrar la información básica de dueños y mascotas y mantiene su relación en la base de datos. Las reglas de negocio y las pruebas publicadas ayudaron a comprobar las respuestas principales del sistema.

Para cerrar todos los entregables del laboratorio falta ejecutar Scaffold Database First desde Visual Studio y guardar las capturas reales de Swagger/API junto con el informe.

# Verificación de la API publicada

- Fecha: 2026-11-05 (hora de Perú)
- API: <https://lab12-chumbirayco.onrender.com>
- Swagger UI: <https://lab12-chumbirayco.onrender.com/swagger>
- Resultado: 29 de 29 comprobaciones recibieron el código esperado.

| # | Solicitud | Esperado | Obtenido |
|---:|---|---:|---:|
| 1 | `GET /api/owners` | 200 | 200 |
| 2 | `GET /api/owners?name=Ana&email=example.com` | 200 | 200 |
| 3 | `GET /api/owners/1` | 200 | 200 |
| 4 | `GET /api/owners/2147483646` | 404 | 404 |
| 5 | `GET /api/owners/1/pets` | 200 | 200 |
| 6 | `GET /api/owners/2147483646/pets` | 404 | 404 |
| 7 | `POST /api/owners` con datos inválidos | 400 | 400 |
| 8 | `POST /api/owners` con correo duplicado | 409 | 409 |
| 9 | `POST /api/owners` válido | 201 | 201 |
| 10 | `PUT /api/owners/{id}` con ID de ruta distinto | 400 | 400 |
| 11 | `PUT /api/owners/{id}` con correo duplicado | 409 | 409 |
| 12 | `PUT /api/owners/2147483646` | 404 | 404 |
| 13 | `PUT /api/owners/{id}` válido | 204 | 204 |
| 14 | `GET /api/pets` | 200 | 200 |
| 15 | `GET /api/pets?ownerId=1&species=...&name=...` | 200 | 200 |
| 16 | `GET /api/pets/{id}` | 200 | 200 |
| 17 | `GET /api/pets/2147483646` | 404 | 404 |
| 18 | `POST /api/pets` con dueño inexistente | 400 | 400 |
| 19 | `POST /api/pets` con edad fuera de rango | 400 | 400 |
| 20 | `POST /api/pets` válido | 201 | 201 |
| 21 | `PUT /api/pets/{id}` con ID de ruta distinto | 400 | 400 |
| 22 | `PUT /api/pets/2147483646` | 404 | 404 |
| 23 | `PUT /api/pets/{id}` con dueño inexistente | 400 | 400 |
| 24 | `PUT /api/pets/{id}` válido | 204 | 204 |
| 25 | `DELETE /api/owners/{id}` con mascota asociada | 409 | 409 |
| 26 | `DELETE /api/pets/{id}` | 204 | 204 |
| 27 | `DELETE /api/pets/2147483646` | 404 | 404 |
| 28 | `DELETE /api/owners/{id}` sin mascotas | 204 | 204 |
| 29 | `DELETE /api/owners/2147483646` | 404 | 404 |

Los registros usados para los casos de creación/actualización/eliminación fueron temporales y se limpiaron al terminar. La lectura final de la API devolvió únicamente los dos dueños y las tres mascotas de ejemplo. La captura de Render compartida por la usuaria muestra el Web Service en estado **Live**. No se presenta una captura de pantalla de Swagger como si hubiera sido tomada; debe añadirse al expediente visual de entrega.

# ClubNaval — Sistema de Gestión para Club Náutico

Sistema de escritorio para la administración de personas (socios/capitanes), barcos y salidas náuticas de un club marino.

---

## 1. Arquitectura del Sistema

```
ClubNaval (WinForms UI)
    ↓ depende
LogicaNegocio (BLL)
    ↓ depende
AccesoDatos (DAL)
    ↓ depende
Entidades (VO/DTO)
```

| Proyecto | Tipo | Rol |
|----------|------|-----|
| **ClubNaval** | WinForms EXE | Capa de presentación — formularios e interacción con el usuario |
| **LogicaNegocio** | Class Library | Lógica de negocio — validaciones y orquestación |
| **AccesoDatos** | Class Library | Acceso a datos — ejecución de stored procedures |
| **Entidades** | Class Library | Objetos de valor (VOs) compartidos entre capas |

Patrón utilizado: **Arquitectura en capas** con Value Objects como DTOs. No se utiliza ORM; todo el acceso a datos es mediante stored procedures con `SqlClient`.

---

## 2. Alcance del Sistema

El sistema permite gestionar tres módulos principales:

- **Personas**: registro, consulta, actualización y eliminación de socios y capitanes
- **Barcos**: registro, consulta, actualización y eliminación de embarcaciones
- **Salidas**: registro de salidas náuticas y finalización de las mismas

---

## 3. Requerimientos Funcionales

### Módulo Personas

| ID | Función | Descripción |
|----|---------|-------------|
| RF-01 | Alta de persona | Registrar persona con nombre, teléfono, dirección, correo, cargo (Socio/Capitán/Socio-Capitán) y foto |
| RF-02 | Consulta de personas | Listar todas las personas; filtrar por ID o por cargo |
| RF-03 | Actualización de persona | Editar datos de una persona existente (nombre, teléfono, dirección, correo, cargo, foto) |
| RF-04 | Eliminación de persona | Borrar una persona previa confirmación |

**Campos de persona:** IdPersona, Nombre, Teléfono, Dirección, Correo, Cargo (int — SOCIO=1, CAPITAN=2, SOCIO_CAPITAN=3), Disponibilidad, UrlFoto.

### Módulo Barcos

| ID | Función | Descripción |
|----|---------|-------------|
| RF-05 | Alta de barco | Registrar barco con matrícula, NoAmarre, nombre, cuota, dueño (seleccionable de personas registradas) y foto |
| RF-06 | Consulta de barcos | Listar todos los barcos (mostrando nombre del propietario); filtrar por ID o por dueño |
| RF-07 | Actualización de barco | Editar datos de un barco existente |
| RF-08 | Eliminación de barco | Borrar un barco previa confirmación |

**Campos de barco:** IdBarco, Matrícula, NoAmarre, Nombre, Cuota (double), IdPersona (dueño), UrlFoto, Disponibilidad.

### Módulo Salidas

| ID | Función | Descripción |
|----|---------|-------------|
| RF-09 | Registro de salida | Registrar salida con fecha/hora actual (automática), destino, barco (ComboBox), capitán (ComboBox), estado fijo "EN_PROCESO" |
| RF-10 | Consulta de salidas | Listar todas las salidas con datos extendidos (nombre del barco y del capitán); filtrar por estado |
| RF-11 | Finalizar salida | Cambiar estado de una salida de "EN_PROCESO" a "FINALIZADA" |

**Campos de salida:** IdSalida, FechaHoraSalida, Destino, Estado (EN_PROCESO / FINALIZADA), IdBarco, IdCapitán.
**Datos extendidos:** + NombreBarco, UrlFotoBarco, NombreCapitán, UrlFotoCapitán.

### Enumeradores

| Enum | Valores |
|------|---------|
| `CargoPersona` | SOCIO = 1, CAPITAN = 2, SOCIO_CAPITAN = 3 |
| `EstadosSalida` | EN_PROCESO, FINALIZADA |

---

## 4. Modelo de Datos (Entidades)

### VOPersona
```
IdPersona (int), Telefono (string), Direccion (string), Nombre (string),
Correo (string), Cargo (int?), Disponibilidad (bool?), UrlFoto (string)
```

### VOBarco
```
IdBarco (int), Matricula (string), NoAmarre (string), Nombre (string),
Cuota (double?), IdPersona (int?), UrlFoto (string), Disponibilidad (bool?)
```

### VOSalida
```
IdSalida (int), FechaHoraSalida (DateTime), Destino (string),
Estado (string), IdBarco (int), IdCapitan (int)
```

### VOSalidaExtendida (hereda de VOSalida)
```
+ NombreCapitan (string), UrlFotoCapitan (string),
  NombreBarco (string), UrlFotoBarco (string)
```

---

## 5. Base de Datos

- **Motor:** SQL Server LocalDB `(LocalDB)\MSSQLLocalDB`
- **Archivo:** `AccesoDatos\DBmarina.mdf`
- **Conexión:** Integrated Security, definida en `ClubNaval\App.config`

### Stored Procedures (17 total)

| Módulo | SP |
|--------|----|
| Persona | `SP_InsertarPersona`, `SP_ActualizarPersona`, `SP_EliminarPersona`, `SP_ConsultarPersonaPorId`, `SP_ConsultarPersonasPorCargo`, `SP_ConsultarPersonas` |
| Barco | `SP_InsertarBarco`, `SP_ActualizarBarco`, `SP_EliminarBarco`, `SP_ConsultarBarcoPorId`, `SP_ConsultarBarcos`, `SP_ConsultarBarcosPorOwner` |
| Salida | `SP_InsertarSalida`, `SP_FinalizarSalida`, `SP_ConsultarSalidasPorEstado`, `SP_ConsultarSalidasPorEstadoExtendida`, `SP_ConsultarSalidasPorId`, `SP_ConsultarSalidasPorIdExtendida` |

---

## 6. Requerimientos No Funcionales

| Aspecto | Detalle |
|---------|---------|
| Plataforma | Windows (.NET Framework 4.7.2) |
| Interfaz | Windows Forms |
| Lenguaje | C# |
| Base de datos | SQL Server LocalDB |
| Arquitectura | 4 capas (UI, BLL, DAL, Entities) |
| IDE | Visual Studio 2017+ |

---

## 7. Navegación y Flujo de la Aplicación

```
FRMConsultaSalidas (INICIO)
    ├── Buscar por estado
    ├── Refrescar listado
    └── Finalizar salida seleccionada

FRMConsultaPersonas
    ├── Buscar por ID o cargo
    ├── Actualizar → abre FRMActualizarPersona (modal)
    └── Eliminar (con confirmación)

FRMConsultaBarcos
    ├── Buscar por ID o dueño
    ├── Actualizar → abre FRMActualizarBarco (modal)
    └── Eliminar (con confirmación)

FRMAltaPersona — formulario independiente (sin acceso desde la UI principal)
FRMAltaBarco — formulario independiente (sin acceso desde la UI principal)
FRMAltaSalida — formulario independiente (sin acceso desde la UI principal)
```

**Nota importante:** Actualmente `FRMConsultaSalidas` es el único formulario de inicio. Los formularios de alta (Persona, Barco, Salida) y las consultas de Personas y Barcos no tienen un punto de acceso desde la interfaz principal. Falta implementar un menú de navegación o botones que enlace estos formularios.

---

## 8. Observaciones y Bugs Conocidos

1. **BLLSalida.InsertarSalida** — Actualiza la disponibilidad del barco a `false` como efecto secundario sin verificar el resultado de la operación.

2. **BLLBarco.ConsultarBarco** — Usa `barco.Equals(null)` en vez de `barco == null`. `Equals(null)` siempre retorna `false` aunque `barco` sea `null`, por lo que la validación nunca se cumple.

3. **BLLBarco.ConsultarBarco** — Lanza excepción cuando no encuentra un registro, en vez de retornar `null` silenciosamente (diseño inconsistente con el resto del sistema).

4. **Enum duplicado** — `Enumeradores.EstadosSalida` (en ClubNaval) es idéntico a `BLLSalida.EstadoSalida` (en LogicaNegocio). Solo el segundo se usa realmente.

5. **Inconsistencia en DAL** — `DALPersona` usa `SqlConnection`/`SqlCommand` directamente, mientras que `DALBarco` y `DALSalida` usan la clase helper `Consultas`. Esto duplica lógica de conexión.

6. **Falta de navegación** — Los formularios `FRMAltaPersona`, `FRMAltaBarco`, `FRMAltaSalida`, `FRMConsultaPersonas` y `FRMConsultaBarcos` no son accesibles desde `FRMConsultaSalidas` (único formulario de inicio).

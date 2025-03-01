# Manual de Usuario - AutoGest Pro (Fase 1)

## Introducción

AutoGest Pro es una aplicación diseñada para la gestión de servicios automotrices, permitiendo el manejo eficiente de usuarios, repuestos, facturación y vehículos.

## Requisitos del Sistem

- Sistema Operativo: Windows 10 o superior
- .NET 8.0 o superior
- 4GB de RAM mínimo
- 500MB de espacio en disco disponible

## Instalación

- Clonar el repositorio del proyecto:  

```sh
    git clone https://github.com/edyrrg/-EDD-1S2025_201730511.git 
```

- Acceder a la carpeta del proyecto:

```sh
    cd AutoGest_Pro/Fase1
```

- Instalar las dependencias necesarias:

```sh
   dotnet restore
```

- Compilar el proyecto:

```bash
   dotnet build
```

- Ejecutar la aplicación:

```bash
   dotnet run
```

## Interfaz de Usuario

### Pantalla de Inicio

La pantalla de inicio presenta el formulario de inicio de sesión.

## Inicio de Sesión

Ingresar nombre de usuario y contraseña root.

Presionar el botón "Iniciar Sesión".

En caso de credenciales incorrectas, se mostrará un mensaje de error.

![screenshot-login](../images/login.png)

## Menú Principal

Desde aquí, los usuarios pueden acceder a las siguientes funcionalidades:

- Carga Masiva
- Ingreso Individual
- Gestion de Usuarios
- Generar Servicio
- Cancelar Factura
- Generar Reportes

![screen-shot](../images/carga_masiva.png)

## Carga Masiva

Permite al usuario root(Administrador) Cargar de manera masiva Usuarios, Vehiculos y/o Repuestos por medio de archivos JSON este debe de cumplir con lo requisitos estrictos de estructura que a continuación de ejemplifican:

```JSON
## Usuario
[
    {
        "ID": 1,
        "Nombres": "Juan",
        "Apellidos": "Pérez",
        "Correo": "juan.perez@mail.com",
        "Contrasenia": "123456"
    },
    {
        "ID": 2,
        "Nombres": "María",
        "Apellidos": "Gómez",
        "Correo": "maria.gomez@mail.com",
        "Contrasenia": "password123"
    }
]
## Vehículos
[
    {
        "ID": 1,
        "ID_Usuario": 1,
        "Marca": "Toyota",
        "Modelo": "Corolla",
        "Placa": "ABC123"
    },
    {
        "ID": 2,
        "ID_Usuario": 2,
        "Marca": "Ford",
        "Modelo": "Focus",
        "Placa": "XYZ456"
    }
]
## Repuestos
[
    {
        "ID": 1,
        "Repuesto": "Filtro de aceite",
        "Detalles": "Filtro de aceite para motor 1.8L",
        "Costo": 15.75
    },
    {
        "ID": 2,
        "Repuesto": "Bujías",
        "Detalles": "Juego de bujías para motor 2.0L",
        "Costo": 30.50
    }
]
```

Soporte Técnico

> *by Edy Rolando Rojas González - 3251938781401@ingenieria.usac.edu.gt*

[Regresar](/README.md)

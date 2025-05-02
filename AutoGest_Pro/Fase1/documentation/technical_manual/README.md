# Manul Tecnico | AutoGest Pro | Fase 1

## Descripción del Proyecto

AutoGest Pro es una aplicación de gestión de servicios automotrices que permite la administración de usuarios, vehículos, repuestos, servicios y facturas. La aplicación está desarrollada en C# utilizando .NET y GTK# para la interfaz gráfica de usuario.

## Estructura del Proyecto

El proyecto está organizado en los siguientes directorios y archivos principales:

```directory
📂AutoGest_Pro/
    📂Fase1/
        📂documentation/
            📂images/
        📂banner_edd_2025.png
            📂technical_manual/
                README.md
            📂user_manual/
                README.md
        📂bin/
        📂obj/
        Program.cs
        Fase1.csproj
        Fase1.sln
        📂src/
            📂adt/
                CircularList.cs
                DoubleLinkedList.cs
                Queue.cs
                SimpleList.cs
                Stack.cs
            📂gui/
                CargaMasiva.cs
                GenerarServicio.cs
                GestionUsuarios.cs
                IngresoRepuesto.cs
                IngresoServicio.cs
                IngresoUsuario.cs
                IngresoVehiculos.cs
                Login.cs
                MainWindow.cs
                MenuIngresoManual.cs
                MyWindow.cs
                styles/
                    styles.css
            📂models/
                Factura.cs
                NodoFactura.cs
                NodoRepuesto.cs
                NodoServicio.cs
                NodoUsuario.cs
                NodoVehiculo.cs
                Repuesto.cs
                Servicio.cs
                Usuario.cs
                Vehiculo.cs
            📂services/
                DataService.cs
            📂auth/
                AuthService.cs

```

## Componentes Principales

1. Estructuras de Datos (ADT)
    - CircularList.cs: Implementa una lista circular para la gestión de repuestos.
    - DoubleLinkedList.cs: Implementa una lista doblemente enlazada para la gestión de vehículos.
    - Queue.cs: Implementa una cola para la gestión de servicios.
    - SimpleList.cs: Implementa una lista simplemente enlazada para la gestión de usuarios.
    - Stack.cs: Implementa una pila para la gestión de facturas.
2. Interfaz Gráfica (GUI)
    - CargaMasiva.cs: Ventana para la carga masiva de datos desde archivos JSON.
    - GenerarServicio.cs: Ventana para la generación de servicios.
    - GestionUsuarios.cs: Ventana para la gestión de usuarios.
    - IngresoRepuesto.cs: Ventana para el ingreso de repuestos.
    - IngresoServicio.cs: Ventana para el ingreso de servicios.
    - IngresoUsuario.cs: Ventana para el ingreso de usuarios.
    - IngresoVehiculos.cs: Ventana para el ingreso de vehículos.
    - Login.cs: Ventana de inicio de sesión.
    - MainWindow.cs: Ventana principal del sistema.
    - MenuIngresoManual.cs: Menú para el ingreso manual de datos.
    - MyWindow.cs: Clase base para las ventanas del sistema.
    - styles.css: Archivo de estilos CSS para la interfaz gráfica.
3. Modelos (Models)
    - Factura.cs: Modelo para las facturas.
    - NodoFactura.cs: Nodo para la pila de facturas.
    - NodoRepuesto.cs: Nodo para la lista circular de repuestos.
    - NodoServicio.cs: Nodo para la cola de servicios.
    - NodoUsuario.cs: Nodo para la lista simplemente enlazada de usuarios.
    - NodoVehiculo.cs: Nodo para la lista doblemente enlazada de vehículos.
    - Repuesto.cs: Modelo para los repuestos.
    - Servicio.cs: Modelo para los servicios.
    - Usuario.cs: Modelo para los usuarios.
    - Vehiculo.cs: Modelo para los vehículos.
4. Servicios (Services)
    - DataService.cs: Servicio para la gestión de datos, implementa el patrón Singleton.
5. Autenticación (Auth)
    - AuthService.cs: Servicio para la autenticación de usuarios.

## Instalación y Configuración

### Requisitos

- .NET SDK
- GTK# para .NET
- Graphviz

## Entorno de Desarrollo

Entorno de desarrollo utilizado en AutoGest Pro  

1. **Visual Studio Code**
    - Versión: 1.97.2 (user setup)
    - Fecha: 2025-02-12T23:20:35.343Z
    - Electron: 32.2.7
    - ElectronBuildId: 10982180
    - Chromium: 128.0.6613.186
    - Node.js: 20.18.1
    - V8: 12.8.374.38-electron.0
    - SO: Windows_NT x64 10.0.26100
2. **.NET SDK**
    - Version:           8.0.112
    - Commit:            3f0c4a16e5
    - Workload version:  8.0.100-manifests.784cc8f7
    - Runtime Environment:
        - OS Name:     ubuntu
        - OS Version:  22.04
        - OS Platform: Linux
        - RID:         ubuntu.22.04-x64
        - Base Path:   /usr/lib/dotnet/sdk/8.0.112/
3. **GTK**
    - v3.24.33
4. **Project 'Fase1' has the following package references [net8.0]**
    - DotNetGraph | 3.2.0
    - GtkSharp | 3.24.24.95

5. **Graphviz**
    - dot - graphviz version 2.43.0 (0)
    - libdir = "/usr/lib/x86_64-linux-gnu/graphviz"
    - Activated plugin library: libgvplugin_dot_layout.so.6
    - Using layout: dot:dot_layout
    - Activated plugin library: libgvplugin_core.so.6
    - Using render: dot:core
    - Using device: dot:dot:core
6. **WSL 2 | (Windows Subsystem for Linux)**
    - Versión de WSL: 2.4.11.0
    - Versión de kernel: 5.15.167.4-1
    - Versión de WSLg: 1.0.65
    - Versión de MSRDC: 1.2.5716
    - Versión de Direct3D: 1.611.1-81528511
    - Versión DXCore: 10.0.26100.1-240331-1435.ge-release
    - Versión de Windows: 10.0.26100.3194
7. **SO**
    - Edición Windows 11 Pro
    - Versión 24H2
    - Instalado el 14/02/2025
    - Versión del sistema operativo 26100.3194
8. **Hardware**
    - Nombre del dispositivo ryuk
    - Procesador 12th Gen Intel(R) Core(TM) i5-12400   2.50 GHz
    - RAM instalada 32.0 GB (31.7 GB usable)
    - Tipo de sistema Sistema operativo de 64 bits, procesador basado en x64

> *by Edy Rolando Rojas González*

[Regresar](/README.md)

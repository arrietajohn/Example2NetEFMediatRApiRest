# Pasos para crear una aplicación simple usando .NET 8 y EF

1. **Crear la carpeta de la solución:**
    ```bash
    mkdir Example2NetEf
    ```

2. **Crear la solución:**
    ```bash
    cd Example1NetEf
    dotnet new sln -n MyProjectCleanArchitecture
    ```
   Una alternativa de 1 y 2 en un solo comando:
    ```bash
    dotnet new sln -n MyProjectCleanArchitecture -o Example2NetEf
    ```

3. **Crear las carpetas donde organizaremos los componentes de la aplicación:**
    ```bash
    mkdir Doc
    mkdir Src
    mkdir Tests
    cd Src
    mkdir Core
    mkdir Api
    mkdir Infrastructure
    ```

4. **Crear los proyectos:**
    ```bash
    dotnet new classlib -n MyProject.Domain
    dotnet new classlib -n MyProject.Application
    dotnet new classlib -n MyProject.Infrastructure
    dotnet new webapi -n MyProject.Api
    ```

5. **Agregar los proyectos a las respectivas carpetas de la solución:**
    ```bash
    move MyProject.Domain Core
    move MyProject.Application Core
    move MyProject.Infrastructure Infrastructure
    move MyProject.Api Api
    ```

6. **Agregar los proyectos a la solución:**
    ```bash
    dotnet sln add Core\MyProject.Domain\MyProject.Domain.csproj
    dotnet sln add Core\MyProject.Application\MyProject.Application.csproj
    dotnet sln add Infrastructure\MyProject.Infrastructure\MyProject.Infrastructure.csproj
    dotnet sln add Api\MyProject.Api\MyProject.Api.csproj
    ```

7. **Agregamos las referencias de los proyectos según el concepto de Dependencia Invertida:**
    - Una referencia del proyecto `MyProject.Domain` al proyecto `MyProject.Application`
    - Una referencia del proyecto `MyProject.Domain` y del proyecto `MyProject.Application` al proyecto `MyProject.Infrastructure`
    - Una referencia de los proyectos `MyProject.Domain`, `Application`, `Infrastructure` al proyecto `MyProject.Api`
    - Para no tener que estar entrando a cada proyecto, mejor nos colocamos en la carpeta raíz de la solución.

      ```bash
      dotnet add MyProject.Application\MyProject.Application.csproj reference MyProject.Domain\MyProject.Domain
      dotnet add MyProject.Infrastructure\MyProject.Infrastructure.csproj reference MyProject.Domain\MyProject.Domain
      dotnet add MyProject.Infrastructure\MyProject.Infrastructure.csproj reference MyProject.Application\MyProject.Application
      dotnet add MyProject.Api\MyProject.Api.csproj reference MyProject.Domain\MyProject.Domain
      dotnet add MyProject.Api\MyProject.Api.csproj reference MyProject.Application\MyProject.Application
      dotnet add MyProject.Api\MyProject.Api.csproj reference MyProject.Infrastructure\MyProject.Infrastructure
      ```

    - Si lo hacemos entrando a cada proyecto, debe ser así (suponiendo que estamos dentro del proyecto API):

      ```bash
      dotnet add reference ..\MyProject.Domain\MyProject.Domain.csproj
      dotnet add reference ..\MyProject.Application\MyProject.Application.csproj
      dotnet add reference ..\MyProject.Infrastructure\MyProject.Infrastructure.csproj
      ```

8. **Crear las clases Entidades en el proyecto Domain:**
    ```bash
    cd MyProject.Domain
    dotnet new class -n MyProject.Domain.Common.BaseDomainModel -o Common
    dotnet new class -n MyProject.Domain.Video
    dotnet new class -n MyProject.Domain.Streamer
    dotnet new class -n MyProject.Domain.Director
    ```

    La relación entre las clases es la siguiente:
    - Un Video tiene un director y un director solo puede tener un video.
    - Un Video puede tener varios Actores y un Actor puede actuar en varios Videos.
    - Un Video es subido por un Streamer y un Streamer puede subir varios Videos.
    ```
# Pasos para crear una aplicación simple usando .NET 8 y EF

8.1. **Definir el código de cada clase (ver el código)**

9. **Crear las interfaces o contratos a nivel de Application:**
    - Nos colocamos en la carpeta raíz de la solución y luego entramos en la carpeta `MyProject.Application`.
    - Creamos las siguientes carpetas:
        - `Contracts`
        - `Infrastructure`
        - `Persistence`
    - Dentro de la carpeta `Persistence`, creamos las interfaces:
        - `IAsyncRepository`
        - `IStreamerRepository`
        - `IVideoRepository`
        - La interfaz `IStreamerRepository` debe heredar de la interfaz `IAsyncRepository`.
        - La interfaz `IVideoRepository` debe heredar de la interfaz `IAsyncRepository`.

10. **Crear la base de datos (BD) asumiendo que usamos SQL Server y MS-SSMS:**
    - Ejecutamos el siguiente comando en SQL Server Management Studio (SSMS):
        ```sql
        CREATE DATABASE StreamerDb;
        ```

11. **Obtenemos la cadena de conexión de la BD y la aseguramos, ya que la necesitaremos más adelante.**

12. **Crear el archivo de configuración y colocarlo en el proyecto Api:**
    - Nos colocamos en la carpeta raíz de la solución y luego entramos en la carpeta `MyProject.Api`.
    - Creamos un archivo llamado `appsettings.json` con las siguientes propiedades:
        ```json
        {
          "ConnectionStrings": {
            "ConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StreamersDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
          },
          "EmailSettings": {
            "FromAddress": "elcostenioarrieta@gmail.com",
            "ApiKey": "",
            "FromName": "John Carlos Arrieta Arrieta"
          },
          "Logging": {
            "LogLevel": {
              "Default": "Information",
              "Microsoft.AspNetCore": "Warning"
            }
          },
          "AllowedHosts": "*"
        }
        ```

13. **Instalamos las librerías necesarias en el proyecto MyProject.Infrastructure:**
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    ```

14. **Crear la clase que representa el contexto de BD usando EF:**
    - Nos ubicamos en la carpeta `MyProject.Infrastructure` y luego en la subcarpeta `Persistence`.
    - Creamos la clase `StreamerDbContext`.

15. **Crear una clase para insertar algunos datos en la BD:**
    - Creamos una clase llamada `StreamerDbContextSeed` en la carpeta `Persistence`.

16. **Creamos una carpeta para organizar las clases Repository:**
    - Nos ubicamos en la carpeta raíz de la solución.
    - Creamos la carpeta `Repositories`.

17. **Creamos las clases repository:**
    - Creamos las siguientes clases:
        - `RepositoryBase` (debe implementar la interfaz `IAsyncRepository`)
        - `StreamerRepository` (debe heredar de `RepositoryBase` e implementar la interfaz `IStreamerRepository`)
        - `VideoRepository` (debe heredar de `RepositoryBase` e implementar la interfaz `IVideoRepository`).

18. **Creamos una clase que actúe como extensión para registrar fácilmente la Inyección de dependencias de los Repositorios y Servicios de la capa de Infraestructura:**
    - Creamos la clase `InfrastructureServiceRegistrationExtension`.

19. **Definir la clase Program (Punto de inicio o ejecución de la solución):**
    - Creamos la clase `Program` en el proyecto `MyProject.Api`.
    - Usamos el método de extensión creado en el punto anterior y todas las inyecciones de dependencia necesarias.

20. **Ejecutar las Migraciones de EntityFrameworkCore para que se genere la BD a partir de las clases del proyecto MyProject.Domain y definidas como DbSet en la clase StreamerDbContext del proyecto MyProject.Infrastructure.**


20.1) **Instalar la herramienta de migraciones de EfCore**
 ```bash
dotnet tool install --global dotnet-ef 
  ```
  

20.2) **Crear las migraciones EF**
 ```bash
dotnet ef migrations add Migracion1StreamerDb --startup-project ..\MyProject.ConsoleApp\MyProject.ConsoleApp.csproj 
  ```
  

21) **Verificamos en MS-SSMS si las tablas fueron creadas con sus respectivas columnas y relaciones.** 

  

22) **Limpiar, recompilar y ejecutar la aplicación**

 

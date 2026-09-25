# Práctica de Recursividad

Sitio colaborativo en Blazor WebAssembly con un apartado independiente para cada ejercicio.

## Para cada integrante

1. Clona el repositorio.
2. Abre la carpeta con su agente.
3. Indica únicamente: **`trabaja en el ejercicio 1`** o **`trabaja en los ejercicios 1 y 2`**.
4. Revisa y sube el Pull Request preparado por el agente.

El agente encontrará el enunciado, los archivos permitidos, las pruebas requeridas y la regla de explicación en `AGENTS.md`.

## Para revisar el código

Los archivos de cada actividad llevan el nombre de su tema (Factorial, Fibonacci, Máximo Común Divisor, Cambio de Monedas y Torres de Hanói). `docs/GUIA_CODIGO.md` relaciona cada número con su página, algoritmo, pruebas y métodos principales. Las rutas públicas `/ejercicio-1` a `/ejercicio-5` no cambian.

## Desarrollo local

```powershell
dotnet restore PracticaRecursividad.slnx
dotnet run --project src/RecursividadWeb/RecursividadWeb.csproj
```

## Validación

```powershell
dotnet test PracticaRecursividad.slnx
dotnet publish src/RecursividadWeb/RecursividadWeb.csproj -c Release
```

Cada Pull Request se valida automáticamente. Al integrar cambios en `main`, GitHub Actions publica el sitio en GitHub Pages. Después de crear el remoto, activa **Settings → Pages → Source → GitHub Actions** y agrega a los integrantes como colaboradores.

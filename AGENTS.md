# Instrucciones automáticas para agentes

## Interpretación de la petición

Cuando el usuario diga `trabaja en el ejercicio N` o `trabaja en los ejercicios N y M`, ejecuta el flujo completo sin pedirle que identifique archivos, rutas, comandos o requisitos. Lee primero `docs/EJERCICIOS.md` y trabaja todos los números mencionados.

No resuelvas ejercicios que el usuario no haya seleccionado. Si menciona varios, completa todos en el mismo turno y verifica el conjunto.

## Mapa de trabajo

| Ejercicio | Página que debes reemplazar | Algoritmo que debes crear | Pruebas que debes crear |
|---|---|---|---|
| 1 | `src/RecursividadWeb/Pages/Ejercicio1.razor` | `src/RecursividadWeb/Algorithms/Ejercicio1Algorithm.cs` | `tests/RecursividadWeb.Tests/Ejercicio1Tests.cs` |
| 2 | `src/RecursividadWeb/Pages/Ejercicio2.razor` | `src/RecursividadWeb/Algorithms/Ejercicio2Algorithm.cs` | `tests/RecursividadWeb.Tests/Ejercicio2Tests.cs` |
| 3 | `src/RecursividadWeb/Pages/Ejercicio3.razor` | `src/RecursividadWeb/Algorithms/Ejercicio3Algorithm.cs` | `tests/RecursividadWeb.Tests/Ejercicio3Tests.cs` |
| 4 | `src/RecursividadWeb/Pages/Ejercicio4.razor` | `src/RecursividadWeb/Algorithms/Ejercicio4Algorithm.cs` | `tests/RecursividadWeb.Tests/Ejercicio4Tests.cs` |
| 5 | `src/RecursividadWeb/Pages/Ejercicio5.razor` | `src/RecursividadWeb/Algorithms/Ejercicio5Algorithm.cs` | `tests/RecursividadWeb.Tests/Ejercicio5Tests.cs` |

## Flujo obligatorio

1. Comprueba que estás en una rama distinta de `main`. Si estás en `main`, crea `ejercicio-N-descripcion`; para varios usa `ejercicios-N-M`.
2. Lee el requisito completo en `docs/EJERCICIOS.md`.
3. Implementa el algoritmo en la clase C# indicada, separado de la interfaz.
4. Sustituye la página pendiente por una interfaz funcional, responsiva y en español.
5. Valida entradas y muestra errores comprensibles.
6. Conserva la ruta `/ejercicio-N`.
7. Añade pruebas de casos base, normales y límites razonables.
8. Ejecuta `dotnet test PracticaRecursividad.slnx` y `dotnet publish src/RecursividadWeb/RecursividadWeb.csproj -c Release`.
9. Revisa `git diff` para confirmar que no modificaste ejercicios no asignados.
10. Crea commits claros. Si hay un remoto con permisos, sube la rama y abre un Pull Request; si no, informa el comando exacto.

## Regla obligatoria: ¿Cómo funciona?

Cada ejercicio implementado debe conservar dentro de su tarjeta o panel principal una sección visible con el título exacto `¿Cómo funciona?`. No basta con comentarios en el código ni documentación externa.

La explicación debe describir en lenguaje sencillo:

- las entradas solicitadas;
- el caso base;
- el paso o llamada recursiva;
- el resultado generado;
- un ejemplo concreto.

El ejercicio no está terminado si la sección conserva el texto pendiente, está vacía o solo afirma que usa recursividad.

## Límites de modificación

- No cambies páginas de ejercicios no seleccionados.
- No cambies `docs/EJERCICIOS.md` para adaptar el requisito a tu solución.
- No elimines pruebas existentes.
- Evita modificar `Home.razor`, `NavMenu.razor`, componentes compartidos o estilos globales salvo que sea imprescindible.
- No incluyas secretos, binarios, `bin/` ni `obj/`.

## Definición de terminado

Un ejercicio está completo únicamente si funciona desde su tarjeta, usa C# y recursividad, valida las entradas, presenta el resultado, contiene `¿Cómo funciona?`, incluye pruebas y toda la solución pasa compilación y pruebas.

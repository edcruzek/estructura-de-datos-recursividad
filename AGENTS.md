# Instrucciones automáticas para agentes

## Interpretación de la petición

Cuando el usuario diga `trabaja en el ejercicio N` o `trabaja en los ejercicios N y M`, ejecuta el flujo completo sin pedirle que identifique archivos, rutas, comandos o requisitos. Lee primero `docs/EJERCICIOS.md` y trabaja todos los números mencionados.

No resuelvas ejercicios que el usuario no haya seleccionado. Si menciona varios, completa todos en el mismo turno y verifica el conjunto.

## Mapa de trabajo

| Ejercicio | Tema | Página | Algoritmo | Pruebas |
|---|---|---|---|---|
| 1 | Factorial | `src/RecursividadWeb/Pages/Factorial.razor` | `src/RecursividadWeb/Algorithms/FactorialAlgorithm.cs` | `tests/RecursividadWeb.Tests/FactorialTests.cs` |
| 2 | Fibonacci | `src/RecursividadWeb/Pages/Fibonacci.razor` | `src/RecursividadWeb/Algorithms/FibonacciAlgorithm.cs` | `tests/RecursividadWeb.Tests/FibonacciTests.cs` |
| 3 | Máximo común divisor | `src/RecursividadWeb/Pages/MaximoComunDivisor.razor` | `src/RecursividadWeb/Algorithms/MaximoComunDivisorAlgorithm.cs` | `tests/RecursividadWeb.Tests/MaximoComunDivisorTests.cs` |
| 4 | Cambio de monedas | `src/RecursividadWeb/Pages/CambioMonedas.razor` | `src/RecursividadWeb/Algorithms/CambioMonedasAlgorithm.cs` | `tests/RecursividadWeb.Tests/CambioMonedasTests.cs` |
| 5 | Torres de Hanói | `src/RecursividadWeb/Pages/TorresHanoi.razor` | `src/RecursividadWeb/Algorithms/TorresHanoiAlgorithm.cs` | `tests/RecursividadWeb.Tests/TorresHanoiTests.cs` |

Los nombres de archivo indican el tema; los números siguen identificando el enunciado en `docs/EJERCICIOS.md` y la ruta pública `/ejercicio-N`. Los estilos específicos usan el mismo nombre que su página con sufijo `.razor.css`. Consulta `docs/GUIA_CODIGO.md` para orientarte en el código ya implementado.

## Flujo obligatorio

1. Comprueba que estás en una rama distinta de `main`. Si estás en `main`, crea `ejercicio-N-descripcion`; para varios usa `ejercicios-N-M`.
2. Lee el requisito completo en `docs/EJERCICIOS.md`.
3. Implementa o ajusta el algoritmo en la clase C# indicada, separado de la interfaz.
4. Implementa o ajusta la página con una interfaz funcional, responsiva y en español.
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

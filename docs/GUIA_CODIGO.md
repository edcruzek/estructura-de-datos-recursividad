# Guía breve del código

Cada actividad mantiene su número en el enunciado (`EJERCICIOS.md`) y en la dirección pública `/ejercicio-N`. Los nombres de archivos y clases describen el tema para que sea más fácil encontrarlos en GitHub. La interfaz de cada tema está en `src/RecursividadWeb/Pages/`, la lógica recursiva en `src/RecursividadWeb/Algorithms/` y sus pruebas en `tests/RecursividadWeb.Tests/`. Los archivos `.razor.css` contienen solo estilos de su página.

| Ejercicio y ruta | Página | Algoritmo | Pruebas |
|---|---|---|---|
| 1 · `/ejercicio-1` | `Factorial.razor` | `FactorialAlgorithm.cs` | `FactorialTests.cs` |
| 2 · `/ejercicio-2` | `Fibonacci.razor` | `FibonacciAlgorithm.cs` | `FibonacciTests.cs` |
| 3 · `/ejercicio-3` | `MaximoComunDivisor.razor` | `MaximoComunDivisorAlgorithm.cs` | `MaximoComunDivisorTests.cs` |
| 4 · `/ejercicio-4` | `CambioMonedas.razor` | `CambioMonedasAlgorithm.cs` | `CambioMonedasTests.cs` |
| 5 · `/ejercicio-5` | `TorresHanoi.razor` | `TorresHanoiAlgorithm.cs` | `TorresHanoiTests.cs` |

## Cómo leer cada solución

- **Factorial:** `Calcular` valida la entrada y llama a `CalcularRecursivo`, cuyo caso base es 0 o 1. `ObtenerPasos` registra los resultados cuando regresan las llamadas; `ObtenerExpresionMatematica` prepara la multiplicación que se muestra en la página.
- **Fibonacci:** `CalcularTerminoRecursivo` expresa la definición F(n) = F(n − 1) + F(n − 2). Para mostrar una serie, `GenerarSerieRecursivo` añade un término por llamada; `GenerarDetalleRecursivo` añade también la fórmula explicativa de cada posición.
- **Máximo común divisor:** `CalcularRecursivo` aplica el algoritmo de Euclides con el residuo hasta que el segundo número es cero. `AgregarPasos` guarda cada división para la explicación visual. Las entradas se convierten a valores absolutos antes del cálculo.
- **Cambio de monedas:** `CalcularCambio` valida importes y convierte el cambio a centavos. `ResolverRecursivo` minimiza el número de piezas, memoriza subproblemas y explora las combinaciones de centavos porque esas denominaciones no siempre admiten una elección codiciosa óptima. `ConstruirPasos` convierte la solución en una traza legible.
- **Torres de Hanói:** `Resolver` produce los movimientos y `MoverTorre` divide cada problema en mover n − 1 discos, mover el mayor y volver a mover n − 1. En la página, `ConstruirTorres` reconstruye el tablero para el paso seleccionado y `ConstruirArbol` muestra la jerarquía de llamadas. La reproducción automática solo recorre la solución ya calculada.

Las pruebas tienen el mismo nombre que su tema y cubren casos base, resultados esperados, límites y propiedades particulares de cada solución. Para verificar todo el proyecto ejecuta `dotnet test PracticaRecursividad.slnx` y `dotnet publish src/RecursividadWeb/RecursividadWeb.csproj -c Release`.

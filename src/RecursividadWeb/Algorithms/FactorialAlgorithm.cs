using System.Numerics;

namespace RecursividadWeb.Algorithms;

/// <summary>
/// Proporciona métodos recursivos para el cálculo del factorial y la inspección de su desarrollo.
/// </summary>
public static class FactorialAlgorithm
{
    /// <summary>Límite de entrada mostrado en la interfaz para mantener legibles los pasos.</summary>
    public const int MaximoNumero = 25;

    /// <summary>
    /// Calcula el factorial de un número n (n!) de forma recursiva.
    /// </summary>
    /// <param name="n">Número entero mayor o igual a 0 y menor o igual a MaximoNumero.</param>
    /// <returns>El valor del factorial como BigInteger.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Se lanza si n es menor que 0 o mayor que MaximoNumero.</exception>
    public static BigInteger Calcular(int n)
    {
        if (n < 0 || n > MaximoNumero)
        {
            throw new ArgumentOutOfRangeException(
                nameof(n),
                $"El número debe estar entre 0 y {MaximoNumero}.");
        }

        return CalcularRecursivo(n);
    }

    /// <summary>Aplica n! = n × (n - 1)! hasta llegar a 0 o 1.</summary>
    private static BigInteger CalcularRecursivo(int n)
    {
        // Caso base: 0! = 1 y 1! = 1
        if (n <= 1)
        {
            return BigInteger.One;
        }

        // Paso recursivo: n! = n * (n - 1)!
        return n * CalcularRecursivo(n - 1);
    }

    /// <summary>
    /// Obtiene el desglose de llamadas recursivas para visualizar el proceso de apilado y resolución.
    /// </summary>
    public static IReadOnlyList<PasoFactorial> ObtenerPasos(int n)
    {
        if (n < 0 || n > MaximoNumero)
        {
            throw new ArgumentOutOfRangeException(
                nameof(n),
                $"El número debe estar entre 0 y {MaximoNumero}.");
        }

        var pasos = new List<PasoFactorial>();
        ConstruirPasos(n, pasos);
        return pasos;
    }

    /// <summary>Registra cada resultado al regresar de las llamadas recursivas.</summary>
    private static BigInteger ConstruirPasos(int n, List<PasoFactorial> pasos)
    {
        if (n <= 1)
        {
            pasos.Add(new PasoFactorial(
                Numero: n,
                Tipo: "Caso base",
                Operacion: $"{n}! = 1",
                ResultadoParcial: BigInteger.One));
            return BigInteger.One;
        }

        var subresultado = ConstruirPasos(n - 1, pasos);
        var resultadoActual = n * subresultado;

        pasos.Add(new PasoFactorial(
            Numero: n,
            Tipo: "Paso recursivo",
            Operacion: $"{n}! = {n} × {n - 1}! = {n} × {subresultado:N0} = {resultadoActual:N0}",
            ResultadoParcial: resultadoActual));

        return resultadoActual;
    }

    /// <summary>
    /// Genera la representación matemática expandida del factorial (por ejemplo: "5 × 4 × 3 × 2 × 1 = 120").
    /// </summary>
    public static string ObtenerExpresionMatematica(int n)
    {
        if (n < 0 || n > MaximoNumero)
        {
            throw new ArgumentOutOfRangeException(
                nameof(n),
                $"El número debe estar entre 0 y {MaximoNumero}.");
        }

        if (n <= 1)
        {
            return $"{n}! = 1";
        }

        var factores = Enumerable.Range(1, n).Reverse().Select(x => x.ToString());
        var multiplicacion = string.Join(" × ", factores);
        var total = Calcular(n);

        return $"{n}! = {multiplicacion} = {total:N0}";
    }
}

/// <summary>Una fila del desglose visual del factorial.</summary>
public sealed record PasoFactorial(int Numero, string Tipo, string Operacion, BigInteger ResultadoParcial);

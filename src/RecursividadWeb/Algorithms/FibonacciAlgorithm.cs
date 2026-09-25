using System.Numerics;

namespace RecursividadWeb.Algorithms;

/// <summary>
/// Proporciona métodos recursivos para generar la serie de Fibonacci y calcular sus términos.
/// </summary>
public static class FibonacciAlgorithm
{
    /// <summary>Cantidad máxima de términos que admite la aplicación.</summary>
    public const int MaximoTerminos = 30;

    /// <summary>
    /// Calcula de forma recursiva el n-ésimo término de la serie de Fibonacci (F(n)),
    /// donde F(0) = 0, F(1) = 1, y F(n) = F(n - 1) + F(n - 2).
    /// </summary>
    /// <param name="n">Índice del término (base 0), entre 0 y MaximoTerminos.</param>
    /// <returns>El valor del n-ésimo término de Fibonacci.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Se lanza si n es menor que 0 o mayor que MaximoTerminos.</exception>
    public static BigInteger CalcularTermino(int n)
    {
        if (n < 0 || n > MaximoTerminos)
        {
            throw new ArgumentOutOfRangeException(
                nameof(n),
                $"El índice debe estar entre 0 y {MaximoTerminos}.");
        }

        return CalcularTerminoRecursivo(n);
    }

    /// <summary>Evalúa F(n) mediante sus dos llamadas recursivas y los casos base F(0) y F(1).</summary>
    private static BigInteger CalcularTerminoRecursivo(int n)
    {
        // Casos base: F(0) = 0, F(1) = 1
        if (n == 0) return BigInteger.Zero;
        if (n == 1) return BigInteger.One;

        // Paso recursivo: F(n) = F(n - 1) + F(n - 2)
        return CalcularTerminoRecursivo(n - 1) + CalcularTerminoRecursivo(n - 2);
    }

    /// <summary>
    /// Genera recursivamente la serie de Fibonacci compuesta por la cantidad especificada de términos.
    /// </summary>
    /// <param name="cantidad">Cantidad de términos a generar (mínimo 1, máximo MaximoTerminos).</param>
    /// <returns>Lista de valores que componen la serie generada.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Se lanza si la cantidad no está en el rango permitido.</exception>
    public static IReadOnlyList<BigInteger> GenerarSerie(int cantidad)
    {
        if (cantidad < 1 || cantidad > MaximoTerminos)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                $"La cantidad de términos debe estar entre 1 y {MaximoTerminos}.");
        }

        var serie = new List<BigInteger>(cantidad);
        GenerarSerieRecursivo(cantidad, serie);
        return serie;
    }

    /// <summary>Agrega un término por llamada hasta alcanzar la cantidad solicitada.</summary>
    private static void GenerarSerieRecursivo(int meta, List<BigInteger> serie)
    {
        // Caso base: se ha alcanzado la cantidad deseada de términos
        if (serie.Count >= meta)
        {
            return;
        }

        if (serie.Count == 0)
        {
            serie.Add(BigInteger.Zero);
        }
        else if (serie.Count == 1)
        {
            serie.Add(BigInteger.One);
        }
        else
        {
            // Paso recursivo: el nuevo elemento es la suma de los dos anteriores
            var nuevoTermino = serie[^1] + serie[^2];
            serie.Add(nuevoTermino);
        }

        // Llamada recursiva para el siguiente término
        GenerarSerieRecursivo(meta, serie);
    }

    /// <summary>
    /// Genera la serie de Fibonacci con detalles explicativos para cada término.
    /// </summary>
    public static IReadOnlyList<TerminoFibonacci> GenerarDetalleSerie(int cantidad)
    {
        if (cantidad < 1 || cantidad > MaximoTerminos)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                $"La cantidad de términos debe estar entre 1 y {MaximoTerminos}.");
        }

        var lista = new List<TerminoFibonacci>(cantidad);
        GenerarDetalleRecursivo(cantidad, lista);
        return lista;
    }

    /// <summary>Construye la misma secuencia junto con la fórmula que explica cada término.</summary>
    private static void GenerarDetalleRecursivo(int meta, List<TerminoFibonacci> lista)
    {
        if (lista.Count >= meta)
        {
            return;
        }

        var indice = lista.Count;
        if (indice == 0)
        {
            lista.Add(new TerminoFibonacci(
                Indice: 0,
                Etiqueta: "F(0)",
                Valor: BigInteger.Zero,
                Tipo: "Caso base",
                Formula: "F(0) = 0"));
        }
        else if (indice == 1)
        {
            lista.Add(new TerminoFibonacci(
                Indice: 1,
                Etiqueta: "F(1)",
                Valor: BigInteger.One,
                Tipo: "Caso base",
                Formula: "F(1) = 1"));
        }
        else
        {
            var ant1 = lista[indice - 1];
            var ant2 = lista[indice - 2];
            var valor = ant1.Valor + ant2.Valor;

            lista.Add(new TerminoFibonacci(
                Indice: indice,
                Etiqueta: $"F({indice})",
                Valor: valor,
                Tipo: "Paso recursivo",
                Formula: $"F({indice}) = F({indice - 1}) + F({indice - 2}) = {ant1.Valor:N0} + {ant2.Valor:N0} = {valor:N0}"));
        }

        GenerarDetalleRecursivo(meta, lista);
    }
}

/// <summary>Dato y explicación de un término para presentarlo en la página.</summary>
public sealed record TerminoFibonacci(
    int Indice,
    string Etiqueta,
    BigInteger Valor,
    string Tipo,
    string Formula);

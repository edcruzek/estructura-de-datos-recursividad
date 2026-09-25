namespace RecursividadWeb.Algorithms;

/// <summary>
/// Calcula el máximo común divisor mediante el algoritmo recursivo de Euclides.
/// </summary>
public static class MaximoComunDivisorAlgorithm
{
    /// <summary>Obtiene el MCD de dos enteros; acepta negativos y cero salvo el par (0, 0).</summary>
    public static long Calcular(int primerNumero, int segundoNumero)
    {
        var primero = Math.Abs((long)primerNumero);
        var segundo = Math.Abs((long)segundoNumero);

        Validar(primero, segundo);
        return CalcularRecursivo(primero, segundo);
    }

    /// <summary>Devuelve las divisiones sucesivas que permiten explicar el algoritmo de Euclides.</summary>
    public static IReadOnlyList<PasoMcd> ObtenerPasos(int primerNumero, int segundoNumero)
    {
        var primero = Math.Abs((long)primerNumero);
        var segundo = Math.Abs((long)segundoNumero);

        Validar(primero, segundo);

        var pasos = new List<PasoMcd>();
        AgregarPasos(primero, segundo, pasos);
        return pasos;
    }

    /// <summary>Repite MCD(a, b) = MCD(b, a mod b) hasta que b sea cero.</summary>
    private static long CalcularRecursivo(long primero, long segundo) =>
        segundo == 0 ? primero : CalcularRecursivo(segundo, primero % segundo);

    /// <summary>Registra cada residuo y la última llamada, que identifica el resultado.</summary>
    private static void AgregarPasos(long primero, long segundo, List<PasoMcd> pasos)
    {
        if (segundo == 0)
        {
            pasos.Add(new PasoMcd(primero, segundo, null));
            return;
        }

        var residuo = primero % segundo;
        pasos.Add(new PasoMcd(primero, segundo, residuo));
        AgregarPasos(segundo, residuo, pasos);
    }

    /// <summary>Rechaza el único par para el que el MCD no está definido.</summary>
    private static void Validar(long primero, long segundo)
    {
        if (primero == 0 && segundo == 0)
        {
            throw new ArgumentException("El MCD de 0 y 0 no está definido.");
        }
    }
}

/// <summary>Una división de Euclides; el residuo nulo señala el caso base.</summary>
public sealed record PasoMcd(long Primero, long Segundo, long? Residuo);

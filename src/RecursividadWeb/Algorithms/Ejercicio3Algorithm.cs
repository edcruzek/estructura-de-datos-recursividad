namespace RecursividadWeb.Algorithms;

/// <summary>
/// Calcula el máximo común divisor mediante el algoritmo recursivo de Euclides.
/// </summary>
public static class Ejercicio3Algorithm
{
    public static long Calcular(int primerNumero, int segundoNumero)
    {
        var primero = Math.Abs((long)primerNumero);
        var segundo = Math.Abs((long)segundoNumero);

        Validar(primero, segundo);
        return CalcularRecursivo(primero, segundo);
    }

    public static IReadOnlyList<PasoMcd> ObtenerPasos(int primerNumero, int segundoNumero)
    {
        var primero = Math.Abs((long)primerNumero);
        var segundo = Math.Abs((long)segundoNumero);

        Validar(primero, segundo);

        var pasos = new List<PasoMcd>();
        AgregarPasos(primero, segundo, pasos);
        return pasos;
    }

    private static long CalcularRecursivo(long primero, long segundo) =>
        segundo == 0 ? primero : CalcularRecursivo(segundo, primero % segundo);

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

    private static void Validar(long primero, long segundo)
    {
        if (primero == 0 && segundo == 0)
        {
            throw new ArgumentException("El MCD de 0 y 0 no está definido.");
        }
    }
}

public sealed record PasoMcd(long Primero, long Segundo, long? Residuo);

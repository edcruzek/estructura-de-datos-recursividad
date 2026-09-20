namespace RecursividadWeb.Algorithms;

/// <summary>
/// Resuelve las Torres de Hanói mediante el algoritmo recursivo clásico.
/// </summary>
public static class Ejercicio5Algorithm
{
    public const int MaximoDiscos = 12;

    public static IReadOnlyList<MovimientoHanoi> Resolver(int numeroDiscos)
    {
        if (numeroDiscos < 1 || numeroDiscos > MaximoDiscos)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numeroDiscos),
                $"El número de discos debe estar entre 1 y {MaximoDiscos}.");
        }

        var movimientos = new List<MovimientoHanoi>();
        MoverTorre(numeroDiscos, "Origen", "Auxiliar", "Destino", movimientos);
        return movimientos;
    }

    private static void MoverTorre(
        int discos,
        string origen,
        string auxiliar,
        string destino,
        List<MovimientoHanoi> movimientos)
    {
        if (discos == 1)
        {
            movimientos.Add(new MovimientoHanoi(
                movimientos.Count + 1,
                1,
                origen,
                destino));
            return;
        }

        MoverTorre(discos - 1, origen, destino, auxiliar, movimientos);

        movimientos.Add(new MovimientoHanoi(
            movimientos.Count + 1,
            discos,
            origen,
            destino));

        MoverTorre(discos - 1, auxiliar, origen, destino, movimientos);
    }
}

public sealed record MovimientoHanoi(int Numero, int Disco, string Desde, string Hacia);

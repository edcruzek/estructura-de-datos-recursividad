namespace RecursividadWeb.Algorithms;

/// <summary>
/// Resuelve las Torres de Hanói mediante el algoritmo recursivo clásico.
/// </summary>
public static class TorresHanoiAlgorithm
{
    /// <summary>Límite de discos para mantener manejable la lista de 2^n - 1 movimientos.</summary>
    public const int MaximoDiscos = 12;

    /// <summary>Genera, en orden, los movimientos para llevar todos los discos de Origen a Destino.</summary>
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

    /// <summary>
    /// Mueve n - 1 discos al auxiliar, el disco mayor al destino y los n - 1 restantes al destino.
    /// Con un solo disco, registra el movimiento directamente (caso base).
    /// </summary>
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

/// <summary>Movimiento numerado de un disco entre dos torres.</summary>
public sealed record MovimientoHanoi(int Numero, int Disco, string Desde, string Hacia);

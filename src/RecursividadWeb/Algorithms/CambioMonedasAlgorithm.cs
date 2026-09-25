namespace RecursividadWeb.Algorithms;

/// <summary>
/// Representa una de las denominaciones monetarias disponibles.
/// </summary>
public sealed record DenominacionMoneda(
    string Nombre,
    decimal ValorEnPesos,
    int ValorEnCentavos,
    bool EsPeso);

/// <summary>
/// Representa la cantidad devuelta de una denominación específica.
/// </summary>
public sealed record ItemCambio(
    DenominacionMoneda Denominacion,
    int Cantidad,
    decimal Subtotal);

/// <summary>
/// Representa un paso en el proceso recursivo de cálculo del cambio.
/// </summary>
public sealed record PasoRecursivoCambio(
    int Nivel,
    string Denominacion,
    int MonedasAsignadas,
    int CentavosAntes,
    int CentavosDespues,
    string Explicacion);

/// <summary>
/// Contiene el resultado completo de la operación de cambio comercial.
/// </summary>
public sealed record ResultadoCambio(
    decimal ImporteCompra,
    decimal CantidadPagada,
    decimal CambioTotal,
    int CambioEnCentavos,
    int TotalPiezas,
    IReadOnlyList<ItemCambio> Desglose,
    IReadOnlyList<PasoRecursivoCambio> Pasos);

/// <summary>
/// Resuelve el problema del cambio de monedas utilizando un algoritmo recursivo
/// que garantiza el mínimo número total de piezas devueltas.
/// </summary>
public static class CambioMonedasAlgorithm
{
    /// <summary>Máximo permitido para compra y pago.</summary>
    public const decimal MaximoMonto = 1_000_000m;

    /// <summary>
    /// Denominaciones oficiales disponibles ordenadas de mayor a menor:
    /// 100, 50, 20, 10, 5 y 1 peso; 50, 20 y 1 centavo.
    /// </summary>
    public static readonly IReadOnlyList<DenominacionMoneda> Denominaciones =
    [
        new("100 pesos", 100.00m, 10000, true),
        new("50 pesos", 50.00m, 5000, true),
        new("20 pesos", 20.00m, 2000, true),
        new("10 pesos", 10.00m, 1000, true),
        new("5 pesos", 5.00m, 500, true),
        new("1 peso", 1.00m, 100, true),
        new("50 centavos", 0.50m, 50, false),
        new("20 centavos", 0.20m, 20, false),
        new("1 centavo", 0.01m, 1, false)
    ];

    /// <summary>
    /// Calcula el desglose del cambio comercial utilizando recursividad
    /// para minimizar el total de piezas de monedas.
    /// </summary>
    public static ResultadoCambio CalcularCambio(decimal importeCompra, decimal cantidadPagada)
    {
        ValidarEntradas(importeCompra, cantidadPagada);

        var cambioTotal = cantidadPagada - importeCompra;
        var cambioEnCentavos = (int)Math.Round(cambioTotal * 100m, MidpointRounding.AwayFromZero);

        if (cambioEnCentavos == 0)
        {
            return new ResultadoCambio(
                importeCompra,
                cantidadPagada,
                0.00m,
                0,
                0,
                [],
                [new PasoRecursivoCambio(0, "Ninguna", 0, 0, 0, "Pago exacto: el cambio restante es $0.00 (caso base).")]);
        }

        var memo = new Dictionary<(int Centavos, int Indice), SolucionOptima>();
        var solucion = ResolverRecursivo(cambioEnCentavos, 0, memo);

        var desglose = new List<ItemCambio>();
        for (var i = 0; i < Denominaciones.Count; i++)
        {
            var cantidad = solucion.ConteoMonedas[i];
            if (cantidad > 0)
            {
                var denom = Denominaciones[i];
                desglose.Add(new ItemCambio(denom, cantidad, cantidad * denom.ValorEnPesos));
            }
        }

        var pasos = new List<PasoRecursivoCambio>();
        ConstruirPasos(cambioEnCentavos, 0, solucion.ConteoMonedas, 0, pasos);

        return new ResultadoCambio(
            importeCompra,
            cantidadPagada,
            cambioTotal,
            cambioEnCentavos,
            solucion.TotalPiezas,
            desglose,
            pasos);
    }

    /// <summary>Comprueba importes positivos, pago suficiente y precisión de centavos.</summary>
    private static void ValidarEntradas(decimal importeCompra, decimal cantidadPagada)
    {
        if (importeCompra <= 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(importeCompra),
                "El importe de la compra debe ser mayor que cero.");
        }

        if (importeCompra > MaximoMonto)
        {
            throw new ArgumentOutOfRangeException(
                nameof(importeCompra),
                $"El importe de la compra no puede superar ${MaximoMonto:N0} pesos.");
        }

        if (cantidadPagada > MaximoMonto)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidadPagada),
                $"La cantidad pagada no puede superar ${MaximoMonto:N0} pesos.");
        }

        if (cantidadPagada < importeCompra)
        {
            throw new ArgumentException(
                "La cantidad pagada debe ser mayor o igual al importe de la compra.",
                nameof(cantidadPagada));
        }

        if (TieneMasDeDosDecimales(importeCompra))
        {
            throw new ArgumentException(
                "El importe de la compra no puede tener más de dos cifras decimales (centavos).",
                nameof(importeCompra));
        }

        if (TieneMasDeDosDecimales(cantidadPagada))
        {
            throw new ArgumentException(
                "La cantidad pagada no puede tener más de dos cifras decimales (centavos).",
                nameof(cantidadPagada));
        }
    }

    /// <summary>Detecta fracciones menores que un centavo antes de convertir a enteros.</summary>
    private static bool TieneMasDeDosDecimales(decimal valor)
    {
        return decimal.Round(valor, 2) != valor;
    }

    /// <summary>
    /// Función recursiva que calcula el número mínimo de piezas para cubrir
    /// el monto en centavos a partir de la denominación indicada.
    /// </summary>
    private static SolucionOptima ResolverRecursivo(
        int centavosRestantes,
        int indiceDenominacion,
        Dictionary<(int Centavos, int Indice), SolucionOptima> memo)
    {
        // Caso base 1: no queda cambio por devolver
        if (centavosRestantes == 0)
        {
            return new SolucionOptima(0, new int[Denominaciones.Count]);
        }

        // Caso base 2: última denominación disponible (1 centavo)
        if (indiceDenominacion == Denominaciones.Count - 1)
        {
            var conteoBase = new int[Denominaciones.Count];
            conteoBase[indiceDenominacion] = centavosRestantes;
            return new SolucionOptima(centavosRestantes, conteoBase);
        }

        var clave = (centavosRestantes, indiceDenominacion);
        if (memo.TryGetValue(clave, out var resultadoMemorizado))
        {
            return resultadoMemorizado;
        }

        var valorDenominacion = Denominaciones[indiceDenominacion].ValorEnCentavos;

        // Para denominaciones de pesos (>= 100 centavos): el sistema {100, 50, 20, 10, 5, 1}
        // es canónico; la elección codiciosa de tomar el máximo número de monedas
        // es matemáticamente óptima y reduce el tamaño del problema de forma recursiva.
        if (valorDenominacion >= 100)
        {
            var monedas = centavosRestantes / valorDenominacion;
            var residuo = centavosRestantes % valorDenominacion;

            var subSolucion = ResolverRecursivo(residuo, indiceDenominacion + 1, memo);
            var conteo = (int[])subSolucion.ConteoMonedas.Clone();
            conteo[indiceDenominacion] = monedas;

            var optima = new SolucionOptima(subSolucion.TotalPiezas + monedas, conteo);
            memo[clave] = optima;
            return optima;
        }

        // Para denominaciones de centavos (50¢, 20¢, 1¢):
        // Al no existir denominación de 10¢, el sistema no es canónico (ej. 60¢ se compone
        // con tres monedas de 20¢ en lugar de una de 50¢ y diez de 1¢).
        // Exploramos recursivamente las opciones posibles para garantizar el mínimo global de piezas.
        SolucionOptima? mejor = null;
        var maxMonedas = centavosRestantes / valorDenominacion;

        for (var cantidad = maxMonedas; cantidad >= 0; cantidad--)
        {
            var residuo = centavosRestantes - (cantidad * valorDenominacion);
            var subSolucion = ResolverRecursivo(residuo, indiceDenominacion + 1, memo);
            var piezasTotales = subSolucion.TotalPiezas + cantidad;

            if (mejor is null || piezasTotales < mejor.TotalPiezas)
            {
                var conteo = (int[])subSolucion.ConteoMonedas.Clone();
                conteo[indiceDenominacion] = cantidad;
                mejor = new SolucionOptima(piezasTotales, conteo);
            }
        }

        memo[clave] = mejor!;
        return mejor!;
    }

    /// <summary>
    /// Construye la secuencia de pasos recursivos que explican cómo se descompone el cambio.
    /// </summary>
    private static void ConstruirPasos(
        int centavosRestantes,
        int indiceDenominacion,
        int[] conteoOptimo,
        int nivel,
        List<PasoRecursivoCambio> pasos)
    {
        if (centavosRestantes == 0)
        {
            pasos.Add(new PasoRecursivoCambio(
                nivel,
                "Finalización",
                0,
                0,
                0,
                "Caso base: Cambio restante cubierto por completo ($0.00)."));
            return;
        }

        if (indiceDenominacion >= Denominaciones.Count)
        {
            return;
        }

        var denom = Denominaciones[indiceDenominacion];
        var monedas = conteoOptimo[indiceDenominacion];
        var valorRestado = monedas * denom.ValorEnCentavos;
        var centavosDespues = centavosRestantes - valorRestado;

        string explicacion;
        if (monedas > 0)
        {
            var textoMoneda = monedas == 1 ? "1 pieza" : $"{monedas} piezas";
            explicacion = $"Llamada {nivel + 1}: Se toman {textoMoneda} de {denom.Nombre} (${valorRestado / 100m:F2}). " +
                          $"Resta por cubrir: ${centavosDespues / 100m:F2}.";
        }
        else
        {
            explicacion = $"Llamada {nivel + 1}: No se requieren piezas de {denom.Nombre}. Resta por cubrir: ${centavosDespues / 100m:F2}.";
        }

        pasos.Add(new PasoRecursivoCambio(
            nivel + 1,
            denom.Nombre,
            monedas,
            centavosRestantes,
            centavosDespues,
            explicacion));

        ConstruirPasos(centavosDespues, indiceDenominacion + 1, conteoOptimo, nivel + 1, pasos);
    }

    /// <summary>Resultado intermedio memorizado para un monto y una denominación.</summary>
    private sealed record SolucionOptima(int TotalPiezas, int[] ConteoMonedas);
}

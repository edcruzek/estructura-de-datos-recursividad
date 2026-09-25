using RecursividadWeb.Algorithms;

namespace RecursividadWeb.Tests;

/// <summary>Comprueba el cambio mínimo, casos no canónicos, validación y desglose.</summary>
public class CambioMonedasTests
{
    [Fact]
    public void EjemploDocumentacion_Compra73_26_Pago100_ProduceCambioYDesgloseCorrecto()
    {
        // Caso exacto del enunciado: compra de 73.26 pesos, pago de 100 pesos -> cambio 26.74 pesos
        var resultado = CambioMonedasAlgorithm.CalcularCambio(73.26m, 100.00m);

        Assert.Equal(26.74m, resultado.CambioTotal);
        Assert.Equal(2674, resultado.CambioEnCentavos);
        Assert.Equal(9, resultado.TotalPiezas);

        // Desglose: 1 de 20 pesos, 1 de 5 pesos, 1 de 1 peso, 1 de 50¢, 1 de 20¢ y 4 de 1¢
        Assert.Equal(6, resultado.Desglose.Count);

        var moneda20Pesos = Assert.Single(resultado.Desglose, i => i.Denominacion.Nombre == "20 pesos");
        Assert.Equal(1, moneda20Pesos.Cantidad);

        var moneda5Pesos = Assert.Single(resultado.Desglose, i => i.Denominacion.Nombre == "5 pesos");
        Assert.Equal(1, moneda5Pesos.Cantidad);

        var moneda1Peso = Assert.Single(resultado.Desglose, i => i.Denominacion.Nombre == "1 peso");
        Assert.Equal(1, moneda1Peso.Cantidad);

        var moneda50Centavos = Assert.Single(resultado.Desglose, i => i.Denominacion.Nombre == "50 centavos");
        Assert.Equal(1, moneda50Centavos.Cantidad);

        var moneda20Centavos = Assert.Single(resultado.Desglose, i => i.Denominacion.Nombre == "20 centavos");
        Assert.Equal(1, moneda20Centavos.Cantidad);

        var moneda1Centavo = Assert.Single(resultado.Desglose, i => i.Denominacion.Nombre == "1 centavo");
        Assert.Equal(4, moneda1Centavo.Cantidad);
    }

    [Fact]
    public void PagoExacto_DevuelveCeroPiezas()
    {
        var resultado = CambioMonedasAlgorithm.CalcularCambio(50.00m, 50.00m);

        Assert.Equal(0.00m, resultado.CambioTotal);
        Assert.Equal(0, resultado.CambioEnCentavos);
        Assert.Equal(0, resultado.TotalPiezas);
        Assert.Empty(resultado.Desglose);
    }

    [Fact]
    public void CasoBase_UnCentavoDeCambio_DevuelveUnaSolaPieza()
    {
        var resultado = CambioMonedasAlgorithm.CalcularCambio(0.99m, 1.00m);

        Assert.Equal(0.01m, resultado.CambioTotal);
        Assert.Equal(1, resultado.CambioEnCentavos);
        Assert.Equal(1, resultado.TotalPiezas);

        var item = Assert.Single(resultado.Desglose);
        Assert.Equal("1 centavo", item.Denominacion.Nombre);
        Assert.Equal(1, item.Cantidad);
    }

    [Fact]
    public void MinimoGlobalNoCanonico_SesentaCentavos_DevuelveTresMonedasDeVeinte()
    {
        // Con 60 centavos, un algoritmo codicioso daría 1 de 50¢ y 10 de 1¢ (11 monedas).
        // El algoritmo óptimo debe devolver 3 monedas de 20 centavos (3 piezas).
        var resultado = CambioMonedasAlgorithm.CalcularCambio(0.40m, 1.00m);

        Assert.Equal(0.60m, resultado.CambioTotal);
        Assert.Equal(3, resultado.TotalPiezas);

        var item = Assert.Single(resultado.Desglose);
        Assert.Equal("20 centavos", item.Denominacion.Nombre);
        Assert.Equal(3, item.Cantidad);
    }

    [Fact]
    public void MinimoGlobalNoCanonico_OchentaCentavos_DevuelveCuatroMonedasDeVeinte()
    {
        // Con 80 centavos, codicioso daría 50 + 20 + 10x1 = 12 monedas.
        // Óptimo devuelve 4 monedas de 20 centavos = 4 piezas.
        var resultado = CambioMonedasAlgorithm.CalcularCambio(0.20m, 1.00m);

        Assert.Equal(0.80m, resultado.CambioTotal);
        Assert.Equal(4, resultado.TotalPiezas);

        var item = Assert.Single(resultado.Desglose);
        Assert.Equal("20 centavos", item.Denominacion.Nombre);
        Assert.Equal(4, item.Cantidad);
    }

    [Theory]
    [InlineData(100.00, "100 pesos")]
    [InlineData(50.00, "50 pesos")]
    [InlineData(20.00, "20 pesos")]
    [InlineData(10.00, "10 pesos")]
    [InlineData(5.00, "5 pesos")]
    [InlineData(1.00, "1 peso")]
    [InlineData(0.50, "50 centavos")]
    [InlineData(0.20, "20 centavos")]
    [InlineData(0.01, "1 centavo")]
    public void CadaDenominacionIndividual_SeDesglosaEnUnaSolaPieza(double valorDouble, string nombreEsperado)
    {
        var valor = (decimal)valorDouble;
        var compra = 10.00m;
        var pago = compra + valor;

        var resultado = CambioMonedasAlgorithm.CalcularCambio(compra, pago);

        Assert.Equal(valor, resultado.CambioTotal);
        Assert.Equal(1, resultado.TotalPiezas);

        var item = Assert.Single(resultado.Desglose);
        Assert.Equal(nombreEsperado, item.Denominacion.Nombre);
        Assert.Equal(1, item.Cantidad);
    }

    [Fact]
    public void CambioGrande_CalculaCorrectamenteConMonedasDeCien()
    {
        var resultado = CambioMonedasAlgorithm.CalcularCambio(100.00m, 1000.00m);

        Assert.Equal(900.00m, resultado.CambioTotal);
        Assert.Equal(9, resultado.TotalPiezas);

        var item = Assert.Single(resultado.Desglose);
        Assert.Equal("100 pesos", item.Denominacion.Nombre);
        Assert.Equal(9, item.Cantidad);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15.5)]
    public void ImporteCompraInvalido_CeroONegativo_LanzaExcepcion(double importeInvalido)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => CambioMonedasAlgorithm.CalcularCambio((decimal)importeInvalido, 50.00m));
    }

    [Fact]
    public void CantidadPagadaMenorQueCompra_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(
            () => CambioMonedasAlgorithm.CalcularCambio(100.00m, 80.00m));
    }

    [Fact]
    public void MasDeDosDecimales_EnCompraOPago_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(
            () => CambioMonedasAlgorithm.CalcularCambio(12.345m, 50.00m));

        Assert.Throws<ArgumentException>(
            () => CambioMonedasAlgorithm.CalcularCambio(10.00m, 50.999m));
    }

    [Fact]
    public void MontoExcedeMaximoPermitido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => CambioMonedasAlgorithm.CalcularCambio(CambioMonedasAlgorithm.MaximoMonto + 1m, CambioMonedasAlgorithm.MaximoMonto + 10m));
    }

    [Theory]
    [InlineData(17.83, 50.00)]
    [InlineData(2.39, 10.00)]
    [InlineData(89.99, 100.00)]
    [InlineData(432.17, 500.00)]
    public void SumaDeSubtotales_CoincideConCambioTotal(double compraDouble, double pagoDouble)
    {
        var compra = (decimal)compraDouble;
        var pago = (decimal)pagoDouble;

        var resultado = CambioMonedasAlgorithm.CalcularCambio(compra, pago);

        var sumaSubtotales = resultado.Desglose.Sum(d => d.Subtotal);
        Assert.Equal(resultado.CambioTotal, sumaSubtotales);

        var sumaPiezas = resultado.Desglose.Sum(d => d.Cantidad);
        Assert.Equal(resultado.TotalPiezas, sumaPiezas);
    }

    [Fact]
    public void TrazaRecursiva_GeneraPasosConNivelesYExplicaciones()
    {
        var resultado = CambioMonedasAlgorithm.CalcularCambio(73.26m, 100.00m);

        Assert.NotEmpty(resultado.Pasos);
        Assert.Contains(resultado.Pasos, p => p.Denominacion == "20 pesos" && p.MonedasAsignadas == 1);
        Assert.Contains(resultado.Pasos, p => p.Denominacion == "5 pesos" && p.MonedasAsignadas == 1);
        Assert.Contains(resultado.Pasos, p => p.Denominacion == "1 centavo" && p.MonedasAsignadas == 4);
        Assert.Equal(0, resultado.Pasos[^1].CentavosDespues);
    }
}

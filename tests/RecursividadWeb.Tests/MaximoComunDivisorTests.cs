using RecursividadWeb.Algorithms;

namespace RecursividadWeb.Tests;

/// <summary>Comprueba el algoritmo de Euclides con signos, ceros, límites y traza.</summary>
public class MaximoComunDivisorTests
{
    [Fact]
    public void NumerosPositivos_CalculaElMcd()
    {
        Assert.Equal(6, MaximoComunDivisorAlgorithm.Calcular(48, 18));
    }

    [Theory]
    [InlineData(-48, 18, 6)]
    [InlineData(48, -18, 6)]
    [InlineData(-48, -18, 6)]
    public void NumerosNegativos_UsaSusValoresAbsolutos(int primero, int segundo, long esperado)
    {
        Assert.Equal(esperado, MaximoComunDivisorAlgorithm.Calcular(primero, segundo));
    }

    [Theory]
    [InlineData(0, 25, 25)]
    [InlineData(25, 0, 25)]
    public void UnNumeroCero_DevuelveElValorAbsolutoDelOtro(int primero, int segundo, long esperado)
    {
        Assert.Equal(esperado, MaximoComunDivisorAlgorithm.Calcular(primero, segundo));
    }

    [Fact]
    public void AmbosNumerosCero_ProduceUnErrorComprensible()
    {
        var error = Assert.Throws<ArgumentException>(() => MaximoComunDivisorAlgorithm.Calcular(0, 0));

        Assert.Contains("no está definido", error.Message);
    }

    [Fact]
    public void LimiteMinimoDeEntero_NoSeDesborda()
    {
        Assert.Equal(2_147_483_648L, MaximoComunDivisorAlgorithm.Calcular(int.MinValue, 0));
    }

    [Fact]
    public void ObtenerPasos_ConservaLaSecuenciaRecursivaDeEuclides()
    {
        var pasos = MaximoComunDivisorAlgorithm.ObtenerPasos(48, 18);

        Assert.Equal(
            [
                new PasoMcd(48, 18, 12),
                new PasoMcd(18, 12, 6),
                new PasoMcd(12, 6, 0),
                new PasoMcd(6, 0, null)
            ],
            pasos);
    }
}

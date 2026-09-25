using System.Numerics;
using RecursividadWeb.Algorithms;

namespace RecursividadWeb.Tests;

/// <summary>Comprueba casos base, valores habituales, límites y pasos del factorial.</summary>
public class FactorialTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void CasosBase_RetornanUno(int n, long esperado)
    {
        var resultado = FactorialAlgorithm.Calcular(n);

        Assert.Equal(new BigInteger(esperado), resultado);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(3, 6)]
    [InlineData(4, 24)]
    [InlineData(5, 120)]
    [InlineData(6, 720)]
    [InlineData(7, 5040)]
    [InlineData(10, 3628800)]
    [InlineData(12, 479001600)]
    public void CasosNormales_CalculanFactorialCorrecto(int n, long esperado)
    {
        var resultado = FactorialAlgorithm.Calcular(n);

        Assert.Equal(new BigInteger(esperado), resultado);
    }

    [Fact]
    public void LimiteMaximo_CalculaSinDesbordamiento()
    {
        // 25! = 15511210043330985984000000
        var esperado = BigInteger.Parse("15511210043330985984000000");

        var resultado = FactorialAlgorithm.Calcular(FactorialAlgorithm.MaximoNumero);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(FactorialAlgorithm.MaximoNumero + 1)]
    [InlineData(100)]
    public void EntradasFueraDeRango_LanzanExcepcion(int n)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FactorialAlgorithm.Calcular(n));
    }

    [Fact]
    public void ObtenerPasos_ContieneDesgloseAdecuado()
    {
        var pasos = FactorialAlgorithm.ObtenerPasos(4);

        Assert.Equal(4, pasos.Count);
        Assert.Equal("Caso base", pasos[0].Tipo);
        Assert.Equal(1, pasos[0].ResultadoParcial);
        Assert.Equal("Paso recursivo", pasos[^1].Tipo);
        Assert.Equal(24, pasos[^1].ResultadoParcial);
    }

    [Fact]
    public void ExpresionMatematica_GeneraFormatoEsperado()
    {
        var expresion = FactorialAlgorithm.ObtenerExpresionMatematica(5);

        Assert.Contains("5! = 5 × 4 × 3 × 2 × 1 = 120", expresion);
    }
}

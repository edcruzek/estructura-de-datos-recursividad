using System.Numerics;
using RecursividadWeb.Algorithms;

namespace RecursividadWeb.Tests;

/// <summary>Comprueba términos, serie, detalle explicativo y límites de Fibonacci.</summary>
public class FibonacciTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void CasosBase_TerminoRetornaValoresIniciales(int n, long esperado)
    {
        var resultado = FibonacciAlgorithm.CalcularTermino(n);

        Assert.Equal(new BigInteger(esperado), resultado);
    }

    [Fact]
    public void GenerarSerie_CasosBase_RetornaTerminosIniciales()
    {
        var serie1 = FibonacciAlgorithm.GenerarSerie(1);
        var serie2 = FibonacciAlgorithm.GenerarSerie(2);

        Assert.Equal([BigInteger.Zero], serie1);
        Assert.Equal([BigInteger.Zero, BigInteger.One], serie2);
    }

    [Theory]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    [InlineData(4, 3)]
    [InlineData(5, 5)]
    [InlineData(6, 8)]
    [InlineData(7, 13)]
    [InlineData(10, 55)]
    [InlineData(15, 610)]
    public void CasosNormales_CalculanTerminoCorrecto(int n, long esperado)
    {
        var resultado = FibonacciAlgorithm.CalcularTermino(n);

        Assert.Equal(new BigInteger(esperado), resultado);
    }

    [Fact]
    public void GenerarSerie_GeneraSecuenciaEsperada()
    {
        var serie = FibonacciAlgorithm.GenerarSerie(8);
        BigInteger[] esperado = [0, 1, 1, 2, 3, 5, 8, 13];

        Assert.Equal(esperado, serie);
    }

    [Fact]
    public void GenerarSerie_CumplePropiedadRecursivaEnTodosLosElementos()
    {
        const int cantidad = 20;
        var serie = FibonacciAlgorithm.GenerarSerie(cantidad);

        Assert.Equal(cantidad, serie.Count);

        for (int i = 2; i < serie.Count; i++)
        {
            Assert.Equal(serie[i - 1] + serie[i - 2], serie[i]);
        }
    }

    [Fact]
    public void LimiteMaximo_GeneraCantidadIndicada()
    {
        var serie = FibonacciAlgorithm.GenerarSerie(FibonacciAlgorithm.MaximoTerminos);

        Assert.Equal(FibonacciAlgorithm.MaximoTerminos, serie.Count);
        Assert.Equal(serie[^1], FibonacciAlgorithm.CalcularTermino(FibonacciAlgorithm.MaximoTerminos - 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(FibonacciAlgorithm.MaximoTerminos + 1)]
    public void GenerarSerie_CantidadFueraDeRango_LanzaExcepcion(int cantidad)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FibonacciAlgorithm.GenerarSerie(cantidad));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(FibonacciAlgorithm.MaximoTerminos + 1)]
    public void CalcularTermino_IndiceFueraDeRango_LanzaExcepcion(int n)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FibonacciAlgorithm.CalcularTermino(n));
    }

    [Fact]
    public void GenerarDetalleSerie_IncluyeInformacionDescriptiva()
    {
        var detalles = FibonacciAlgorithm.GenerarDetalleSerie(4);

        Assert.Equal(4, detalles.Count);
        Assert.Equal("F(0)", detalles[0].Etiqueta);
        Assert.Equal("Caso base", detalles[0].Tipo);
        Assert.Equal("F(3)", detalles[3].Etiqueta);
        Assert.Equal("Paso recursivo", detalles[3].Tipo);
        Assert.Equal(2, detalles[3].Valor);
    }
}

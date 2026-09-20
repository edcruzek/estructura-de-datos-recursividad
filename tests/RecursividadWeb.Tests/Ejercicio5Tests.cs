using RecursividadWeb.Algorithms;

namespace RecursividadWeb.Tests;

public class Ejercicio5Tests
{
    [Fact]
    public void UnDisco_GeneraUnMovimientoDirecto()
    {
        var movimientos = Ejercicio5Algorithm.Resolver(1);

        var movimiento = Assert.Single(movimientos);
        Assert.Equal(new MovimientoHanoi(1, 1, "Origen", "Destino"), movimiento);
    }

    [Theory]
    [InlineData(2, 3)]
    [InlineData(3, 7)]
    [InlineData(5, 31)]
    public void VariosDiscos_GeneranCantidadMinimaDeMovimientos(int discos, int esperados)
    {
        var movimientos = Ejercicio5Algorithm.Resolver(discos);

        Assert.Equal(esperados, movimientos.Count);
    }

    [Fact]
    public void TresDiscos_RespetaOrdenInicialYFinal()
    {
        var movimientos = Ejercicio5Algorithm.Resolver(3);

        Assert.Equal(new MovimientoHanoi(1, 1, "Origen", "Destino"), movimientos[0]);
        Assert.Equal(new MovimientoHanoi(4, 3, "Origen", "Destino"), movimientos[3]);
        Assert.Equal(new MovimientoHanoi(7, 1, "Origen", "Destino"), movimientos[^1]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(Ejercicio5Algorithm.MaximoDiscos + 1)]
    public void CantidadFueraDeRango_ProduceError(int discos)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Ejercicio5Algorithm.Resolver(discos));
    }

    [Fact]
    public void LimiteMaximo_ConservaLaCantidadEsperada()
    {
        var movimientos = Ejercicio5Algorithm.Resolver(Ejercicio5Algorithm.MaximoDiscos);

        Assert.Equal((1 << Ejercicio5Algorithm.MaximoDiscos) - 1, movimientos.Count);
    }
}

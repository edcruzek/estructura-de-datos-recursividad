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

    [Fact]
    public void CadaMovimiento_MantieneUnTableroValidoYTerminaEnDestino()
    {
        const int discos = 6;
        var movimientos = Ejercicio5Algorithm.Resolver(discos);
        var torres = new Dictionary<string, List<int>>
        {
            ["Origen"] = Enumerable.Range(1, discos).Reverse().ToList(),
            ["Auxiliar"] = [],
            ["Destino"] = []
        };

        foreach (var movimiento in movimientos)
        {
            Assert.Equal(movimiento.Disco, torres[movimiento.Desde][^1]);

            var destino = torres[movimiento.Hacia];
            Assert.True(destino.Count == 0 || destino[^1] > movimiento.Disco);

            torres[movimiento.Desde].RemoveAt(torres[movimiento.Desde].Count - 1);
            destino.Add(movimiento.Disco);
        }

        Assert.Empty(torres["Origen"]);
        Assert.Empty(torres["Auxiliar"]);
        Assert.Equal(Enumerable.Range(1, discos).Reverse(), torres["Destino"]);
    }
}

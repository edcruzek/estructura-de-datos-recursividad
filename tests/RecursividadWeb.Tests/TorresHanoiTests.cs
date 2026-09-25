using RecursividadWeb.Algorithms;

namespace RecursividadWeb.Tests;

/// <summary>Comprueba la cantidad, el orden y la legalidad de los movimientos de Hanói.</summary>
public class TorresHanoiTests
{
    [Fact]
    public void UnDisco_GeneraUnMovimientoDirecto()
    {
        var movimientos = TorresHanoiAlgorithm.Resolver(1);

        var movimiento = Assert.Single(movimientos);
        Assert.Equal(new MovimientoHanoi(1, 1, "Origen", "Destino"), movimiento);
    }

    [Theory]
    [InlineData(2, 3)]
    [InlineData(3, 7)]
    [InlineData(5, 31)]
    public void VariosDiscos_GeneranCantidadMinimaDeMovimientos(int discos, int esperados)
    {
        var movimientos = TorresHanoiAlgorithm.Resolver(discos);

        Assert.Equal(esperados, movimientos.Count);
    }

    [Fact]
    public void TresDiscos_RespetaOrdenInicialYFinal()
    {
        var movimientos = TorresHanoiAlgorithm.Resolver(3);

        Assert.Equal(new MovimientoHanoi(1, 1, "Origen", "Destino"), movimientos[0]);
        Assert.Equal(new MovimientoHanoi(4, 3, "Origen", "Destino"), movimientos[3]);
        Assert.Equal(new MovimientoHanoi(7, 1, "Origen", "Destino"), movimientos[^1]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(TorresHanoiAlgorithm.MaximoDiscos + 1)]
    public void CantidadFueraDeRango_ProduceError(int discos)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => TorresHanoiAlgorithm.Resolver(discos));
    }

    [Fact]
    public void LimiteMaximo_ConservaLaCantidadEsperada()
    {
        var movimientos = TorresHanoiAlgorithm.Resolver(TorresHanoiAlgorithm.MaximoDiscos);

        Assert.Equal((1 << TorresHanoiAlgorithm.MaximoDiscos) - 1, movimientos.Count);
    }

    [Fact]
    public void CadaMovimiento_MantieneUnTableroValidoYTerminaEnDestino()
    {
        const int discos = 6;
        var movimientos = TorresHanoiAlgorithm.Resolver(discos);
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

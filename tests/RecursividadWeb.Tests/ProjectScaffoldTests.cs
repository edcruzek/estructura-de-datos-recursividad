namespace RecursividadWeb.Tests;

public class ProjectScaffoldTests
{
    [Fact]
    public void ExerciseNumbers_AreExactlyOneThroughFive()
    {
        int[] exerciseNumbers = [1, 2, 3, 4, 5];

        Assert.Equal(Enumerable.Range(1, 5), exerciseNumbers);
    }
}

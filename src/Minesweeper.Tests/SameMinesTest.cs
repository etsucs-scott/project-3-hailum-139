namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class SameMinesTest
{
    [Fact]
    public void SameSeedAndSizeShouldProduceTheSameMinePlacements()
    {
        int seed = 123;
        int size = 8;
        var game1 = new Game(size, seed);
        var game2 = new Game(size, seed);

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                Assert.Equal(game1.GameBoard.Grid[r, c].IsMine, game2.GameBoard.Grid[r, c].IsMine);
            }
        }
    }
}

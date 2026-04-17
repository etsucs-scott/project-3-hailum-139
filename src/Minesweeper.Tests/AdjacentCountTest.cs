namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class AdjacentCountTest
{
    [Fact]
    public void Cell00ShouldHaveExactlyOneAdjacentMineWhenYouUseSeed123()
    {
        var game = new Game(8, 123);
        var cell = game.GameBoard.Grid[0, 0];
        Assert.Equal(1, cell.AdjacentMines);
    }
}

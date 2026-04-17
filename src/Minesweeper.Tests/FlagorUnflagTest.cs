namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class FlagorUnflagTest
{
    [Fact]
    public void FlagorUnflagShouldSwitchStatusAfterEachUse()
    {
        var game = new Game(8, 123);
        game.GameBoard.FlagorUnflag(0, 0);
        Assert.True(game.GameBoard.Grid[0, 0].IsFlagged);
        game.GameBoard.FlagorUnflag(0, 0);
        Assert.False(game.GameBoard.Grid[0, 0].IsFlagged);
    }
}

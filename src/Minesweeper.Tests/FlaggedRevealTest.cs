namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class FlaggedRevealTest
{
    [Fact]
    public void FlaggedCellShouldNotBeRevealed()
    {
        var game = new Game(8, 123);
        game.GameBoard.FlagorUnflag(0, 0);
        game.GameBoard.Reveal(0, 0);
        Assert.False(game.GameBoard.Grid[0, 0].IsRevealed);
    }
}

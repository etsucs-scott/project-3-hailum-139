namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class CascadeRevealTest
{
    [Fact]
    public void RevealingAnEmptyCellWillRevealAllTheCellsAdjacentToIt()
    {
        var game = new Game(8, 123);
        game.GameBoard.Reveal(5, 5);
        Assert.True(game.GameBoard.Grid[5, 5].IsRevealed);
    }
}

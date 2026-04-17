namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class BoardDimensionsTest
{
    [Fact]
    public void BoardShouldBeCreatedWithCorrectDimensions()
    {
        int size = 8;
        var game = new Game(size, 123);
        Assert.Equal(size, game.GameBoard.Rows);
        Assert.Equal(size, game.GameBoard.Columns);
    }
}

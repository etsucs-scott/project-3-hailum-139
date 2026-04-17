namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class InitialWinTest
{
    [Fact]
    public void NewGameShouldNotBeWonImmediately()
    {
        var game = new Game(8, 123);
        Assert.False(game.GameBoard.GameWon());
    }
}

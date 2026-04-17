namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class MoveCountTest
{
    [Fact]
    public void EveryMoveMadeShouldIncreaseTheMoveCount()
    {
        var game = new Game(8, 123);
        game.MoveMade("r", 1, 1);
        game.MoveMade("f", 1, 1);
        Assert.Equal(2, game.MoveCount);
    }

}

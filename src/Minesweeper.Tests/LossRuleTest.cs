namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class LossRuleTest
{
    [Fact]
    public void RevealingAMineMeansYouHitAMine()
    {
        var game = new Game(8, 123);
        int r = 0;
        int c = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (game.GameBoard.Grid[i, j].IsMine)
                {
                    r = i;
                    c = j;
                }
            }
        }
        game.GameBoard.Reveal(r, c);
        Assert.True(game.GameBoard.HitMine);
    }
}

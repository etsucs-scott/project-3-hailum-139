namespace Minesweeper.Tests;
using Xunit;
using Minesweeper.Core;

public class WinRulesTest
{
    [Fact]
    public void RevealingAllSafeCellsMeansGameIsWon()
    {
        var game = new Game(8, 123);
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                if (!game.GameBoard.Grid[r, c].IsMine)
                {
                    game.GameBoard.Reveal(r, c);
                }
            }
        }
        Assert.True(game.GameBoard.GameWon());
    }
}

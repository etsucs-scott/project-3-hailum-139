namespace Minesweeper.Core;

public class Cell
{
    public bool IsMine;
    public int AdjacentMines;
    public bool IsRevealed;
    public bool IsFlagged;

    public Cell()
    { 
        IsMine = false;
        AdjacentMines = 0;
        IsRevealed = false;
        IsFlagged = false;
    }
}

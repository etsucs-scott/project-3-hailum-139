namespace Minesweeper.Core;

public class Board
{
    public Cell[,] Grid;
    public int Rows;
    public int Columns;
    public int NumberOfMines;
    public int Seed;
    public bool HitMine;

    public Board(int size, int seed)
    { 
        Rows = size;
        Columns = size;
        //number of mines depend on the size 
        if (size == 8) this.NumberOfMines = 10;
        else if (size == 12) this.NumberOfMines = 25;
        else if (size == 16) this.NumberOfMines = 40;

        Seed = seed;
        HitMine = false;
        this.Grid = new Cell[Rows, Columns];
    }

    public void CreateGrid()//method to create the two dimesional array that holds the cells
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                Grid[r, c] = new Cell();
            }
        }
    }

    public void PlaceMines()//method for placing the mines on the board that depends on the seed you chose so you can always recreate the game
    {
        Random rand = new Random(Seed);
        int placedmines = 0;

        while (placedmines < NumberOfMines)
        { 
            int r = rand.Next(Rows);
            int c = rand.Next(Columns);

            if (!Grid[r, c].IsMine)
            {
                Grid[r,c].IsMine = true;
                placedmines++;
            }
        }
    }

    public bool CellExists(int r, int c) // checks to see if a cell exists
    {
        if (r >= 0 && r < Rows && c >= 0 && c < Columns) // the only cells that exist in the grid are those that have positions that are within these restrictions
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    public void CalculateAdjacentMines()//method used to calculate of how many adjacent mines are next to a cell 
    { 
        for (int r = 0; r < Rows; r++)//for every row
        {
            for (int c = 0; c < Columns; c++)// for every column in that row
            {
                if (Grid[r, c].IsMine) continue;//you dont do this for mines

                int count = 0;

                for (int i = -1; i <= 1; i++)//to get adjacent rows coordinates you add these three numbers to the current row 
                {
                    for (int j = -1; j <= 1; j++)// the same for columns
                    { 
                        int nextrow = r + i;
                        int nextcolumn = c + j;

                        if (CellExists(nextrow, nextcolumn) && Grid[nextrow, nextcolumn].IsMine) count++;//if a cell exists and an adjacent cell is a mine add 1 to the count
                    }
                }
                Grid[r,c].AdjacentMines = count;
            }
        }
    }

    public void Reveal(int r, int c)// method to reveal what the cell underneath # actually is 
    {

        if (!CellExists(r, c) || Grid[r, c].IsRevealed || Grid[r, c].IsFlagged)//this method should immediately return without doing anythng if a cell doesn't exist, if it is already revealed, or if it is flagged
        {
            return;
        }
        Grid[r,c].IsRevealed = true;

        if (Grid[r, c].IsMine)
        { 
            HitMine = true;
            return;
        }

        if (Grid[r, c].AdjacentMines == 0)//if the adjacent mine count of the cell we chose to reveal is 0 then keep revealing all its adjacent cells and keep doing this if the adjacent cells dont have adjacent mines either
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    Reveal(r + i, c + j);
                }
            }
        }
    }

    public void FlagorUnflag(int r, int c) // this method is used to either flag an unflagged cell or unflagg a flagged cell
    {
        if (CellExists(r, c) && !Grid[r, c].IsRevealed) // this if statement is to ensure that the cell exists and is not already revealed because we can only put flags on existing, unrevealed cells
        {
            Grid[r, c].IsFlagged = !Grid[r, c].IsFlagged;
        }
    }

    public bool GameWon() // method to find out if the game is won or not
    {
        int revealedCells = 0;

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (Grid[r, c].IsRevealed) revealedCells++; // for each tile that is revealed the revealed cell count is increased
            }
        }

        if (revealedCells == (Rows * Columns) - NumberOfMines) // game is won when the number of cells that are revealed is the same as the total number of cells minus the amount of mines there are
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

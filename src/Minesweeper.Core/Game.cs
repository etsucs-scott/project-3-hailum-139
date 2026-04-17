
using System.Diagnostics;
namespace Minesweeper.Core;

public class Game
{
    public Board GameBoard;
    public HighScore HighScoreManager;
    public int MoveCount;
    public Stopwatch Timer;

    public Game(int size, int? seed)
    {
        int finalSeed = seed ?? (int)DateTime.Now.Ticks;

        GameBoard = new Board(size, finalSeed);
        this.GameBoard.CreateGrid();
        this.GameBoard.PlaceMines();
        this.GameBoard.CalculateAdjacentMines();

        HighScoreManager = new HighScore();
        MoveCount = 0;
        Timer = new Stopwatch();
    }
    public void StartTimer()//method to start timer for encapsulation of the Timer variable
    { 
        this.Timer.Start();
    }
    public void StopTimer()//do the same thing for stoping the timer
    {
        this.Timer.Stop();
    }
    public void MoveMade(string key, int row, int column)//method we use to actually reveal after receiving an appropriate command
    {
        if (key == "r")
        {
            this.GameBoard.Reveal(row, column);
            this.MoveCount++;
        }
        else if (key == "f")
        {
            this.GameBoard.FlagorUnflag(row, column);
            this.MoveCount++;
        }
    }
    public int GetSeconds()//method to return the number of seconds it took to complete a game
    { 
        int seconds = (int)this.Timer.Elapsed.TotalSeconds;
        return seconds;
    }
    public void SaveScore()
    {
        this.Timer.Stop(); //this command stops the timer
        if (this.GameBoard.GameWon())// and this statement saves the score if the gamewon requirement is fulfilled
        {
            Score finalscore = new Score(this.GameBoard.Rows, GetSeconds(), MoveCount, this.GameBoard.Seed);
            HighScoreManager.AddScore(finalscore); 
        }
    }

    public bool IsGameOver()
    {
        // It checks if you hit a mine OR if you've cleared all safe cells
        return this.GameBoard.HitMine || this.GameBoard.GameWon();
    }

    public void Play()
    {
        StartTimer();

        while (!IsGameOver())//keep playing as long as game isnt over
        {
            
            PrepareBoard();
            Console.WriteLine($"Moves: {MoveCount} | Time: {GetSeconds()}s");
            Console.Write("Commands: r row col / f row col / q ");

            string input = Console.ReadLine()?.ToLower();//this is to make sure the letters are turned to lower case if you accidently use capital letters
            if (input == "q") return;

            try
            {
                string[] parts = input.Split(' ');// splits the input we got into an array 
                string cmd = parts[0];
                int r = int.Parse(parts[1]);
                int c = int.Parse(parts[2]);

                if(cmd == "r" || cmd == "f")
                {
                    MoveMade(cmd, r, c);//uses the instances of the array to feed the appropriate input into the MoveMade command
                }
                
            }
            catch
            {
                Console.WriteLine("Invalid input! Use correct command format: ");
            }
        }

        EndGame();
    }

    public void PrepareBoard()//method to print out the actual look of the board
    {
       
        Console.Write("   "); // gives inital space so the row and column headers are correctly aligned to the rows they're representing

        for (int i = 0; i < this.GameBoard.Columns; i++) Console.Write(i.ToString().PadRight(3));//prints the column index pads it with 3 spaces so it aligns properly
        Console.WriteLine();

        for (int r = 0; r < this.GameBoard.Rows; r++)//for every row in the board
        {
            Console.Write(r.ToString().PadRight(3));//prints the row header

            for (int c = 0; c < this.GameBoard.Columns; c++)// for every column in the row we are right now
            {
                var cell = this.GameBoard.Grid[r, c];
                char symbol = '#'; //all cells are given the initial unrevealed symbol

                if (cell.IsFlagged) symbol = 'f';

                else if (cell.IsRevealed)
                {
                    if (cell.IsMine) symbol = 'b';
                    else if (cell.AdjacentMines == 0) symbol = '.';
                    else symbol = cell.AdjacentMines.ToString()[0];
                }
                Console.Write(symbol.ToString().PadRight(3));
            }
            Console.WriteLine();
        }
    }

    public void ShowHighScores()//method to retreive the scores from the by using the getscores method and then print them out 
    {
        var scores = HighScoreManager.GetScores();
        
        Console.WriteLine("=== TOP 5 SCORES PER SIZE ===");
        Console.WriteLine("Size | Time | Moves | Date");
        Console.WriteLine("---------------------------");

        foreach (var s in scores.OrderBy(x => x.Size).ThenBy(x => x.Seconds))
        {
            Console.WriteLine($"{s.Size}x{s.Size} | {s.Seconds}s | {s.Moves} | {s.Timestamp}");
        }
        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    public void EndGame()//method to show what happenns when game is over
    {
        StopTimer();
        
        PrepareBoard();//shows what the final board looks like

        if (this.GameBoard.GameWon())
        {
            Console.WriteLine($"\nWINNER! Time: {GetSeconds()}s | Moves: {MoveCount}");
     
            Score finalScore = new Score(this.GameBoard.Rows, GetSeconds(), MoveCount, this.GameBoard.Seed);
            HighScoreManager.AddScore(finalScore);
        }
        else
        {
            Console.WriteLine("\nBOOM! You hit a mine. Game Over.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

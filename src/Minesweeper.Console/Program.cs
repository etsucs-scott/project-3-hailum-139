using Minesweeper.Core;

while (true)
{
    
    Console.WriteLine("================================");
    Console.WriteLine("       MINESWEEPER 2026         ");
    Console.WriteLine("================================");
    Console.WriteLine("1. Play New Game");
    Console.WriteLine("2. View High Scores");
    Console.WriteLine("3. Exit");
    Console.WriteLine("================================");
    Console.Write("Select an option: ");

    string choice = Console.ReadLine();

    if (choice == "3") break;

    if (choice == "2")
    {
        // We create a temporary game instance just to call the High Score display
        Game scoreViewer = new Game(8, null);
        scoreViewer.ShowHighScores();
    }
    else if (choice == "1")
    {
        int size = 0;
        bool allowedsize = false;

        while (!allowedsize)
        {

            Console.WriteLine("Menu:"); // give the options for the board sizes
            Console.WriteLine("1) 8x8 ");
            Console.WriteLine("2) 12x12 ");
            Console.WriteLine("3) 16x16 ");

            string input = Console.ReadLine();

            if (input == "1" || input == "2" || input == "3")//ensures that the correct option is picked
            {
                if (input == "1")
                { size = 8; }
                else if (input == "2")
                { size = 12; }
                else
                { size = 16; }
               
                allowedsize = true;
            }
            else 
            {
                Console.WriteLine("Invalid Choice. Choose between 1, 2 or 3 according to the size you want.");
            }
        }

        Console.Write("Seed (blank = time): ");// asks for the seed input
        string seedInput = Console.ReadLine();
        int? seed = string.IsNullOrWhiteSpace(seedInput) ? null : int.Parse(seedInput);//if the question is left unanswered the seed is left as a null value

        
        Game activeGame = new Game(size, seed);//using the size and seed we just got from the user we start a new game
        activeGame.Play(); 
    }
}

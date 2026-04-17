namespace Minesweeper.Core;

public class Score
{
    public int Size;
    public int Seconds;
    public int Moves;
    public int Seed;
    public string Timestamp;

    public Score(int size, int seconds, int moves, int seed)
    {
        Size = size;
        Seconds = seconds;
        Moves = moves;
        Seed = seed;
        Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// when the score is first inputted we get the exact time that happens
    }

    public Score(int size, int seconds, int moves, int seed, string timestamp )
    {
        Size = size;
        Seconds = seconds;
        Moves = moves;
        Seed = seed;
        Timestamp = timestamp;
    }
    public string ToCSV()//method to return the score information which are stored as variables in the score class in CSV format
    {
        return $"{Size},{Seconds},{Moves},{Seed},{Timestamp}";
    }
}

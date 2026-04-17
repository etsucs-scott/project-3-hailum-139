namespace Minesweeper.Core;

public class HighScore
{
    public string FilePath = "data/highscores.csv";
    public List<Score> GetScores()// method that returns the scores stored 
    {
        List<Score> scores = new List<Score>();
        try
        {
            if (!File.Exists(FilePath)) return scores;//if file doesnt exist it just returns an empty string

            string[] lines = File.ReadAllLines(FilePath);
            for (int i = 1; i < lines.Length; i++) // i starts at 1 because the index 0 for the lines array represents the header of the csv which we wont use
            {
                string[] words = lines[i].Split(',');//we split the input we got into an array that each index can function as a parameter for creating a new score instance
                scores.Add(new Score(int.Parse(words[0]), int.Parse(words[1]), int.Parse(words[2]), int.Parse(words[3]), words[4]));
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine($"Could not get scores.{ex.Message}");
        }
        return scores;
    }
    public void AddScore(Score newscore)//method for adding scores into the already established list
    {
        try
        {
            List<Score> scores = GetScores();
            scores.Add(newscore);

            scores.Sort((a, b) =>//compares the scores on the list 
            {
                int timeComparison = a.Seconds.CompareTo(b.Seconds);
                if (timeComparison == 0) return a.Moves.CompareTo(b.Moves);
                return timeComparison;
            });
            if (!Directory.Exists("data")) Directory.CreateDirectory("data");//if the data directory doesnt exist then create one

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.WriteLine("size,seconds,moves,seed,timestamp");
                //initialize the counts of scores for each size
                int count8 = 0;
                int count12 = 0;
                int count16 = 0;

                foreach (var s in scores)
                {
                    if (s.Size == 8 && count8 < 5)//picks the top 5
                    {
                        sw.WriteLine($"{s.Size},{s.Seconds},{s.Moves},{s.Seed},{s.Timestamp}");
                        count8++;
                    }
                    else if (s.Size == 12 && count12 < 5)
                    {
                        sw.WriteLine($"{s.Size},{s.Seconds},{s.Moves},{s.Seed},{s.Timestamp}");
                        count12++;
                    }
                    else if (s.Size == 16 && count16 < 5)
                    {
                        sw.WriteLine($"{s.Size},{s.Seconds},{s.Moves},{s.Seed},{s.Timestamp}");
                        count16++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not add new score.{ ex.Message}");
        }
    }
}

namespace AutomatedCleaning.Cleaner;

public static class VisitedPoints
{
    public static void RecordVisitedPoints(string commands, FinalInformation finalInformation, int x, int y)
    {
        switch (commands)
        {
            case "A":
                finalInformation.Visited.Add(new Coordinates(x, y));
                break;

            case "B":
                finalInformation.Visited.Add(new Coordinates(x, y));
                break;

            case "C":
                finalInformation.Cleaned.Add(new Coordinates(x, y));
                break;
        }
    }
}
namespace AutomatedCleaning.Cleaner;

public static class WorkRobot
{
    public static CleanerCoordinates GetNewCoordinate(
        CleanerCoordinates cleanerCoordinates,
        string commands)
    {
        var middleCoord = new CleanerCoordinates(cleanerCoordinates.Facing, cleanerCoordinates.X, cleanerCoordinates.Y);

        switch (commands)
        {
            case "A":
                middleCoord.StepForward();
                break;

            case "B":
                middleCoord.StepBackward();
                break;

            case "TR":
                middleCoord.TernRight();
                break;

            case "TL":
                middleCoord.TernLeft();
                break;

            case "C":
                break;
        }

        return middleCoord;
    }
}
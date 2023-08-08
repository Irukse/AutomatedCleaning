namespace AutomatedCleaning.Cleaner;

public static class WorkBattery
{
    public static int GetWorkCapacityBattery(string commands) =>
        commands switch
        {
            "A" => -2,
            "B" => -3,
            "TR" => -1,
            "TL" => -1,
            "C" => -5,
            _ => 0
        };
}
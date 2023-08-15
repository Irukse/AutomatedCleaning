using System.Collections.Generic;
using System.Linq;
using AutomatedCleaning.Cleaner.Log;

namespace AutomatedCleaning.Cleaner;

public static class WayOutOfADeadEnd
{
    private static int _countNext = 0;

    public static FinalInformation GetRetreatStrategyCoordinates(
        FinalInformation finalInformation,
        string[,] map,
        string[][] strategy,
        List<Coordinates> visited,
        List<Coordinates> cleaned)
    {
        for (var i = _countNext; i < strategy.Length; i++)
        {
            _countNext++;

            var firstVisited = finalInformation.Visited.Count;

            Logger.WriteLog("strategy", strategy[i]);

            var startInfo =
                InitialStartInformation(finalInformation, map, strategy[i].ToList(), visited, cleaned);
            finalInformation = Robot.GetClean(startInfo);

            var secondVisited = finalInformation.Visited.Count;
            if (firstVisited < secondVisited) break;
        }

        _countNext = 0;
        return finalInformation;
    }

    private static StartInformation InitialStartInformation(
        FinalInformation finalInformation,
        string[,] map,
        List<string> strategy,
        List<Coordinates> visited,
        List<Coordinates> cleaned)
    {
        var cleanerCoordinates = new CleanerCoordinates()
        {
            Facing = finalInformation.FinalCoordinates.Facing,
            X = finalInformation.FinalCoordinates.X,
            Y = finalInformation.FinalCoordinates.Y,
        };

        var command = strategy;

        var startInfo =
            new StartInformation(
                map,
                cleanerCoordinates,
                command,
                finalInformation.FinalBattary,
                finalInformation.Visited,
                finalInformation.Cleaned);

        return startInfo;
    }
}
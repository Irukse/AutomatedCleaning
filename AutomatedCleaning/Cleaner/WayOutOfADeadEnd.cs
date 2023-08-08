using System.Collections.Generic;
using AutomatedCleaning.Cleaner.Log;

namespace AutomatedCleaning.Cleaner;

public static class WayOutOfADeadEnd
{
    private static int _countNext;

    public static FinalInformation GetRetreatStrategyCoordinates(
        FinalInformation finalInformation,
        string[,] map,
        string[][] strategy)
    {
        var flag = false;
        for (var i = 0; i < strategy.Length; i++)
        {
            // выход из стратегии если робот застрял

            for (var j = 0; j < strategy[i].Length; j++)
            {
                if (_countNext != j)
                {
                    _countNext = 0;
                    break;
                }

                _countNext++;

                var firstVisited = finalInformation.Visited.Count;
                var strategyList = new List<string> { strategy[i][j] };

                Logger.WriteLog("strategy", strategy[i][j]);

                var startInfo =
                    InitialStartInformation(finalInformation, map, strategyList);

                finalInformation = Robot.GetClean(startInfo);

                var secondVisited = finalInformation.Visited.Count;

                if (firstVisited == secondVisited) continue;
                flag = true;
                break;
            }

            if (flag) break;
        }

        return finalInformation;
    }

    private static StartInformation InitialStartInformation(
        FinalInformation finalInformation,
        string[,] map,
        List<string> strategy)
    {
        var cleanerCoordinates = new CleanerCoordinates()
        {
            Facing = finalInformation.FinalCoordinates.Facing,
            X = finalInformation.FinalCoordinates.X,
            Y = finalInformation.FinalCoordinates.Y,
        };

        var command = strategy;

        var startInfo =
            new StartInformation(map, cleanerCoordinates, command, finalInformation.FinalBattary);

        return startInfo;
    }
}
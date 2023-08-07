using System.Collections.Generic;
using AutomatedCleaning.Cleaner.Log;

namespace AutomatedCleaning.Cleaner;

public static class WayOutOfADeadEnd
{
    private static int _countNext;
    private static int count = 0;

    public static  FinalInformation GetRetreatStrategyCoordinates(
        FinalInformation finalInformation,
        string[,] map,
        string[][] strategy)
    {
        var flag = false;
       // var startInfo = new StartInformation();

        for (var i = 0; i < strategy.Length; i++)
        {
            // if (count != i)
            // {
            //     return finalInformation;
            // }
            //
            // count++;
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
                var secondVisited  = finalInformation.Visited.Count;

                //var robotWork = Robot.GetClean(startInfo);
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
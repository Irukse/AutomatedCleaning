using System.Collections.Generic;
using AutomatedCleaning.Cleaner.Log;

namespace AutomatedCleaning.Cleaner;

public static class Robot
{
    private static int _capacityBattery;

    public static FinalInformation GetClean(StartInformation startInformation)
    {
        var finalInformation = new FinalInformation
        {
            Visited = new List<Coordinates>(),
            Cleaned = new List<Coordinates>(),
            FinalCoordinates = new CleanerCoordinates(
                startInformation.Start.Facing,
                startInformation.Start.X,
                startInformation.Start.Y),
            FinalBattary = startInformation.Battery
        };

        finalInformation.Visited.Add(new Coordinates(startInformation.Start.X, startInformation.Start.Y));

        foreach (var command in startInformation.Commands)
        {
            _capacityBattery =
                GetCapacityBattery(finalInformation.FinalBattary, WorkBattery.GetWorkCapacityBattery(command));
            
            
            
            if (_capacityBattery < 0)
            {
                return finalInformation;
            }

            var robotStep = WorkRobot.GetNewCoordinate(finalInformation.FinalCoordinates, command);

            var mapInfo = PointOnMap.CheckPointOnMap(robotStep.X, robotStep.Y, startInformation.Map);

            if (mapInfo.Equals("C") || mapInfo.Equals("0"))
            {
                finalInformation = WayOutOfADeadEnd.GetRetreatStrategyCoordinates(
                    finalInformation,
                    startInformation.Map,
                    RetreatStrategyCase.Strategy);
                
               VisitedPoints.RecordVisitedPoints(command, finalInformation, finalInformation.FinalCoordinates.X, finalInformation.FinalCoordinates.Y);
    
                continue;
            }

            finalInformation.FinalCoordinates = robotStep;

            finalInformation.FinalBattary = _capacityBattery;

            VisitedPoints.RecordVisitedPoints(command, finalInformation, robotStep.X, robotStep.Y);
            Logger.WriteLog("command", command);
        }

        return (finalInformation);
    }

    private static int GetCapacityBattery(int first, int second)
    {
        return first + second;
    }
}
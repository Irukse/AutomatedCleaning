using AutomatedCleaning.Cleaner.Log;

namespace AutomatedCleaning.Cleaner;

public static class Robot
{
    private static int _capacityBattery;

    public static FinalInformation GetClean(StartInformation startInformation)
    {
        var finalInformation = new FinalInformation
        {
            Visited = startInformation.Visited,
            Cleaned = startInformation.Cleaned,
            FinalCoordinates = new CleanerCoordinates(
                startInformation.Start.Facing,
                startInformation.Start.X,
                startInformation.Start.Y),
            FinalBattary = startInformation.Battery
        };

        foreach (var command in startInformation.Commands)
        {
            Logger.WriteLog("command", command);
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
                    RetreatStrategyCase.Strategy,
                    finalInformation.Visited,
                    finalInformation.Cleaned);

                continue;
            }

            finalInformation.FinalCoordinates = robotStep;

            finalInformation.FinalBattary = _capacityBattery;

            VisitedPoints.RecordVisitedPoints(command, finalInformation, robotStep.X, robotStep.Y);
        }

        return (finalInformation);
    }

    private static int GetCapacityBattery(int first, int second)
    {
        return first + second;
    }
}
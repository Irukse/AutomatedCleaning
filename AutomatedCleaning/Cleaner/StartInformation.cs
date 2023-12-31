using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedCleaning.Cleaner;

public class StartInformation
{
    private const string LocationExceptionMessage =
        "The position of the cleaner is set incorrectly, C or null is incorrect";

    private const string RangeExceptionMessage =
        "The position of the cleaner is set incorrectly, cleaner out of bounds";

    public StartInformation(string[,] map, CleanerCoordinates start, List<string> commands, int battery)
    {
        Map = map;
        Start = start;
        Commands = commands;
        Battery = battery;

        ChangeNullToZero(map);

        var startRobotLocation = CheckStartCoordinate(map, start.X, start.Y);

        if (startRobotLocation is "0" or "C")
        {
            throw new ArgumentException(LocationExceptionMessage);
        }

        if (start.X < 0 || start.X >= map.GetLength(0)
                        || start.Y < 0 && start.Y >= map.GetLength(1))
        {
            throw new IndexOutOfRangeException(RangeExceptionMessage);
        }
    }

    public string[,] Map { get; set; }

    public CleanerCoordinates Start { get; set; }

    public List<string> Commands { get; set; }

    public int Battery { get; set; }

    private string CheckStartCoordinate(string[,] map, int x, int y)
    {
        var data = (
            from i in Enumerable.Range(x, map.GetLength(0))
            from j in Enumerable.Range(y, map.GetLength(1))
            select map[i, j]);

        return data.FirstOrDefault();
    }

    private void ChangeNullToZero(string[,] map)
    {
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == null)
                {
                    map[i, j] = "0";
                }
            }
        }
    }
}
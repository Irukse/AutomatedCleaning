namespace AutomatedCleaning.Cleaner;

public static class PointOnMap
{
    public static string CheckPointOnMap(int x, int y, string [,] map)
    {
        string mapInfo = "0";
        if (x >= 0 && y >= 0 && x < map.GetLength(0)
            && y < map.GetLength(1))
        {
            mapInfo = map[x, y];
        }

        return mapInfo;

    }
}
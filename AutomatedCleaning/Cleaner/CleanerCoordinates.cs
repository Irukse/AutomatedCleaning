using System.Collections.Generic;
using System.Linq;
using AutomatedCleaning.Cleaner.Enums;

namespace AutomatedCleaning.Cleaner;

public class CleanerCoordinates : Coordinates
{
    //public Coordinates Coordinates { get; set; }
    // public int X { get; set; }
    //
    // public int Y { get; set; }

    public string Facing { get; set; }

    public CleanerCoordinates(string face, int x, int y)
    {
        Facing = face;
        X = x;
        Y = y;
    }

    public CleanerCoordinates()
    {
    }

    private readonly Dictionary<string, string> _ternFacingClockwise = new()
    {
        [Face.N.ToString()] = Face.E.ToString(),
        [Face.W.ToString()] = Face.N.ToString(),
        [Face.S.ToString()] = Face.W.ToString(),
        [Face.E.ToString()] = Face.S.ToString(),
    };

    public void TernRight()
    {
        var direction = _ternFacingClockwise.First(x => x.Key.Equals(Facing));
        Facing = direction.Value;
    }

    public void TernLeft()
    {
        var direction = _ternFacingClockwise.First(x => x.Value.Equals(Facing));
        Facing = direction.Key;
    }

    public void StepForward()
    {
        if (Facing.Equals(Face.N.ToString()))
        {
            X--;
            return;
        }

        if (Facing.Equals(Face.W.ToString()))
        {
            Y--;
            return;
        }

        if (Facing.Equals(Face.E.ToString()))
        {
            Y++;
            return;
        }

        if (Facing.Equals(Face.S.ToString()))
        {
            X++;
        }
    }

    public void StepBackward()
    {
        if (Facing.Equals(Face.N.ToString()))
        {
            X++;
            return;
        }

        if (Facing.Equals(Face.W.ToString()))
        {
            Y++;
            return;
        }

        if (Facing.Equals(Face.E.ToString()))
        {
            Y--;
            return;
        }

        if (Facing.Equals(Face.S.ToString()))
        {
            X--;
        }
    }
}
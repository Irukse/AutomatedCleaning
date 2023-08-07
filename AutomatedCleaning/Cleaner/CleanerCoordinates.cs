using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutomatedCleaning.Cleaner.Enums;

namespace AutomatedCleaning.Cleaner;

public class CleanerCoordinates : Coordinates
{
     //public Coordinates Coordinates { get; set; }
    // public int X { get; set; }
    //
    // public int Y { get; set; }

    [RegularExpression("N|S|W|E", ErrorMessage = "The Face must match 'N' or 'S' or 'W' or 'E' only.")]
    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
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

    private List<string> direction = new List<string>()
    {
        Face.N.ToString(),
        Face.E.ToString(),
        Face.S.ToString(),
        Face.W.ToString(),
    };

    // return current.Next ?? current.List.First;

    // public void TernRight()
    // {
    //     for (int i = 0; i < direction.Count; i++)
    //     {
    //         if (direction[i] == Facing)
    //         {
    //             var val = direction[i+1];
    //             Facing = val;
    //             break;
    //         }
    //     }
    // }
    
    // public void TernLeft()
    // {
    //     for (int i = 0; i < direction.Count; i++)
    //     {
    //         if (direction[i] == Facing)
    //         {
    //             var val = direction[i - 1] ?? direction[direction.Count-1];
    //             Facing = val;
    //             break;
    //         }
    //     }
    // }
    
    public void TernRight()
    {
        if (Facing.Equals(Face.N.ToString()))
        {
            Facing = Face.E.ToString();
            return;
        }
    
        if (Facing.Equals(Face.W.ToString()))
        {
            Facing = Face.N.ToString();
            return;
        }
    
        if (Facing.Equals(Face.S.ToString()))
        {
            Facing = Face.W.ToString();
            return;
        }
    
        if (Facing.Equals(Face.E.ToString()))
        {
            Facing = Face.S.ToString();
        }
    }

   public void TernLeft()
    {
        if (Facing.Equals(Face.N.ToString()))
        {
            Facing = Face.W.ToString();
            return;
        }
    
        if (Facing.Equals(Face.W.ToString()))
        {
            Facing = Face.S.ToString();
            return;
        }
    
        if (Facing.Equals(Face.S.ToString()))
        {
            Facing = Face.E.ToString();
            return;
        }
    
        if (Facing.Equals(Face.E.ToString()))
        {
            Facing = Face.N.ToString();
        }
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
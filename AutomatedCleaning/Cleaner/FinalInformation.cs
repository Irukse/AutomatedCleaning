using System.Collections.Generic;

namespace AutomatedCleaning.Cleaner;

public class FinalInformation
{
    public List<Coordinates> Visited { get; set; }
    
    public List<Coordinates> Cleaned { get; set; }

    public CleanerCoordinates FinalCoordinates { get; set; }
    
    public int FinalBattary { get; set; }
}
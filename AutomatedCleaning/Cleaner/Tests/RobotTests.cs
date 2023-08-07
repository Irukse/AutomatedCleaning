using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AutomatedCleaning.Cleaner.Tests;

public class RobotTests
{
    //private readonly Robot _robot;

    public RobotTests()
    {
       // _robot = new Robot();
    }

    [Test]
    public void RobotTests_CorrectData_CanBeSuccess()
    {
        //Arrange
        var map = new string[,]
        {
            { "S", "S", "S", "S" },
            { "S", "S", "C", "S" },
            { "S", "S", "S", "S" },
            { "S", null, "S", "S" },
        };
        
        var start = new CleanerCoordinates()
        {
            // Coordinates = new Coordinates()
            // {
                X = 3,
                Y = 0,
          //  },
            Facing = "S",
            
        };
        var command = new List<string>() { "A", "C", "A", "C", "TR", "A", "C" };
        var batt = 80;
        
        var startInfo = new StartInformation(map, start, command, batt);
        
        //Act
        var r = Robot.GetClean(startInfo);

        //Assert
        Assert.NotNull(r);
    }
    
    [Test]
    public void RobotTests_CorrectData_CanBeSuccessddd()
    {
        //Arrange
        var map = new string[,]
        {
            { "C", "C", "C", "C" },
            { "C", "C", "C", "C" },
            { "C", "C", "C", "C" },
            { "S", "C", "C", "C" },
        };
        
        var start = new CleanerCoordinates()
        {
            X = 3,
            Y = 0,
      
            Facing = "S",
            
        };
        var command = new List<string>() { "A", "C", "A", "C", "TR", "A", "C" };
        var batt = 80;
        
        var startInfo = new StartInformation(map, start, command, batt);
        
        //Act
        var r = Robot.GetClean(startInfo);

        //Assert
        Assert.NotNull(r);
    }
    
    [Test]
    public void RobotTests_CorrectData_CanBeSuccessxxx()
    {
        //Arrange
        var final = new FinalInformation();
        string path =
            @"C:\Users\Cats\Documents\Practical\AutomatedCleaning\AutomatedCleaning\AutomatedCleaning\informationToTheRobot.json";
        
        foreach (var startInformation in JsonConvertor.ReadLazy(path))
        {
            if (startInformation != null)
            {
                final =  Robot.GetClean(startInformation);
            }

            JsonConvertor.WriterLazy(final);

        }
        // var map = new string[,]
        // {
        //     { "S", "S", "S", "S" },
        //     { "S", "S", "C", "S" },
        //     { "S", "S", "S", "S" },
        //     { "S", null, "S", "S" },
        // };
        //
        // var start = new CleanerCoordinates()
        // {
        //     // Coordinates = new Coordinates()
        //     // {
        //     X = 3,
        //     Y = 0,
        //     //  },
        //     Facing = "S",
        //     
        // };
        // var command = new List<string>() { "A", "C", "A", "C", "TR", "A", "C" };
        // var batt = 80;
        //
        // var startInfo = new StartInformation(map, start, command, batt);
        //
        // //Act
        // var r = Robot.GetClean(startInfo);

        //Assert
        Assert.NotNull(final);
    }
    

    
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    public void RobotTests_RobotStepIncorrect_CanBeSuccess(int x, int y)
    {
        //Arrange
        var map = new string[,]
        {
            { "S", "S", "S", "S" },
            { "S", "S", "C", "S" },
            { "S", "S", "S", "S" },
            { "S", null, "S", "S" },
        };
        
        var start = new CleanerCoordinates()
        {
         //   X = x,
         //   Y = y,
            Facing = "N",
        };
        var command = new List<string>() { "TL", "A", "C", "A", "C", "TR", "A", "C" };
        var batt = 80;

        //Assert
        Assert.Throws<ArgumentException>(() => new StartInformation(map, start, command, batt));
    }
    
    [Test]
    public void RobotTests_IncorrectDataMap_CanBeSuccess()
    {
        //Arrange
        var map = new string[,]
        {
            { "S", "S", "DDD", "S" },
            { "S", "S", "C", "S" },
            { "S", "S", "S", "S" },
            { "S", "S", "S", "S" },
        };
        
        var start = new CleanerCoordinates()
        {
         //   X = 0,
         //   Y = 0,
            Facing = "N",
        };
        var command = new List<string>() { "TL", "A", "C", "A", "C", "TR", "A", "C" };
        var batt = 0;
   
        //Assert
        Assert.Throws<ArgumentException>(() => new StartInformation(map, start, command, batt));
    }
}
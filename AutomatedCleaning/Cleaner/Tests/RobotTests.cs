using System;
using System.Collections.Generic;
using AutomatedCleaning.Cleaner.Validator;
using NUnit.Framework;

namespace AutomatedCleaning.Cleaner.Tests;

public class RobotTests
{
    private string _path =
        @"C:\Users\Cats\Documents\Practical\AutomatedCleaning\AutomatedCleaning\AutomatedCleaning\informationToTheRobot.json";

    [Test]
    public void RobotTests_CorrectData_CanBeSuccess()
    {
        //Arrange
        var map = InitialMap();
        var start = InitialCleanerCoordinates(3, 3, "S");
        var command = InitialCommand();
        var battery = 80;
        var startInfo = new StartInformation(map, start, command, battery);

        //Act
        var result = Robot.GetClean(startInfo);

        //Assert
        Assert.NotNull(result);
    }

    [Test]
    public void RobotTests_StrangeCase_CanBeSuccess()
    {
        //Arrange
        var map = new string[,]
        {
            { "C", "C", "C", "C" },
            { "C", "C", "C", "C" },
            { "C", "C", "C", "C" },
            { "S", "C", "C", "C" },
        };

        var start = InitialCleanerCoordinates(3, 3, "S");
        var command = InitialCommand();
        var battery = 80;

        var startInfo = new StartInformation(map, start, command, battery);

        //Act
        var result = Robot.GetClean(startInfo);

        //Assert
        Assert.NotNull(result);
    }

    [TestCase(3, 1)]
    [TestCase(1, 2)]
    public void RobotTests_RobotStepIncorrect_CanBeSuccess(int x, int y)
    {
        //Arrange
        var map = InitialMap();
        var start = InitialCleanerCoordinates(x, y, "S");
        var command = InitialCommand();
        var battery = 80;

        //Assert
        Assert.Throws<ArgumentException>(() => new StartInformation(map, start, command, battery));
    }

    [Test]
    public void RobotTests_IncorrectDataBattery_CanBeSuccess()
    {
        //Arrange
        var map = InitialMap();
        var start = InitialCleanerCoordinates(0, 0, "S");
        var command = InitialCommand();
        var battery = -2;

        //Act
        StartInformation startInfo = new StartInformation(map, start, command, battery);
        StartInformationValidator validator = new StartInformationValidator();

        //Assert
        Assert.False(validator.Validate(startInfo).IsValid);
    }

    [Test]
    public void RobotTests_IncorrectDataCleanerCoordinates_CanBeSuccess()
    {
        //Arrange
        var map = InitialMap();
        var start = InitialCleanerCoordinates(0, 0, "DDD");
        var command = InitialCommand();
        var battery = 2;

        //Act
        StartInformation startInfo = new StartInformation(map, start, command, battery);
        StartInformationValidator validator = new StartInformationValidator();

        //Assert
        Assert.False(validator.Validate(startInfo).IsValid);
    }

    [Test]
    public void RobotTests_IncorrectDataCleanerCommand_CanBeSuccess()
    {
        //Arrange
        var map = InitialMap();
        var start = InitialCleanerCoordinates(0, 0, "S");
        var command = new List<string>() { "BBB" };
        var battery = 2;

        //Act
        StartInformation startInfo = new StartInformation(map, start, command, battery);
        StartInformationValidator validator = new StartInformationValidator();

        //Assert
        Assert.False(validator.Validate(startInfo).IsValid);
    }

    [Test]
    public void RobotTests_IncorrectDataCleanerCoordinatesXY_CanBeSuccess()
    {
        //Arrange
        var map = InitialMap();
        var start = InitialCleanerCoordinates(-1, 3, "S");
        var command = InitialCommand();
        var battery = 80;

        //Assert
        Assert.Throws<IndexOutOfRangeException>(() => new StartInformation(map, start, command, battery));
    }

    [Test]
    public void RobotTests_ReadJson_CanBeSuccess()
    {
        //Arrange
        var startInformation = JsonConvertor.ReadLazy(_path);

        //Assert
        Assert.NotNull(startInformation);
    }

    [Test]
    public void RobotTests_WriteJson_CanBeSuccess()
    {
        //Arrange
        var map = InitialMap();
        var start = InitialCleanerCoordinates(3, 3, "S");
        var command = InitialCommand();
        var battery = 80;

        var startInfo = new StartInformation(map, start, command, battery);

        //Act
        var result = Robot.GetClean(startInfo);
        JsonConvertor.WriterLazy(result);

        //Assert
    }

    #region initial

    private string[,] InitialMap()
    {
        return new string[,]
        {
            { "S", "S", "S", "S" },
            { "S", "S", "C", "S" },
            { "S", "S", "S", "S" },
            { "S", "S", "S", "S" },
        };
    }

    private CleanerCoordinates InitialCleanerCoordinates(int x, int y, string facing)
    {
        return new CleanerCoordinates()
        {
            X = x,
            Y = y,
            Facing = facing,
        };
    }

    private List<string> InitialCommand()
    {
        return new List<string>() { "A", "C", "A", "C", "TR", "A", "C" };
    }

    #endregion
}
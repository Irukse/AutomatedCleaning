using System;
using AutomatedCleaning.Cleaner;
using AutomatedCleaning.Cleaner.Validator;

namespace AutomatedCleaning
{
    internal abstract class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            var final = new FinalInformation();
            string path =
                @"C:\Users\Cats\Documents\Practical\CleaningAuto\AutomatedCleaning\AutomatedCleaning\informationToTheRobot.json";

            foreach (var startInformation in JsonConvertor.ReadLazy(path))
            {
                if (startInformation != null)
                {
                    var cs = startInformation;
                    var evd = new StartInformationValidator();

                    if (evd.Validate(cs).IsValid)
                    {
                        final = Robot.GetClean(startInformation);
                    }
                }

                JsonConvertor.WriterLazy(final);
            }
        }
    }
}
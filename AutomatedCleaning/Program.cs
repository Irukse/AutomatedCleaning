using System;
using AutomatedCleaning.Cleaner;

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
                    final =  Robot.GetClean(startInformation);
                }

                JsonConvertor.WriterLazy(final);
            }
        }
    }
}
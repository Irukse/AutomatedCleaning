using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedCleaning.Cleaner.Log;

public class Logger
{
    private static BlockingCollection<string> _blockingCollection;
    //спросить
    private static string _filename = "log.txt";
    private static Task _task;

    static Logger()
    {
        _blockingCollection = new BlockingCollection<string>();

        _task = Task.Factory.StartNew(() =>
            {
                using (var streamWriter = new StreamWriter(_filename, true, Encoding.UTF8))
                {
                    streamWriter.AutoFlush = true;

                    foreach (var s in _blockingCollection.GetConsumingEnumerable())
                        streamWriter.WriteLine(s);
                }
            },
            TaskCreationOptions.LongRunning);
    }

    public static void WriteLog(string action, string command)
    {
        _blockingCollection.Add(
            $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")} действие: " +
                $"{action}, код: {command.ToString()}");
    }

    public static void Flush()
    {
        _blockingCollection.CompleteAdding();
        _task.Wait();
    }
}
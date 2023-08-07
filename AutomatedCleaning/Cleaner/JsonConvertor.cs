using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutomatedCleaning.Cleaner;

public static class JsonConvertor
{
    public static IEnumerable<StartInformation> ReadLazy(string path)
    {
        var serializer = new JsonSerializer();
        using var tr = File.OpenText(path);
        using var jr = new JsonTextReader(tr);

        jr.Read();
        while (jr.TokenType == JsonToken.StartObject)
        {
            yield return serializer.Deserialize<StartInformation>(jr);
            if (jr.TokenType != JsonToken.EndObject)
                throw new FormatException("Object end expected");
            jr.Read();
        }
    }

    public static void WriterLazy(FinalInformation product)
    {
        var serializer = new JsonSerializer();
        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter("json.txt"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, product);
        }
    }
}
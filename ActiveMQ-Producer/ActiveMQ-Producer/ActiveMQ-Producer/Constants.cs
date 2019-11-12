using System.Collections.Generic;

public class Constants
{
    public List<string> WEATHERAPI = new List<string>();

    public Constants()
    {
        ReadFileConstantsWeather();
    }

    public void ReadFileConstantsWeather ()
    {
        string[] lines = System.IO.File.ReadAllLines(@"../../src/InfosAPI.txt");

        foreach (string line in lines)
        {
            WEATHERAPI.Add(line);
        }
    }
}
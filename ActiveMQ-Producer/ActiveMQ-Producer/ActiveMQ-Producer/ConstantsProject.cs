using System.Collections.Generic;

public class ConstantsProject
{
    public List<string> WEATHERAPI = new List<string>();

    public ConstantsProject()
    {
        ReadFileConstantsWeather();
    }

    public void ReadFileConstantsWeather ()
    {
        string[] lines = System.IO.File.ReadAllLines(@"../../InfosAPI.txt");

        foreach (string line in lines)
        {
            WEATHERAPI.Add(line);
        }
    }
}
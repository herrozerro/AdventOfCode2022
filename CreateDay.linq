<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

private const string path = "";

async Task Main()
{
	Console.WriteLine("Please enter a day to setup.");
	int day;

	if (!int.TryParse(Console.ReadLine(), out day))
		Console.WriteLine("Error: Invalid entry, please enter an Integer.");

	string TestDataPath = Path.GetDirectoryName(Util.CurrentQueryPath) + $"\\Data\\d{day:00}t.txt";
	string DataPath = Path.GetDirectoryName(Util.CurrentQueryPath) + $"\\Data\\d{day:00}.txt";
	string DayPath = Path.GetDirectoryName(Util.CurrentQueryPath) + $"\\d{day:00}.linq";

	if (!File.Exists(DataPath))
	{
		File.Create(DataPath);
	}
	if (!File.Exists(TestDataPath))
	{
		File.Create(TestDataPath);
	}
	if (!File.Exists(DayPath))
	{
		var lines = (await File.ReadAllLinesAsync(Path.GetDirectoryName(Util.CurrentQueryPath) + "\\dtemplate.linq")).ToList();

		for (int i = 0; i < lines.Count; i++)
		{
			lines[i] = lines[i].Replace("{DayOfWeek}", $"d{day:00}");
		}
		
		await File.WriteAllLinesAsync(DayPath,lines);
	}
	
	Console.WriteLine("Day Created.");
}

// You can define other methods, fields, classes and namespaces here
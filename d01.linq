<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "utilities.linq"

async Task Main()
{
	//testing
	Debug.WriteLine("Beginning Tests");
	Debug.Assert(await p1(true) == 24000);
	Debug.Assert(await p2(true) == 45000);
	Debug.WriteLine("Tests Complete");

	await p1().Dump();
	await p2().Dump();
}

// You can define other methods, fields, classes and namespaces here
public async Task<long> p1(bool test = false)
{
	var lines = await GetLinesFromFile("d01", test);
	
	Dictionary<int,long> elves = new();
	
	int elf = 1;
	long cal = 0;
	
	for (int i = 0; i < lines.Length; i++)
	{
		if (lines[i] == "")
		{
			elves.Add(elf++, cal);
			cal = 0;
			continue;
		}
		
		cal += long.Parse(lines[i]);
	}
	elves.Add(elf++, cal);
	elves.OrderByDescending(e => e.Value).Dump();
	return elves.OrderByDescending(e => e.Value).First().Value;
}

public async Task<long> p2(bool test = false)
{
	var lines = await GetLinesFromFile("d01", test);

	Dictionary<int, long> elves = new();

	int elf = 1;
	long cal = 0;

	for (int i = 0; i < lines.Length; i++)
	{
		if (lines[i] == "")
		{
			elves.Add(elf++, cal);
			cal = 0;
			continue;
		}

		cal += long.Parse(lines[i]);
	}
	elves.Add(elf++, cal);

	return elves.OrderByDescending(e => e.Value).Take(3).Sum(s => s.Value);
}

<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "utilities.linq"

async Task Main()
{
	//testing
	Debug.WriteLine("Beginning Tests");
	Debug.Assert(await p1(true) == 2);
	Debug.Assert(await p2(true) == 4);
	Debug.WriteLine("Tests Complete");

	await p1().Dump();
	await p2().Dump();
}

// You can define other methods, fields, classes and namespaces here
public async Task<int> p1(bool test = false)
{
	var lines = await GetLinesFromFile("d04", test);
	
	int matches = 0;
	
	foreach (var line in lines)
	{
		var elves = line.Split(',');
		
		var first = elves[0].Split('-').Select(x=>int.Parse(x)).ToList();
		var second = elves[1].Split('-').Select(x=>int.Parse(x)).ToList();
		
		var firstRange = Enumerable.Range(first[0], first[1] - first[0] + 1);
		var secondRange = Enumerable.Range(second[0], second[1] - second[0] + 1);

		if (firstRange.Intersect(secondRange).Count() == Math.Min(firstRange.Count(), secondRange.Count()))
		{
			matches++;
		}
	}
	
	return matches;
}

public async Task<int> p2(bool test = false)
{
	var lines = await GetLinesFromFile("d04", test);

	int matches = 0;

	foreach (var line in lines)
	{
		var elves = line.Split(',');

		var first = elves[0].Split('-').Select(x => int.Parse(x)).ToList();
		var second = elves[1].Split('-').Select(x => int.Parse(x)).ToList();

		var firstRange = Enumerable.Range(first[0], first[1] - first[0] + 1);
		var secondRange = Enumerable.Range(second[0], second[1] - second[0] + 1);

		if (firstRange.Intersect(secondRange).Count() > 0)
		{
			matches++;
		}
	}

	return matches;
}

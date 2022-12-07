<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "utilities.linq"

async Task Main()
{
	//testing
	Debug.WriteLine("Beginning Tests");
	Debug.Assert(await p1(true) == 7);
	Debug.Assert(await p2(true) == 19);
	Debug.WriteLine("Tests Complete");

	await p1().Dump();
	await p2().Dump();
}

// You can define other methods, fields, classes and namespaces here
public async Task<int> p1(bool test = false)
{
	var line = await GetStringFromFile("d06", test);
	
	for (int i = 0; i < line.Length; i++)
	{
		var buffer = line.Substring(i,4);
		bool matchesfound = false;
		foreach (var c in buffer)
		{
			if (buffer.Count(x=> x == c) > 1)
			{
				matchesfound = true;
				break;
			}
		}
		
		if (!matchesfound)
		{
			return i + 4;
		}
	}
	
	
	return 0;
}

public async Task<int> p2(bool test = false)
{
	var line = await GetStringFromFile("d06", test);

	for (int i = 0; i < line.Length; i++)
	{
		var buffer = line.Substring(i, 14);
		bool matchesfound = false;
		foreach (var c in buffer)
		{
			if (buffer.Count(x => x == c) > 1)
			{
				matchesfound = true;
				break;
			}
		}

		if (!matchesfound)
		{
			return i + 14;
		}
	}


	return 0;
}

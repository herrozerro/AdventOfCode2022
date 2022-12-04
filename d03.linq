<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "utilities.linq"

Dictionary<char, int> values = new();

async Task Main()
{
	//testing
	Debug.WriteLine("Beginning Tests");
	Debug.Assert(await p1(true) == 157);
	Debug.Assert(await p2(true) == 70);
	Debug.WriteLine("Tests Complete");

	await p1().Dump();
	await p2().Dump();
}

// You can define other methods, fields, classes and namespaces here
public async Task<int> p1(bool test = false)
{
	int score = 0;
	var lines = await GetLinesFromFile("d03", test);
	var vs = GetLetterValues();
	foreach (var line in lines)
	{
		var left = line.Take(line.Length/2);
		var right = line.Skip(line.Length/2);
		
		score += vs[left.Intersect(right).First()];
	}
	
	
	
	return score;
}

Dictionary<char, int> GetLetterValues()
{
	if (values.Any())
	{
		return values;
	}
	
	List<char> letters = new();
	letters.AddRange(Enumerable.Range('a', 'z' - 'a' + 1)
					.Select(c => (char)c).ToList()
					);
	letters.AddRange(Enumerable.Range('A', 'Z' - 'A' + 1)
					.Select(c => (char)c).ToList()
					);
	
	int i = 1;
	
	foreach (var letter in letters)
	{
		values.Add(letter, i++);
	}
	
	return values;
}

public async Task<int> p2(bool test = false)
{
	var lines = await GetLinesFromFile("d03", test);
	var vs = GetLetterValues();
	int score = 0;
	
	for (int i = 0; i < lines.Length; i += 3)
	{
		var group = lines.Skip(i).Take(3).ToList();
		
		//get common letters
		score += vs[group[0].Intersect(group[1]).Intersect(group[2]).First()];
	}
	
	return score;
}

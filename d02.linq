<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "utilities.linq"

async Task Main()
{
	//testing
	Debug.WriteLine("Beginning Tests");
	Debug.Assert(await p1(true) == 15);
	Debug.Assert(await p2(true) == 12);
	Debug.WriteLine("Tests Complete");

	await p1().Dump();
	await p2().Dump();
}

// You can define other methods, fields, classes and namespaces here
public async Task<int> p1(bool test = false)
{
	var lines = await GetLinesFromFile("d02", test);
	int score = 0;
	Dictionary<string, int> values = new();
	values.Add("X", 1);
	values.Add("Y", 2);
	values.Add("Z", 3);
	
	
	
	Dictionary<string, int> scores = new();
	scores.Add("A X", 3); //tie
	scores.Add("A Y", 6); //lose
	scores.Add("A Z", 0); //win
	scores.Add("B X", 0); //win
	scores.Add("B Y", 3); //tie
	scores.Add("B Z", 6); //lose
	scores.Add("C X", 6); //lose
	scores.Add("C Y", 0); //win
	scores.Add("C Z", 3); //tie

	foreach (var line in lines)
	{

		score += scores[line] + values[line.Split(" ")[1]];
	}
	
	return score;
}

public async Task<int> p2(bool test = false)
{
	var lines = await GetLinesFromFile("d02", test);
	int score = 0;

	Dictionary<string, int> scores = new();
	scores.Add("A X", 0 + 3);
	scores.Add("A Y", 3 + 1);
	scores.Add("A Z", 6 + 2);
	scores.Add("B X", 0 + 1);
	scores.Add("B Y", 3 + 2);
	scores.Add("B Z", 6 + 3);
	scores.Add("C X", 0 + 2);
	scores.Add("C Y", 3 + 3);
	scores.Add("C Z", 6 + 1);

	foreach (var line in lines)
	{

		score += scores[line];
	}

	return score;
}

<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "utilities.linq"

async Task Main()
{
	//testing
	Debug.WriteLine("Beginning Tests");
	Debug.Assert(await p1(true) == "CMZ");
	Debug.Assert(await p2(true) == "MCD");
	Debug.WriteLine("Tests Complete");

	await p1().Dump();
	await p2().Dump();
}

// You can define other methods, fields, classes and namespaces here
public async Task<string> p1(bool test = false)
{
	var lines = await GetLinesFromFile("d05", test);

	var stacks = GetStartingConditions(test);

	foreach (var line in lines)
	{
		var inst = line.Split(' ');

		for (int i = 0; i < int.Parse(inst[1]); i++)
		{
			var container = stacks[int.Parse(inst[3])].Pop();

			stacks[int.Parse(inst[5])].Push(container);
		}
	}

	var sb = new StringBuilder();

	for (int i = 1; i <= stacks.Count; i++)
	{
		sb.Append(stacks[i].First());
	}
	return sb.ToString();
}

public async Task<string> p2(bool test = false)
{
	var lines = await GetLinesFromFile("d05", test);

	var stacks = GetStartingConditions(test);

	foreach (var line in lines)
	{
		var inst = line.Split(' ');
		var grabber = new Stack<char>();
		for (int i = 0; i < int.Parse(inst[1]); i++)
		{
			var container = stacks[int.Parse(inst[3])].Pop();
			grabber.Push(container);
		}
		for (int i = 0; i < int.Parse(inst[1]); i++)
		{
			stacks[int.Parse(inst[5])].Push(grabber.Pop());
		}
		
	}

	var sb = new StringBuilder();

	for (int i = 1; i <= stacks.Count; i++)
	{
		sb.Append(stacks[i].First());
	}
	return sb.ToString();
}

public Dictionary<int, Stack<char>> GetStartingConditions(bool test)
{
	if (test)
	{
		//   [D]
		//[N][C]
		//[Z][M][P]
		// 1  2  3
		return new Dictionary<int, Stack<char>>()
		{
			{1, new Stack<char>(new []{'Z','N',})},
			{2, new Stack<char>(new []{'M','C','D', })},
			{3, new Stack<char>(new []{'P'})}
		};
	}


	//                        [Z] [W] [Z]
	//        [D] [M]         [L] [P] [G]
	//    [S] [N] [R]         [S] [F] [N]
	//    [N] [J] [W]     [J] [F] [D] [F]
	//[N] [H] [G] [J]     [H] [Q] [H] [P]
	//[V] [J] [T] [F] [H] [Z] [R] [L] [M]
	//[C] [M] [C] [D] [F] [T] [P] [S] [S]
	//[S] [Z] [M] [T] [P] [C] [D] [C] [D]
	// 1   2   3   4   5   6   7   8   9
	return new Dictionary<int, Stack<char>>()
	{
		{1, new Stack<char>(new []{'S','C','V','N',})},
		{2, new Stack<char>(new []{'Z','M','J','H','N','D', })},
		{3, new Stack<char>(new []{'M','C','T','G','J','N','D',})},
		{4, new Stack<char>(new []{'T','D','F','J','W','R','M',})},
		{5, new Stack<char>(new []{'P','F','H',})},
		{6, new Stack<char>(new []{'C','T','Z','H','J',})},
		{7, new Stack<char>(new []{'D','P','R','Q','F','S','L','Z',})},
		{8, new Stack<char>(new []{'C','S','L','H','D','F','P','W',})},
		{9, new Stack<char>(new []{'D','S','M','P','F','N','G','Z',})}
	};

}
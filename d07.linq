<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "utilities.linq"

async Task Main()
{
	//testing
	Debug.WriteLine("Beginning Tests");
	Debug.Assert(await p1(true) == 95437);
	Debug.Assert(await p2(true) == 24933642);
	Debug.WriteLine("Tests Complete");

	await p1().Dump();
	await p2().Dump();
}

// You can define other methods, fields, classes and namespaces here
public async Task<long> p1(bool test = false)
{
	var lines = await GetLinesFromFile("d07", test);
	Node currentDirectory;
	List<Node> Nodes = new();
	Nodes.Add(new Node() { name = "/", size = 0, NodeType = NodeType.dir });
	
	currentDirectory = Nodes[0];
	
	foreach (var line in lines.Skip(1))
	{
		var inst = line.Split(' ');

		switch (line)
		{
			case "$ cd ..":
				currentDirectory = currentDirectory.ParentNode;
				break;
			case string s when s.StartsWith("$ cd"):
				currentDirectory = currentDirectory.ChildrenNodes.First(cr => cr.name == inst[2]);
				break;
			case "$ ls":
				break;
			default:
				var newnode = new Node();
				newnode.NodeType = line.StartsWith("dir") ? NodeType.dir : NodeType.file;
				newnode.size = line.StartsWith("dir") ? 0 : int.Parse(inst[0]);
				newnode.name = inst[1];
				newnode.ParentNode = currentDirectory;
				currentDirectory.ChildrenNodes.Add(newnode);
				Nodes.Add(newnode);
				break;
		}
	}
	
	Nodes.First(n => n.name == "/").GetChildLengths();
	
	//Nodes.Where(n => n.NodeType == NodeType.dir).Dump();
	
	return Nodes.Where(x=>x.totalSize < 100000).Sum(x => x.totalSize);
}

public async Task<long> p2(bool test = false)
{
	var lines = await GetLinesFromFile("d07", test);

	int totalspace = 70000000;
	Node currentDirectory;
	List<Node> Nodes = new();
	Nodes.Add(new Node() { name = "/", size = 0, NodeType = NodeType.dir });

	currentDirectory = Nodes[0];

	foreach (var line in lines.Skip(1))
	{
		var inst = line.Split(' ');

		switch (line)
		{
			case "$ cd ..":
				currentDirectory = currentDirectory.ParentNode;
				break;
			case string s when s.StartsWith("$ cd"):
				currentDirectory = currentDirectory.ChildrenNodes.First(cr => cr.name == inst[2]);
				break;
			case "$ ls":
				break;
			default:
				var newnode = new Node();
				newnode.NodeType = line.StartsWith("dir") ? NodeType.dir : NodeType.file;
				newnode.size = line.StartsWith("dir") ? 0 : int.Parse(inst[0]);
				newnode.name = inst[1];
				newnode.ParentNode = currentDirectory;
				currentDirectory.ChildrenNodes.Add(newnode);
				Nodes.Add(newnode);
				break;
		}
	}
	Nodes[0].GetChildLengths();
	int combinedspace = 70000000;
	var spaceneeded = 30000000;
	var usedspace = Nodes[0].totalSize;
	
	var minimumSpaceToDelete = spaceneeded - (combinedspace - usedspace);
	


	return Nodes.Where(n => n.totalSize >= minimumSpaceToDelete).OrderBy(n => n.totalSize).First().totalSize;
}

public class Node
{
	public Node ParentNode { get; set; }
	public NodeType NodeType { get; set; }
	public string name { get; set; }
	public long size { get; set; }
	public List<Node> ChildrenNodes { get; set; } = new();
	
	public long totalSize = 0;
	
	public long GetChildLengths(){
		if (NodeType == NodeType.file)
		{
			return size;
		}
		else
		{
			if (totalSize > 0)
			{
				return totalSize;
			}
			foreach (var child in ChildrenNodes)
			{
				totalSize += child.GetChildLengths();
			}
			
			return totalSize;
		}
	}
}

public enum NodeType
{
	dir,
	file
}
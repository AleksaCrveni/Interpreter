/******* INPUT *******/
string filename = "basic.txt";
if (args.Length == 1)
{
  filename = args[0];
}
// Change to stream buffer later
var deep = Directory.GetParent(filename);

string root = deep.Parent.Parent.Parent.Parent.FullName;
string filepath = Path.Combine(root, "Inputs", filename);
string input = File.ReadAllText(filepath);











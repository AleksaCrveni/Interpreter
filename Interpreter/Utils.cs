using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
  public static class Utils
  {
    public static string LoadInputFromDisk(string filename)
    {
      // Change to stream buffer later
      var deep = Directory.GetParent(filename);

      string root = deep.Parent.Parent.Parent.Parent.FullName;
      string filepath = Path.Combine(root, "Inputs", filename);
      return File.ReadAllText(filepath);
    }

  }
}

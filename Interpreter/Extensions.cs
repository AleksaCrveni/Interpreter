using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
  public static class Extensions
  {
    // byte representation \r\n is 
    private static byte c1 = 13;
    private static byte c2 = 10;
    private static byte c3 = 92;
    private static byte c4 = 99;
    public static int IndexOfNewLine(this Span<byte> source)
    {
      // probably not most efficeint way but here we are
      for (int i =0; i < source.Length; i++)
      {
        if (source[i] == c1 && i + 1 < source.Length)
          if (source[++i] == c2)
            return i - 1;
      }
      return -1;
    }
    
  }
}

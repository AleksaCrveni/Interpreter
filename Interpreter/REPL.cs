using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
  
  internal class REPL
  {
    private Stream _stdIn;
    private byte[] _buffer;
    int offset = 0;
    int size = 10;
    // Not sure if this is right approach
    StringBuilder _sb;
    public REPL(Stream _in)
    {
      _stdIn = _in;
      _buffer = new byte[size];
      _sb = new StringBuilder();
    }
    public void Start()
    {
      bool end = false;
      string input = "";
      Console.WriteLine("REPL started");
      int ind = 0;
      Span<byte> span = new Span<byte>();
      span = _buffer.AsSpan();
      while (!end)
      {       
        ind = span.IndexOfNewLine();
        if (ind != -1)
          input = Encoding.Default.GetString(span.Slice(0, ind));
        else
          input = Encoding.Default.GetString(span);

        if (input == "EXIT()")
        {
          end = true;
          break;
        }
        Console.WriteLine(input);
      }

      Console.WriteLine("REPL closed");
      Console.ReadLine();
    }
  }
}

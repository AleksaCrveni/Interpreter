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
    int size = 4096;
    public REPL(Stream _in)
    {
      _stdIn = _in;
      _buffer = new byte[size];
    }
    public void Start()
    {
      bool end = false;
      string input = "";
      Console.WriteLine("REPL started");
      ReadOnlySpan<char> span;
      while (!end)
      {
        _stdIn.Read(_buffer, offset, size);
        input = Encoding.Default.GetString(_buffer);
        span = input.AsSpan();
        int ind = span.IndexOf("\r\n");
        input = span.Slice(0, ind).ToString();
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

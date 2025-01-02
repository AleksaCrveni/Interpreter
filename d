[1mdiff --git a/Interpreter/REPL.cs b/Interpreter/REPL.cs[m
[1mindex 84c37e1..4c33430 100644[m
[1m--- a/Interpreter/REPL.cs[m
[1m+++ b/Interpreter/REPL.cs[m
[36m@@ -6,37 +6,48 @@[m [musing System.Threading.Tasks;[m
 [m
 namespace Interpreter[m
 {[m
[32m+[m[41m  [m
   internal class REPL[m
   {[m
     private Stream _stdIn;[m
     private byte[] _buffer;[m
     int offset = 0;[m
[31m-    int size = 4096;[m
[32m+[m[32m    int size = 10;[m
[32m+[m[32m    // Not sure if this is right approach[m
[32m+[m[32m    StringBuilder _sb;[m
     public REPL(Stream _in)[m
     {[m
       _stdIn = _in;[m
       _buffer = new byte[size];[m
[32m+[m[32m      _sb = new StringBuilder();[m
     }[m
     public void Start()[m
     {[m
       bool end = false;[m
       string input = "";[m
       Console.WriteLine("REPL started");[m
[31m-      ReadOnlySpan<char> span;[m
[32m+[m[32m      Span<byte> span = new Span<byte>();[m
[32m+[m[32m      //ReadOnlySpan<char> human = new ReadOnlySpan<char>();[m
[32m+[m[32m      int ind = 0;[m
       while (!end)[m
       {[m
[31m-        _stdIn.Read(_buffer, offset, size);[m
[31m-        input = Encoding.Default.GetString(_buffer);[m
[31m-        span = input.AsSpan();[m
[31m-        int ind = span.IndexOf("\r\n");[m
[31m-        input = span.Slice(0, ind).ToString();[m
[32m+[m[32m        _sb.Clear();[m
[32m+[m
[32m+[m[41m       [m
[32m+[m[32m        _stdIn.Read(span);[m
[32m+[m[32m        ind = span.IndexOfNewLine();[m
[32m+[m[32m        if (ind != -1)[m
[32m+[m[32m          input = Encoding.Default.GetString(span.Slice(0, ind));[m
[32m+[m[32m        else[m
[32m+[m[32m          input = Encoding.Default.GetString(span);[m
[32m+[m[41m        [m
[32m+[m
[32m+[m[32m        //input = _sb.ToString();[m
         if (input == "EXIT()")[m
         {[m
           end = true;[m
           break;[m
         }[m
[31m-          [m
[31m-[m
         Console.WriteLine(input);[m
       }[m
 [m

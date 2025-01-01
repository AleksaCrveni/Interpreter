using System.ComponentModel.Design;

namespace Interpreter
{
  public ref struct Lexer
  {
    private ReadOnlySpan<char> _internal;
    private int _position = 0; // current posion
    private int _readPosition = 0; // next position
    private char _char; // current char
    public Lexer(string input)
    {
      _internal = input.AsSpan();
      ReadChar();
    }
    
    public void AdvanceToken()
    {
      _position = _readPosition++;
    }
    
    public void ReadChar()
    {
      if (_readPosition >= _internal.Length)
        _char = (char)0;
      else
        _char = _internal[_readPosition];

      // set curr and go next
      _position = _readPosition++;
    }
    public Token NextToken()
    {
      Token t = _char switch
      {
        '=' => new Token(TokenType.ASSIGN, _char.ToString()),
        ';' => new Token(TokenType.SEMICOLON, _char.ToString()),
        '(' => new Token(TokenType.LPAREN, _char.ToString()),
        ')' => new Token(TokenType.RPAREN, _char.ToString()),
        ',' => new Token(TokenType.COMMA, _char.ToString()),
        '+' => new Token(TokenType.PLUS, _char.ToString()),
        '{' => new Token(TokenType.LBRACE, _char.ToString()),
        '}' => new Token(TokenType.RBRACE, _char.ToString()),
        (char)0 => new Token(TokenType.EOF, "")
      };
      
      ReadChar();
      return t;
    }
  }
}

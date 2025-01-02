using System.ComponentModel.Design;
using System.Text;

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
      SkipWhitespace();
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
        '-' => new Token(TokenType.MINUS, _char.ToString()),
        '!' => new Token(TokenType.BANG, _char.ToString()),
        '*' => new Token(TokenType.ASTERISK, _char.ToString()),
        '/' => new Token(TokenType.SLASH, _char.ToString()),
        '<' => new Token(TokenType.LT, _char.ToString()),
        '>' => new Token(TokenType.GT, _char.ToString()),
        // 0  means NUL
        (char)0 => new Token(TokenType.EOF, ""),
        _ => ReadOther()
      };
      // If its keyword or ident, no need to move next position because
      // we did it in while loop in ReadIdentifier()
      // for this to work we must put all of these before IDENT
      // will change it later if it becomes messy
      if ((int)t.Type <= (int)TokenType.IDENT)
        return t;

      ReadChar();
      return t;
    }

    public Token ReadOther()
    {
      return _char switch
      {
        '_' => ReadIdentifier(),
        >= 'a' and <= 'z' => ReadIdentifier(),
        >= 'A' and <= 'Z' => ReadIdentifier(),
        >= '0' and <= '9' => ReadNumber(),
        _ => new Token(TokenType.ILLEGAL, _char.ToString())
      };
    }

    public Token ReadIdentifier()
    {
      int starter = _position;
      while (char.IsLetter(_char) || _char == '_')
      {
        ReadChar();
      }

      string literal = _internal.Slice(starter, _position - starter).ToString();
      return literal switch
      {
        "let" => new Token(TokenType.LET, literal),
        "fn" => new Token(TokenType.FUNCTION, literal),
        _ => new Token(TokenType.IDENT, literal)
      };
    }

    public Token ReadNumber()
    {
      int starter = _position;
      while (char.IsDigit(_char))
      {
        ReadChar();
      }

      string literal = _internal.Slice(starter, _position - starter).ToString();
      return new Token(TokenType.INT, literal);
    }

    public void SkipWhitespace()
    {
      while (_char == ' ' || _char == '\r' || _char == '\n' || _char == '\t')
        ReadChar();
    }
  }
}

namespace Interpreter
{
  public class Lexer
  {
    private char[] _charBuffer;
    private int _position = 0; // current posion
    private int _readPosition = 0; // next position
    private char _char; // current char
    public Lexer(string input)
    {
      _charBuffer = input.ToCharArray();
      ReadChar();
    }
    
    public void AdvanceToken()
    {
      _position = _readPosition++;
    }
    
    public void ReadChar()
    {
      if (_readPosition >= _charBuffer.Length)
        _char = (char)0;
      else
        _char = _charBuffer[_readPosition];

      // set curr and go next
      _position = _readPosition++;
    }
    public Token NextToken()
    {
      SkipWhitespace();
      (Token t, bool next) = _char switch
      {
        '=' => (ReadDoubleCharOperator(), false),
        ';' => (new Token(TokenType.SEMICOLON, _char.ToString()), true),
        '(' => (new Token(TokenType.LPAREN, _char.ToString()), true),
        ')' => (new Token(TokenType.RPAREN, _char.ToString()), true),
        ',' => (new Token(TokenType.COMMA, _char.ToString()), true),
        '+' => (new Token(TokenType.PLUS, _char.ToString()), true),
        '{' => (new Token(TokenType.LBRACE, _char.ToString()), true),
        '}' => (new Token(TokenType.RBRACE, _char.ToString()), true),
        '-' => (new Token(TokenType.MINUS, _char.ToString()), true),
        '!' => (ReadDoubleCharOperator(), false),
        '*' => (new Token(TokenType.ASTERISK, _char.ToString()), true),
        '/' => (new Token(TokenType.SLASH, _char.ToString()), true),
        '<' => (new Token(TokenType.LT, _char.ToString()), true),
        '>' => (new Token(TokenType.GT, _char.ToString()), true),
        // 0  means NUL
        (char)0 => (new Token(TokenType.EOF, ""), true),
        _ => (ReadOther(), false)
      };
      //// If its keyword or ident, no need to move next position because
      //// we did it in while loop in ReadIdentifier()
      //// for this to work we must put all of these before IDENT
      //// will change it later if it becomes messy
      //// more i look at this , it feels more stupid
      //if ((int)t.Type <= (int)TokenType.IDENT)
      //  return t;

      if (!next)
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
    public Token ReadDoubleCharOperator()
    {
      ReadOnlySpan<char> buffer = _charBuffer.AsSpan();
      int starter = _position;
      while (_char == '=' || _char == '!')
      {
        ReadChar();
      }

      string literal = buffer.Slice(starter, _position - starter).ToString();
      return literal switch
      {
        "==" => new Token(TokenType.EQ, literal),
        "!=" => new Token(TokenType.NOT_EQ, literal),
        "=" => new Token(TokenType.ASSIGN, literal),
        "!" => new Token(TokenType.BANG, literal),
        _ => new Token(TokenType.ILLEGAL, literal)
      };
    }

    public Token ReadIdentifier()
    {
      ReadOnlySpan<char> buffer = _charBuffer.AsSpan();
      int starter = _position;
      while (char.IsLetter(_char) || _char == '_')
      {
        ReadChar();
      }

      string literal = buffer.Slice(starter, _position - starter).ToString();
      return literal switch
      {
        "let" => new Token(TokenType.LET, literal),
        "fn" => new Token(TokenType.FUNCTION, literal),
        "if" => new Token(TokenType.IF, literal),
        "else" => new Token(TokenType.ELSE, literal),
        "true" => new Token(TokenType.TRUE, literal),
        "false" => new Token(TokenType.FALSE, literal),
        "return" => new Token(TokenType.RETURN, literal),
        _ => new Token(TokenType.IDENT, literal)
      };
    }

    public Token ReadNumber()
    {
      ReadOnlySpan<char> buffer = _charBuffer.AsSpan();
      int starter = _position;
      while (char.IsDigit(_char))
      {
        ReadChar();
      }

      string literal = buffer.Slice(starter, _position - starter).ToString();
      return new Token(TokenType.INT, literal);
    }

    public void SkipWhitespace()
    {
      while (_char == ' ' || _char == '\r' || _char == '\n' || _char == '\t')
        ReadChar();
    }
  }
}

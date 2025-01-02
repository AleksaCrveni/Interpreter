using System.Dynamic;

namespace Interpreter
{
  public struct Token
  {
    public TokenType Type;
    public string Literal;

    public Token(TokenType type, string literal)
    {
      Type = type;
      Literal = literal;
    }
  }
  public enum TokenType
  {
    // Keywords
    FUNCTION,
    LET,

    // Identifiers + literals
    INT, // 132153
    IDENT, // add, foobar, x, y
   

    ILLEGAL,
    EOF,

    // Operators
    ASSIGN,
    PLUS,

    // Delimiters
    COMMA,
    SEMICOLON,

    LPAREN,
    RPAREN,
    LBRACE,
    RBRACE,

    
  }
  public static class Helpers
  {
    public static string GetTokenTypeValue(TokenType type) => type switch
    {
      TokenType.ILLEGAL => "ILLEGAL",
      TokenType.EOF => "EOF",
      TokenType.IDENT => "IDENT",
      TokenType.INT => "INT",
      TokenType.ASSIGN => "=",
      TokenType.PLUS => "+",
      TokenType.COMMA => ",",
      TokenType.SEMICOLON => ";",
      TokenType.LPAREN => "(",
      TokenType.RPAREN => ")",
      TokenType.LBRACE => "{",
      TokenType.RBRACE => "}",
      TokenType.FUNCTION => "FUNCTION",
      TokenType.LET => "LET",
      _ => throw new NotImplementedException()
    };
  }
  // Token Type values
  
}

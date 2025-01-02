namespace Tests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestNextToken1()
    {
      string input = "=+(){},;";
      Token[] expected = new Token[]
      {
        new Token(TokenType.ASSIGN, "="),
        new Token(TokenType.PLUS, "+"),
        new Token(TokenType.LPAREN, "("),
        new Token(TokenType.RPAREN, ")"),
        new Token(TokenType.LBRACE, "{"),
        new Token(TokenType.RBRACE, "}"),
        new Token(TokenType.COMMA, ","),
        new Token(TokenType.SEMICOLON, ";")
      };

      Lexer lx = new Lexer(input);
      Token t;
      Token tt;
      for (int i =0; i < expected.Length; i++)
      {
        t = lx.NextToken();
        tt = expected[i];
        if (t.Type != tt.Type)
          Assert.Fail($"Index {i} tokenType wrong. Expected {tt.Type}, got {t.Type}");
        if (t.Literal != tt.Literal)
          Assert.Fail($"Index {i} literal wrong. Expected {tt.Literal}, got {t.Literal}");
      }

      Assert.IsTrue(1 == 1);
    }

    [TestMethod]
    public void TestNextToken2()
    {
      string input = Utils.LoadInputFromDisk("TestNextToken2.txt");
      Token[] expected = new Token[]
      {
        new Token(TokenType.LET, "let"),
        new Token(TokenType.IDENT, "five"),
        new Token(TokenType.ASSIGN, "="),
        new Token(TokenType.INT, "5"),
        new Token(TokenType.SEMICOLON, ";"),
        new Token(TokenType.LET, "let"),
        new Token(TokenType.IDENT, "ten"),
        new Token(TokenType.ASSIGN, "="),
        new Token(TokenType.INT, "10"),
        new Token(TokenType.SEMICOLON, ";"),
        new Token(TokenType.LET, "let"),
        new Token(TokenType.IDENT, "add"),
        new Token(TokenType.ASSIGN, "="),
        new Token(TokenType.FUNCTION, "fn"),
        new Token(TokenType.LPAREN, "("),
        new Token(TokenType.IDENT, "x"),
        new Token(TokenType.SEMICOLON, ","),
        new Token(TokenType.IDENT, "y"),
        new Token(TokenType.RPAREN, ")"),
        new Token(TokenType.LBRACE, "{"),
        new Token(TokenType.IDENT, "x"),
        new Token(TokenType.PLUS, "+"),
        new Token(TokenType.IDENT, "y"),
        new Token(TokenType.SEMICOLON, ";"),
        new Token(TokenType.RBRACE, "}"),
        new Token(TokenType.SEMICOLON, ";"),
        new Token(TokenType.LET, "let"),
        new Token(TokenType.IDENT, "result"),
        new Token(TokenType.ASSIGN, "="),
        new Token(TokenType.IDENT, "add"),
        new Token(TokenType.LPAREN, "("),
        new Token(TokenType.IDENT, "five"),
        new Token(TokenType.COMMA, ","),
        new Token(TokenType.IDENT, "ten"),
        new Token(TokenType.RPAREN, ")"),
        new Token(TokenType.SEMICOLON, ";"),
        new Token(TokenType.EOF, ""),
      };

      Lexer lx = new Lexer(input);
      Token t;
      Token tt;
      for (int i = 0; i < expected.Length; i++)
      {
        t = lx.NextToken();
        tt = expected[i];
        if (t.Type != tt.Type)
          Assert.Fail($"Index {i} tokenType wrong. Expected {tt.Type}, got {t.Type}");
        if (t.Literal != tt.Literal)
          Assert.Fail($"Index {i} literal wrong. Expected {tt.Literal}, got {t.Literal}");
      }

      Assert.IsTrue(1 == 1);
    }
  }
}
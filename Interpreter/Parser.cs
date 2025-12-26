
namespace Interpreter
{
  public class Parser
  {
    public Lexer _l;
    public Token _currToken;
    public Token _peekToken;
    public List<string> _errors;
    public Dictionary<TokenType, Func<Expression>> _prefixParseFns;
    public Dictionary<TokenType, Func<Expression, Expression>> _infixParseFns;
    public enum PRECENDECES
    {
      LOWEST,
      EQUALS, // ==
      LESSGREATER, // > or <
      SUM, // +
      PRODUCT, // *
      PREFIX, // -X or IX
      CALL // myFunction(X)
    }

    public Parser(Lexer l)
    {
      _l = l;
      _currToken = new Token();
      _peekToken = new Token();
      _errors = new List<string>();
      // read two tokens so current and next token are both set
      NextToken();
      NextToken();

      _prefixParseFns = new Dictionary<TokenType, Func<Expression>>();
      _infixParseFns = new Dictionary<TokenType, Func<Expression, Expression>>();
      RegisterFunctions();
    }
    public void RegisterFunctions()
    {
      // Prefixes
      RegisterPrefix(TokenType.IDENT, ParseIdentifierAsExpression);
      RegisterPrefix(TokenType.INT, ParseIntegerLiteral);
    }
    public void NextToken()
    {
      _currToken = _peekToken;
      _peekToken = _l.NextToken();
    }

    public Program ParseProgram()
    {
      Program p = new Program(new List<Statement>());
      Statement s;
      while (_currToken.Type != TokenType.EOF)
      {
        s = ParseStatement();
        if (s != null)
          p._statements.Add(s);
        NextToken();
      }
      return p;
    }

    public Statement? ParseStatement() =>
      _currToken.Type switch
      {
        TokenType.LET => ParseLetStatement(),
        TokenType.RETURN => ParseReturnStatement(),
        _ => ParseExpressionStatement()
      };

    public ExpressionStatement? ParseExpressionStatement()
    {
      ExpressionStatement s = new ExpressionStatement(_currToken);
      s.Expression = ParseExpression(PRECENDECES.LOWEST);

      if (_peekToken.Type == TokenType.SEMICOLON)
        NextToken();

      return s;
    }

    public Expression? ParseExpression(PRECENDECES precendece)
    {
      Func<Expression>? func = _prefixParseFns.GetValueOrDefault(_currToken.Type, null);
      if (func == null)
        return null;

      Expression leftExp = func();
      return leftExp;
    }

    public ReturnStatement? ParseReturnStatement()
    {
      ReturnStatement s = new ReturnStatement(_currToken);
      NextToken();

      while (_currToken.Type != TokenType.SEMICOLON)
        NextToken();

      return s;
    }

    public LetStatement? ParseLetStatement()
    {
      LetStatement s = new LetStatement();
      s.Token = _currToken;

      if (!ExpectPeek(TokenType.IDENT))
          return null;
      s.Name = ParseIdentifier();

      if (!ExpectPeek(TokenType.ASSIGN))
        return null;
      // TODO: implement parse expression.
      // for now we will just skip until semicolon;
      //s.value = ParseExpession();

      while (_currToken.Type != TokenType.SEMICOLON)
        NextToken();
      return s;
    }

    public Expression ParseIdentifierAsExpression() => (Expression)ParseIdentifier();
    public Identifier ParseIdentifier()
    {
      return new Identifier(_currToken, _currToken.Literal);
    }
    public Expression ParseIntegerLiteral()
    {
      IntegerLiteral e = new IntegerLiteral();
      e.Token = _currToken;
      bool isNum = long.TryParse(_currToken.Literal, out e.Value);
      if (!isNum)
        return null;
      return e;
    }
    public bool ExpectPeek(TokenType t)
    {
      if (_peekToken.Type == t)
      {
        NextToken();
        return true;
      }
      else
      {
        LogPeekError(t);
        return false;
      }
    }

    public void LogPeekError(TokenType t) => _errors.Add($"Expected next token type to be {t}, got {_peekToken.Type}");
    public void RegisterPrefix(TokenType type, Func<Expression> func) => _prefixParseFns.Add(type, func);
    public void RegsiterInfix(TokenType type, Func<Expression, Expression> func) => _infixParseFns.Add(type, func);
  }
}

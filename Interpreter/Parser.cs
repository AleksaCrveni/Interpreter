namespace Interpreter
{
  public ref struct Parser
  {
    public Lexer _l;
    public Token _currToken;
    public Token _peekToken;
    public List<string> _errors;
    public Parser(Lexer l)
    {
      _l = l;
      _currToken = new Token();
      _peekToken = new Token();
      _errors = new List<string>();
      // read two tokens so current and next token are both set
      NextToken();
      NextToken();
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
        _ => null
      };
    

    public ReturnStatement? ParseReturnStatement()
    {
      ReturnStatement s = new ReturnStatement(_currToken);
      NextToken();
      //s.returnValue = ParseExpression();
      while (_currToken.Type != TokenType.SEMICOLON)
        NextToken();

      return s;
    }
    public LetStatement? ParseLetStatement()
    {
      LetStatement s = new LetStatement();
      s.token = _currToken;

      if (!ExpectPeek(TokenType.IDENT))
          return null;
      s.name = ParseIdentifier();

      if (!ExpectPeek(TokenType.ASSIGN))
        return null;
      // TODO: implement parse expression.
      // for now we will just skip until semicolon;
      //s.value = ParseExpession();

      while (_currToken.Type != TokenType.SEMICOLON)
        NextToken();
      return s;
    }
    public Identifier ParseIdentifier()
    {
      Identifier i = new Identifier(_currToken, _currToken.Literal);
      return i;
    }
    /*********** PSEUDO **********************/
    //public Statement ParseLetStatement()
    //{
    //  _l.AdvanceToken();
    //  Identifier ident = ParseIdentifier();
    //  _l.AdvanceToken();

    //  if (_currToken.Type != TokenType.EQ)
    //    ParseError("Expecting equal sign!");

    //  Expression exp = ParseExpression();
    //}

    //public Identifier ParseIdentifier()
    //{
    //  Identifier i = new Identifier();
    //  i.token = _currToken;
    //  return i;
    //}

    //public Expression ParseExpression()
    //{
    //  if (_currToken.Type == TokenType.INT)
    //  {
    //    if (_peekToken.Type == TokenType.PLUS)
    //      return ParseOperatorExpression();
    //    else if (_peekToken.Type == TokenType.SEMICOLON)
    //      return ParseIntegerLiteral();
    //    else
    //      ParseError("Unexpected token in expression. Expecting + or ;");
    //  } else if (_currToken.Type == TokenType.LPAREN)
    //  {
    //    return ParseGroupedExpression();
    //  } else
    //  {
    //    ParseError("Unexpected token in expression, expected int or )");
    //  }
    //  return null; // do this for comp, i think i will go with idea that parse error should throw expection
    //}

    //public OperatorExpression ParseOperatorExpression()
    //{
    //  OperatorExpression opE = new OperatorExpression();

    //  opE.left = ParseIntegerLiteral();
    //  _l.AdvanceToken();
    //  opE.operator = _currToken;
    //  _l.AdvanceToken();
    //  opE.right = ParseExpression();
    //  return opE;
    //}

    //public void ParseError(string err)
    //{

    //}
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

  }
}

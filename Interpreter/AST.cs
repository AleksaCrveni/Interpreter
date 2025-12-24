namespace Interpreter
{
  public interface ASTNode
  {
    public string TokenLiteral();
  }

  public interface Statement : ASTNode
  {
    public void StatementNode();
  }

  public interface Expression : ASTNode
  {
    public void ExpressionNode();
  }

  public class Program : ASTNode
  {
    public List<Statement> _statements;

    public Program(List<Statement> statements)
    {
      _statements = statements;
    }
    public string TokenLiteral()
    {
      if (_statements.Count > 0)
        return _statements[0].TokenLiteral();
      else
        return "";
    }
  }

  public struct LetStatement : Statement
  {
    public Token token;
    public Identifier name;
    public Expression value;
    public LetStatement(Token token, Identifier identifier, Expression expression)
    {
      this.token = token;
      this.name = identifier;
      this.value = expression;
    }

    public void StatementNode()
    {
      throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
      return token.Literal;
    }
  }

  public struct ReturnStatement : Statement
  {
    public Token token;
    public Expression returnValue;
    public ReturnStatement(Token t) => token = t;
    public void StatementNode() => throw new NotImplementedException();
    public string TokenLiteral() => token.Literal;
  }
  public struct Identifier : Expression
  {
    public Token token;
    public string value;

    public Identifier(Token token, string value)
    {
      this.token = token;
      this.value = value;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
      return token.Literal;
    }
  }

}

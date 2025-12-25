using System.Text;

namespace Interpreter
{
  public interface ASTNode
  {
    public string TokenLiteral();
    public string String();
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

    public Program() => _statements = new List<Statement>();

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
    public string String()
    {
      StringBuilder sb = new StringBuilder();
      foreach (Statement s in _statements)
        sb.Append(s.String());

      return sb.ToString();
    }
  }

  public struct LetStatement : Statement
  {
    public Token Token;
    public Identifier Name;
    public Expression Value;
    public LetStatement(Token token, Identifier identifier, Expression expression)
    {
      this.Token = token;
      this.Name = identifier;
      this.Value = expression;
    }
   

    public void StatementNode()
    {
      throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
      return Token.Literal;
    }

    public string String()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(TokenLiteral());
      sb.Append(" ");
      sb.Append(Name.String());
      sb.Append(" = ");
      if (Value != null)
        sb.Append(Value.String());
      sb.Append(";");

      return sb.ToString();
    }
  }

  public struct ReturnStatement : Statement
  {
    public Token Token;
    public Expression ReturnValue;
    public ReturnStatement(Token t) => Token = t;
    public void StatementNode() => throw new NotImplementedException();
    public string TokenLiteral() => Token.Literal;

    public string String()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(TokenLiteral());
      sb.Append(" ");
      if (ReturnValue != null)
        sb.Append(ReturnValue.String());
      sb.Append(";");
      return sb.ToString();
    }
  }
  public struct Identifier : Expression
  {
    public Token Token;
    public string Value;

    public Identifier(Token token, string value)
    {
      this.Token = token;
      this.Value = value;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
      return Token.Literal;
    }

    public string String()
    {
      return Value;
    }
  }

}

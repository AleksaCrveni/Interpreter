namespace Interpreter
{
  /// <summary>
  /* Examples of expressions
   * ******* PREFIX ********
   * -5
   * !true
   * !false
   * ******* INFIX *********
   * 5 + 5 etc
   * ******* Arihmetic *****
   * foo == bar
   * foo != bar
   * foo < bar
   * foo > bar
   * ******* GroupExpressions *******
   * 5 * (5 + 5)
   * ((5 + 5) * 5) * 5
   * ******** Call Expressions ******
   * add(2, 3)
   * add(add(2,3), add(5, 30))
   * max(5, add(5, (5 * 5)))
   * ******* Identifiers ********
   * foo * bar / foobar
   * add(foo, bar)
   * 
   * ******* FUCNCTION LITERALS *******
   * let add = fn(x, y) { return x + y }; 
   * And here we use a function literal in place of an identifier:
   * fn(x, y) { return x + y }(5, 5) (fn(x) { return x }(5) + 10 ) * 10 
   * In contrast to a lot of widely used programming languages we also have “if expressions” in Monkey:  
   * let result = if (10 > 5) { true } else { false }; result // => true
   */
  /// </summary>
  public struct ExpressionStatement : Statement
  {
    public Token Token;
    public Expression Expression;

    public ExpressionStatement(Token t) => Token = t;


    public void StatementNode() => throw new NotImplementedException();
    public string TokenLiteral() => Token.Literal;
    public string String()
    {
      if (Expression != null)
        return Expression.String();

      return "";
    }
  }
}

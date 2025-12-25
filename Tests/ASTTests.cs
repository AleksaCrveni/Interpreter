namespace Tests
{
  [TestClass]
  public class ASTTests
  {
    [TestMethod]
    public void TestString()
    {
      Program p = new Program();
      p._statements.Add
      (
        new LetStatement(
          new Token(TokenType.LET, "let"),
          new Identifier(
            new Token(TokenType.IDENT, "myVar"), "myVar"),
          new Identifier(
            new (TokenType.IDENT, "anotherVar"), "anotherVar"))
      );
      Assert.AreEqual("let myVar = anotherVar;", p.String(), $"Program.String() wrong, got {p.String()}");

    }
  }
}

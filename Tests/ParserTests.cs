using System.Diagnostics;

namespace Tests
{
  [TestClass]
  public class ParserTests
  {
    [TestMethod]
    public void TestLetStatementsSuccess()
    {
      string input = "let x = 5; let y = 10; let foobar = 838383;";
      Lexer l = new Lexer(input);
      Parser p = new Parser(l);

      Program program = p.ParseProgram();
      CheckParserErrors(p._errors);
      if (p._errors.Count > 0)
        Assert.Fail();
      Assert.IsNotNull(program, "Parse program returned null!");
      Assert.AreEqual(3, program._statements.Count, $"Expected 3 statements, got {program._statements.Count}");

      string[] tests = ["x", "y", "foobar"];
      for (int i = 0; i < tests.Length; i++)
        TestLetStatement(program._statements[i], tests[i]);

    }

    [TestMethod]
    public void TestLetStatementsFail()
    {
      string input = "let x 5; let = 10; let 838383;";
      Lexer l = new Lexer(input);
      Parser p = new Parser(l);

      Program program = p.ParseProgram();
      CheckParserErrors(p._errors);
      Assert.IsTrue(p._errors.Count > 0);
    }

    [TestMethod]
    public void TestReturnStatementsSuccess()
    {
      string input = "return 5;return 10; return 993213;";
      Lexer l = new Lexer(input);
      Parser p = new Parser(l);

      Program program = p.ParseProgram();
      CheckParserErrors(p._errors);
      if (p._errors.Count > 0)
        Assert.Fail();
      Assert.IsNotNull(program, "Parse program returned null!");
      Assert.AreEqual(3, program._statements.Count, $"Expected 3 statements, got {program._statements.Count}");

      foreach (Statement s in program._statements)
      {
        Assert.AreEqual("return", s.TokenLiteral(), $"Statement tokenLiteral is not 'return', got {s.TokenLiteral()}");
        Assert.IsInstanceOfType(s, typeof(ReturnStatement), "Statement is not ReturnStatement");
      }
    }

    public void TestLetStatement(Statement s, string name)
    {
      Assert.AreEqual("let", s.TokenLiteral(), $"Statement tokenLiteral is not 'let', got {s.TokenLiteral()}");

      Assert.IsInstanceOfType(s, typeof(LetStatement) ,"Statement is not Let Statement");
      LetStatement letStatement = (LetStatement)s;
      Assert.AreEqual(name, letStatement.Name.Value, $"letStatement.name.value not {name}, got {letStatement.Name.Value}");
      Assert.AreEqual(name, letStatement.Name.TokenLiteral(), $"letStatement.name.TokenLiteral() not {name}, got {letStatement.Name.TokenLiteral()}");
    }

    public void CheckParserErrors(List<string> errors)
    {
      if (errors.Count == 0)
        return;

      Trace.WriteLine($"Parser has {errors.Count} errors");
      foreach (string err in errors)
        Trace.WriteLine(err);
      
    }
  }
}

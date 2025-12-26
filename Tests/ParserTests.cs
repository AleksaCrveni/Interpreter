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
    [TestMethod]
    public void TestIdentifierExpression()
    {
      string input = "foobar";
      Lexer l = new Lexer(input);
      Parser p = new Parser(l);

      Program program = p.ParseProgram();
      CheckParserErrors(p._errors);

      Assert.AreEqual(1, program._statements.Count, $"Program has wrong number of statements, got {program._statements.Count}");
      Assert.IsInstanceOfType(program._statements[0], typeof(ExpressionStatement), $"Expected ExpressionStatement, got {typeof(ExpressionStatement)}");
      ExpressionStatement s = (ExpressionStatement)program._statements[0];

      Assert.IsInstanceOfType(s.Expression, typeof(Identifier), $"Expected Identifier, got {typeof(Identifier)}");
      Identifier i = (Identifier)s.Expression;

      Assert.AreEqual("foobar", i.Value, $"Ident.Value not foobar, got {i.Value}");
      Assert.AreEqual("foobar", i.TokenLiteral(), $"Ident.TokenLiteral() not foobar, got {i.TokenLiteral()}");
    }
    [TestMethod]
    public void TextIntLiteralExpression()
    {
      string input = "5;";
      Lexer l = new Lexer(input);
      Parser p = new Parser(l);

      Program program = p.ParseProgram();
      CheckParserErrors(p._errors);

      Assert.AreEqual(1, program._statements.Count, $"Program has wrong number of statements, got {program._statements.Count}");
      Assert.IsInstanceOfType(program._statements[0], typeof(ExpressionStatement), $"Expected ExpressionStatement, got {typeof(ExpressionStatement)}");
      ExpressionStatement s = (ExpressionStatement)program._statements[0];

      Assert.IsInstanceOfType(s.Expression, typeof(IntegerLiteral), $"Expected IntegerLiteral, got {typeof(IntegerLiteral)}");
      IntegerLiteral i = (IntegerLiteral)s.Expression;
      Assert.AreEqual(5, i.Value, $"IntegerLiteral.Value not 5, got {i.Value}");
      Assert.AreEqual("5", i.TokenLiteral(), $"IntegerLiteral.TokenLiteral() not 5, got {i.TokenLiteral()}");
    }

    struct PrefixTest
    {
      public string Input;
      public string Operator;
      public long IntegerValue;
    }
    [TestMethod]
    public void TestPrefixExpression()
    {
      // <prefix operator><expression>
      PrefixTest[] tests =
        [
          new PrefixTest() {
            Input = "!5;",
            Operator = "!",
            IntegerValue = 5
          },
          new PrefixTest() {
            Input = "-15;",
            Operator = "-",
            IntegerValue = 15
          }
        ];

      foreach (PrefixTest t in tests)
      {
        Lexer l = new Lexer(t.Input);
        Parser p = new Parser(l);

        Interpreter.Program program = p.ParseProgram();
        CheckParserErrors(p._errors);

        Assert.AreEqual(1, program._statements.Count, $"Program has wrong number of statements, got {program._statements.Count}");
        Assert.IsInstanceOfType(program._statements[0], typeof(ExpressionStatement), $"Expected ExpressionStatement, got {typeof(ExpressionStatement)}");
        ExpressionStatement s = (ExpressionStatement)program._statements[0];

        Assert.IsInstanceOfType(s.Expression, typeof(PrefixExpression), $"Expected PrefixExpression, got {typeof(PrefixExpression)}");
        PrefixExpression i = (PrefixExpression)s.Expression;

        
        Assert.AreEqual(t.Operator, i.Operator, $"Operator not {t.Operator}, got {i.Operator}");
        TestIntegerLiteral(i.Right, t.IntegerValue);

      }
    }

    public void TestIntegerLiteral(Expression right, long value)
    {
      Assert.IsInstanceOfType(right, typeof(IntegerLiteral), $"Expected IntegerLiteral, got {typeof(IntegerLiteral)}");
      IntegerLiteral i = (IntegerLiteral)right;
      Assert.AreEqual(value, i.Value, $"IntegerLiteral.Value not {value}, got {i.Value}");
      Assert.AreEqual(value.ToString(), i.TokenLiteral(), $"IntegerLiteral.TokenLiteral not {value}, got {i.TokenLiteral()}");
    }
  }
}

using Dioxide.ExpressionEvaluator.Evaluation;
using Dioxide.ExpressionEvaluator.Exceptions;
using Xunit;

namespace Dioxide.ExpressionEvaluator.Tests
{
    public class TestTwo
    {
        [Fact]
        public void T1()
        {
            var parser = new Parser();
            var context = new Context();

            var node = parser.Parse("33.0 - 88.0 * (12.0 + 3.0 - 10.0) / 100.0 + 88.0 + 10.0");
            var value = node.Eval(context);

            var fact = new decimal(33.0 - 88.0 * (12.0 + 3.0 - 10.0) / 100.0 + 88.0 + 10.0);
            Assert.Equal(fact, value);
        }


        [Fact]
        public void T2()
        {
            var parser = new Parser();
            var context = new Context()
                .AddFunction("testA", args => 14)
                .AddFunction("testB", args => 7)
                ;

            var node = parser.Parse("   testA()  /   testB()");
            var value = node.Eval(context);

            Assert.Equal(2, value);
        }

        [Fact]
        public void T3()
        {
            var parser = new Parser();
            var context = new Context()
                .AddFunction("testA", args => args[0])
                .AddFunction("testB", args => args[0])
                ;

            var node = parser.Parse("   testA(10)  /   testB(20)");
            var value = node.Eval(context);

            Assert.Equal(0.5m, value);
        }

        [Fact]
        public void T4()
        {
            var parser = new Parser();
            var context = new Context()
                .AddFunction("testa", args => args[0] - 10 + args[1])
                .AddFunction("testb", args => args[0])
                .AddVariable("f", 2)
                ;

            var node = parser.Parse("   testA(10  , 30.5)  /   testB(F)");
            var value = node.Eval(context);

            Assert.Equal(15.25m, value);
        }

        [Fact]
        public void T5()
        {
            var parser = new Parser();
            var context = new Context()
                .AddFunction("f1", args => 1 * args[0])
                .AddFunction("f2", args => 2 * args[0])
                .AddFunction("f3", args => 3 * args[0])
                .AddVariable("a", 10)
                .AddVariable("b", 20)
                .AddVariable("c", 30)
                .AddVariable("d", 40)
                .AddVariable("e", 50)
                .AddVariable("f", 60)
                .AddVariable("longValue", 100)
                ;

            var node = parser.Parse("f1(f2(f3(a+b+c---d/e*f))) * longValue");
            var value = node.Eval(context);
            var fact = new decimal((1.0 * (2.0 * (3.0 * (10.0 + 20.0 + 30.0 - 40.0 / 50.0 * 60.0)))) * 100);
            Assert.Equal(fact, value);
        }

        [Fact]
        public void T6()
        {
            var parser = new Parser();
            var context = new Context();

            var node = parser.Parse("-102.98");
            var value = node.Eval(context);

            Assert.Equal(-102.98m, value);
        }

        [Fact]
        public void T7()
        {
            var parser = new Parser();
            var context = new Context()
                .AddFunction("f", args => 1);

            var node = parser.Parse("f(f(100))");

            Assert.Throws<RecursiveFunctionCallException>(() => node.Eval(context));
        }

        [Theory]
        [InlineData("100*")]
        [InlineData("100)")]
        [InlineData("100---")]
        public void T8(string expression)
        {
            var parser = new Parser();

            Assert.Throws<SyntaxException>(() => parser.Parse(expression));
        }

        [Theory]
        [InlineData("functionX(100)")]
        public void T9(string expression)
        {
            var parser = new Parser();
            var context = new Context();
            var node = parser.Parse(expression);

            Assert.Throws<UnknownFunctionException>(() => node.Eval(context));
        }

        [Fact]
        public void T10()
        {
            var parser = new Parser();
            var context = new Context();
            var node = parser.Parse("var1 + var2 - var3");

            Assert.Throws<UnknownVariableException>(() => node.Eval(context));
        }
    }
}

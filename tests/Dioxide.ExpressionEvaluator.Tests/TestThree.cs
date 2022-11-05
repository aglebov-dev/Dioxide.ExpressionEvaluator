using System;
using Dioxide.ExpressionEvaluator.Exceptions;
using Xunit;

namespace Dioxide.ExpressionEvaluator.Tests
{
    public class TestThree
    {
        [Fact]
        public void T1()
        {
            IExpressionEvaluator evaluator = new ExpressionEvaluator();
            var value = evaluator.Calculate("33.0 - 88.0 * (12.0 + 3.0 - 10.0) / 100.0 + 88.0 + 10.0");

            var fact = 33.0 - 88.0 * (12.0 + 3.0 - 10.0) / 100.0 + 88.0 + 10.0;
            Assert.Equal(fact, value);
        }


        [Fact]
        public void T2()
        {
            var evaluator = new ExpressionEvaluator()
                .AddFunction("testA", args => 14)
                .AddFunction("testB", args => 7)
                ;

            var value = evaluator.Calculate("   testA()  /   testB()");

            Assert.Equal(2, value);
        }

        [Fact]
        public void T3()
        {
            var evaluator = new ExpressionEvaluator()
                .AddFunction("testA", args => args[0])
                .AddFunction("testB", args => args[0])
                ;

            var value = evaluator.Calculate("   testA(10)  /   testB(20)");

            Assert.Equal(0.5, value);
        }

        [Fact]
        public void T4()
        {
            var evaluator = new ExpressionEvaluator()
                .AddFunction("testa", args => args[0] - 10 + args[1])
                .AddFunction("testb", args => args[0])
                .AddVariable("f", 2)
                ;

            var value = evaluator.Calculate("   testA(10  , 30.5)  /   testB(F)");

            Assert.Equal(15.25, value);
        }

        [Fact]
        public void T5()
        {
            var evaluator = new ExpressionEvaluator()
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

            var value = evaluator.Calculate("f1(f2(f3(a+b+c---d/e*f))) * longValue");

            var fact = (1.0 * (2.0 * (3.0 * (10.0 + 20.0 + 30.0 - 40.0 / 50.0 * 60.0)))) * 100;
            Assert.Equal(fact, value);
        }

        [Fact]
        public void T6()
        {
            var evaluator = new ExpressionEvaluator();

            var value = evaluator.Calculate("-102.98");

            Assert.Equal(-102.98, value);
        }

        [Fact]
        public void T7()
        {
            var evaluator = new ExpressionEvaluator()
                .AddFunction("f", args => 1);

            Assert.Throws<RecursiveFunctionCallException>(() => evaluator.Calculate("f(f(100))"));
        }

        [Theory]
        [InlineData("100*")]
        [InlineData("100)")]
        [InlineData("100---")]
        public void T8(string expression)
        {
            var evaluator = new ExpressionEvaluator();

            Assert.Throws<SyntaxException>(() => evaluator.Calculate(expression));
        }

        [Theory]
        [InlineData("functionX(100)")]
        public void T9(string expression)
        {
            var evaluator = new ExpressionEvaluator();

            Assert.Throws<UnknownFunctionException>(() => evaluator.Calculate(expression));
        }

        [Fact]
        public void T10()
        {
            var evaluator = new ExpressionEvaluator();

            Assert.Throws<UnknownVariableException>(() => evaluator.Calculate("var1 + var2 - var3"));
        }

        [Fact]
        public void T11()
        {
            var evaluator = new ExpressionEvaluator();

            var value = evaluator.Calculate("-102.98^3");

            Assert.Equal(Math.Pow(-102.98, 3), value);
        }

        [Fact]
        public void T12()
        {
            var evaluator = new ExpressionEvaluator();

            var value = evaluator.Calculate("2.48 + 3 ^ 3 * 2");

            Assert.Equal(2.48 + Math.Pow(3.0, 3.0) * 2.0, value);
        }

        [Fact]
        public void T13()
        {
            var evaluator = new ExpressionEvaluator()
                .AddFunction("FUNC_A", args => args[0] + 100)
                .AddFunction("FUNC_B", args => args[0] + args[1] + args[2])
                .AddVariable("VAR_F", 88.09)
                ;

            var value = evaluator.Calculate("-2.7^8 + func_a( 9*33.5+func_b(80, 3, 17.07) )"); //3325.8653648100017

            Assert.Equal(3325.8653648100017, value);
        }
    }
}

using SetsOperations;
using SetsOperations.Expressions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SetsOperationsTests
{
    public class SetsOperationsTests
    {
        [Theory]
        [InlineData("a+b", "1 2 3 4 5 6")]
        [InlineData("b + a", "1 2 3 4 5 6")]
        [InlineData("a/b", "3 4")]
        [InlineData("a-b", "1 2")]
        [InlineData("^a", "5 6")]
        [InlineData("^^a", "1 2 3 4")]
        public void StandardTests(string expression, string expectedResult)
        {
            var variables = new Dictionary<string, IEnumerable<string>>()
            {
                { "a", new List<string>() { "1", "2", "3", "4" } },
                { "b", new List<string>() { "3", "4", "5", "6" } }
            };

            var actualResult = Evaluator.EvaluateExpression(Expression.ReadExpression(Lexer.Tokenize(expression)), variables);
            Assert.Equal(Lexer.Tokenize(expectedResult).ToArray().OrderBy(a => a), actualResult.ToArray().OrderBy(a => a));
        }
    }
}

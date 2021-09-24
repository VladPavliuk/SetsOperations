using SetsOperations.Operators;
using System.Collections.Generic;
using System.Linq;

namespace SetsOperations.Expressions
{
    public abstract class Expression
    {
        public static Expression ReadExpression(IEnumerable<string> tokens)
        {
            int index = 0;
            return ReadExpression(tokens, ref index);
        }

        private static Expression ReadExpression(IEnumerable<string> tokens, ref int index)
        {
            return ReadDifference(tokens, ref index);
        }

        private static Expression ReadDifference(IEnumerable<string> tokens, ref int index)
        {
            var union = ReadUnion(tokens, ref index);

            while (GetToken(tokens, index) == "-")
            {
                index++;
                union = new BinaryExpression()
                {
                    Left = union,
                    Right = ReadUnion(tokens, ref index),
                    Operator = BinaryOperator.Difference
                };
            }

            return union;
        }

        private static Expression ReadUnion(IEnumerable<string> tokens, ref int index)
        {
            var intersection = ReadIntersection(tokens, ref index);

            while (GetToken(tokens, index) == "+")
            {
                index++;
                intersection = new BinaryExpression()
                {
                    Left = intersection,
                    Right = ReadIntersection(tokens, ref index),
                    Operator = BinaryOperator.Union
                };
            }

            return intersection;
        }

        private static Expression ReadIntersection(IEnumerable<string> tokens, ref int index)
        {
            var negation = ReadNegation(tokens, ref index);

            while (GetToken(tokens, index) == "/")
            {
                index++;
                negation = new BinaryExpression()
                {
                    Left = negation,
                    Right = ReadNegation(tokens, ref index),
                    Operator = BinaryOperator.Intersection
                };
            }

            return negation;
        }

        private static Expression ReadNegation(IEnumerable<string> tokens, ref int index)
        {
            if (GetToken(tokens, index) == "^")
            {
                index++;
                var value = ReadNegation(tokens, ref index);
                return new UnaryExpression()
                {
                    Value = value,
                    Operator = UnaryOperator.Negation
                };
            }

            return ReadParenthesis(tokens, ref index);
        }

        private static Expression ReadParenthesis(IEnumerable<string> tokens, ref int index)
        {
            if (GetToken(tokens, index) == "(")
            {
                index++;
                var expression = ReadExpression(tokens, ref index);
                index++;
                return expression;
            }

            return ReadValue(tokens, ref index);
        }

        private static Expression ReadValue(IEnumerable<string> tokens, ref int index)
        {
            return new ValueExpression()
            {
                Value = tokens.ElementAt(index++)
            };
        }

        private static string GetToken(IEnumerable<string> tokens, int index)
        {
            return tokens.ElementAtOrDefault(index);
        }
    }
}

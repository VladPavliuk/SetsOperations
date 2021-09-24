using SetsOperations.Expressions;
using SetsOperations.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SetsOperations
{
    public static class Evaluator
    {
        public static IEnumerable<string> EvaluateExpression(Expression expression, IDictionary<string, IEnumerable<string>> variables)
        {
            var universalSet = variables.SelectMany(variable => variable.Value);

            return EvaluateExpression(expression, variables, universalSet).Distinct();
        }

        public static IEnumerable<string> EvaluateExpression(
            Expression expression,
            IDictionary<string, IEnumerable<string>> variables,
            IEnumerable<string> universalSet)
        {
            switch (expression)
            {
                case ValueExpression valueExpression:
                    return variables[valueExpression.Value];
                case UnaryExpression unaryExpression:
                    return EvaluateExpression(unaryExpression.Value, variables, universalSet).Negation(universalSet);
                case BinaryExpression binaryExpression:
                    {
                        var left = EvaluateExpression(binaryExpression.Left, variables, universalSet);
                        var right = EvaluateExpression(binaryExpression.Right, variables, universalSet);

                        switch (binaryExpression.Operator)
                        {
                            case BinaryOperator.Intersection: return left.Intersect(right);
                            case BinaryOperator.Union: return left.Union(right);
                            case BinaryOperator.Difference: return left.Difference(right);
                        }
                        break;
                    }
            }

            throw new Exception("FFFFF");
        }

        private static IEnumerable<string> Difference(this IEnumerable<string> a, IEnumerable<string> b)
        {
            return a.Where(item => !b.Contains(item));
        }

        private static IEnumerable<string> Negation(this IEnumerable<string> a, IEnumerable<string> universalSet)
        {
            return universalSet.Difference(a);
        }

    }
}

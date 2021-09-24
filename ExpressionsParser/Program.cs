using SetsOperations.Expressions;
using System;
using System.Collections.Generic;

namespace SetsOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            var variables = new Dictionary<string, IEnumerable<string>>();

            while (true)
            {
                Console.WriteLine("Set variable name (empty to write an expression):");
                var variableName = Console.ReadLine();

                if (string.IsNullOrEmpty(variableName))
                {
                    break;
                }

                Console.WriteLine("Set variable's value (with space as an elements separator):");
                var variableValue = Lexer.Tokenize(Console.ReadLine());
                
                variables.Add(variableName, variableValue);
            }

            while (true)
            {
                Console.WriteLine("Write an expression:");
                var tokens = Lexer.Tokenize(Console.ReadLine());

                try
                {
                    var expression = Expression.ReadExpression(tokens);
                    var result = Evaluator.EvaluateExpression(expression, variables);

                    Console.WriteLine(string.Join(" ", result));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}

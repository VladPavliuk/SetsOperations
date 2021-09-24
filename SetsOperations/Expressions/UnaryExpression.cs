using SetsOperations.Operators;

namespace SetsOperations.Expressions
{
    public class UnaryExpression : Expression
    {
        public Expression Value { get; set; }

        public UnaryOperator Operator { get; set; }
    }
}

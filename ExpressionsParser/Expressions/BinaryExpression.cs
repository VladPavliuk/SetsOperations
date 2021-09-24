using SetsOperations.Operators;

namespace SetsOperations.Expressions
{
    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public BinaryOperator Operator { get; set; }
    }
}

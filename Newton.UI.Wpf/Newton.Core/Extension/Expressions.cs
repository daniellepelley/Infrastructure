using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Pluto.Extensions
{
    public static class Expressions
    {
        /// <summary>
        /// Changes the expression type of the binary expression
        /// </summary>
        public static BinaryExpression ChangeType(this BinaryExpression expression, ExpressionType newExpressionType)
        {
            return Pluto.Expressions.Converter.ChangeType(expression, newExpressionType);
        }

        /// <summary>
        /// Changes the expression on the left hand side of the binary expression.
        /// </summary>
        public static BinaryExpression ChangeLeft(this BinaryExpression expression, Expression left)
        {
            return Pluto.Expressions.Converter.ChangeLeft(expression, left);
        }

        /// <summary>
        /// Changes the expression on the right hand side of the binary expression.
        /// </summary>
        public static BinaryExpression ChangeRight(this BinaryExpression expression, Expression right)
        {
            return Pluto.Expressions.Converter.ChangeRight(expression, right);
        }
    }
}

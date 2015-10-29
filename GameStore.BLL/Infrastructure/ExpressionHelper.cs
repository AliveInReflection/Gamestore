using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Infrastructure
{
    public static class ExpressionHelper
    {
        public static Expression<Func<Game, bool>> CombineOr(this Expression<Func<Game, bool>> sourceExpression, Expression<Func<Game, bool>> otherExpression)
        {
            var parameter = Expression.Parameter(typeof(Game));

            var leftVisitor = new ReplaceExpressionVisitor(sourceExpression.Parameters[0], parameter);
            var left = leftVisitor.Visit(sourceExpression.Body);

            var rightVisitor = new ReplaceExpressionVisitor(otherExpression.Parameters[0], parameter);
            var right = rightVisitor.Visit(otherExpression.Body);

            return Expression.Lambda<Func<Game, bool>>(
                Expression.OrElse(left, right), parameter);
        }
        public static Expression<Func<Game, bool>> CombineAnd(this Expression<Func<Game, bool>> sourceExpression, Expression<Func<Game, bool>> otherExpression)
        {
            var parameter = Expression.Parameter(typeof(Game));

            var leftVisitor = new ReplaceExpressionVisitor(sourceExpression.Parameters[0], parameter);
            var left = leftVisitor.Visit(sourceExpression.Body);

            var rightVisitor = new ReplaceExpressionVisitor(otherExpression.Parameters[0], parameter);
            var right = rightVisitor.Visit(otherExpression.Body);

            return Expression.Lambda<Func<Game, bool>>(
                Expression.AndAlso(left, right), parameter);
        }
    }

    public class ReplaceExpressionVisitor
        : ExpressionVisitor
    {
        private readonly Expression oldValue;
        private readonly Expression newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == oldValue)
                return newValue;
            return base.Visit(node);
        }
    }
}

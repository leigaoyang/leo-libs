using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Lif.Common.Extensions
{
    public static class ExpressionEx
    {
        #region 条件拼接
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
        #endregion


        #region 转为Path字符串
        private static readonly Regex _removeSelect = new Regex(@"\.Select\s*\(\s*\w+\s*=>\s*\w+\.", RegexOptions.Compiled);
        private static readonly Regex _removeSelectEnd = new Regex(@"\.Select\s*\(\s*\w+\s*=>\s*\w+", RegexOptions.Compiled);
        private static readonly Regex _removeList = new Regex(@"\.get_Item\(\d+\)", RegexOptions.Compiled);
        private static readonly Regex _removeListEnd = new Regex(@"\.get_Item\(\d+", RegexOptions.Compiled);
        private static readonly Regex _removeArray = new Regex(@"\[\d+\]", RegexOptions.Compiled);

        public static string GetPath(this Expression expr)
        {
            while (expr.NodeType == ExpressionType.Convert || expr.NodeType == ExpressionType.ConvertChecked)
            {
                expr = ((UnaryExpression)expr).Operand;
            }

            // if is a method call, get first
            while (expr.NodeType == ExpressionType.Lambda)
            {
                if (((LambdaExpression)expr).Body is UnaryExpression unary)
                {
                    expr = unary.Operand;
                }
                else
                {
                    break;
                }
            }

            var str = expr.ToString(); // gives you: "o => o.Whatever"
            var firstDelim = str.IndexOf('.'); // make sure there is a beginning property indicator; the "." in "o.Whatever" -- this may not be necessary?

            var path = firstDelim < 0 ? str : str.Substring(firstDelim + 1).TrimEnd(')');

            path = _removeList.Replace(path, "");
            path = _removeListEnd.Replace(path, ".");
            path = _removeArray.Replace(path, "");
            path = _removeSelect.Replace(path, ".")
                .Replace(")", "");
            path = _removeSelectEnd.Replace(path, ".");

            return path;
        }
        #endregion
    }

    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }
}

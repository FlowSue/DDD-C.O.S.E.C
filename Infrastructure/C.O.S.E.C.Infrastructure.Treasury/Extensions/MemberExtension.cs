//系统包

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace C.O.S.E.C
{
    public static class MemberExtension
    {
        /// <summary>Removes the unary.</summary>
        /// <param name="toUnwrap">To unwrap.</param>
        /// <returns>MemberExpression.</returns>
        private static MemberExpression RemoveUnary(Expression toUnwrap)
        {
            return toUnwrap is UnaryExpression expression ? expression.Operand as MemberExpression : toUnwrap as MemberExpression;
        }

        /// <summary>Gets the member.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty">The type of the t property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>MemberInfo.</returns>
        public static MemberInfo GetMember<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return RemoveUnary(expression.Body)?.Member;
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ideas.DataAccess.EFHelpers
{
    /// <summary>
    /// Handles custom entity materialization settings.
    /// <para/>- Specifies all DateTime values as Utc kind
    /// </summary>
    internal class CustomEntityMaterializerSource : EntityMaterializerSource
    {
        private static readonly MethodInfo NormalizeMethod = typeof(CustomEntityMaterializerSource).GetTypeInfo().GetMethod(nameof(CustomEntityMaterializerSource.Normalize), BindingFlags.NonPublic | BindingFlags.Static);

        public override Expression CreateReadValueExpression(Expression valueBuffer, Type type, int index, IProperty property = null)
        {
            if (type == typeof(DateTime))
            {
                return Expression.Call(
                    NormalizeMethod,
                    base.CreateReadValueExpression(valueBuffer, type, index, property)
                );
            }

            return base.CreateReadValueExpression(valueBuffer, type, index, property);
        }

        private static DateTime Normalize(DateTime value)
        {
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}

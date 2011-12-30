using System;
using System.Linq.Expressions;
using Catnap.Database;
using Catnap.Mapping;

namespace Catnap.Find.Conditions
{
    public abstract class PropertyCondition<T> : IConditionMarker where T : class, new()
    {
        private readonly Expression<Func<T, object>> property;
        private readonly string format;
        private readonly object value;

        protected PropertyCondition(Expression<Func<T, object>> property, string format, object value) 
        {
            this.property = property;
            this.format = format;
            this.value = value;
        }

        public Parameter ToParameter(string paramterName)
        {
            return new Parameter(paramterName, value);
        }

        public string ToSql(IEntityMap<T> entityMap, string parameterName)
        {
            var columnName = entityMap.GetColumnNameForProperty(property);
            return string.Format("({0})", string.Format(format, columnName, parameterName));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Linq.Dynamic;
using System.Reflection;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public class DefaultFilterTranslator : IFilterTranslator
    {
        public virtual FilterInfo Translate<T>(SearchFilter searchFilter)
        {
            FilterInfo filter = null;
            StringBuilder predicate = new StringBuilder();
            List<object> values = new List<object>();
            for (int i = 0; i < searchFilter.rules.Length; i++)
            {
                var r = searchFilter.rules[i];

                object value;
                PropertyInfo prop = GetColumnPropertyInfo<T>(r.FieldName);
                if (prop == null)
                    continue;
                string expression = GetExpression(prop, r, i, out value);
                predicate.Append(expression);
                values.Add(value);

                if (i < searchFilter.rules.Length - 1)
                    predicate.AppendFormat(" {0} ", searchFilter.Operator);
            }

            var filterString = predicate.ToString();
            if (!string.IsNullOrEmpty(filterString))
            {
                filter = new FilterInfo()
                {
                    Predicate = filterString,
                    ParameterValues = values.ToArray()
                };
            }
            return filter;
        }

        protected virtual string GetExpression(PropertyInfo prop, SearchFilter.SearchFilterItem filterItem, int filterIndex, out object value)
        {
            var filterItemMetadata = new FilterItemMetadata()
           {
               PropertyInfo = prop,
               FilterItem = filterItem,
               FilterIndex = filterIndex,
           };
            var expressionBuilder = ServiceLocator.Current.Resolve<IFilterExpressionProvider>().GetExpressionBuilder(filterItemMetadata);
            return expressionBuilder.BuildExpression(out value);
        }

        private static PropertyInfo GetColumnPropertyInfo<T>(string columnName)
        {
            Type columnType = typeof(T);
            PropertyInfo foundProperty = null;

            string[] propertyHeirarch = columnName.Split('.');

            foreach (string prop in propertyHeirarch)
            {
                PropertyInfo[] props = columnType.GetProperties();

                var query = from p in props
                            where p.Name == prop
                            select p;

                foundProperty = query.FirstOrDefault();
                if (foundProperty == null) return null;
                columnType = foundProperty.PropertyType;
            }
            return foundProperty;
        }

        private bool IsNonNumericOperator(SearchFilter.SearchFilterItemOperator op)
        {
            return op == SearchFilter.SearchFilterItemOperator.BeginWith
                || op == SearchFilter.SearchFilterItemOperator.EndWith
                || op == SearchFilter.SearchFilterItemOperator.Contain
                || op == SearchFilter.SearchFilterItemOperator.NotBeginWith
                || op == SearchFilter.SearchFilterItemOperator.NotEndWith
                || op == SearchFilter.SearchFilterItemOperator.NotContain;
        }

        private bool IsNumericType(Type t)
        {
            string name = t.Name;
            switch (name)
            {
                case "Int16": return true;
                case "Int32": return true;
                case "Int64": return true;
                case "UInt16": return true;
                case "UInt32": return true;
                case "UInt64": return true;
                case "Double": return true;
                case "Single": return true;
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Mehr.Web.Mvc.JqGrid
{
    public class SearchFilter
    {
        public string Operator
        {
            get
            {
                if (groupOp == "AND") return "&&";
                if (groupOp == "OR") return "||";
                throw new InvalidOperationException();
            }
        }

        public string groupOp;
        public SearchFilterItem[] rules;
        public class SearchFilterItem
        {
            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public string field;
            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public string op;
            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public string data;

            public string FieldName { get { return field.Replace('_', '.'); } }

            public string GetExpression(int index, string shapedFieldNameFormat = null)
            {

                var shapedFieldName = string.Format(shapedFieldNameFormat ?? "{0}", FieldName);
                return string.Format(ExpressionTemplate, shapedFieldName, index);
            }

            public string ExpressionTemplate
            {
                get
                {
                    switch (Operator)
                    {
                        case SearchFilterItemOperator.Equal: return "{0}==@{1}";
                        case SearchFilterItemOperator.NotEqual: return "{0}!=@{1}";
                        case SearchFilterItemOperator.Less: return "{0}<@{1}";
                        case SearchFilterItemOperator.LessOrEqual: return "{0}<=@{1}";
                        case SearchFilterItemOperator.Greater: return "{0}>@{1}";
                        case SearchFilterItemOperator.GreaterOrEqual: return "{0}>=@{1}";
                        case SearchFilterItemOperator.BeginWith: return "{0}.StartsWith(@{1})";
                        case SearchFilterItemOperator.NotBeginWith: return "!{0}.StartsWith(@{1})";
                        // case SearchFilterItemOperator.In: return "";
                        // case SearchFilterItemOperator.NotIn: return "";
                        case SearchFilterItemOperator.EndWith: return "{0}.EndsWith(@{1})";
                        case SearchFilterItemOperator.NotEndWith: return "!{0}.EndsWith(@{1})";
                        case SearchFilterItemOperator.Contain: return "{0}.Contains(@{1})";
                        case SearchFilterItemOperator.NotContain: return "!{0}.Contains(@{1})";
                    }
                    throw new InvalidOperationException(Operator.ToString());
                }
            }

            public SearchFilterItemOperator Operator
            {
                get
                {
                    switch (op)
                    {
                        case "eq": return SearchFilterItemOperator.Equal;
                        case "ne": return SearchFilterItemOperator.NotEqual;
                        case "lt": return SearchFilterItemOperator.Less;
                        case "le": return SearchFilterItemOperator.LessOrEqual;
                        case "gt": return SearchFilterItemOperator.Greater;
                        case "ge": return SearchFilterItemOperator.GreaterOrEqual;
                        case "bw": return SearchFilterItemOperator.BeginWith;
                        case "bn": return SearchFilterItemOperator.NotBeginWith;
                        case "in": return SearchFilterItemOperator.In;
                        case "ni": return SearchFilterItemOperator.NotIn;
                        case "ew": return SearchFilterItemOperator.EndWith;
                        case "en": return SearchFilterItemOperator.NotEndWith;
                        case "cn": return SearchFilterItemOperator.Contain;
                        case "nc": return SearchFilterItemOperator.NotContain;
                    }
                    throw new NotSupportedException();
                }
            }

        }

        public enum SearchFilterItemOperator
        {
            Equal, NotEqual, Less, LessOrEqual, Greater, GreaterOrEqual,
            BeginWith, NotBeginWith, In, NotIn, EndWith, NotEndWith,
            Contain, NotContain
        }
    }
}

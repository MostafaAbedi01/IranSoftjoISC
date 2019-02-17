using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Metadata.Edm;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Data.Objects;

namespace System.Linq
{
    //http://blogs.msdn.com/b/alexj/archive/2009/09/17/tip-35-how-to-write-oftypeonly-tentity.aspx
    public static class EntityFrameworkTip35
    {
        public static IQueryable<TEntity> OfTypeOnly<TEntity>(this ObjectQuery query)
        {
            query.CheckArgumentNotNull("query");

            // Get the C-Space EntityType
            var queryable = query as IQueryable;
            var wkspace = query.Context.MetadataWorkspace;
            var elementType = typeof(TEntity);

            // Filter
            IQueryable<TEntity> filter = query.OfType<TEntity>();

            // See if there are any derived types of TDerivedEntity
            EntityType cspaceEntityType = wkspace.GetCSpaceEntityType(elementType);

            if (cspaceEntityType == null)
                throw new NotSupportedException("Unable to find C-Space type");

            EntityType[] subTypes = wkspace.GetImmediateDescendants(cspaceEntityType).ToArray();

            if (subTypes.Length == 0) return filter;

            // Get the CLRTypes.
            Type[] clrTypes = subTypes.Select(st => wkspace.GetClrTypeName(st))
                                    .Select(tn => elementType.Assembly.GetType(tn))
                                    .ToArray();

            // Need to build the !(a is type1) && !(a is type2) predicate and call it 
            // via the provider
            var lambda = GetIsNotOfTypePredicate(elementType, clrTypes);
            return filter.Where(lambda as Expression<Func<TEntity, bool>>);
        }
        public static EntityType GetCSpaceEntityType(this MetadataWorkspace workspace, Type type)
        {
            workspace.CheckArgumentNotNull("workspace");
            // Make sure the metadata for this assembly is loaded.
            workspace.LoadFromAssembly(type.Assembly);
            // Try to get the ospace type and if that is found 
            // look for the cspace type too. 
            EntityType ospaceEntityType = null;
            StructuralType cspaceEntityType = null;
            if (workspace.TryGetItem<EntityType>(
                type.FullName,
                DataSpace.OSpace,
                out ospaceEntityType))
            {
                if (workspace.TryGetEdmSpaceType(
                    ospaceEntityType,
                    out cspaceEntityType))
                {
                    return cspaceEntityType as EntityType;
                }
            }
            return null;
        }
        public static IEnumerable<EntityType> GetImmediateDescendants(this MetadataWorkspace workspace, EntityType entityType)
        {
            foreach (var dtype in workspace.GetItemCollection(DataSpace.CSpace)
                                            .GetItems<EntityType>()
                                            .Where(e => e.BaseType != null && e.BaseType.FullName == entityType.FullName))
            {
                yield return dtype;
            }
        }
        public static string GetClrTypeName(this MetadataWorkspace workspace, EntityType cspaceEntityType)
        {
            StructuralType ospaceEntityType = null;

            if (workspace.TryGetObjectSpaceType(cspaceEntityType, out ospaceEntityType))
                return ospaceEntityType.FullName;
            else
                throw new Exception("Unexpected Exception");
        }
        public static LambdaExpression GetIsNotOfTypePredicate(Type parameterType, params Type[] clrTypes)
        {
            ParameterExpression predicateParam = Expression.Parameter(parameterType, "parameter");
            return Expression.Lambda(predicateParam.IsNot(clrTypes), predicateParam);
        }
        public static Expression IsNot(this ParameterExpression parameter, params Type[] types)
        {
            types.CheckArgumentNotNull("types");
            types.CheckArrayNotEmpty("types");

            Expression merged = parameter.IsNot(types[0]);
            for (int i = 1; i < types.Length; i++)
            {
                merged = Expression.AndAlso(merged, parameter.IsNot(types[i]));
            }
            return merged;
        }
        public static Expression IsNot(this ParameterExpression parameter, Type type)
        {
            type.CheckArgumentNotNull("type");

            var parameterIs = Expression.TypeIs(parameter, type);
            var parameterIsNot = Expression.Not(parameterIs);
            return parameterIsNot;
        }
        public static void CheckArgumentNotNull<TArg>(this TArg arg, string argumentName) where TArg : class
        {
            if (arg != null) return;
            StackTrace trace = new StackTrace(1);
            var method = trace.GetFrame(0).GetMethod();
            var exceptionMessage = string.Format("{0}.{1} expects '{2}' to be non-null", method.DeclaringType.Name, method.Name, argumentName);
            throw new ArgumentNullException(exceptionMessage);
        }
        public static void CheckArrayNotEmpty<TArray>(this TArray[] array, string argumentName)
        {
            if (array.Length > 0) return;
            StackTrace trace = new StackTrace(1);
            var method = trace.GetFrame(0).GetMethod();
            var exceptionMessage = string.Format("{0}.{1} expects '{2}' to not be empty", method.DeclaringType.Name, method.Name, argumentName);
            throw new ArgumentNullException(exceptionMessage);
        }
    }
}

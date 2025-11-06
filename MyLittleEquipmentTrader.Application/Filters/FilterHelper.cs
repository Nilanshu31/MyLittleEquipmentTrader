using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyLittleEquipmentTrader.Application.Helpers
{
    public static class FilterHelper
    {
        // Apply filters for Product, Manufacturer, SalesOrder, etc.
        public static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, List<FilterCriteria> filters)
        {
            if (filters == null || filters.Count == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            Expression? combined = null;

            foreach (var filter in filters)
            {
                var predicate = BuildPredicate(parameter, filter);
                if (predicate == null)
                    continue;

                if (combined == null)
                {
                    combined = predicate;
                }
                else
                {
                    if (string.Equals(filter.LogicalOperator, "OR", StringComparison.OrdinalIgnoreCase))
                        combined = Expression.OrElse(combined, predicate);
                    else
                        combined = Expression.AndAlso(combined, predicate);
                }
            }

            if (combined == null)
                return query;

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            return query.Where(lambda);
        }

        // Build predicate for each entity type
        private static Expression? BuildPredicate(ParameterExpression parameter, FilterCriteria filter)
        {
            try
            {
                // ====== Handle Product Attributes ======
                if (filter.Field.StartsWith("productAttributes.", StringComparison.OrdinalIgnoreCase))
                {
                    var fieldName = filter.Field.Split('.')[1];
                    var productAttributesProperty = Expression.Property(parameter, nameof(Product.ProductAttributes));
                    var attrParam = Expression.Parameter(typeof(ProductAttribute), "attr");
                    var attrProperty = Expression.PropertyOrField(attrParam, fieldName);

                    var constant = Expression.Constant(filter.Value);
                    Expression? comparison = filter.Operator.ToLower() switch
                    {
                        "eq" => Expression.Equal(attrProperty, constant),
                        "neq" => Expression.NotEqual(attrProperty, constant),
                        "contains" => Expression.Call(attrProperty, nameof(string.Contains), null, constant),
                        _ => null
                    };

                    if (comparison == null) return null;

                    var lambda = Expression.Lambda<Func<ProductAttribute, bool>>(comparison, attrParam);
                    var anyMethod = typeof(Enumerable).GetMethods()
                        .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(ProductAttribute));

                    return Expression.Call(anyMethod, productAttributesProperty, lambda);
                }

                // ====== Handle Product Tags ======
                if (filter.Field.StartsWith("productTags.", StringComparison.OrdinalIgnoreCase))
                {
                    var fieldName = filter.Field.Split('.')[1];
                    var productTagsProperty = Expression.Property(parameter, nameof(Product.ProductTags));
                    var tagParam = Expression.Parameter(typeof(ProductTag), "tag");
                    var tagProperty = Expression.PropertyOrField(tagParam, fieldName);

                    var propertyInfo = (System.Reflection.PropertyInfo)tagProperty.Member;
                    object typedValue = Convert.ChangeType(filter.Value, propertyInfo.PropertyType);
                    var constant = Expression.Constant(typedValue);

                    Expression? comparison = filter.Operator.ToLower() switch
                    {
                        "eq" => Expression.Equal(tagProperty, constant),
                        "neq" => Expression.NotEqual(tagProperty, constant),
                        "lt" => Expression.LessThan(tagProperty, constant),
                        "lte" => Expression.LessThanOrEqual(tagProperty, constant),
                        "gt" => Expression.GreaterThan(tagProperty, constant),
                        "gte" => Expression.GreaterThanOrEqual(tagProperty, constant),
                        "contains" when propertyInfo.PropertyType == typeof(string) =>
                            Expression.Call(tagProperty, nameof(string.Contains), null, constant),
                        _ => null
                    };

                    if (comparison == null) return null;

                    var lambda = Expression.Lambda<Func<ProductTag, bool>>(comparison, tagParam);
                    var anyMethod = typeof(Enumerable).GetMethods()
                        .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(ProductTag));

                    return Expression.Call(anyMethod, productTagsProperty, lambda);
                }

                // ====== Product Entity ======
                if (parameter.Type == typeof(Product))
                    return BuildEntityPredicate(parameter, filter);

                // ====== Tenant Entity ======
                if (parameter.Type == typeof(Tenant))
                    return BuildEntityPredicate(parameter, filter);

                // ====== AccessRole Entity ======
                if (parameter.Type == typeof(AccessRole))
                    return BuildEntityPredicate(parameter, filter);

                // ====== Manufacturer Entity ======
                if (parameter.Type == typeof(Manufacturer))
                    return BuildEntityPredicate(parameter, filter);

                // ====== Plan Entity ======
                if (parameter.Type == typeof(Plan))
                    return BuildEntityPredicate(parameter, filter);

                // ====== 🆕 SalesOrder Entity ======
                if (parameter.Type == typeof(SalesOrder))
                    return BuildEntityPredicate(parameter, filter);

                if (parameter.Type == typeof(Category))
                    return BuildEntityPredicate(parameter, filter);

                if (parameter.Type == typeof(Subscription))
                    return BuildEntityPredicate(parameter, filter);

                if (parameter.Type == typeof(Report))
                    return BuildEntityPredicate(parameter, filter);
                if (parameter.Type == typeof(UserInfo))
                    return BuildEntityPredicate(parameter, filter);


                return null;
            }
            catch
            {
                return null;
            }
        }

        // Generic predicate builder (used by all entity types)
        private static Expression? BuildEntityPredicate(ParameterExpression parameter, FilterCriteria filter)
        {
            var property = Expression.Property(parameter, filter.Field);
            var propertyType = ((System.Reflection.PropertyInfo)property.Member).PropertyType;

            object typedValue = Convert.ChangeType(filter.Value, propertyType);
            var constantExpr = Expression.Constant(typedValue);

            return filter.Operator.ToLower() switch
            {
                "eq" => Expression.Equal(property, constantExpr),
                "neq" => Expression.NotEqual(property, constantExpr),
                "lt" => Expression.LessThan(property, constantExpr),
                "lte" => Expression.LessThanOrEqual(property, constantExpr),
                "gt" => Expression.GreaterThan(property, constantExpr),
                "gte" => Expression.GreaterThanOrEqual(property, constantExpr),
                "contains" when propertyType == typeof(string) =>
                    Expression.Call(property, nameof(string.Contains), null, constantExpr),
                _ => null
            };
        }

        // Sorting logic
        public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortBy, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda(property, parameter);

            var method = sortOrder?.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(
                typeof(Queryable), method, new[] { typeof(T), property.Type },
                query.Expression, Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}

using System;
using System.Linq.Expressions;
using System.Reflection;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class SysStatusFilter : PaginationModel
	{
        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public string? NameTh { get; set; } = null!;

        public string? NameEn { get; set; } = null!;

        public string? Category { get; set; } = null!;

        public bool? IsActive { get; set; }

        public static IQueryable<SysStatus> ApplySorting(IQueryable<SysStatus> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SysStatus).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SysStatus), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(SysStatus), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<SysStatus>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }
    }
}


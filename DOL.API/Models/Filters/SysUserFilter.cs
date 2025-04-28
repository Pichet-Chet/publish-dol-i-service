using System;
using System.Linq.Expressions;
using System.Reflection;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class SysUserFilter : PaginationModel
	{
		public SysUserFilter()
		{
		}

        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public string? Username { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public string? Name { get; set; } = null!;

        public string? UserGroup { get; set; } = null!;

        public bool? IsActive { get; set; }


        public static IQueryable<SysUser> ApplySorting(IQueryable<SysUser> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SysUser).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SysUser), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(SysUser), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<SysUser>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }
    }
}


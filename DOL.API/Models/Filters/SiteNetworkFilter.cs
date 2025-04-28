using System;
using System.Linq.Expressions;
using System.Reflection;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class SiteNetworkFilter : PaginationModel
	{
		public SiteNetworkFilter()
		{
		}

        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public string? Name { get; set; } = null!;

        public bool? JobWan1 { get; set; }

        public bool? JobWan2 { get; set; }

        public bool? JobInternet { get; set; }

        public bool? JobCellular { get; set; }

        public bool? JobDevice { get; set; }

        public bool? JobCorpnet { get; set; }

        public bool? IsActive { get; set; }

        public static IQueryable<SiteNetwork> ApplySorting(IQueryable<SiteNetwork> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SiteNetwork).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SiteNetwork), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(SiteNetwork), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<SiteNetwork>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }
    }
}


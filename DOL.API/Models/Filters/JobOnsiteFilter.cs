using System;
using System.Linq.Expressions;
using System.Reflection;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class JobOnsiteFilter : PaginationModel
	{
		public JobOnsiteFilter()
		{
		}

        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public string? DocumentNo { get; set; }

        public int? SiteInformationId { get; set; }

        public int? SysUserId { get; set; }

        public int? SysStatusId { get; set; }

        public string? TypeOnsiteValue { get; set; }

        public static IQueryable<JobOnsite> ApplySorting(IQueryable<JobOnsite> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(JobOnsite).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(JobOnsite), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(JobOnsite), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<JobOnsite>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }
    }
}


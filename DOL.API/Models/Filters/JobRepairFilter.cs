using System;
using System.Linq.Expressions;
using System.Reflection;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
    public class JobRepairFilter : PaginationModel
    {
        public JobRepairFilter()
        {

        }


        public string? TextSearch { get; set; }
        public int? NetworkId { get; set; }
        public string? ProvinceName { get; set; }
        public string? LocationName { get; set; }
        public int? StatusId { get; set; }
        public string? JobType { get; set; }
        public bool? OutOfSla { get; set; }

        public DateTime? JobCreateDate { get; set; }

        public DateTime? JobCreateDateFrom { get; set; }

        public DateTime? JobCreateDateTo { get; set; }

        public string? DocumentRequet { get; set; }

        public static IQueryable<JobRepair> ApplySorting(IQueryable<JobRepair> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(JobRepair).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(JobRepair), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(JobRepair), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<JobRepair>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }
    }
}


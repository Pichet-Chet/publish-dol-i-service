using System;
using System.Linq.Expressions;
using System.Reflection;
using DOL.API.Models.Customs.Response;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class SiteInformationFilter : PaginationModel
	{
        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public int? SiteNetworkId { get; set; }

        public string? SiteNetworkName { get; set; } = null!;

        public int? SiteNetworkSeq { get; set; }

        public string? ProviceName { get; set; } = null!;

        public string? LocationName { get; set; } = null!;

        public string? Address { get; set; } = null!;

        public string? StaffOrganize { get; set; } = null!;

        public string? TelephoneNumber { get; set; } = null!;

        public int? SysStatusId { get; set; }

        public int? SysUserId { get; set; }

        public static IQueryable<SiteInformation> ApplySorting(IQueryable<SiteInformation> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SiteInformation).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SiteInformation), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(SiteInformation), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<SiteInformation>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }

        


    }
}


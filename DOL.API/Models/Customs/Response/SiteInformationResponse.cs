using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DOL.API.Models.Customs.Response
{
	public class SiteInformationResponse : SiteInformation
	{
		public SiteInformationResponse()
		{
            SiteNetwork = new SiteNetwork();
            SysStatus = new SysStatus();
            SysUser = new SysUser();
        }

        public SiteNetwork? SiteNetwork { get; set; }
        public SysStatus? SysStatus { get; set; }
        public SysUser? SysUser { get; set; }

        public static IQueryable<SiteInformationResponse> ApplySorting(IQueryable<SiteInformationResponse> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SiteInformationResponse).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SiteInformationResponse), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(SiteInformationResponse), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<SiteInformationResponse>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }
    }
}


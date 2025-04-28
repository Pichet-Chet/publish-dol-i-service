using System;
using DOL.API.Models.Customs.Response;
using System.Linq.Expressions;
using System.Reflection;

namespace DOL.API.Models.Customs.View
{
    public class JobRepairResponse : JobRepair
    {
        public JobRepairResponse()
        {

            siteInformation = new SiteInformation();
            siteNetwork = new SiteNetwork();
            SysStatus = new SysStatus();
            caseOfFixed = new CaseOfFixed();
            caseOfIssue = new CaseOfIssue();
            caseOfIssueSub = new CaseOfIssueSub();
            sumJobRepair = new SumJobRepair();
        }

        public SiteInformation? siteInformation { get; set; }
        public SiteNetwork? siteNetwork { get; set; }
        public SysStatus? SysStatus { get; set; }
        public CaseOfFixed? caseOfFixed { get; set; }
        public CaseOfIssue? caseOfIssue { get; set; }
        public CaseOfIssueSub? caseOfIssueSub { get; set; }
        public SumJobRepair sumJobRepair { get; set; }

        public string? LocationName { get; set; }
        public string? ProvinceName { get; set; }
        public string? SiteNetworkName { get; set; }
        public int? SiteNetworkSeq { get; set; }

        public string? RemainingTime { get; set; }
        public bool? OutOfSla { get; set; }

        public string? jobCreatedByName { get; set; }
        public string? jobAcceptByName { get; set; }
        public string? jobProcessByName { get; set; }
        public string? jobCompleteByName { get; set; }

        public static IQueryable<JobRepairResponse> ApplySorting(IQueryable<JobRepairResponse> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(JobRepairResponse).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(JobRepairResponse), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(JobRepairResponse), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<JobRepairResponse>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }

    }


    public partial class SumJobRepair
    {
        public int? acceptJobToday { get; set; }
        public int? onProcessJobToday { get; set; }
        public int? succesJobToday { get; set; }

        public int? JobAll { get; set; }
        public int? acceptJobAll { get; set; }
        public int? onProcessJobAll { get; set; }
        public int? succesJobAll { get; set; }
        public int? outOfSlaAll { get; set; } = 0;

        public double? acceptJobPercent { get; set; }
        public double? onpProcessJobPercent { get; set; }
        public double? succesJobPercent { get; set; }
        public double? outOfSlaPercent { get; set; }
    }
}


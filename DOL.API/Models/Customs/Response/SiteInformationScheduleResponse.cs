using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DOL.API.Models.Customs.Response
{
    public class SiteInformationScheduleResponse
    {
        public SiteInformationScheduleResponse()
        {
            files = new List<DashboardFiles>();
        }

        public int? Id { get; set; }

        public string? Location { get; set; }

        public string? Province { get; set; }

        public string? NetworkName { get; set; }

        public int? Seq { get; set; }

        public List<DashboardFiles>? files { get; set; }

        public int? AssignWan1Status { get; set; }
        public string? AssignWan1StatusName { get; set; }

        public int? AssignWan2Status { get; set; }
        public string? AssignWan2StatusName { get; set; }

        public int? AssignInternetStatus { get; set; }
        public string? AssignInternetStatusName { get; set; }

        public int? AssignCellularStatus { get; set; }
        public string? AssignCellularStatusName { get; set; }

        public int? AssignInstallDeviceStatus { get; set; }
        public string? AssignInstallDeviceStatusName { get; set; }

        public int? StatusId { get; set; }
        public string? StatusName { get; set; }

        public SumJobOnSite? sumJobOnSite { get; set; }

        public static IQueryable<SiteInformationScheduleResponse> ApplySorting(IQueryable<SiteInformationScheduleResponse> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SiteInformationScheduleResponse).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SiteInformationScheduleResponse), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(SiteInformationScheduleResponse), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<SiteInformationScheduleResponse>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }

    }

    public partial class DashboardFiles
    {
        public string? FileName { get; set; }
        public string? FileSize { get; set; }
        public string? FileSizeUnit { get; set; }
        public string? FilePath { get; set; }
        //public string? UploadBy { get; set; }
        //public DateTime? UploadDate { get; set; }
    }

    public partial class SumJobOnSite
    {
        public int? JobAll { get; set; } = 0;

        public int? JobWaitingAssign { get; set; } = 0;
        public int? JobWaitingAssignPercent { get; set; } = 0;

        public int? JobPending { get; set; } = 0;
        public double? JobPendingPercent { get; set; } = 0;

        public int? JobOnProcess { get; set; } = 0;
        public double? JobOnProcessPercent { get; set; } = 0;

        public int? JobComplete { get; set; } = 0;
        public double? JobCompletePercent { get; set; } = 0;

    }
}


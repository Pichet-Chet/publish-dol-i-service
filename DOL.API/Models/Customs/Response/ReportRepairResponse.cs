using System;
using System.Linq.Expressions;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;
using DOL.API.Models.Customs.View;

namespace DOL.API.Models.Customs.Response
{
    public class ReportRepairResponse
    {
        public ReportRepairResponse()
        {
            MonthName = string.Empty;
            Year = 0;
            Month = 0;
            Dc1 = 0;
            Dc2 = 0;
            Other = 0;
            SiteNetwork1 = 0;
            SiteNetwork2 = 0;
            SiteNetwork3 = 0;
            SiteNetwork4 = 0;
            All = 0;
            Uih = 0;
            Awn = 0;
            Cat = 0;
            Interlink = 0;
            Symphony = 0;
            Jinet = 0;
            Hr4 = 0;
            Hr5 = 0;
            Hr15 = 0;
            Hr24 = 0;
            Hr30 = 0;
            Link2 = 0;
            fine = 0.00;
        }

        public string? MonthName { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Dc1 { get; set; }
        public int? Dc2 { get; set; }
        public int? Other { get; set; }
        public int? SiteNetwork1 { get; set; }
        public int? SiteNetwork2 { get; set; }
        public int? SiteNetwork3 { get; set; }
        public int? SiteNetwork4 { get; set; }
        public int? All { get; set; }
        public int? Uih { get; set; }
        public int? Awn { get; set; }
        public int? Cat { get; set; }
        public int? Interlink { get; set; }
        public int? Symphony { get; set; }
        public int? Jinet { get; set; }
        public int? Hr4 { get; set; }
        public int? Hr5 { get; set; }
        public int? Hr15 { get; set; }
        public int? Hr24 { get; set; }
        public int? Hr30 { get; set; }
        public int? Link2 { get; set; }

        public double? fine { get; set; }

        public static IQueryable<ReportRepairResponse> ApplySorting(IQueryable<ReportRepairResponse> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(ReportRepairResponse).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(ReportRepairResponse), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(ReportRepairResponse), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<ReportRepairResponse>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }
    }
}


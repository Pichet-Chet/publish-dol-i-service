using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DOL.API.Models.Customs.Response
{
    public class SiteInformationOverviewResponse
    {
        public SiteInformationOverviewResponse()
        {
            JobOnsitePendings = new List<JobOnsitePendings>();
            JobOnsiteOnprocess = new List<JobOnsiteOnprocess>();
            JobOnsiteSuccesses = new List<JobOnsiteSuccess>();
            SumCard = new SumCard();
        }

        public int? Running { get; set; } = 0;

        public string? Team { get; set; }

        public int? UserId { get; set; }

        public int? JobOnsitePendingsCount { get; set; } = 0;
        public int? JobOnsiteOnprocessCount { get; set; } = 0;
        public int? JobOnsiteSuccessesCount { get; set; } = 0;

        public double? JobOnsitePendingsPercent { get; set; } = 0;
        public double? JobOnsiteOnprocessPercent { get; set; } = 0;
        public double? JobOnsiteSuccessesPercent { get; set; } = 0;

        public List<JobOnsitePendings> JobOnsitePendings { get; set; }
        public List<JobOnsiteOnprocess> JobOnsiteOnprocess { get; set; }
        public List<JobOnsiteSuccess> JobOnsiteSuccesses { get; set; }

        public SumCard SumCard { get; set; }

        public double? PercentComplete { get; set; } = 0;

        public string? FileZipUrl { get; set; }

        public static IQueryable<SiteInformationOverviewResponse> ApplySorting(IQueryable<SiteInformationOverviewResponse> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(SiteInformationOverviewResponse).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(SiteInformationOverviewResponse), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda(property, parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        var methodName = sortType.ToLower() == "asc" ? "OrderBy" : sortType.ToLower() == "desc" ? "OrderByDescending" : null;

                        if (methodName != null)
                        {
                            var methodCall = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(SiteInformationOverviewResponse), propertyInfo.PropertyType }, queryable.Expression, lambda);
                            return queryable.Provider.CreateQuery<SiteInformationOverviewResponse>(methodCall);
                        }
                    }
                }
            }

            return queryable;
        }

    }

    public partial class JobOnsitePendings
    {
        public int? Id { get; set; }
        public string? Province { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
    }

    public partial class JobOnsiteOnprocess
    {
        public int? Id { get; set; }
        public string? Province { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
    }

    public partial class JobOnsiteSuccess
    {
        public int? Id { get; set; }
        public string? Province { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
    }

    public partial class SumCard
    {
        public int? CardPendingsCount { get; set; } = 0;
        public int? CardOnprocessCount { get; set; } = 0;
        public int? CardSuccessesCount { get; set; } = 0;

        public double? CardPendingsPercent { get; set; } = 0;
        public double? CardOnprocessPercent { get; set; } = 0;
        public double? CardSuccessesPercent { get; set; } = 0;


    }
}


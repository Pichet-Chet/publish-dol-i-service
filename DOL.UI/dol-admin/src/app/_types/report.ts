type ReportForm = {
    pageNumber?: number;
    pageSize?: number;
    sortName?: string;
    sortType?: string;
    effectRow?: number;
    dataList?: ReportsList[];
};

type ReportsList = {
    monthName?: string;
    year?: number;
    month?: number;
    dc1?: number;
    dc2?: number;
    other?: number;
    siteNetwork1?: number;
    siteNetwork2?: number;
    siteNetwork3?: number;
    siteNetwork4?: number;
    all?: number;
    uih?: number;
    awn?: number;
    cat?: number;
    interlink?: number;
    symphony?: number;
    jinet?: number;
    hr4?: number;
    hr5?: number;
    hr15?: number;
    hr24?: number;
    hr30?: number;
    link2?: number;
    fine?: number;
};

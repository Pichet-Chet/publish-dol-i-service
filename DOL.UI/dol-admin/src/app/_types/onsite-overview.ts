type OnsiteOverviewForm = {
  effectRow?: number;
  dataList?: OnsiteOverviewList[];
} & OnsiteOverviewRequest;

type OnsiteOverviewList = {
  running?: number;
  userId?: string;
  team?: string;
  jobOnsitePendingsCount?: number;
  jobOnsiteOnprocessCount?: number;
  jobOnsiteSuccessesCount?: number;
  jobOnsitePendingsPercent?: number;
  jobOnsiteOnprocessPercent?: number;
  jobOnsiteSuccessesPercent?: number;
  jobOnsitePendings?: JobOnsiteList[];
  jobOnsiteOnprocess?: JobOnsiteList[];
  jobOnsiteSuccesses?: JobOnsiteList[];
  sumCard?: SumCard;
  percentComplete?: number;
  fileZipUrl?: string;
};
type JobOnsiteList = {
  id?: string;
  province?: string;
  location?: string;
  category?: string;
};
type SumCard = {
  cardPendingsCount?: number;
  cardPendingsPercent?: number;
  cardOnprocessCount?: number;
  cardOnprocessPercent?: number;
  cardSuccessesCount?: number;
  cardSuccessesPercent?: number;
};

type OnsiteOverviewRequest = {
  pageNumber?: number;
  pageSize?: number;
  sortName?: string;
  sortType?: string;
};

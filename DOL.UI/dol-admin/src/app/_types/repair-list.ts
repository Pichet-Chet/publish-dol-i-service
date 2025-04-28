type RepairListForm = {
  effectRow?: number;
  dataList?: TrnJobRepairList[];
} & RepairListRequest;

type TrnJobRepairList = {
  id?: number;
  documentNo?: string;
  documentRequest?: string;
  remainingTime?: string;
  siteNetworkId?: number;
  siteInformationId?: number;
  jobDescription?: string;
  jobContactName?: string;
  jobContactTel?: string;
  jobSenderContactName?: string;
  jobSenderContactTel?: string;
  jobSenderRemark?: string;
  jobFixedDescription?: string;
  jobFixedComment?: string;
  jobFixedContactName?: string;
  jobFixedContactTel?: string;
  jobImage1?: string;
  jobImage2?: string;
  jobImage3?: string;
  jobImage4?: string;
  sysStatusId?: number;
  jobCreatedBy?: string;
  jobCreatedDate?: Date;
  jobAcceptBy?: string;
  jobAcceptDate?: Date;
  jobProcessBy?: string;
  jobProcessDate: null;
  jobCompleteBy?: string;
  jobCompleteDate: null;
  typeRepairData?: string;
  typeRepairValue?: string;
  siteInformation?: TrnSiteInformation;
  siteNetwork?: TrnSiteNetwork;
  sysStatus?: TrnSysStatus;
  sumJobRepair?: TrnSumJobRepair;
};

type RepairListRequest = {
  textSearch?: string;
  dateFromTo?: any[];
  jobCreateDateFrom?: string;
  jobCreateDateTo?: string;
  outOfSla?: boolean | string;
  statusId?: string;
  pageNumber?: number;
  pageSize?: number;
  sortName?: string;
  sortType?: string;
};

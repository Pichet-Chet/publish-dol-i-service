type SiteScheduleListForm = {
  effectRow?: number;
  dataList?: SiteScheduleList[];
} & SiteScheduleListRequest;

type SiteScheduleList = {
  assignCellularStatus?: number;
  assignCellularStatusName?: string;
  assignInstallDeviceStatus?: number;
  assignInstallDeviceStatusName?: string;
  assignInternetStatus?: number;
  assignInternetStatusName?: string;
  assignWan1Status?: number;
  assignWan1StatusName?: string;
  assignWan2Status?: number;
  assignWan2StatusName?: string;
  files?: SiteScheduleFileList[];
  id?: number;
  location?: string;
  networkName?: string;
  province?: string;
  seq?: number;
  statusId?: number;
  statusName?: string;
  sumJobOnSite?: SumJobOnSite;
};

type SiteScheduleFileList = {
  fileName?: string;
  fileSize?: string;
  fileSizeUnit?: string;
  filePath?: string;
};
type SumJobOnSite = {
  jobPending?: number;
  jobPendingPercent?: number;
  jobOnProcess?: number;
  jobOnProcessPercent?: number;
  jobComplete?: number;
  jobCompletePercent?: number;
};

type SiteScheduleListRequest = {
  textSearch?: string;
  sysStatusId?: string;
  pageNumber?: number;
  pageSize?: number;
  sortName?: string;
  sortType?: string;
};
type DashboardAllForm = {
  dashboardAccept?: DashboardAcceptForm;
  dashboardOnProcess?: DashboardOnProcessForm;
  dashboardOnSuccess?: DashboardOnSuccessForm;
};

type DashboardAcceptForm = {
  effectRow?: number;
  dataList?: TrnJobRepairList[];
} & RepairListRequest;

type DashboardOnProcessForm = {
  effectRow?: number;
  dataList?: TrnJobRepairList[];
} & RepairListRequest;

type DashboardOnSuccessForm = {
  effectRow?: number;
  dataList?: TrnJobRepairList[];
} & RepairListRequest;

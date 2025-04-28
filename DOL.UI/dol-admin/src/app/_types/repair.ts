type RepairForm = {
  dataFrom?: DataFrom;
  mstCollection?: MstCollection;
};

type DataFrom = {
  id?: number;
  documentNo?: string;
  provinceName?: string;
  locationName?: string;
  siteInformationId?: number;
  siteNetworkId?: number;
  siteNetworkName?: string;
  siteNetworkSeq?: number;
  address?: string;
  typeRepairData?: string;
  typeRepairValue?: string;
  documentRequest?: string;
  wan1Provider?: string;
  wan1Cid?: string;
  wan1Speed?: string;
  wan1AsNumber?: string;
  wan1IpWan1Pe?: string;
  wan1IpWan1Ce?: string;
  wan1Subnet?: string;
  wan2Provider?: string;
  wan2Cid?: string;
  wan2Speed?: string;
  wan2AsNumber?: string;
  wan2IpWan1Pe?: string;
  wan2IpWan1Ce?: string;
  wan2Subnet?: string;
  internetCid?: string;
  internetSpeed?: string;
  internetAsNumber?: string;
  internetWanIpAddress?: string;
  internetSubnet?: string;
  cellularSim?: string;
  cellularAr109?: string;

  jobDescription?: string;
  jobContactName?: string;
  jobContactTel?: string;
  jobSenderContactName?: string;
  jobSenderContactTel?: string;
  jobSenderRemark?: string;

  jobFixedContactName?: string;
  jobFixedContactTel?: string;
  jobFixedDescription?: string;
  jobFixedComment?: string;

  fileUpload1?: File;
  fileUpload2?: File;
  fileUpload3?: File;
  fileUpload4?: File;

  jobImage1?: string;
  jobImage2?: string;
  jobImage3?: string;
  jobImage4?: string;

  sysStatusId?: string;

  caseOfIssueId?: number;
  caseOfIssueName?: string;
  caseOfIssueSubId?: number;
  caseOfIssueSubName?: string;
  caseOfFixName?: string;

  jobAcceptBy?: string;
  jobAcceptByName?: string;
  jobAcceptDate?: Date;
  jobCompleteBy?: string;
  jobCompleteByName?: string;
  jobCompleteDate?: Date;
  jobCreatedBy?: string;
  jobCreatedByName?: string;
  jobCreatedDate?: Date;
  jobProcessBy?: string;
  jobProcessByName?: string;
  jobProcessDate?: Date;
};

type MstCollection = {
  mstProvince?: MstProvince[];
  mstLocation?: MstLocation[];
  mstTypeRepair?: MstTypeRepair[];
  // mstCaseOfFixed?: MstCaseOfFixed[];
  mstCaseOfIssue?: MstCaseOfIssue[];
  mstCaseOfIssueSub?: MstCaseOfIssueSub[];
  mstFixCase?: MstFixCase[];
};

type DataRequest = {
  SiteNetworkId?: string;
  SiteInformationId?: number;
  JobDescription?: string;
  JobContactName?: string;
  JobContactTel?: string;
  JobSenderContactName?: string;
  JobSenderContactTel?: string;
  JobSenderRemark?: string;
  JobFixedDescription?: string;
  JobFixedComment?: string;
  JobFixedContactName?: string;
  JobFixedContactTel?: string;
  SysStatusId?: string;
  JobCreatedBy?: string;
  JobAcceptBy?: string;
  JobProcessBy?: string;
  JobCompleteBy?: string;
  TypeRepairData?: string;
  TypeRepairValue?: string;
  FileUpload1?: File;
  FileUpload2?: File;
  FileUpload3?: File;
  FileUpload4?: File;
};

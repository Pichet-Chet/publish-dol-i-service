type UserMgmtForm = {
  effectRow?: number;
  dataList?: TrnUserMgmt[];
} & UserMgmtRequest;

type TrnUserMgmt = {
  id?: number;
  username?: string;
  password?: string;
  name?: string;
  userGroup?: string;
  createDate?: Date;
  createBy?: string;
  updateDate?: Date;
  updateBy?: string;
  isActive?: boolean | string;
};

type UserMgmtRequest = {
  textSearch?: string;
  pageNumber?: number;
  pageSize?: number;
  sortName?: string;
  sortType?: string;
};

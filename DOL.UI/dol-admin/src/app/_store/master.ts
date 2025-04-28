import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";

interface MasterState {
  getMstProvince: () => Promise<MstProvince[]>;
  getMstLocationByProvinceName: (
    provinceName: String
  ) => Promise<MstLocation[]>;
  getMstTypeRepairBySiteNetworkId: (
    siteNetworkId: number
  ) => Promise<MstTypeRepair[]>;
  // getMstCaseOfFixed: () => Promise<MstCaseOfFixed[]>;
  getMstCaseOfIssue: () => Promise<MstCaseOfIssue[]>;
  getMstCaseOfIssueSub: (caseOfIssueId: string) => Promise<MstCaseOfIssueSub[]>;
  getMstFixCase: (caseOfIssueSubId: string) => Promise<MstFixCase[]>;
  // userInfo?: Employee;
  // roleLists: Role[] | undefined;
  // permissionLists: string[] | undefined;
  // permissionTableLists: PermissionApp[] | undefined;
  // getUserInfo: () => Promise<string>;
  // getRoleLists: () => Promise<string>;
  // getPermissionLists: () => Promise<string>;
  // getAllEmployee: () => Promise<any | undefined>;
  // getEmployeeById: (employeeId: string | undefined) => Promise<any | undefined>;
  // getAllInitialNames: () => Promise<string[]>;
}

export const useMasterState = create<MasterState>((set, get) => ({
  getMstProvince: async () => {
    const response = await axios.get(
      `${API_APP_URL}/api/SiteInformation/Province`
    );

    var data: MstProvince[] = [];
    if (response.status == 200 && response.data) {
      const dataMaster = (response.data.data as string[]) || [];
      dataMaster.map((ct) => {
        data.push({
          provinceId: ct,
          provinceName: ct,
        });
      });
    }
    return data;
  },
  getMstLocationByProvinceName: async (provinceName) => {
    const response = await axios.get(
      `${API_APP_URL}/api/SiteInformation/Location?province=${provinceName}`
    );

    var data: MstLocation[] = [];
    if (response.status == 200 && response.data) {
      data = (response.data.data as MstLocation[]) || [];
    }
    return data;
  },
  getMstTypeRepairBySiteNetworkId: async (siteId) => {
    const response = await axios.get(
      `${API_APP_URL}/api/SiteNetwork/repairActive?SiteId=${siteId}`
    );

    var data: MstTypeRepair[] = [];
    if (response.status == 200 && response.data) {
      data = (response.data.data as MstTypeRepair[]) || [];
    }
    return data;
  },
  // getMstCaseOfFixed: async () => {
  //   const response = await axios.get(
  //     `${API_APP_URL}/api/CaseOfFixed/Dropdrown`
  //   );

  //   var data: MstCaseOfFixed[] = [];
  //   if (response.status == 200 && response.data) {
  //     data = (response.data.data as MstCaseOfFixed[]) || [];
  //   }
  //   return data;
  // },
  getMstCaseOfIssue: async () => {
    const response = await axios.get(
      `${API_APP_URL}/api/CaseOfIssue/Dropdrown`
    );

    var data: MstCaseOfIssue[] = [];
    if (response.status == 200 && response.data) {
      data = (response.data.data as MstCaseOfIssue[]) || [];
    }
    return data;
  },
  getMstCaseOfIssueSub: async (caseOfIssueId: string) => {
    const response = await axios.get(
      `${API_APP_URL}/api/CaseOfIssueSub/Dropdrown/${caseOfIssueId}`
    );

    var data: MstCaseOfIssueSub[] = [];
    if (response.status == 200 && response.data) {
      data = (response.data.data as MstCaseOfIssueSub[]) || [];
    }
    return data;
  },
  getMstFixCase: async (caseOfIssueSubId: string) => {
    const response = await axios.get(
      `${API_APP_URL}/api/CaseOfIssueSub/GetFixCase/${caseOfIssueSubId}`
    );

    var data: MstFixCase[] = [];
    if (response.status == 200 && response.data) {
      data = (response.data.data as MstFixCase[]) || [];
    }
    return data;
  },
}));

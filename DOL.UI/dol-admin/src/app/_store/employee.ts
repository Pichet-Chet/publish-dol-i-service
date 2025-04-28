import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";

interface EmployeeState {
  userInfo?: Employee;
  roleLists: Role[] | undefined;
  permissionLists: string[] | undefined;
  permissionTableLists: PermissionApp[] | undefined;
  getUserInfo: () => Promise<string>;
  // getRoleLists: () => Promise<string>;
  // getPermissionLists: () => Promise<string>;
  // getAllEmployee: () => Promise<any | undefined>;
  // getEmployeeById: (employeeId: string | undefined) => Promise<any | undefined>;
  // getAllInitialNames: () => Promise<string[]>;
}

export const useEmployeeStore = create<EmployeeState>((set, get) => ({
  userInfo: {},
  roleLists: [],
  permissionLists: [],
  permissionTableLists: [],
  getUserInfo: async () => {
    // const response = await axios.get(
    //     `${API_IDENTITY_URL}/Users/GetUserInformation`
    // );
    // const res = response.data || {};
    // const data: Employee = {
    //     employeeId: "ping",
    //     positionId: "",
    //     positionAbbreviation: "",
    //     positionName: "",
    //     level: "",
    //     active: "1",
    //     fullNameTh: "fullNameTh",
    //     fullNameEn: "fullNameEn",
    //     userName: "username",
    //     sub: "",
    // };
    // set({ userInfo: data });

    return "";
  },
  // getRoleLists: async () => {
  //     const responseRole = await axios.get(
  //         `${API_IDENTITY_URL}/Roles/GetRoleInformation`
  //     );
  //     const dataRole = responseRole.data || [];

  //     set({ roleLists: dataRole || [] });

  //     return "";
  // },
  // getPermissionLists: async () => {
  //     const responsePermission = await axios.get(
  //         `${API_IDENTITY_URL}/Permission/GetPermissions`
  //     );
  //     const dataPermission = responsePermission.data || [];

  //     const responsePermissionTable = await axios.get(
  //         `${API_IDENTITY_URL}/Permission/GetAppPermissions`
  //     );
  //     const dataPermissionTable = responsePermissionTable.data || {};

  //     set({
  //         permissionTableLists: dataPermissionTable || [],
  //         permissionLists: dataPermission || [],
  //     });

  //     return "";
  // },
  // getAllEmployee: async () => {
  //     var result = <any | undefined>{};
  //     await axios
  //         .get(`${API_APP_URL}/employees/get`, {})
  //         .then((res) => {
  //             if (res.status == 200) {
  //                 if (res.data.status == "success" && res.data.data.length > 0) {
  //                     result = res.data.data;
  //                 }
  //             }
  //         })
  //         .catch((res: any) => {
  //             result = undefined;
  //         });
  //     return result;
  // },
  // getEmployeeById: async (employeeId = "") => {
  //     var result = <any | undefined>{};
  //     await axios
  //         .get(`${API_APP_URL}/employees/get/${employeeId}`, {})
  //         .then((res) => {
  //             if (res.status == 200) {
  //                 if (res.data.status == "success" && res.data.data.length > 0) {
  //                     result = res.data.data[0];
  //                 }
  //             }
  //         })
  //         .catch((res: any) => {
  //             result = res.response;
  //         });
  //     return result;
  // },
  // getAllInitialNames: async () => {
  //     var result: string[] = [];
  //     await axios
  //         .get(`${API_IDENTITY_URL}/Users/initialNames`, {})
  //         .then((res) => {
  //             result = res.data || [];
  //         })
  //         .catch((res: any) => {
  //             console.error("getAllInitialName", res);
  //         });
  //     return result;
  // },
}));

import { create } from "zustand";
// import axios from "@/app/_lib/axios";

interface NavigationStore {
  keyActive: string;
  setKeyActive: (e: string) => string;
  getPageName: (pageRoute: string) => Promise<string>;
}

export const useNavigations = create<NavigationStore>()((set, get) => ({
  keyActive: "1",
  setKeyActive: (e) => {
    set({ keyActive: e });
    return e;
  },
  getPageName: async (pageRoute) => {
    var pageName = "Dashboard";
    if (pageRoute == "/api/auth/signin") {
      pageName = "เข้าสู่ระบบ";
    } else if (pageRoute == "/dashboard") {
      pageName = "Dashboard";
    } else if (pageRoute == "/user-mgmt") {
      pageName = "User Mgmt";
    } else if (pageRoute == "/onsite-overview") {
      pageName = "Onsite Overview";
    } else if (pageRoute == "/site-schedule") {
      pageName = "Site Schedule";
    } else if (pageRoute.includes("/siteinfo/")) {
      pageName = "Site info";
    } else if (pageRoute == "/repair-list") {
      pageName = "รายการแจ้งซ่อม";
    } else if (pageRoute == "/repair") {
      pageName = "สร้างรายการแจ้งซ่อม";
    } else if (pageRoute.includes("/repair/")) {
      pageName = "รายการแจ้งซ่อม";
      // } else if (pageRoute == "/internet-list") {
      //   pageName = "ข้อมูลการให้บริการและใช้งานอินเตอร์เน็ต";
    } else if (pageRoute == "/report") {
      pageName = "รายงานการแจ้งซ่อม";
    }
    return pageName || "";
  },
}));

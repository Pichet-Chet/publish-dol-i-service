import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import _ from "lodash";
import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";
import timezone from "dayjs/plugin/timezone";
dayjs.extend(utc);
dayjs.extend(timezone);
dayjs.tz.setDefault("Asia/Bangkok");

interface SiteInfoState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  getData: (orderId?: string) => Promise<TrnSiteInformation>;
  updateSiteInfo: (param: TrnSiteInformation) => Promise<any>;
  updateImageSiteInfo: (
    id: string,
    flag: string,
    username: string,
    fileUpload: File
  ) => Promise<any>;
  updateImages: (id: string, username: string, files: File[]) => Promise<any>;
  deleteImageSiteInfo: (
    id: string,
    flag: string,
    username: string
  ) => Promise<any>;
  generatePdf: (id: string) => Promise<any>;
  generateOnsitePdf: (id: string) => Promise<any>;
}

export const useSiteInfoState = create<SiteInfoState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  getData: async (orderId) => {
    var resultForm: TrnSiteInformation = {};

    const url = `${API_APP_URL}/api/SiteInformation?Id=${orderId}`;
    await axios
      .get(url)
      .then((res) => {
        if (res.status == 200) {
          if (res.data.status && res.data?.data && res.data?.data.length > 0) {
            resultForm = res.data.data[0];
          }
        }
      })
      .catch((res: any) => {
        resultForm = res.response;
      });
    return resultForm;
  },
  updateSiteInfo: async (data) => {
    var result = <any | undefined>{};
    await axios
      .patch(`${API_APP_URL}/api/SiteInformation`, {
        ...data,
      })
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
  updateImageSiteInfo: async (id, flag, username, fileUpload) => {
    var result = <any | undefined>{};

    const formData = new FormData();
    formData.append(`Id`, id || "");
    formData.append(`Flag`, flag || "");
    formData.append(`Username`, username || "");
    if (fileUpload) {
      formData.append(`FileUpload`, fileUpload);
    }

    await axios
      .patch(`${API_APP_URL}/api/SiteInformation/UpdateImage`, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
  updateImages: async (id, username, files) => {
    var result = <any | undefined>{};

    const formData = new FormData();
    formData.append(`Id`, id || "");
    formData.append(`Username`, username || "");
    files.map((ct) => {
      formData.append(`FileUpload`, ct);
    });

    await axios
      .patch(`${API_APP_URL}/api/SiteInformation/UpdateImages`, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
  deleteImageSiteInfo: async (id, flag, username) => {
    var result = <any | undefined>{};
    await axios
      .patch(`${API_APP_URL}/api/SiteInformation/DeleteImage`, {
        id: id,
        flag: flag,
        username: username,
      })
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
  generatePdf: async (id) => {
    var result = <any | undefined>{};
    await axios
      .get(`${API_APP_URL}/api/SiteInformation/GeneratePdf?id=${id}`, {
        responseType: "blob",
      })
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
  generateOnsitePdf: async (id) => {
    var result = <any | undefined>{};
    await axios
      .get(`${API_APP_URL}/api/SiteInformation/GenerateOnsitePdf?id=${id}`)
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
}));

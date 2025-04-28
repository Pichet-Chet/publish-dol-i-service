import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import _ from "lodash";
import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";
import timezone from "dayjs/plugin/timezone";
dayjs.extend(utc);
dayjs.extend(timezone);
dayjs.tz.setDefault("Asia/Bangkok");

interface OnsiteOverviewState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingTable: boolean;
  setIsLoadingTable: (isLoadingTable: boolean) => void;
  getData: (param?: OnsiteOverviewForm) => Promise<OnsiteOverviewForm>;
  generateFileByUser: (
    id: string
  ) => Promise<any>;
}

export const useOnsiteOverviewState = create<OnsiteOverviewState>(
  (set: any, get: any) => ({
    isLoading: false,
    setIsLoading: (isLoading) => {
      set({ isLoading: isLoading });
    },
    isLoadingTable: false,
    setIsLoadingTable: (isLoadingTable) => {
      set({ isLoadingTable: isLoadingTable });
    },
    getData: async (param) => {
      var resultForm: OnsiteOverviewForm = {};

      var _param = "isAll=true";
      if (param) {
        // if (param.pageNumber && _.isNumber(param.pageNumber)) {
        //   _param = `${_param}&PageNumber=${param.pageNumber}`;
        // } else {
        //   _param = `${_param}&PageNumber=1`;
        // }

        // if (param.pageSize && _.isNumber(param.pageSize)) {
        //   _param = `${_param}&PageSize=${param.pageSize}`;
        // } else {
        //   _param = `${_param}&PageSize=10`;
        // }
      }

      await axios
        .get(`${API_APP_URL}/api/SiteInformation/Overview?${_param}`)
        .then((res) => {
          if (res.data.status) {
            resultForm = {
              pageNumber: param?.pageNumber,
              pageSize: param?.pageSize,
              effectRow: res.data.effectRow,
              dataList: res.data.data,
            };
          } else {
            resultForm = {
              pageNumber: param?.pageNumber,
              pageSize: param?.pageSize,
              effectRow: 0,
              dataList: [],
            };
          }
        })
        .catch((res: any) => {
          resultForm = res.response;
        });
      return resultForm;
    },
    generateFileByUser: async (id) => {
      var result = <any | undefined>{};
      await axios
        .get(`${API_APP_URL}/api/SiteInformation/GeneratePdfByUser?userId=${id}`, {
          responseType: 'blob',
        })
        .then((res) => {
          result = res;
        })
        .catch((res: any) => {
          result = res.response;
        });
      return result;
    },
  })
);

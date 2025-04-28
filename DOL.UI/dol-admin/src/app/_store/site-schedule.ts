import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import _ from "lodash";
import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";
import timezone from "dayjs/plugin/timezone";
dayjs.extend(utc);
dayjs.extend(timezone);
dayjs.tz.setDefault("Asia/Bangkok");

interface SiteScheduleState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingInfo: boolean;
  setIsLoadingInfo: (isLoadingInfo: boolean) => void;
  isLoadingTable: boolean;
  setIsLoadingTable: (isLoadingTable: boolean) => void;
  getData: (param?: SiteScheduleListForm) => Promise<SiteScheduleListForm>;
}

export const useSiteScheduleState = create<SiteScheduleState>(
  (set: any, get: any) => ({
    isLoading: false,
    setIsLoading: (isLoading) => {
      set({ isLoading: isLoading });
    },
    isLoadingInfo: false,
    setIsLoadingInfo: (isLoadingInfo) => {
      set({ isLoadingInfo: isLoadingInfo });
    },
    isLoadingTable: false,
    setIsLoadingTable: (isLoadingTable) => {
      set({ isLoadingTable: isLoadingTable });
    },
    getData: async (param) => {
      var resultForm: SiteScheduleListForm = {};

      var _param = "isAll=false";
      if (param) {
        if (param.textSearch && !_.isEmpty(param.textSearch)) {
          _param = `${_param}&TextSearch=${param.textSearch}`;
        }

        if (param.sysStatusId && !_.isEmpty(param.sysStatusId)) {
          _param = `${_param}&SysStatusId=${param.sysStatusId}`;
        }

        if (param.sortName && !_.isEmpty(param.sortName) && !_.isEmpty(param.sortType) && !_.isEmpty(param.sortType)) {
          _param = `${_param}&SortName=${param.sortName}`;
          _param = `${_param}&SortType=${param.sortType}`;
        }

        if (param.pageNumber && _.isNumber(param.pageNumber)) {
          _param = `${_param}&PageNumber=${param.pageNumber}`;
        } else {
          _param = `${_param}&PageNumber=1`;
        }

        if (param.pageSize && _.isNumber(param.pageSize)) {
          _param = `${_param}&PageSize=${param.pageSize}`;
        } else {
          _param = `${_param}&PageSize=10`;
        }
      }

      await axios
        .get(`${API_APP_URL}/api/SiteInformation/Schedule?${_param}`)
        .then((res) => {
          if (res.data.status) {
            resultForm = {
              textSearch: param?.textSearch,
              sysStatusId: param?.sysStatusId,
              pageNumber: param?.pageNumber,
              pageSize: param?.pageSize,
              effectRow: res.data.effectRow,
              dataList: res.data.data,
            };
          } else {
            resultForm = {
              textSearch: param?.textSearch,
              sysStatusId: param?.sysStatusId,
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
  })
);

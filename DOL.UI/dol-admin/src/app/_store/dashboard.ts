import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import _ from "lodash";
import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";
import timezone from "dayjs/plugin/timezone";
dayjs.extend(utc);
dayjs.extend(timezone);
dayjs.tz.setDefault("Asia/Bangkok");

interface DashboardState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingTableAccept: boolean;
  setIsLoadingTableAccept: (isLoadingTableAccept: boolean) => void;
  isLoadingTableOnProcess: boolean;
  setIsLoadingTableOnProcess: (isLoadingTableOnProcess: boolean) => void;
  getDataAccept: (param?: DashboardAcceptForm) => Promise<DashboardAcceptForm>;
  getDataOnProcess: (
    param?: DashboardOnProcessForm
  ) => Promise<DashboardOnProcessForm>;
  getDataOnSuccess: () => Promise<DashboardOnSuccessForm>;
}

export const useDashboardState = create<DashboardState>(
  (set: any, get: any) => ({
    isLoading: false,
    setIsLoading: (isLoading) => {
      set({ isLoading: isLoading });
    },
    isLoadingTableAccept: false,
    setIsLoadingTableAccept: (isLoadingTableAccept) => {
      set({ isLoadingTableAccept: isLoadingTableAccept });
    },
    isLoadingTableOnProcess: false,
    setIsLoadingTableOnProcess: (isLoadingTableOnProcess) => {
      set({ isLoadingTableOnProcess: isLoadingTableOnProcess });
    },
    getDataAccept: async (param) => {
      var resultForm: DashboardAcceptForm = {};

      var _param = `StatusId=5&jobCreateDate=${dayjs().format("YYYY-MM-DD")}`;
      if (param) {
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
        .get(`${API_APP_URL}/api/JobRepair?${_param}`)
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
    getDataOnProcess: async (param) => {
      var resultForm: DashboardOnProcessForm = {};

      var _param = `StatusId=6&jobCreateDate=${dayjs().format("YYYY-MM-DD")}`;
      if (param) {
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
        .get(`${API_APP_URL}/api/JobRepair?${_param}`)
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
    getDataOnSuccess: async () => {
      var resultForm: DashboardOnSuccessForm = {};
      var _param = `StatusId=7&jobCreateDate=${dayjs().format(
        "YYYY-MM-DD"
      )}&PageNumber=1&PageSize=1`;
      await axios
        .get(`${API_APP_URL}/api/JobRepair?${_param}`)
        .then((res) => {
          if (res.data.status) {
            resultForm = {
              effectRow: res.data.effectRow,
              dataList: res.data.data,
            };
          } else {
            resultForm = {
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

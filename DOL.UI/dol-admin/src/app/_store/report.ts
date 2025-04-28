import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import * as yup from "yup";
import _ from "lodash";

interface ReportState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingTable: boolean;
  setIsLoadingTable: (isLoadingTable: boolean) => void;
  getData: (param: ReportForm) => Promise<ReportForm>;
  generateExport: (
    year?: number,
    month?: number,
    type?: string,
    isAdmin?: boolean
  ) => Promise<any>;
  generateExportMonthPdf: (year?: number, month?: number) => Promise<any>;
}

export const useReportState = create<ReportState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  isLoadingTable: false,
  setIsLoadingTable: (isLoadingTable) => {
    set({ isLoadingTable: isLoadingTable });
  },
  getData: async (param) => {
    var resultForm: ReportForm = {};

    var _param = "isAll=false";
    if (param) {
      if (
        param.sortName &&
        !_.isEmpty(param.sortName) &&
        !_.isEmpty(param.sortType) &&
        !_.isEmpty(param.sortType)
      ) {
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

    const url = `${API_APP_URL}/api/JobRepair/ReportRepairAdmin?${_param}`;
    await axios
      .get(url)
      .then((res) => {
        if (res.status == 200) {
          if (res.data.status && res.data?.data && res.data?.data.length > 0) {
            const data = res.data;
            resultForm = {
              pageNumber: data.pageNumber,
              pageSize: data.pageSize,
              effectRow: data.effectRow,
              dataList: data.data,
            };
          }
        }
      })
      .catch((res: any) => {
        resultForm = res.response;
      });
    return resultForm;
  },
  generateExport: async (year, month, type, isAdmin) => {
    var result = <any | undefined>{};
    const url = `${API_APP_URL}/api/JobRepair/ExportJobRepair?Year=${year}&Month=${month}&Type=${type}&IsAdmin=${isAdmin}`;
    await axios
      .get(url, {
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
  generateExportMonthPdf: async (year, month) => {
    var result = <any | undefined>{};
    const url = `${API_APP_URL}/api/JobRepair/ExportJobRepairMonth?Year=${year}&Month=${month}`;
    await axios
      .get(url)
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;

    // var result = <any | undefined>{};
    // const url = `${API_APP_URL}/api/JobRepair/ExportJobRepairMonth?Year=${year}&Month=${month}`;
    // await axios
    //   .get(url, {
    //     responseType: "blob",
    //   })
    //   .then((res) => {
    //     result = res;
    //   })
    //   .catch((res: any) => {
    //     result = res.response;
    //   });
    // return result;
  },
}));

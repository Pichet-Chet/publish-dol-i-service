import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import * as yup from "yup";
import _ from "lodash";

interface RepairListState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingTable: boolean;
  setIsLoadingTable: (isLoadingTable: boolean) => void;
  getData: (param: RepairListForm) => Promise<RepairListForm>;
}

export const useRepairListState = create<RepairListState>(
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
      var resultForm: RepairListForm = {};

      var _param = "isAll=false";
      if (param) {
        if (param.textSearch && !_.isEmpty(param.textSearch)) {
          _param = `${_param}&TextSearch=${param.textSearch}`;
        }

        if (
          param.jobCreateDateFrom &&
          !_.isEmpty(param.jobCreateDateFrom) &&
          param.jobCreateDateTo &&
          !_.isEmpty(param.jobCreateDateTo)
        ) {
          _param = `${_param}&JobCreateDateFrom=${param.jobCreateDateFrom}&JobCreateDateTo=${param.jobCreateDateTo}`;
        }

        if (param.outOfSla == true || param.outOfSla == false) {
          _param = `${_param}&OutOfSla=${param.outOfSla}`;
        }

        if (param.statusId && !_.isEmpty(param.statusId)) {
          _param = `${_param}&StatusId=${param.statusId}`;
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
        .get(`${API_APP_URL}/api/JobRepair?${_param}`)
        .then((res) => {
          if (res.data.status) {
            resultForm = {
              textSearch: param.textSearch,
              statusId: param.statusId,
              pageNumber: param.pageNumber,
              pageSize: param.pageSize,
              effectRow: res.data.effectRow,
              dataList: res.data.data,
            };
          } else {
            resultForm = {
              textSearch: param.textSearch,
              statusId: param.statusId,
              pageNumber: param.pageNumber,
              pageSize: param.pageSize,
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

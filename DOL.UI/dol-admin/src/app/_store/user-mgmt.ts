import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import * as yup from "yup";
import _ from "lodash";

interface UserMgmtState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingTable: boolean;
  setIsLoadingTable: (isLoadingTable: boolean) => void;
  validationData: () => yup.ObjectSchema<{}, TrnUserMgmt, {}, "">;
  getData: (param: UserMgmtForm) => Promise<UserMgmtForm>;
  userCreate: (param: TrnUserMgmt) => Promise<any>;
  userUpdate: (param: TrnUserMgmt) => Promise<any>;
}

export const useUserMgmtState = create<UserMgmtState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  isLoadingTable: false,
  setIsLoadingTable: (isLoadingTable) => {
    set({ isLoadingTable: isLoadingTable });
  },
  validationData: () => {
    return yup
      .object<TrnUserMgmt>()
      .test("TrnUserMgmt", function validate(value: TrnUserMgmt) {
        const errors: yup.ValidationError[] = [];
        if (_.isEmpty(value.name)) {
          errors.push(
            new yup.ValidationError(`โปรดระบุ Name`, value.name, "name")
          );
        }
        if (_.isEmpty(value.userGroup)) {
          errors.push(
            new yup.ValidationError(
              `โปรดระบุ User Group`,
              value.userGroup,
              "userGroup"
            )
          );
        }

        if (value.isActive == "") {
          errors.push(
            new yup.ValidationError(
              `โปรดระบุ Status`,
              value.isActive,
              "isActive"
            )
          );
        }

        if (value.id && value.id != null) {
        } else {
          if (_.isEmpty(value.password)) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ Password`,
                value.password,
                "password"
              )
            );
          }
          if (_.isEmpty(value.username)) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ Username`,
                value.username,
                "username"
              )
            );
          }
        }

        if (errors.length > 0) {
          throw new yup.ValidationError(errors);
        }
        return true;
      });
  },
  getData: async (param) => {
    var resultForm: UserMgmtForm = {};

    var _param = "isAll=true";
    if (param) {
      if (param.textSearch && !_.isEmpty(param.textSearch)) {
        _param = `${_param}&TextSearch=${param.textSearch}`;
      }

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
      .get(`${API_APP_URL}/api/SysUser?${_param}`)
      .then((res) => {
        if (res.data.status) {
          resultForm = {
            pageNumber: param.pageNumber,
            pageSize: param.pageSize,
            effectRow: res.data.effectRow,
            dataList: res.data.data,
          };
        } else {
          resultForm = {
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

    var _resultForm = resultForm.dataList?.map((ct) => {
      return {
        ...ct,
        isActive: ct.isActive == true ? "1" : "0",
      };
    });
    return { ...resultForm, dataList: _resultForm };
  },
  userCreate: async (data) => {
    var result = <any | undefined>{};
    await axios
      .post(`${API_APP_URL}/api/SysUser`, {
        ...data,
        isActive: data.isActive == "1" ? true : false,
      })
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
  userUpdate: async (data) => {
    var result = <any | undefined>{};
    await axios
      .patch(`${API_APP_URL}/api/SysUser`, {
        ...data,
        isActive: data.isActive == "1" ? true : false,
      })
      .then((res) => {
        result = res;
      })
      .catch((res: any) => {
        result = res.response;
      });
    return result;
  },
}));

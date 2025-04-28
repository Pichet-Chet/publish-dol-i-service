import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import * as yup from "yup";
import _ from "lodash";

interface RepairState {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  validationData: () => yup.ObjectSchema<{}, RepairForm, {}, "">;
  getData: (getData: string) => Promise<DataFrom>;
  jobCreateUpdate: (data?: DataFrom) => Promise<any | undefined>;
  updateJobDateTime: (
    id: string,
    dateTimeString?: string,
    type?: string
  ) => Promise<any | undefined>;
}

export const useRepairState = create<RepairState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  validationData: () => {
    return yup
      .object<RepairForm>()
      .test("RepairForm", function validate(value: RepairForm) {
        const errors: yup.ValidationError[] = [];

        const _dataFrom = value?.dataFrom;
        const _sysStatusId = _dataFrom?.sysStatusId;
        const _id = _dataFrom?.id;

        const _provinceName = _dataFrom?.provinceName;
        if (_.isEmpty(_provinceName)) {
          errors.push(
            new yup.ValidationError(
              `โปรดระบุ จังหวัด`,
              _provinceName,
              "dataFrom.provinceName"
            )
          );
        }

        const _locationName = _dataFrom?.locationName;
        if (_.isEmpty(_locationName)) {
          errors.push(
            new yup.ValidationError(
              `โปรดระบุ ชื่อสถานที่`,
              _locationName,
              "dataFrom.locationName"
            )
          );
        }

        const _typeRepairData = _dataFrom?.typeRepairData;
        if (_.isEmpty(_typeRepairData)) {
          errors.push(
            new yup.ValidationError(
              `โปรดระบุ วงจร`,
              _typeRepairData,
              "dataFrom.typeRepairData"
            )
          );
        }

        if (!_id || (_sysStatusId && _sysStatusId == "6")) {
          const _jobDescription = _dataFrom?.jobDescription;
          if (_.isEmpty(_jobDescription)) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ รายละเอียดการแจ้งซ่อม`,
                _jobDescription,
                "dataFrom.jobDescription"
              )
            );
          }
        }

        // const _jobContactName = _dataFrom?.jobContactName;
        // if (_.isEmpty(_jobContactName)) {
        //   errors.push(
        //     new yup.ValidationError(
        //       `โปรดระบุ ชื่อ - นามสกุล ผู้ติดต่อ`,
        //       _jobContactName,
        //       "dataFrom.jobContactName"
        //     )
        //   );
        // }

        // const _jobContactTel = _dataFrom?.jobContactTel;
        // if (_.isEmpty(_jobContactTel)) {
        //   errors.push(
        //     new yup.ValidationError(
        //       `โปรดระบุ เบอร์โทรผู้ติดต่อ`,
        //       _jobContactTel,
        //       "dataFrom.jobContactTel"
        //     )
        //   );
        // }

        // const _jobSenderContactName = _dataFrom?.jobSenderContactName;
        // if (_.isEmpty(_jobSenderContactName)) {
        //   errors.push(
        //     new yup.ValidationError(
        //       `โปรดระบุ ชื่อ - นามสกุล ผู้ส่งใบแจ้งซ่อม`,
        //       _jobSenderContactName,
        //       "dataFrom.jobSenderContactName"
        //     )
        //   );
        // }

        // const _jobSenderContactTel = _dataFrom?.jobSenderContactTel;
        // if (_.isEmpty(_jobSenderContactTel)) {
        //   errors.push(
        //     new yup.ValidationError(
        //       `โปรดระบุ เบอร์โทรผู้ส่งใบแจ้งซ่อม`,
        //       _jobSenderContactTel,
        //       "dataFrom.jobSenderContactTel"
        //     )
        //   );
        // }

        // const _jobSenderRemark = _dataFrom?.jobSenderRemark;
        // if (_.isEmpty(_jobSenderRemark)) {
        //   errors.push(
        //     new yup.ValidationError(
        //       `โปรดระบุ หมายเหตุ`,
        //       _jobSenderRemark,
        //       "dataFrom.jobSenderRemark"
        //     )
        //   );
        // }

        if (_id && _sysStatusId && _sysStatusId == "6") {
          // const _caseOfFixId = _dataFrom?.caseOfFixId;
          // if (_.isEmpty(_caseOfFixId)) {
          //   errors.push(
          //     new yup.ValidationError(
          //       `หมวดหมู่การแก้ไข`,
          //       _caseOfFixId,
          //       "dataFrom.caseOfFixId"
          //     )
          //   );
          // }

          const _jobFixedDescription = _dataFrom?.jobFixedDescription;
          if (_.isEmpty(_jobFixedDescription)) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ รายละเอียดการแก้ปัญหา`,
                _jobFixedDescription,
                "dataFrom.jobFixedDescription"
              )
            );
          }

          const _caseOfIssueId = _dataFrom?.caseOfIssueId;
          if (
            (!_caseOfIssueId && _caseOfIssueId == null) ||
            _.isEmpty(_caseOfIssueId.toLocaleString())
          ) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ สาเหตุเสียหลัก`,
                _caseOfIssueId,
                "dataFrom.caseOfIssueId"
              )
            );
          }

          const _caseOfIssueSubId = _dataFrom?.caseOfIssueSubId;
          if (
            (!_caseOfIssueSubId && _caseOfIssueSubId == null) ||
            _.isEmpty(_caseOfIssueSubId.toLocaleString())
          ) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ สาเหตุเสียย่อย`,
                _caseOfIssueSubId,
                "dataFrom.caseOfIssueSubId"
              )
            );
          }

          // const _jobFixedComment = _dataFrom?.jobFixedComment;
          // if (_.isEmpty(_jobFixedComment)) {
          //   errors.push(
          //     new yup.ValidationError(
          //       `โปรดระบุ Comment`,
          //       _jobFixedComment,
          //       "dataFrom.jobFixedComment"
          //     )
          //   );
          // }

          const _jobFixedContactName = _dataFrom?.jobFixedContactName;
          if (_.isEmpty(_jobFixedContactName)) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ ชื่อ - นามสกุล ผู้แก้ไข`,
                _jobFixedContactName,
                "dataFrom.jobFixedContactName"
              )
            );
          }

          const _jobFixedContactTel = _dataFrom?.jobFixedContactTel;
          if (_.isEmpty(_jobFixedContactTel)) {
            errors.push(
              new yup.ValidationError(
                `โปรดระบุ เบอร์โทรผู้แก้ไข`,
                _jobFixedContactTel,
                "dataFrom.jobFixedContactTel"
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
  getData: async (orderId) => {
    var resultForm: DataFrom = {};

    const url = `${API_APP_URL}/api/JobRepair/${orderId}`;
    await axios
      .get(url)
      .then((res) => {
        if (res.status == 200) {
          if (res.data.status && res.data?.data && res.data?.data.length > 0) {
            const data = res.data.data[0];
            resultForm = {
              id: data.id,
              documentNo: data.documentNo,
              provinceName: data.siteInformation.provinceName,
              locationName: data.siteInformation.locationName,
              siteInformationId: data.siteInformationId,
              siteNetworkId: data.siteNetworkId,
              siteNetworkName: data.siteInformation.siteNetworkName,
              siteNetworkSeq: data.siteInformation.siteNetworkSeq,
              address: data.siteInformation.address,

              typeRepairData: data.typeRepairData,
              typeRepairValue: data.typeRepairValue,
              documentRequest: data.documentRequest,

              wan1Provider: data.siteInformation.wan1Provider,
              wan1Cid: data.siteInformation.wan1Cid,
              wan1Speed: data.siteInformation.wan1Speed,
              wan1AsNumber: data.siteInformation.wan1AsNumber,
              wan1IpWan1Pe: data.siteInformation.wan1IpWan1Pe,
              wan1IpWan1Ce: data.siteInformation.wan1IpWan1Ce,
              wan1Subnet: data.siteInformation.wan1Subnet,
              wan2Provider: data.siteInformation.wan2Provider,
              wan2Cid: data.siteInformation.wan2Cid,
              wan2Speed: data.siteInformation.wan2Speed,
              wan2AsNumber: data.siteInformation.wan2AsNumber,
              wan2IpWan1Pe: data.siteInformation.wan2IpWan1Pe,
              wan2IpWan1Ce: data.siteInformation.wan2IpWan1Ce,
              wan2Subnet: data.siteInformation.wan2Subnet,
              internetCid: data.siteInformation.internetCid,
              internetSpeed: data.siteInformation.internetSpeed,
              internetAsNumber: data.siteInformation.internetAsNumber,
              internetWanIpAddress: data.siteInformation.internetWanIpAddress,
              internetSubnet: data.siteInformation.internetSubnet,
              cellularSim: data.siteInformation.cellularSim,
              cellularAr109: data.siteInformation.cellularAr109,

              jobDescription: data.jobDescription,
              jobContactName: data.jobContactName,
              jobContactTel: data.jobContactTel,
              jobSenderContactName: data.jobSenderContactName,
              jobSenderContactTel: data.jobSenderContactTel,
              jobSenderRemark: data.jobSenderRemark,

              jobFixedContactName: data.jobFixedContactName,
              jobFixedContactTel: data.jobFixedContactTel,
              jobFixedDescription: data.jobFixedDescription,
              jobFixedComment: data.jobFixedComment,

              jobImage1: data.jobImage1,
              jobImage2: data.jobImage2,
              jobImage3: data.jobImage3,
              jobImage4: data.jobImage4,

              sysStatusId: data.sysStatusId,

              caseOfIssueId: data.caseOfIssueId,
              caseOfIssueName: "",
              caseOfIssueSubId: data.caseOfIssueSubId,
              caseOfIssueSubName: "",
              caseOfFixName: "",

              jobAcceptBy: data.jobAcceptBy,
              jobAcceptByName: data.jobAcceptByName,
              jobAcceptDate: data.jobAcceptDate,
              jobCompleteBy: data.jobCompleteBy,
              jobCompleteByName: data.jobCompleteByName,
              jobCompleteDate: data.jobCompleteDate,
              jobCreatedBy: data.jobCreatedBy,
              jobCreatedByName: data.jobCreatedByName,
              jobCreatedDate: data.jobCreatedDate,
              jobProcessBy: data.jobProcessBy,
              jobProcessByName: data.jobProcessByName,
              jobProcessDate: data.jobProcessDate,
            };
          }
        }
      })
      .catch((res: any) => {
        resultForm = res.response;
      });
    return resultForm;
  },
  jobCreateUpdate: async (data) => {
    console.log("jobCreateUpdate => data", data);

    var result = <any | undefined>{};
    if (data) {
      const formData = new FormData();
      formData.append(
        `SiteNetworkId`,
        data.siteNetworkId ? data.siteNetworkId.toLocaleString() : ""
      );
      formData.append(
        `SiteInformationId`,
        data.siteInformationId ? data.siteInformationId.toLocaleString() : ""
      );
      formData.append(`JobDescription`, data.jobDescription || "");
      formData.append(`JobContactName`, data.jobContactName || "");
      formData.append(`JobContactTel`, data.jobContactTel || "");
      formData.append(`JobSenderContactName`, data.jobSenderContactName || "");
      formData.append(`JobSenderContactTel`, data.jobSenderContactTel || "");
      formData.append(`JobSenderRemark`, data.jobSenderRemark || "");
      formData.append(`JobFixedDescription`, data.jobFixedDescription || "");
      formData.append(`JobFixedComment`, data.jobFixedComment || "");
      formData.append(`JobFixedContactName`, data.jobFixedContactName || "");
      formData.append(`JobFixedContactTel`, data.jobFixedContactTel || "");
      formData.append(`JobImage1`, data.jobImage1 || "");
      formData.append(`JobImage2`, data.jobImage2 || "");
      formData.append(`JobImage3`, data.jobImage3 || "");
      formData.append(`JobImage4`, data.jobImage4 || "");
      formData.append(`SysStatusId`, data.sysStatusId || "");
      formData.append(`JobCreatedBy`, data.jobCreatedBy || "");
      formData.append(`JobAcceptBy`, data.jobAcceptBy || "");
      formData.append(`JobProcessBy`, data.jobProcessBy || "");
      formData.append(`JobCompleteBy`, data.jobCompleteBy || "");
      formData.append(`TypeRepairData`, data.typeRepairData || "");
      formData.append(`TypeRepairValue`, data.typeRepairValue || "");
      formData.append(`DocumentRequest`, data.documentRequest || "");
      formData.append(
        `CaseOfIssueId`,
        data.caseOfIssueId ? data.caseOfIssueId.toLocaleString() : ""
      );
      formData.append(
        `CaseOfIssueSubId`,
        data.caseOfIssueSubId ? data.caseOfIssueSubId.toLocaleString() : ""
      );
      // formData.append(
      //   `CaseOfFixId`,
      //   data.caseOfFixId ? data.caseOfFixId.toLocaleString() : ""
      // );
      if (data.fileUpload1) {
        formData.append(`FileUpload1`, data.fileUpload1);
      }
      if (data.fileUpload2) {
        formData.append(`FileUpload2`, data.fileUpload2);
      }
      if (data.fileUpload3) {
        formData.append(`FileUpload3`, data.fileUpload3);
      }
      if (data.fileUpload4) {
        formData.append(`FileUpload4`, data.fileUpload4);
      }

      if (data.id && data.id != null) {
        formData.append(`Id`, data.id.toLocaleString() || "");
        formData.append(`DocumentNo`, data.documentNo || "");

        // console.log("formData", formData);

        await axios
          .post(`${API_APP_URL}/api/JobRepair/Update`, formData, {
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
      } else {
        await axios
          .post(`${API_APP_URL}/api/JobRepair`, formData, {
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
      }
    }
    return result;
  },
  updateJobDateTime: async (id, dateTimeString, type) => {
    var result = <any | undefined>{};

    const formData = new FormData();
    formData.append(`Id`, id);
    formData.append(`Value`, dateTimeString || "");
    formData.append(`Flag`, type || "");

    await axios
      .patch(`${API_APP_URL}/api/JobRepair/UpdateTime`, formData, {
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
}));

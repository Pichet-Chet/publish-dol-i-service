import { useState } from "react";
import { useForm, UseFormReturn } from "react-hook-form";
import {
  Input,
  Timeline,
  Select,
  Button,
  Modal,
  DatePicker,
  ConfigProvider,
} from "antd";
import dayjs from "dayjs";

import type { DatePickerProps, GetProps } from "antd";
import { ClockCircleOutlined, SmileOutlined } from "@ant-design/icons";
import InputFieldComponent, { InputMode } from "../../containers/inputField";
import { useMasterState } from "@/app/_store/master";
import { useRepairState } from "@/app/_store/repair";
const { TextArea } = Input;

export default function FixInformationAfterComponent({
  methods = useForm<RepairForm>(),
  jobCreateUpdate,
  isRoleDOL,
}: {
  methods?: UseFormReturn<RepairForm>;
  jobCreateUpdate: (data?: DataFrom | undefined) => Promise<any>;
  isRoleDOL: boolean;
}) {
  const { getValues, setValue, trigger, watch, setError, resetField } = methods;
  const _masterCollection = getValues().mstCollection;
  var _dataFrom = getValues().dataFrom;
  const requestId = _dataFrom?.id || "";
  const sysStatusId = _dataFrom?.sysStatusId || "";
  const getMstCaseOfIssueSub = useMasterState(
    (state) => state.getMstCaseOfIssueSub
  );
  const getMstFixCase = useMasterState((state) => state.getMstFixCase);
  const updateJobDateTime = useRepairState((state) => state.updateJobDateTime);

  const [editDateOpen, setEditDateOpen] = useState(false);
  const [editDateTitle, setEditDateTitle] = useState("");
  const [editDate, setEditDate] = useState<Date>();
  const [editDateType, setEditDateType] = useState("");

  const handleCancel = () => setEditDateOpen(false);
  const onSubmitEditDate = async () => {
    await updateJobDateTime(
      `${requestId}`,
      dayjs(editDate).format("YYYY-MM-DD HH:mm"),
      editDateType
    ).then((res) => {
      setEditDateOpen(false);
      setValue("dataFrom.jobCreatedDate", res.data.data.jobCreatedDate);
      setValue("dataFrom.jobProcessDate", res.data.data.jobProcessDate);
      setValue("dataFrom.jobCompleteDate", res.data.data.jobCompleteDate);
      setEditDateType("");
      setEditDate(undefined);
      setEditDateTitle("");
    });
  };

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-5">
      <div className="grid grid-cols-1">
        <div className="grid grid-cols-2 gap-x-5">
          <InputFieldComponent
            name="dataFrom.caseOfIssueId"
            label={"สาเหตุเสียหลัก"}
            require={true}
            mode={InputMode.secondary}
            labelAlignClassName="text-left"
            labelClassName="md:w-4/12"
            inputClassName="md:w-8/12"
            renderControl={(field) => (
              <Select
                {...field}
                className="w-full"
                size="large"
                placeholder="กรุณาเลือก"
                disabled={isRoleDOL}
                options={(_masterCollection?.mstCaseOfIssue || []).map((ct) => {
                  return {
                    value: +(ct.value || 0),
                    label: ct.data,
                  };
                })}
                onChange={async (value: string, e: any) => {
                  if (e) {
                    const selected = e as Options;
                    resetField("dataFrom.caseOfIssueId");
                    resetField("dataFrom.caseOfIssueSubId");
                    resetField("dataFrom.caseOfIssueSubName");
                    setValue("dataFrom.caseOfIssueId", +selected.value);
                    setValue("dataFrom.caseOfIssueName", selected.label);
                    await getMstCaseOfIssueSub(selected.value)
                      .then(async (data) => {
                        setValue("mstCollection.mstCaseOfIssueSub", data);
                        var reSaveData = getValues().dataFrom;
                        await jobCreateUpdate(reSaveData);
                      })
                      .catch((error) => {
                        console.log("#Error", error);
                      });
                  }
                }}
              />
            )}
          ></InputFieldComponent>
          <InputFieldComponent
            name="dataFrom.caseOfIssueSubId"
            label={"สาเหตุเสียย่อย"}
            require={true}
            mode={InputMode.secondary}
            labelAlignClassName="text-left"
            labelClassName="md:w-4/12"
            inputClassName="md:w-8/12"
            renderControl={(field) => (
              <Select
                {...field}
                className="w-full"
                size="large"
                placeholder="กรุณาเลือก"
                disabled={isRoleDOL}
                options={(_masterCollection?.mstCaseOfIssueSub || []).map(
                  (ct) => {
                    return {
                      value: +(ct.value || 0),
                      label: ct.data,
                    };
                  }
                )}
                onChange={async (value: string, e: any) => {
                  if (e) {
                    const selected = e as Options;
                    resetField("dataFrom.caseOfIssueSubId");
                    setValue("dataFrom.caseOfIssueSubId", +selected.value);
                    setValue("dataFrom.caseOfIssueSubName", selected.label);
                    await getMstFixCase(selected.value)
                      .then(async (data) => {
                        setValue("mstCollection.mstFixCase", data);
                        setValue(
                          "dataFrom.caseOfFixName",
                          data.length > 0 ? data[0].data : ""
                        );
                        var reSaveData = getValues().dataFrom;
                        await jobCreateUpdate(reSaveData);
                      })
                      .catch((error) => {
                        console.log("#Error", error);
                      });
                  }
                }}
              />
            )}
          ></InputFieldComponent>
          <InputFieldComponent
            name="dataFrom.caseOfFixName"
            label={"วิธีการแก้ไข"}
            require={true}
            mode={InputMode.secondary}
            labelAlignClassName="text-left"
            labelClassName="md:w-4/12"
            inputClassName="md:w-8/12"
            renderControl={(field) => (
              <Input
                {...field}
                className="w-full"
                size="large"
                maxLength={20}
                disabled={true}
              />
              // <Select
              //     {...field}
              //     className="w-full"
              //     size="large"
              //     placeholder="กรุณาเลือก"
              //     disabled={true}
              //     options={(_masterCollection?.mstFixCase || []).map(ct => {
              //         return {
              //             value: +(ct.value || 0),
              //             label: ct.data
              //         };
              //     })}
              //     onBlur={async (a) => {
              //         const reSaveData = getValues().dataFrom;
              //         await jobCreateUpdate(reSaveData);
              //     }}
              // />
            )}
          ></InputFieldComponent>
        </div>
        <InputFieldComponent
          name="dataFrom.jobFixedDescription"
          label={"รายละเอียดการแก้ปัญหา"}
          require={sysStatusId != "5" ? true : false}
          mode={InputMode.secondary}
          labelAlignClassName="text-left"
          labelClassName="md:w-4/12"
          inputClassName="md:w-8/12"
          renderControl={(field) => (
            <TextArea
              {...field}
              className="w-full"
              size="large"
              rows={4}
              disabled={isRoleDOL}
              onBlur={async (a) => {
                if (!isRoleDOL) {
                  if (a.target.value != "") {
                    var reSaveData = getValues().dataFrom;
                    await jobCreateUpdate(reSaveData);
                  } else {
                    setError("dataFrom.jobFixedDescription", {
                      type: "required",
                      message: "โปรดระบุ รายละเอียดการแก้ปัญหา",
                    });
                  }
                }
              }}
            />
          )}
        ></InputFieldComponent>
        <InputFieldComponent
          name="dataFrom.jobFixedComment"
          label={"Comment"}
          // require={true}
          mode={InputMode.secondary}
          labelAlignClassName="text-left"
          labelClassName="md:w-4/12"
          inputClassName="md:w-8/12"
          renderControl={(field) => (
            <TextArea
              {...field}
              className="w-full"
              size="large"
              rows={4}
              disabled={isRoleDOL}
              onBlur={async (a) => {
                if (!isRoleDOL) {
                  if (a.target.value != "") {
                    const reSaveData = getValues().dataFrom;
                    await jobCreateUpdate(reSaveData);
                  }
                }
              }}
            />
          )}
        ></InputFieldComponent>
        <div className="grid grid-cols-2 gap-x-5">
          <InputFieldComponent
            name="dataFrom.jobFixedContactName"
            label={"ชื่อ - นามสกุล ผู้แก้ไข"}
            require={sysStatusId != "5" ? true : false}
            mode={InputMode.secondary}
            labelAlignClassName="text-left"
            labelClassName="md:w-4/12"
            inputClassName="md:w-8/12"
            renderControl={(field) => (
              <Input
                {...field}
                className="w-full"
                size="large"
                disabled={isRoleDOL}
                onBlur={async (a) => {
                  if (!isRoleDOL) {
                    if (a.target.value != "") {
                      var reSaveData = getValues().dataFrom;
                      await jobCreateUpdate(reSaveData);
                    } else {
                      setError("dataFrom.jobFixedContactName", {
                        type: "required",
                        message: "โปรดระบุ ชื่อ - นามสกุล ผู้แก้ไข",
                      });
                    }
                  }
                }}
              />
            )}
          ></InputFieldComponent>
          <InputFieldComponent
            name="dataFrom.jobFixedContactTel"
            label={"เบอร์โทรผู้แก้ไข"}
            require={sysStatusId != "5" ? true : false}
            mode={InputMode.secondary}
            labelAlignClassName="text-left"
            labelClassName="md:w-4/12"
            inputClassName="md:w-8/12"
            renderControl={(field) => (
              <Input
                {...field}
                className="w-full"
                size="large"
                disabled={isRoleDOL}
                onBlur={async (a) => {
                  if (!isRoleDOL) {
                    if (a.target.value != "") {
                      var reSaveData = getValues().dataFrom;
                      await jobCreateUpdate(reSaveData);
                    } else {
                      setError("dataFrom.jobFixedContactTel", {
                        type: "required",
                        message: "โปรดระบุ เบอร์โทรผู้แก้ไข",
                      });
                    }
                  }
                }}
              />
            )}
          ></InputFieldComponent>
        </div>
      </div>
      <div className="grid grid-cols-1">
        <Timeline
          items={
            sysStatusId == "5"
              ? [
                  {
                    children: (
                      <span>
                        รับแจ้ง รอดำเนินการ :{" "}
                        {dayjs(getValues().dataFrom?.jobCreatedDate).format(
                          "DD/MM/YYYY HH:mm"
                        )}
                        <span
                          onClick={() => {
                            if (!isRoleDOL) {
                              setEditDateTitle(
                                "แก้ไขวันที่/เวลา รับแจ้ง รอดำเนินการ"
                              );
                              setEditDate(getValues().dataFrom?.jobCreatedDate);
                              setEditDateType("JobCreatedDate");
                              setEditDateOpen(true);
                            }
                          }}
                        >
                          {" "}
                          โดย{" "}
                        </span>
                        {_dataFrom?.jobCreatedByName}
                      </span>
                    ),
                    color: "green",
                  },
                  {
                    dot: <ClockCircleOutlined />,
                    children: `ระหว่างดำเนินการ : -`,
                  },
                  {
                    children: `ดำเนินการเสร็จสิ้นแล้ว : -`,
                    color: "gray",
                  },
                ]
              : sysStatusId == "6"
              ? [
                  {
                    children: (
                      <span>
                        รับแจ้ง รอดำเนินการ :{" "}
                        {dayjs(getValues().dataFrom?.jobCreatedDate).format(
                          "DD/MM/YYYY HH:mm"
                        )}
                        <span
                          onClick={() => {
                            if (!isRoleDOL) {
                              setEditDateTitle(
                                "แก้ไขวันที่/เวลา รับแจ้ง รอดำเนินการ"
                              );
                              setEditDate(getValues().dataFrom?.jobCreatedDate);
                              setEditDateType("JobCreatedDate");
                              setEditDateOpen(true);
                            }
                          }}
                        >
                          {" "}
                          โดย{" "}
                        </span>
                        {_dataFrom?.jobCreatedByName}
                      </span>
                    ),
                    color: "green",
                  },
                  {
                    children: (
                      <span>
                        ระหว่างดำเนินการ :{" "}
                        {dayjs(getValues().dataFrom?.jobProcessDate).format(
                          "DD/MM/YYYY HH:mm"
                        )}
                        <span
                          onClick={() => {
                            if (!isRoleDOL) {
                              setEditDateTitle(
                                "แก้ไขวันที่/เวลา ระหว่างดำเนินการ"
                              );
                              setEditDate(getValues().dataFrom?.jobProcessDate);
                              setEditDateType("JobProcessDate");
                              setEditDateOpen(true);
                            }
                          }}
                        >
                          {" "}
                          โดย{" "}
                        </span>
                        {_dataFrom?.jobProcessByName}
                      </span>
                    ),
                    color: "green",
                  },
                  {
                    dot: <ClockCircleOutlined />,
                    children: `ดำเนินการเสร็จสิ้นแล้ว : -`,
                  },
                ]
              : sysStatusId == "7"
              ? [
                  {
                    children: (
                      <span>
                        รับแจ้ง รอดำเนินการ :{" "}
                        {dayjs(getValues().dataFrom?.jobCreatedDate).format(
                          "DD/MM/YYYY HH:mm"
                        )}
                        <span
                          onClick={() => {
                            if (!isRoleDOL) {
                              setEditDateTitle(
                                "แก้ไขวันที่/เวลา รับแจ้ง รอดำเนินการ"
                              );
                              setEditDate(getValues().dataFrom?.jobCreatedDate);
                              setEditDateType("JobCreatedDate");
                              setEditDateOpen(true);
                            }
                          }}
                        >
                          {" "}
                          โดย{" "}
                        </span>
                        {_dataFrom?.jobCreatedByName}
                      </span>
                    ),
                    color: "green",
                  },
                  {
                    children: (
                      <span>
                        ระหว่างดำเนินการ :{" "}
                        {dayjs(getValues().dataFrom?.jobProcessDate).format(
                          "DD/MM/YYYY HH:mm"
                        )}
                        <span
                          onClick={() => {
                            if (!isRoleDOL) {
                              setEditDateTitle(
                                "แก้ไขวันที่/เวลา ระหว่างดำเนินการ"
                              );
                              setEditDate(getValues().dataFrom?.jobProcessDate);
                              setEditDateType("JobProcessDate");
                              setEditDateOpen(true);
                            }
                          }}
                        >
                          {" "}
                          โดย{" "}
                        </span>
                        {_dataFrom?.jobProcessByName}
                      </span>
                    ),
                    color: "green",
                  },
                  {
                    dot: <SmileOutlined />,
                    children: (
                      <span>
                        ดำเนินการเสร็จสิ้นแล้ว :{" "}
                        {dayjs(getValues().dataFrom?.jobCompleteDate).format(
                          "DD/MM/YYYY HH:mm"
                        )}
                        <span
                          onClick={() => {
                            if (!isRoleDOL) {
                              setEditDateTitle(
                                "แก้ไขวันที่/เวลา ดำเนินการเสร็จสิ้นแล้ว"
                              );
                              setEditDate(
                                getValues().dataFrom?.jobCompleteDate
                              );
                              setEditDateType("JobCompleteDate");
                              setEditDateOpen(true);
                            }
                          }}
                        >
                          {" "}
                          โดย{" "}
                        </span>
                        {_dataFrom?.jobCompleteByName}
                      </span>
                    ),
                    color: "green",
                  },
                ]
              : []
          }
        />
        {!isRoleDOL && (
          <div className="gap-2 justify-start" style={{ display: "flex" }}>
            <Button
              className="bg-indigo-500 text-white"
              size="large"
              onClick={() => {
                trigger().then((res) => {
                  if (res) {
                    var submitData: RepairForm = {
                      ...getValues(),
                      dataFrom: {
                        ...getValues().dataFrom,
                        sysStatusId: "6",
                      },
                    };
                    // onSubmit(submitData);
                  }
                });
              }}
              disabled={sysStatusId == "5" ? false : true}
            >
              เริ่มดำเนินการ
            </Button>
            <Button
              className="bg-indigo-500 text-white"
              size="large"
              onClick={() => {
                trigger().then((res) => {
                  if (res) {
                    var submitData: RepairForm = {
                      ...getValues(),
                      dataFrom: {
                        ...getValues().dataFrom,
                        sysStatusId: "7",
                      },
                    };
                    // onSubmit(submitData);
                  }
                });
              }}
              disabled={sysStatusId == "6" ? false : true}
            >
              ดำเนินการเสร็จสิ้น
            </Button>
          </div>
        )}
      </div>
      {
        <Modal
          open={editDateOpen}
          title={editDateTitle}
          onCancel={handleCancel}
          footer={[
            <Button
              key="submit"
              className="bg-indigo-500 text-white"
              onClick={() => onSubmitEditDate()}
            >
              ยืนยัน
            </Button>,
          ]}
        >
          <ConfigProvider
            button={{
              style: { backgroundColor: "#4096ff" },
            }}
          >
            <DatePicker
              format={"DD/MM/YYYY HH:mm"}
              showTime={true}
              showNow={false}
              allowClear={false}
              value={dayjs(editDate)}
              onOk={(value) => {
                setEditDate(value.toDate());
              }}
            />
          </ConfigProvider>
        </Modal>
      }
    </div>
  );
}

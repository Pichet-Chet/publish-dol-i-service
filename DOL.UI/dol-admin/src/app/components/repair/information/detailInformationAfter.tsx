import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select } from 'antd';
import InputFieldComponent, { InputMode } from "../../containers/inputField";
import { watch } from "fs";
const { TextArea } = Input;

export default function DetailInformationAfterComponent({
    methods = useForm<RepairForm>(),
    jobCreateUpdate,
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    jobCreateUpdate: (data?: DataFrom | undefined) => Promise<any>;
    isRoleDOL: boolean;
}) {
    const { getValues, setValue, resetField, watch, setError } = methods;
    const _dataFrom = getValues().dataFrom;
    const requestId = _dataFrom?.id || "";

    return (
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-5">
            <div className="grid grid-cols-1">
                <InputFieldComponent
                    name="dataFrom.jobDescription"
                    label={"รายละเอียดการแจ้งซ่อม"}
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <TextArea
                            {...field}
                            className="w-full"
                            size="large"
                            rows={10}
                            disabled={isRoleDOL}
                            onBlur={async (a) => {
                                if (a.target.value != "") {
                                    var reSaveData = getValues().dataFrom;
                                    await jobCreateUpdate(reSaveData);
                                } else {
                                    setError("dataFrom.jobDescription", {
                                        type: "required",
                                        message: "โปรดระบุ รายละเอียดการแจ้งซ่อม",
                                    })
                                }
                            }}
                        // disabled={requestId != "" ? true : false}
                        />
                    )}
                ></InputFieldComponent>
            </div>
            <div className="grid grid-cols-1">
                <div className="grid grid-cols-2 gap-x-5">
                    <InputFieldComponent
                        name="dataFrom.jobContactName"
                        label={"ชื่อ - นามสกุล ผู้ติดต่อ"}
                        // require={true}
                        mode={InputMode.secondary}
                        labelAlignClassName="text-left"
                        labelClassName="md:w-4/12"
                        inputClassName="md:w-8/12"
                        renderControl={field => (
                            <Input
                                {...field}
                                className="w-full"
                                size="large"
                                disabled={isRoleDOL}
                                onBlur={async (a) => {
                                    // console.log("a", a.target.value);
                                    const reSaveData = getValues().dataFrom;
                                    await jobCreateUpdate(reSaveData);
                                }}
                            // disabled={requestId != "" ? true : false}
                            />
                        )}
                    ></InputFieldComponent>
                    <InputFieldComponent
                        name="dataFrom.jobContactTel"
                        label={"เบอร์โทรผู้ติดต่อ"}
                        // require={true}
                        mode={InputMode.secondary}
                        labelAlignClassName="text-left"
                        labelClassName="md:w-4/12"
                        inputClassName="md:w-8/12"
                        renderControl={field => (
                            <Input
                                {...field}
                                className="w-full"
                                size="large"
                                disabled={isRoleDOL}
                                onBlur={async (a) => {
                                    // console.log("a", a.target.value);
                                    const reSaveData = getValues().dataFrom;
                                    await jobCreateUpdate(reSaveData);
                                }}
                            // disabled={requestId != "" ? true : false}
                            />
                        )}
                    ></InputFieldComponent>
                    <InputFieldComponent
                        name="dataFrom.jobSenderContactName"
                        label={"ชื่อ - นามสกุล ผู้ส่งใบแจ้งซ่อม"}
                        // require={true}
                        mode={InputMode.secondary}
                        labelAlignClassName="text-left"
                        labelClassName="md:w-4/12"
                        inputClassName="md:w-8/12"
                        renderControl={field => (
                            <Input
                                {...field}
                                className="w-full"
                                size="large"
                                disabled={isRoleDOL}
                                onBlur={async (a) => {
                                    // console.log("a", a.target.value);
                                    const reSaveData = getValues().dataFrom;
                                    await jobCreateUpdate(reSaveData);
                                }}
                            // disabled={requestId != "" ? true : false}
                            />
                        )}
                    ></InputFieldComponent>
                    <InputFieldComponent
                        name="dataFrom.jobSenderContactTel"
                        label={"เบอร์โทรผู้ส่งใบแจ้งซ่อม"}
                        // require={true}
                        mode={InputMode.secondary}
                        labelAlignClassName="text-left"
                        labelClassName="md:w-4/12"
                        inputClassName="md:w-8/12"
                        renderControl={field => (
                            <Input
                                {...field}
                                className="w-full"
                                size="large"
                                disabled={isRoleDOL}
                                onBlur={async (a) => {
                                    // console.log("a", a.target.value);
                                    const reSaveData = getValues().dataFrom;
                                    await jobCreateUpdate(reSaveData);
                                }}
                            // disabled={requestId != "" ? true : false}
                            />
                        )}
                    ></InputFieldComponent>
                </div>
                <div className="grid grid-cols-1">
                    <InputFieldComponent
                        name="dataFrom.jobSenderRemark"
                        label={"หมายเหตุ"}
                        // require={true}
                        mode={InputMode.secondary}
                        labelAlignClassName="text-left"
                        labelClassName="md:w-4/12"
                        inputClassName="md:w-8/12"
                        renderControl={field => (
                            <TextArea
                                {...field}
                                className="w-full"
                                size="large"
                                rows={4}
                                disabled={isRoleDOL}
                                onBlur={async (a) => {
                                    // console.log("a", a.target.value);
                                    const reSaveData = getValues().dataFrom;
                                    await jobCreateUpdate(reSaveData);
                                }}
                            // disabled={requestId != "" ? true : false}
                            />
                        )}
                    ></InputFieldComponent>
                </div>
            </div>
        </div>
    );
};
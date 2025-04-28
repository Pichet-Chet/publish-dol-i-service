import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select } from 'antd';
import InputFieldComponent, { InputMode } from "../../containers/inputField";
const { TextArea } = Input;

export default function DetailInformationComponent({
    methods = useForm<RepairForm>(),
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    isRoleDOL: boolean;
}) {
    const { getValues, setValue, resetField } = methods;
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
                            disabled={requestId != "" || isRoleDOL ? true : false}
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
                                disabled={requestId != "" || isRoleDOL ? true : false}
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
                                disabled={requestId != "" || isRoleDOL ? true : false}
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
                                disabled={requestId != "" || isRoleDOL ? true : false}
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
                                disabled={requestId != "" || isRoleDOL ? true : false}
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
                                disabled={requestId != "" || isRoleDOL ? true : false}
                            />
                        )}
                    ></InputFieldComponent>
                </div>
            </div>
        </div>
    );
};
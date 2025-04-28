import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select } from 'antd';
import InputFieldComponent, { InputMode } from "../../containers/inputField";
import { useMasterState } from "@/app/_store/master";

export default function RepairInformationComponent({
    methods = useForm<RepairForm>(),
    jobCreateUpdate,
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    jobCreateUpdate?: (data?: DataFrom | undefined) => Promise<any>;
    isRoleDOL: boolean;
}) {
    const getMstCaseOfIssueSub = useMasterState((state) => state.getMstCaseOfIssueSub);
    const getMstFixCase = useMasterState((state) => state.getMstFixCase);
    const { getValues, setValue, resetField, reset } = methods;
    const _masterCollection = getValues().mstCollection;
    const _dataFrom = getValues().dataFrom;
    const requestId = _dataFrom?.id || "";

    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                <InputFieldComponent
                    name="dataFrom.typeRepairData"
                    label={"วงจร"}
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Select
                            {...field}
                            className="w-full"
                            size="large"
                            placeholder="กรุณาเลือก"
                            disabled={requestId != "" || isRoleDOL ? true : false}
                            options={(_masterCollection?.mstTypeRepair || []).map(ct => {
                                return {
                                    value: ct.value,
                                    label: ct.data
                                };
                            })}
                            onChange={(value: string, e: any) => {
                                if (e) {
                                    const selected = e as Options;
                                    resetField("dataFrom.typeRepairData");
                                    setValue("dataFrom.typeRepairValue", selected.value);
                                    setValue("dataFrom.typeRepairData", selected.label);
                                }
                            }}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dataFrom.documentRequest"
                    label={"เลขที่ใบสั่งงาน"}
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
                            maxLength={20}
                            disabled={isRoleDOL}
                            onBlur={async (a) => {
                                if (jobCreateUpdate && requestId != "") {
                                    var reSaveData = getValues().dataFrom;
                                    await jobCreateUpdate(reSaveData);
                                }
                            }}
                        />
                    )}
                ></InputFieldComponent>
            </div>
        </>
    );
};
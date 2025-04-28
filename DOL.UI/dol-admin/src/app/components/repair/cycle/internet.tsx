import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select } from 'antd';
import InputFieldComponent, { InputMode } from "../../containers/inputField";

export default function InternetComponent({
    methods = useForm<RepairForm>(),
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    isRoleDOL: boolean;
}) {
    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                <InputFieldComponent
                    name="dataFrom.internetCid"
                    label={"CID"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dataFrom.internetSpeed"
                    label={"Speed (Mbps)"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                <InputFieldComponent
                    name="dataFrom.internetAsNumber"
                    label={"AS Number"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dataFrom.internetWanIpAddress"
                    label={"WAN IP Address"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dataFrom.internetSubnet"
                    label={"WAN1 Subnet"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
            </div>
        </>
    );
};
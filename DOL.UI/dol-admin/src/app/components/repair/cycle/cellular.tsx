import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select } from 'antd';
import InputFieldComponent, { InputMode } from "../../containers/inputField";

export default function CellularComponent({
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
                    name="dataFrom.cellularSim"
                    label={"SIM"}
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
                    name="dataFrom.cellularAr109"
                    label={"AR109"}
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
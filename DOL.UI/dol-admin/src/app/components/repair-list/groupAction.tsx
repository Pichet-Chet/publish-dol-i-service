import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select, Button, DatePicker } from 'antd';
import Link from "next/link";
import dayjs from "dayjs";
import type { SearchProps } from 'antd/es/input/Search';
import InputFieldComponent, { InputMode } from "../containers/inputField";
const { Search } = Input;
const { RangePicker } = DatePicker;

export default function GroupActionComponent({
    methods = useForm<RepairListForm>(),
    getDataFromAPI,
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairListForm>;
    getDataFromAPI: () => void;
    isRoleDOL: boolean;
}) {
    const { setValue, watch } = methods;
    const onSearch: SearchProps['onSearch'] = (value, _e, info) => {
        setValue("textSearch", value);
        getDataFromAPI();
    }
    const handleChange = (value: string, name: "statusId" | "outOfSla") => {
        setValue(name, value);
        getDataFromAPI();
    };

    return (
        <>
            <div className="grid grid-cols-1 justify-end gap-2" style={{ display: 'flex' }}>
                <InputFieldComponent
                    name="textSearch"
                    label={""}
                    mode={InputMode.nostyle}
                    renderControl={field => (
                        <Search
                            {...field}
                            placeholder="input search text"
                            allowClear
                            size="large"
                            onSearch={onSearch}
                            className="w-1/4"
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dateFromTo"
                    label={""}
                    mode={InputMode.nostyle}
                    renderControl={field => (
                        <RangePicker
                            {...field}
                            size="large"
                            onChange={(dates, dateString) => {
                                if (dateString && dateString.length == 2) {
                                    setValue("jobCreateDateFrom", dateString[0]);
                                    setValue("jobCreateDateTo", dateString[1]);
                                    setValue("dateFromTo", [
                                        dateString[0] != "" ? dayjs(dateString[0], 'YYYY-MM-DD') : '',
                                        dateString[1] != "" ? dayjs(dateString[1], 'YYYY-MM-DD') : ''
                                    ]);
                                    getDataFromAPI();
                                }
                            }}
                            className="w-1/4"
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="outOfSla"
                    label={""}
                    mode={InputMode.nostyle}
                    renderControl={field => (
                        <Select
                            {...field}
                            defaultValue="all"
                            className="w-52"
                            onChange={(e) => handleChange(e, "outOfSla")}
                            size="large"
                            options={[
                                { value: '', label: 'เกิน SLA' },
                                { value: true, label: 'Yes' },
                                { value: false, label: 'No' },
                            ]}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="statusId"
                    label={""}
                    mode={InputMode.nostyle}
                    renderControl={field => (
                        <Select
                            {...field}
                            defaultValue="all"
                            className="w-52"
                            onChange={(e) => handleChange(e, "statusId")}
                            size="large"
                            options={[
                                { value: '', label: 'All Status' },
                                { value: '5', label: 'รับแจ้งแล้ว รอดำเนินการ' },
                                { value: '6', label: 'ระหว่างดำเนินการ' },
                                { value: '7', label: 'ดำเนินการเสร็จแล้ว' },
                            ]}
                        />
                    )}
                ></InputFieldComponent>
                {
                    !isRoleDOL &&
                    <Link href="/repair">
                        <Button className="bg-indigo-500 text-white" size="large">Add new case</Button>
                    </Link>
                }
            </div>
        </>
    );
};
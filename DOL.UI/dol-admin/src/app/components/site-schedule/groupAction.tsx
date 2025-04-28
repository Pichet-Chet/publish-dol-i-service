import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select, Button, DatePicker } from 'antd';
import Link from "next/link";
import dayjs from "dayjs";
import type { SearchProps } from 'antd/es/input/Search';
import InputFieldComponent, { InputMode } from "../containers/inputField";
const { Search } = Input;
const { RangePicker } = DatePicker;

export default function GroupActionComponent({
    methods = useForm<SiteScheduleListForm>(),
    getDataFromAPI,
}: {
    methods?: UseFormReturn<SiteScheduleListForm>;
    getDataFromAPI: () => void;
}) {
    const { setValue, watch } = methods;
    const onSearch: SearchProps['onSearch'] = (value, _e, info) => {
        setValue("textSearch", value);
        getDataFromAPI();
    }
    const handleChange = (value: string, name: "sysStatusId") => {
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
                    name="sysStatusId"
                    label={""}
                    mode={InputMode.nostyle}
                    renderControl={field => (
                        <Select
                            {...field}
                            defaultValue="all"
                            className="w-52"
                            onChange={(e) => handleChange(e, "sysStatusId")}
                            size="large"
                            options={[
                                { value: '', label: 'All Status' },
                                { value: '2', label: 'Pending' },
                                { value: '3', label: 'On Process' },
                                { value: '4', label: 'Complete' },
                            ]}
                        />
                    )}
                ></InputFieldComponent>
            </div>
        </>
    );
};
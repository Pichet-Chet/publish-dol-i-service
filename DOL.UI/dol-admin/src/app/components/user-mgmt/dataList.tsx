import { FormProvider, SubmitHandler, useForm, UseFormReturn } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import AlertContext from "@/app/providers/alertContext";
import { useState, useContext } from 'react';
import SectionComponent from "@/app/components/containers/section";
import { useUserMgmtState } from "@/app/_store/user-mgmt";
import { Table, Pagination, Modal, Button, Input, Select } from 'antd';
import type { TableProps, PaginationProps } from 'antd';
import InputFieldComponent, { InputMode } from "../containers/inputField";
import _ from "lodash";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

export default function DataListComponent({
    methods = useForm<UserMgmtForm>(),
    getDataFromAPI
}: {
    methods?: UseFormReturn<UserMgmtForm>;
    getDataFromAPI: () => void;
}) {
    const columns: TableProps<TrnUserMgmt>['columns'] = [
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name',
            render(value, record, index) {
                return <span
                    className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                    style={{ cursor: 'pointer' }}
                    onClick={() => showModal(record)}
                >
                    {value}
                </span>
            },
            sorter: (a, b) => { return (a.name || "").localeCompare((b.name || "")) },
        },
        {
            title: 'Username',
            dataIndex: 'username',
            key: 'username',
            render(value, record, index) {
                return value;
            },
            sorter: (a, b) => { return (a.username || "").localeCompare((b.username || "")) },
        },
        {
            title: 'User Group',
            dataIndex: 'userGroup',
            key: 'userGroup',
            render(value, record, index) {
                return value;
            },
            sorter: (a, b) => { return (a.userGroup || "").localeCompare((b.userGroup || "")) },
        },
        {
            title: 'Status',
            dataIndex: 'isActive',
            key: 'isActive',
            render(value, record, index) {
                if (value == true) {
                    return <span className="text-green-500">Active</span>
                } else if (value == false) {
                    return <span className="text-red-500">Suspended</span>
                }
            },
            sorter: (a, b) => Number(a.isActive) - Number(b.isActive),
        }
    ];
    const isLoadingTable = useUserMgmtState((state) => state.isLoadingTable);
    const { getValues, setValue } = methods;
    const _dataList = getValues().dataList || [];
    const onChange: PaginationProps['onChange'] = (current, pageSize) => {
        setValue("pageNumber", current);
        setValue("pageSize", pageSize);
        getDataFromAPI();
    };

    const alertCtx = useContext(AlertContext);
    const setIsLoading = useUserMgmtState((state) => state.setIsLoading);
    const validationData = useUserMgmtState((state) => state.validationData);
    const userUpdate = useUserMgmtState((state) => state.userUpdate);

    const methodsModal = useForm<TrnUserMgmt>({
        mode: "onChange",
        resolver: yupResolver(validationData()),
        defaultValues: {},
    });
    const { getValues: getValuesModal, watch, trigger, reset, resetField } = methodsModal;

    const [isModalOpen, setIsModalOpen] = useState(false);
    const showModal = (data: TrnUserMgmt) => {
        resetField("name");
        resetField("username");
        resetField("password");
        resetField("userGroup");
        reset({ ...data, password: data.password || "" });
        setIsModalOpen(true);
    };
    const handleCancel = () => {
        setIsModalOpen(false);
    };

    const onSubmit: SubmitHandler<TrnUserMgmt> = async (data) => {
        setIsLoading(true);
        await userUpdate(data).then(async (res) => {
            console.log("res =>", res);

            if (res.data.status) {
                // const documentNo = res.data.data.documentNo;
                alertCtx.success("สำเร็จ", "แก้ไขข้อมูลสำเร็จ", {
                    okText: "ตกลง",
                    okButtonProps: {
                        className: "bg-indigo-500 text-white"
                    },
                    onOk: () => {
                        getDataFromAPI();
                        setIsModalOpen(false);
                    },
                });
            } else {
                alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                    okText: "ตกลง",
                    okButtonProps: {
                        className: "bg-indigo-500 text-white"
                    }
                });
                setIsLoading(false);
            }
        }).catch(error => {
            console.log("error", error);
            alertCtx.error("ผิดพลาด", `${error.data.message}`, {
                okText: "ตกลง",
                okButtonProps: {
                    className: "bg-indigo-500 text-white"
                }
            });
            setIsLoading(false);
        });
        setIsLoading(false);
    };


    return (
        <>
            <FormProvider {...methodsModal}>
                <div className="grid grid-cols-1 gap-x-5">
                    <SectionComponent
                        title=""
                        iconName=""
                        bodyClass=""
                        // titleStyle={{ styles: { header: { backgroundColor: 'rgb(99 102 241)', color: '#fff', border: 0 } } }}
                        isCustomTitle={true}
                    >
                        {
                            <>
                                <Table
                                    columns={columns}
                                    dataSource={_.orderBy(_dataList, ['id'], ['desc'])}
                                    // pagination={false}
                                    loading={isLoadingTable}
                                />
                                {/* {
                                    _dataList.length > 0 &&
                                    <div className="mt-3 float-end">
                                        <Pagination
                                            showSizeChanger={false}
                                            onChange={onChange}
                                            defaultCurrent={getValues().pageNumber}
                                            total={getValues().effectRow}
                                            pageSize={getValues().pageSize}
                                            disabled={isLoadingTable}
                                        />
                                    </div>
                                } */}
                            </>
                        }
                    </SectionComponent>
                </div>
                <Modal
                    title="แก้ไขข้อมูล"
                    open={isModalOpen}
                    onCancel={handleCancel}
                    footer={[
                        <Button
                            key="submit"
                            className="bg-indigo-500 text-white"
                            onClick={() => {
                                trigger().then(res => {
                                    if (res) {
                                        onSubmit(getValuesModal());
                                    }
                                });
                            }}>
                            Update
                        </Button>
                    ]}
                >
                    <div className="grid grid-cols-1">
                        <InputFieldComponent
                            name="name"
                            label={"Name"}
                            require={true}
                            mode={InputMode.secondary}
                            labelAlignClassName="text-left"
                            labelClassName="md:w-4/12"
                            inputClassName="md:w-8/12"
                            renderControl={field => (
                                <Input
                                    {...field}
                                    className="w-full"
                                />
                            )}
                        ></InputFieldComponent>
                        <InputFieldComponent
                            name="username"
                            label={"Username"}
                            // require={true}
                            mode={InputMode.secondary}
                            labelAlignClassName="text-left"
                            labelClassName="md:w-4/12"
                            inputClassName="md:w-8/12"
                            renderControl={field => (
                                <Input
                                    {...field}
                                    className="w-full"
                                    disabled={true}
                                />
                            )}
                        ></InputFieldComponent>
                        <InputFieldComponent
                            name="password"
                            label={"Password"}
                            require={getValuesModal().id ? false : true}
                            mode={InputMode.secondary}
                            labelAlignClassName="text-left"
                            labelClassName="md:w-4/12"
                            inputClassName="md:w-8/12"
                            renderControl={field => (
                                <Input.Password
                                    {...field}
                                    className="w-full"
                                    size="large"
                                />
                            )}
                        ></InputFieldComponent>
                        <InputFieldComponent
                            name="userGroup"
                            label={"User Group"}
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
                                    options={[
                                        {
                                            value: "Admin",
                                            label: "Admin"
                                        }, {
                                            value: "Staff",
                                            label: "Staff"
                                        }, {
                                            value: "Helpdesk",
                                            label: "Helpdesk"
                                        }, {
                                            value: "Onsite Team",
                                            label: "Onsite Team"
                                        }, {
                                            value: "DOL",
                                            label: "DOL"
                                        }
                                    ]}
                                />
                            )}
                        ></InputFieldComponent>
                        <InputFieldComponent
                            name="isActive"
                            label={"Status"}
                            require={true}
                            mode={InputMode.secondary}
                            labelAlignClassName="text-left"
                            labelClassName="md:w-4/12"
                            inputClassName="md:w-8/12"
                            renderControl={field => (
                                <Select
                                    {...field}
                                    className="w-full"
                                    defaultActiveFirstOption={true}
                                    size="large"
                                    options={[
                                        {
                                            value: "1",
                                            label: "Active"
                                        }, {
                                            value: "0",
                                            label: "Suspended"
                                        }
                                    ]}
                                />
                            )}
                        ></InputFieldComponent>
                    </div>
                    {/* <JsonViewerComponent data={watch()}></JsonViewerComponent> */}
                </Modal>
            </FormProvider>
        </>
    );
};
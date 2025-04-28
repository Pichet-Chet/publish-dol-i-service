
import { FormProvider, SubmitHandler, useForm, UseFormReturn } from "react-hook-form";
import { useSession } from "next-auth/react";
import { yupResolver } from "@hookform/resolvers/yup";
import AlertContext from "@/app/providers/alertContext";
import { useState, useContext } from 'react';
import { Input, Select, Button, Modal } from 'antd';
import type { SearchProps } from 'antd/es/input/Search';
import InputFieldComponent, { InputMode } from "../containers/inputField";
import { useUserMgmtState } from "@/app/_store/user-mgmt";
import _ from "lodash";
const { Search } = Input;

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

export default function GroupActionComponent({
    methodsMain = useForm<UserMgmtForm>(),
    getDataFromAPI
}: {
    methodsMain?: UseFormReturn<UserMgmtForm>;
    getDataFromAPI: () => void;
}) {
    const { data: session } = useSession();
    const alertCtx = useContext(AlertContext);
    const setIsLoading = useUserMgmtState((state) => state.setIsLoading);
    const validationData = useUserMgmtState((state) => state.validationData);
    const userCreate = useUserMgmtState((state) => state.userCreate);

    const methods = useForm<TrnUserMgmt>({
        mode: "onChange",
        resolver: yupResolver(validationData()),
        defaultValues: {
            name: undefined,
            username: undefined,
            password: undefined,
            userGroup: undefined,
            isActive: "1"
        },
    });
    const { formState: { errors }, getValues, watch, setValue, trigger, resetField } = methods;

    const onSearch: SearchProps['onSearch'] = (value, _e, info) => {
        methodsMain.setValue("textSearch", value);
        getDataFromAPI();
    }

    const [isModalOpen, setIsModalOpen] = useState(false);
    const showModal = () => {
        resetField("name");
        resetField("username");
        resetField("password");
        resetField("userGroup");
        setIsModalOpen(true);
    };
    const handleCancel = () => {
        setIsModalOpen(false);
    };

    const onSubmit: SubmitHandler<TrnUserMgmt> = async (data) => {
        setIsLoading(true);
        await userCreate({
            ...data,
            createBy: _.get(session?.user, ['username']),
            updateBy: _.get(session?.user, ['username']),
        }).then(async (res) => {
            // console.log("res =>", res);
            if (res.data.status == 200 || res.data.status == true) {
                // const documentNo = res.data.data.documentNo;
                alertCtx.success("สำเร็จ", "เพิ่มข้อมูลสำเร็จ", {
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
                alertCtx.error("ผิดพลาด", `${res?.data?.title || res?.data?.message}`, {
                    okText: "ตกลง",
                    okButtonProps: {
                        className: "bg-indigo-500 text-white"
                    }
                });
                setIsLoading(false);
            }
        }).catch(error => {
            console.log("error", error);
            alertCtx.error("ผิดพลาด", `${error?.data?.title || error?.data?.message}`, {
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
            <FormProvider {...methods}>
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
                    <Button
                        className="bg-indigo-500 text-white"
                        size="large"
                        onClick={showModal}
                    >
                        Add new user
                    </Button>
                </div>
                <Modal
                    title="เพิ่มข้อมูล"
                    open={isModalOpen}
                    onCancel={handleCancel}
                    footer={[
                        <Button
                            key="submit"
                            className="bg-indigo-500 text-white"
                            onClick={() => {
                                trigger().then(res => {
                                    if (res) {
                                        onSubmit(getValues());
                                    }
                                });
                            }}>
                            Add New User
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
                            name="password"
                            label={"Password"}
                            require={true}
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
                    </div>
                    {/* <JsonViewerComponent data={watch()}></JsonViewerComponent>
                    <JsonViewerComponent data={errors}></JsonViewerComponent> */}
                </Modal>
            </FormProvider>
        </>
    );
};
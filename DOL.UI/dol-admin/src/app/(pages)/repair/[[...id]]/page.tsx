"use client"

import React from 'react';
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import AlertContext from "@/app/providers/alertContext";
import { useEffect, useState, useContext } from "react";
import { useRouter, usePathname, useParams } from "next/navigation";
import { useSession } from "next-auth/react";
import _ from "lodash";
import Link from "next/link";
import { Button } from 'antd';
import LoadingSection from "../../../components/loading/loadingSection";

import Can from '../../../components/rule/Can';
import AccessDenied from '../../../components/utils/403';

import FormNewCaseComponent from "@/app/components/repair/formNewCase";
import FormEditCaseComponent from "@/app/components/repair/formEditCase";

import { useRepairState } from "@/app/_store/repair";
import { useMasterState } from "@/app/_store/master";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const Repair = ({ params }: { params: any | undefined }) => {
    const { data: session } = useSession();
    const alertCtx = useContext(AlertContext);
    const router = useRouter();
    const pathParams = useParams();
    const pathname = usePathname();

    const isLoading = useRepairState((state) => state.isLoading);
    const setIsLoading = useRepairState((state) => state.setIsLoading);
    const validationData = useRepairState((state) => state.validationData);
    const getData = useRepairState((state) => state.getData);
    const jobCreateUpdate = useRepairState((state) => state.jobCreateUpdate);

    const methods = useForm<RepairForm>({
        mode: "onChange",
        resolver: yupResolver(validationData()),
        defaultValues: undefined,
    });
    const { formState: { errors }, getValues, watch, reset, setValue, trigger } = methods;

    const [isRoleDOL, setIsRoleDOL] = useState<boolean>(false);
    const [requestId, setRequestId] = useState<string>("");
    const getMstProvince = useMasterState((state) => state.getMstProvince);
    const getMstCaseOfIssueSub = useMasterState((state) => state.getMstCaseOfIssueSub);
    const getMstFixCase = useMasterState((state) => state.getMstFixCase);
    // const getMstCaseOfFixed = useMasterState((state) => state.getMstCaseOfFixed);
    const getMstCaseOfIssue = useMasterState((state) => state.getMstCaseOfIssue);
    const getMstLocationByProvinceName = useMasterState((state) => state.getMstLocationByProvinceName);
    const getMstTypeRepairBySiteNetworkId = useMasterState((state) => state.getMstTypeRepairBySiteNetworkId);

    const getLocationByProvinceName = (provinceName: string) => {
        setIsLoading(true);
        getMstLocationByProvinceName(provinceName).then((data) => {
            setValue("mstCollection.mstLocation", data);
            setIsLoading(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }
    const getSiteNetworkBySiteNetworkId = (siteNetworkId: number) => {
        setIsLoading(true);
        getMstTypeRepairBySiteNetworkId(siteNetworkId).then((data) => {
            setValue("mstCollection.mstTypeRepair", data);
            setIsLoading(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }

    useEffect(() => {
        setIsLoading(true);

        var requestId: string = "";
        if (_.isArray(params?.id)) {
            requestId = params?.id[0];
            setRequestId(requestId);
        }
        setIsRoleDOL(["DOL"].includes(_.get(session?.user, ['userGroup'])));

        getDataRepair(requestId).then((data) => {
            setIsLoading(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }, [requestId]);

    const getDataRepair = async (requestId: string) => {
        if (requestId && requestId != null) {
            await getData(requestId).then(async (data) => {
                setValue("dataFrom", data);
                await getMstCaseOfIssueSub(data.caseOfIssueId?.toLocaleString() || "").then((dataMstCaseOfIssueSub) => {
                    setValue("mstCollection.mstCaseOfIssueSub", dataMstCaseOfIssueSub);

                    var filterData = dataMstCaseOfIssueSub.filter(a => a.value == getValues().dataFrom?.caseOfIssueSubId);
                    setValue("dataFrom.caseOfIssueSubName", filterData.length > 0 ? filterData[0].data : "");
                }).catch(error => {
                    console.log("#Error", error);
                });
                await getMstFixCase(data.caseOfIssueSubId?.toLocaleString() || "").then((dataMstFixCase) => {
                    setValue("mstCollection.mstFixCase", dataMstFixCase);
                    setValue("dataFrom.caseOfFixName", dataMstFixCase.length > 0 ? dataMstFixCase[0].data : "");
                }).catch(error => {
                    console.log("#Error", error);
                });
            }).catch(error => {
                console.log("#Error", error);
            });
        }

        await getMstProvince().then((data) => {
            setValue("mstCollection.mstProvince", data);
        }).catch(error => {
            console.log("#Error", error);
        });
        // await getMstCaseOfFixed().then((data) => {
        //     setValue("mstCollection.mstCaseOfFixed", data);
        // }).catch(error => {
        //     console.log("#Error", error);
        // });
        await getMstCaseOfIssue().then((data) => {
            setValue("mstCollection.mstCaseOfIssue", data.sort(function (a, b) { return (a?.value || "").localeCompare(b?.value || "") }));
            if (requestId && requestId != null) {
                var filterData = data.filter(a => a.value == getValues().dataFrom?.caseOfIssueId);
                setValue("dataFrom.caseOfIssueName", filterData.length > 0 ? filterData[0].data : "");
            }
        }).catch(error => {
            console.log("#Error", error);
        });
    }

    const onSubmit: SubmitHandler<RepairForm> = async (data) => {
        setIsLoading(true);
        if (data.dataFrom?.sysStatusId == "5") {
            data = {
                ...data,
                dataFrom: {
                    ...data.dataFrom,
                    jobCreatedBy: _.get(session?.user, ['username']),
                    jobAcceptBy: _.get(session?.user, ['username'])
                }
            }
        } else if (data.dataFrom?.sysStatusId == "6") {
            data = {
                ...data,
                dataFrom: {
                    ...data.dataFrom,
                    jobProcessBy: _.get(session?.user, ['username'])
                }
            }
        } else if (data.dataFrom?.sysStatusId == "7") {
            data = {
                ...data,
                dataFrom: {
                    ...data.dataFrom,
                    jobCompleteBy: _.get(session?.user, ['username'])
                }
            }
        }
        await jobCreateUpdate(data.dataFrom).then(async (res) => {
            // console.log("res =>", res);
            // console.log("res => data", res.data);

            if (res.data.status) {
                const documentNo = res.data.data.documentNo;
                alertCtx.success(`${documentNo}`, "บันทึกรายการแจ้งซ่อมสำเร็จ", {
                    okText: "ตกลง",
                    okButtonProps: {
                        className: "bg-indigo-500 text-white"
                    },
                    onOk: () => {
                        router.push(`/repair-list`);
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
        <Can
            rules={["Admin", "Staff", "Helpdesk", "DOL"]}
            perform={_.get(session?.user, ['userGroup'])}
            yes={() => (
                <FormProvider {...methods}>
                    <div className="grid grid-rows-none grid-flow-col">
                        <div className="grid-cols-1">
                            {
                                requestId && requestId != "" ?
                                    <FormEditCaseComponent
                                        methods={methods}
                                        isLoading={isLoading}
                                        onSubmit={onSubmit}
                                        jobCreateUpdate={jobCreateUpdate}
                                        isRoleDOL={isRoleDOL}
                                    ></FormEditCaseComponent>
                                    : <FormNewCaseComponent
                                        methods={methods}
                                        isLoading={isLoading}
                                        getLocationByProvinceName={getLocationByProvinceName}
                                        getSiteNetworkBySiteNetworkId={getSiteNetworkBySiteNetworkId}
                                        isRoleDOL={isRoleDOL}
                                    ></FormNewCaseComponent>
                            }
                        </div>
                    </div>
                    <div className="grid grid-rows-none grid-flow-col" style={{ textAlign: 'end' }}>
                        <div className="grid-cols-1">
                            {
                                !isLoading ?
                                    requestId == "" && !isRoleDOL ?
                                        <div className='gap-2 justify-end' style={{ display: 'flex' }}>
                                            <Link href="/repair-list">
                                                <Button
                                                    className="outline-indigo-500"
                                                    size="large"
                                                >
                                                    Cancel
                                                </Button>
                                            </Link>
                                            <Button
                                                className="bg-indigo-500 text-white"
                                                size="large"
                                                onClick={() => {
                                                    trigger().then(res => {
                                                        if (res) {
                                                            var submitData: RepairForm = {
                                                                ...getValues(),
                                                                dataFrom: {
                                                                    ...getValues().dataFrom,
                                                                    sysStatusId: "5"
                                                                }
                                                            };
                                                            onSubmit(submitData);
                                                        }
                                                    });
                                                }}
                                            >
                                                Add new case
                                            </Button>
                                        </div>
                                        :
                                        <></>
                                    :
                                    <LoadingSection></LoadingSection>
                            }

                        </div>
                    </div>
                    <JsonViewerComponent data={watch()}></JsonViewerComponent>
                    <JsonViewerComponent data={errors}></JsonViewerComponent>
                </FormProvider>
            )}
            no={() => <AccessDenied></AccessDenied>}
        />
    );
};

export default Repair;
"use client"

import React from 'react';
import { FormProvider, useForm } from "react-hook-form";
import { useEffect } from "react";
import { useSession } from "next-auth/react";
import _ from 'lodash';

import LoadingSection from "../../components/loading/loadingSection";

import CardHeaderComponent from "@/app/components/dashboard/cardHeader";
import DataListComponent from "@/app/components/dashboard/dataList";

import Can from '../../components/rule/Can';
import AccessDenied from '../../components/utils/403';

import { useDashboardState } from "@/app/_store/dashboard";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const Dashboard = () => {
    const { data: session } = useSession();
    const isLoading = useDashboardState((state) => state.isLoading);
    const setIsLoading = useDashboardState((state) => state.setIsLoading);
    const setIsLoadingTableAccept = useDashboardState((state) => state.setIsLoadingTableAccept);
    const setIsLoadingTableOnProcess = useDashboardState((state) => state.setIsLoadingTableOnProcess);
    const getDataAccept = useDashboardState((state) => state.getDataAccept);
    const getDataOnProcess = useDashboardState((state) => state.getDataOnProcess);
    const getDataOnSuccess = useDashboardState((state) => state.getDataOnSuccess);

    const methods = useForm<DashboardAllForm>({
        mode: "onChange",
        defaultValues: {
            dashboardAccept: {
                pageNumber: 1,
                pageSize: 10,
                effectRow: 0
            },
            dashboardOnProcess: {
                pageNumber: 1,
                pageSize: 10,
                effectRow: 0
            },
            dashboardOnSuccess: {
                pageNumber: 1,
                pageSize: 10,
                effectRow: 0
            }
        },
    });
    const { formState: { errors }, getValues, watch, reset, setValue } = methods;

    useEffect(() => {
        setIsLoading(true);
        setIsLoadingTableAccept(true);
        setIsLoadingTableOnProcess(true);

        var allData: DashboardAllForm = { ...getValues() };
        getDataAccept(getValues().dashboardAccept).then((dataAccept) => {
            allData = {
                ...allData,
                dashboardAccept: dataAccept
            }
            getDataOnProcess(getValues().dashboardOnProcess).then((dataOnProcess) => {
                allData = {
                    ...allData,
                    dashboardOnProcess: dataOnProcess
                }
                getDataOnSuccess().then((dataOnSuccess) => {
                    allData = {
                        ...allData,
                        dashboardOnSuccess: dataOnSuccess
                    }
                    reset(allData);
                    setIsLoading(false);
                    setIsLoadingTableAccept(false);
                    setIsLoadingTableOnProcess(false);
                }).catch(error => {
                    console.log("#Error", error);
                });
            }).catch(error => {
                console.log("#Error", error);
            });
        }).catch(error => {
            console.log("#Error", error);
        });
    }, []);

    const getDataAcceptFromAPI = () => {
        setIsLoadingTableAccept(true);
        getDataAccept(getValues().dashboardAccept).then((data) => {
            reset({
                ...getValues(),
                dashboardAccept: data
            });
            setIsLoadingTableAccept(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }

    const getDataOnProcessFromAPI = () => {
        setIsLoadingTableOnProcess(true);
        getDataOnProcess(getValues().dashboardOnProcess).then((data) => {
            reset({
                ...getValues(),
                dashboardOnProcess: data
            });
            setIsLoadingTableOnProcess(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }

    return (
        <Can
            rules={["Admin", "Staff", "Helpdesk", "DOL"]}
            perform={_.get(session?.user, ['userGroup'])}
            yes={() => (
                <FormProvider {...methods}>
                    <div className="grid grid-rows-none grid-flow-col">
                        <div className="grid-cols-1">
                            <div className='mb-3'>
                                {
                                    !isLoading ?
                                        <CardHeaderComponent
                                            methods={methods}
                                        ></CardHeaderComponent>
                                        :
                                        <LoadingSection></LoadingSection>
                                }
                            </div>
                            <div>
                                {
                                    !isLoading ?
                                        <DataListComponent
                                            methods={methods}
                                            getDataAcceptFromAPI={getDataAcceptFromAPI}
                                            getDataOnProcessFromAPI={getDataOnProcessFromAPI}
                                        ></DataListComponent>
                                        :
                                        <LoadingSection></LoadingSection>
                                }
                            </div>
                        </div>
                    </div>
                    {/* <JsonViewerComponent data={watch()}></JsonViewerComponent> */}
                    {/* <JsonViewerComponent data={errors}></JsonViewerComponent> */}
                </FormProvider>
            )}
            no={() => <></>}
        />
    );
};

export default Dashboard;
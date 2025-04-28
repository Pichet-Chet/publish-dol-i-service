"use client"

import React from 'react';
import { FormProvider, useForm } from "react-hook-form";
import { useEffect } from "react";
import { useSession } from "next-auth/react";
import _ from 'lodash';

import LoadingSection from "../../components/loading/loadingSection";

import CardHeaderComponent from "@/app/components/site-schedule/cardHeader";
import GroupActionComponent from "@/app/components/site-schedule/groupAction";
import DataListComponent from "@/app/components/site-schedule/dataList";

import Can from '../../components/rule/Can';
import AccessDenied from '../../components/utils/403';

import { useSiteScheduleState } from "@/app/_store/site-schedule";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const SiteSchedule = () => {
    const { data: session } = useSession();
    const isLoading = useSiteScheduleState((state) => state.isLoading);
    const setIsLoading = useSiteScheduleState((state) => state.setIsLoading);
    const setIsLoadingTable = useSiteScheduleState((state) => state.setIsLoadingTable);
    const getData = useSiteScheduleState((state) => state.getData);

    const methods = useForm<SiteScheduleListForm>({
        mode: "onChange",
        // resolver: yupResolver(validationData(t)),
        defaultValues: {
            pageNumber: 1,
            pageSize: 10,
            effectRow: 0
        },
    });
    const { formState: { errors }, getValues, watch, reset, setValue } = methods;

    useEffect(() => {
        setIsLoading(true);
        setIsLoadingTable(true);
        getData(getValues()).then((data) => {
            reset(data);
            setIsLoading(false);
            setIsLoadingTable(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }, []);

    const getDataFromAPI = () => {
        setIsLoadingTable(true);
        getData(getValues()).then((data) => {
            reset({
                ...getValues(),
                effectRow: data.effectRow,
                dataList: data.dataList
            });
            setIsLoadingTable(false);
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
                            <div className='mb-3'>
                                {
                                    !isLoading ?
                                        <GroupActionComponent
                                            methods={methods}
                                            getDataFromAPI={getDataFromAPI}
                                        ></GroupActionComponent>
                                        :
                                        <LoadingSection></LoadingSection>
                                }
                            </div>
                            <div>
                                {
                                    !isLoading ?
                                        <DataListComponent
                                            methods={methods}
                                            getDataFromAPI={getDataFromAPI}
                                            isRoleDOL={["DOL"].includes(_.get(session?.user, ['userGroup']))}
                                        ></DataListComponent>
                                        :
                                        <LoadingSection></LoadingSection>
                                }
                            </div>
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

export default SiteSchedule;
"use client"

import React from 'react';
import { FormProvider, useForm } from "react-hook-form";
import { useEffect } from "react";
import { useSession } from "next-auth/react";
import _ from 'lodash';

import LoadingSection from "../../components/loading/loadingSection";

import CardHeaderComponent from "@/app/components/repair-list/cardHeader";
import GroupActionComponent from "@/app/components/repair-list/groupAction";
import DataListComponent from "@/app/components/repair-list/dataList";

import Can from '../../components/rule/Can';
import AccessDenied from '../../components/utils/403';

import { useRepairListState } from "@/app/_store/repair-list";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const RepairList = ({ params }: { params: any | undefined }) => {
    const { data: session } = useSession();
    const isLoading = useRepairListState((state) => state.isLoading);
    const setIsLoading = useRepairListState((state) => state.setIsLoading);
    const setIsLoadingTable = useRepairListState((state) => state.setIsLoadingTable);
    const getData = useRepairListState((state) => state.getData);

    const methods = useForm<RepairListForm>({
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
                                            isRoleDOL={["DOL"].includes(_.get(session?.user, ['userGroup']))}
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

export default RepairList;
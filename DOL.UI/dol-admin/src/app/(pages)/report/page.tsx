"use client"

import React from 'react';
import { FormProvider, useForm } from "react-hook-form";
import { useEffect } from "react";
import { useSession } from "next-auth/react";
import _ from 'lodash';

import LoadingSection from "../../components/loading/loadingSection";

import DataListComponent from "@/app/components/report/dataList";

import Can from '../../components/rule/Can';
import AccessDenied from '../../components/utils/403';

import { useReportState } from "@/app/_store/report";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const Report = ({ params }: { params: any | undefined }) => {
    const { data: session } = useSession();
    const isLoading = useReportState((state) => state.isLoading);
    const setIsLoading = useReportState((state) => state.setIsLoading);
    const setIsLoadingTable = useReportState((state) => state.setIsLoadingTable);
    const getData = useReportState((state) => state.getData);

    const methods = useForm<ReportForm>({
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
            rules={["Admin", "Staff", "Helpdesk"]}
            perform={_.get(session?.user, ['userGroup'])}
            yes={() => (
                <FormProvider {...methods}>
                    <div className="grid grid-rows-none grid-flow-col">
                        <div className="grid-cols-1">
                            <div>
                                {
                                    !isLoading ?
                                        <DataListComponent
                                            methods={methods}
                                            permission={_.get(session?.user, ['userGroup'])}
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

export default Report;
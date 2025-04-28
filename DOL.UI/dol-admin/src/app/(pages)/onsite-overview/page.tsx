"use client"

import React from 'react';
import { FormProvider, useForm } from "react-hook-form";
import { useEffect } from "react";
import { useSession } from "next-auth/react";
import _ from 'lodash';

import LoadingSection from "../../components/loading/loadingSection";

import CardHeaderComponent from "@/app/components/onsite-overview/cardHeader";
import DataListComponent from "@/app/components/onsite-overview/dataList";

import Can from '../../components/rule/Can';
import AccessDenied from '../../components/utils/403';

import { useOnsiteOverviewState } from "@/app/_store/onsite-overview";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const OnsiteOverview = () => {
    const { data: session } = useSession();
    const isLoading = useOnsiteOverviewState((state) => state.isLoading);
    const setIsLoading = useOnsiteOverviewState((state) => state.setIsLoading);
    const setIsLoadingTable = useOnsiteOverviewState((state) => state.setIsLoadingTable);
    const getData = useOnsiteOverviewState((state) => state.getData);

    const methods = useForm<OnsiteOverviewForm>({
        mode: "onChange",
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

        var allData: OnsiteOverviewForm = { ...getValues() };
        getData(getValues()).then((data) => {
            allData = {
                ...allData,
                ...data,
            }
            reset(allData);
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
                ...data
            });
            setIsLoadingTable(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }




    return (
        <Can
            rules={["Admin", "Staff"]}
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

export default OnsiteOverview;
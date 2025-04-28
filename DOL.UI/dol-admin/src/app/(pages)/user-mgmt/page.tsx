"use client"

import React from 'react';
import { FormProvider, useForm } from "react-hook-form";
import { useEffect } from "react";
import { useSession } from "next-auth/react";
import _ from 'lodash';

import LoadingSection from "../../components/loading/loadingSection";

import GroupActionComponent from "@/app/components/user-mgmt/groupAction";
import DataListComponent from "@/app/components/user-mgmt/dataList";

import Can from '../../components/rule/Can';
import AccessDenied from '../../components/utils/403';

import { useUserMgmtState } from "@/app/_store/user-mgmt";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const UserMgmt = () => {
    const { data: session } = useSession();
    const isLoading = useUserMgmtState((state) => state.isLoading);
    const setIsLoading = useUserMgmtState((state) => state.setIsLoading);
    const setIsLoadingTable = useUserMgmtState((state) => state.setIsLoadingTable);
    const getData = useUserMgmtState((state) => state.getData);

    const methods = useForm<UserMgmtForm>({
        mode: "onChange",
        defaultValues: {},
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
            rules={["Admin"]}
            perform={_.get(session?.user, ['userGroup'])}
            yes={() => (
                <FormProvider {...methods}>
                    <div className="grid grid-rows-none grid-flow-col">
                        <div className="grid-cols-1">
                            <div className='mb-3'>
                                {
                                    !isLoading ?
                                        <GroupActionComponent
                                            methodsMain={methods}
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

export default UserMgmt;
import { useEffect } from "react";
import { useForm, UseFormReturn } from "react-hook-form";
import { useRouter } from "next/navigation";
import SectionComponent from "@/app/components/containers/section";
import { Space, Table, Tag } from 'antd';
import type { TableProps } from 'antd';

import LoadingSection from "../loading/loadingSection";

import AgencyInformationComponent from "@/app/components/repair/information/agencyInformation";
import RepairInformationComponent from "@/app/components/repair/information/repairInformation";
import Wan1Component from "@/app/components/repair/cycle/wan1";
import Wan2Component from "@/app/components/repair/cycle/wan2";
import InternetComponent from "@/app/components/repair/cycle/internet";
import CellularComponent from "@/app/components/repair/cycle/cellular";
import DetailInformationComponent from "@/app/components/repair/information/detailInformation";
import ImageComponent from "@/app/components/repair/information/imageInformation";

export default function FormNewCaseComponent({
    methods = useForm<RepairForm>(),
    isLoading = false,
    getLocationByProvinceName,
    getSiteNetworkBySiteNetworkId,
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    isLoading: boolean;
    getLocationByProvinceName: (provinceName: string) => void;
    getSiteNetworkBySiteNetworkId: (siteNetworkId: number) => void;
    isRoleDOL: boolean;
}) {
    const router = useRouter();
    const { getValues, setValue, resetField } = methods;
    const _dataFrom = getValues().dataFrom;

    return (
        <>
            <SectionComponent
                title={"ข้อมูลหน่วยงาน"}
                iconName=""
                bodyClass=""
            >
                {
                    !isLoading ?
                        <AgencyInformationComponent
                            methods={methods}
                            getLocationByProvinceName={getLocationByProvinceName}
                            getSiteNetworkBySiteNetworkId={getSiteNetworkBySiteNetworkId}
                            isRoleDOL={isRoleDOL}
                        />
                        :
                        <LoadingSection></LoadingSection>
                }
            </SectionComponent>
            <SectionComponent
                title={"ข้อมูลการแจ้งซ่อม"}
                iconName=""
                bodyClass=""
            >
                {
                    !isLoading ?
                        <RepairInformationComponent
                            methods={methods}
                            isRoleDOL={isRoleDOL}
                        />
                        :
                        <LoadingSection></LoadingSection>
                }
            </SectionComponent>
            {
                _dataFrom?.typeRepairValue == "WAN1" ?
                    <SectionComponent
                        title={"WAN1 (วงจรหลัก)"}
                        iconName=""
                        bodyClass=""
                    >
                        {
                            !isLoading ?
                                <Wan1Component
                                    methods={methods}
                                    isRoleDOL={isRoleDOL}
                                />
                                :
                                <LoadingSection></LoadingSection>
                        }
                    </SectionComponent>
                    : _dataFrom?.typeRepairValue == "WAN2" ?
                        <SectionComponent
                            title={"WAN2 (วงจรรอง)"}
                            iconName=""
                            bodyClass=""
                        >
                            {
                                !isLoading ?
                                    <Wan2Component
                                        methods={methods}
                                        isRoleDOL={isRoleDOL}
                                    />
                                    :
                                    <LoadingSection></LoadingSection>
                            }
                        </SectionComponent>
                        : _dataFrom?.typeRepairValue == "Internet" ?
                            <SectionComponent
                                title={"วงจร Internet"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <InternetComponent
                                            methods={methods}
                                            isRoleDOL={isRoleDOL}
                                        />
                                        :
                                        <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            : _dataFrom?.typeRepairValue == "Cellular" ?
                                <SectionComponent
                                    title={"Cellular 20Mbps"}
                                    iconName=""
                                    bodyClass=""
                                >
                                    {
                                        !isLoading ?
                                            <CellularComponent
                                                methods={methods}
                                                isRoleDOL={isRoleDOL}
                                            />
                                            :
                                            <LoadingSection></LoadingSection>
                                    }
                                </SectionComponent>
                                : <></>
            }
            <SectionComponent
                title=""
                iconName=""
                bodyClass=""
                isCustomTitle={true}
            >
                {
                    !isLoading ?
                        <DetailInformationComponent
                            methods={methods}
                            isRoleDOL={isRoleDOL}
                        />
                        :
                        <LoadingSection></LoadingSection>
                }
            </SectionComponent>
            <SectionComponent
                title="แนบรูป"
                iconName=""
                bodyClass="w-6/12"
            >
                {
                    !isLoading ?
                        <ImageComponent
                            methods={methods}
                            isRoleDOL={isRoleDOL}
                        />
                        :
                        <LoadingSection></LoadingSection>
                }
            </SectionComponent>
        </>

    );
};
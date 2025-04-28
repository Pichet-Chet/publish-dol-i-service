import { useForm, UseFormReturn } from "react-hook-form";
import { useRouter } from "next/navigation";
import SectionComponent from "@/app/components/containers/section";
import {
    CheckCircleOutlined,
    ClockCircleOutlined,
    CloseCircleOutlined,
    ExclamationCircleOutlined,
    MinusCircleOutlined,
    SyncOutlined,
} from '@ant-design/icons';
import { Tag } from 'antd';

import LoadingSection from "../loading/loadingSection";

import AgencyInformationComponent from "@/app/components/repair/information/agencyInformation";
import RepairInformationComponent from "@/app/components/repair/information/repairInformation";
import Wan1Component from "@/app/components/repair/cycle/wan1";
import Wan2Component from "@/app/components/repair/cycle/wan2";
import InternetComponent from "@/app/components/repair/cycle/internet";
import CellularComponent from "@/app/components/repair/cycle/cellular";
import DetailInformationComponent from "@/app/components/repair/information/detailInformation";
import DetailInformationAfterComponent from "@/app/components/repair/information/detailInformationAfter";
import ImageComponent from "@/app/components/repair/information/imageInformation";
import ImageAfterComponent from "@/app/components/repair/information/imageInformationAfter";
import FixInformationComponent from "@/app/components/repair/information/fixInformation";
import FixInformationAfterComponent from "@/app/components/repair/information/fixInformationAfter";

export default function FormEditCaseComponent({
    methods = useForm<RepairForm>(),
    isLoading = false,
    onSubmit,
    jobCreateUpdate,
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    isLoading: boolean;
    onSubmit: (data: RepairForm) => void;
    jobCreateUpdate: (data?: DataFrom | undefined) => Promise<any>;
    isRoleDOL: boolean;
}) {
    const router = useRouter();
    const { getValues, setValue, resetField } = methods;
    const _dataFrom = getValues().dataFrom;

    return (
        <>
            <SectionComponent
                title={
                    <div className="grid gap-y-8 pt-2">
                        <div className="flex flex-row items-start items-center justify-between font-semibold text-secondary">
                            <div>
                                <h2 className="text-2xl font-normal text-on-base">{_dataFrom?.locationName}</h2>
                            </div>
                            <div>
                                {
                                    _dataFrom?.sysStatusId == "5" ? <Tag color="#D92450"><h2 className="text-xl font-normal text-on-base"><SyncOutlined spin /> รับแจ้งแล้ว รอดำเนินการ</h2></Tag>
                                        : _dataFrom?.sysStatusId == "6" ? <Tag color="#F7BA25"><h2 className="text-xl font-normal text-on-base"><SyncOutlined spin /> ระหว่างดำเนินการ</h2></Tag>
                                            : _dataFrom?.sysStatusId == "7" ? <Tag color="#3AC47D"><h2 className="text-xl font-normal text-on-base"><CheckCircleOutlined /> ดำเนินการเสร็จแล้ว</h2></Tag>
                                                : ""
                                }
                            </div>
                        </div>
                        <div className="flex flex-row items-start items-center justify-between font-semibold text-secondary gap-x-3">
                            <h2 className="text-lg font-normal text-on-base">ข้อมูลหน่วยงาน</h2>
                        </div>
                    </div>
                }
                isCustomTitle={true}
                iconName=""
                bodyClass=""
            >
                {
                    !isLoading ?
                        <AgencyInformationComponent
                            methods={methods}
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
                            jobCreateUpdate={jobCreateUpdate}
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
                        _dataFrom?.sysStatusId == "7" ?
                            <DetailInformationAfterComponent
                                methods={methods}
                                jobCreateUpdate={jobCreateUpdate}
                                isRoleDOL={isRoleDOL}
                            />
                            :
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
                        _dataFrom?.sysStatusId == "7" ?
                            <ImageAfterComponent
                                methods={methods}
                                jobCreateUpdate={jobCreateUpdate}
                                isRoleDOL={isRoleDOL}
                            />
                            :
                            <ImageComponent
                                methods={methods}
                                isRoleDOL={isRoleDOL}
                            />
                        :
                        <LoadingSection></LoadingSection>
                }
            </SectionComponent>
            <SectionComponent
                title="รายละเอียดการแก้ไข"
                iconName=""
                bodyClass=""
            >
                {
                    !isLoading ?
                        _dataFrom?.sysStatusId == "7" ?
                            <FixInformationAfterComponent
                                methods={methods}
                                jobCreateUpdate={jobCreateUpdate}
                                isRoleDOL={isRoleDOL}
                            />
                            :
                            <FixInformationComponent
                                methods={methods}
                                onSubmit={onSubmit}
                                isRoleDOL={isRoleDOL}
                            />
                        :
                        <LoadingSection></LoadingSection>
                }
            </SectionComponent>
        </>

    );
};
import { useForm, UseFormReturn } from "react-hook-form";
import { Input, Select } from 'antd';
import InputFieldComponent, { InputMode } from "../../containers/inputField";

export default function AgencyInformationComponent({
    methods = useForm<RepairForm>(),
    getLocationByProvinceName,
    getSiteNetworkBySiteNetworkId,
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    getLocationByProvinceName?: (provinceName: string) => void;
    getSiteNetworkBySiteNetworkId?: (siteNetworkId: number) => void;
    isRoleDOL: boolean;
}) {
    const { getValues, setValue, resetField, reset } = methods;
    const _masterCollection = getValues().mstCollection;
    const _dataFrom = getValues().dataFrom;
    const requestId = _dataFrom?.id || "";

    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                <InputFieldComponent
                    name="dataFrom.provinceName"
                    label={"จังหวัด"}
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
                            placeholder="กรุณาเลือก"
                            disabled={requestId != "" || isRoleDOL ? true : false}
                            options={(_masterCollection?.mstProvince || []).map(ct => {
                                return {
                                    value: ct.provinceId,
                                    label: ct.provinceName
                                };
                            })}
                            onChange={(value: string, e: any) => {
                                if (e) {
                                    const selected = e as Options;
                                    resetField("dataFrom.provinceName");
                                    resetField("dataFrom.locationName");

                                    var dataFrom = getValues().dataFrom;
                                    var mstCollection = getValues().mstCollection;
                                    var updateData = {
                                        dataFrom: {
                                            ...dataFrom,
                                            provinceName: selected.value,
                                            locationName: "",
                                            siteInformationId: undefined,
                                            siteNetworkId: undefined,
                                            siteNetworkName: "",
                                            siteNetworkSeq: undefined,
                                            address: "",
                                            wan1Provider: "",
                                            wan1Cid: "",
                                            wan1Speed: "",
                                            wan1AsNumber: "",
                                            wan1IpWan1Pe: "",
                                            wan1IpWan1Ce: "",
                                            wan1Subnet: "",
                                            wan2Provider: "",
                                            wan2Cid: "",
                                            wan2Speed: "",
                                            wan2AsNumber: "",
                                            wan2IpWan1Pe: "",
                                            wan2IpWan1Ce: "",
                                            wan2Subnet: "",
                                            internetCid: "",
                                            internetSpeed: "",
                                            internetAsNumber: "",
                                            internetWanIpAddress: "",
                                            internetSubnet: "",
                                            cellularSim: "",
                                            cellularAr109: "",
                                        },
                                        mstCollection: {
                                            ...mstCollection,
                                            mstTypeRepair: []
                                        }
                                    }

                                    reset({ ...updateData });
                                    getLocationByProvinceName && getLocationByProvinceName(selected.value);
                                }
                            }}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dataFrom.locationName"
                    label={"ชื่อสถานที่"}
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Select
                            {...field}
                            defaultValue={""}
                            className="w-full"
                            size="large"
                            placeholder="กรุณาเลือก"
                            options={(_masterCollection?.mstLocation || []).map(ct => {
                                return {
                                    value: ct.id,
                                    label: ct.locationName
                                };
                            })}
                            disabled={requestId != "" || isRoleDOL ? true : false}
                            onChange={(value: string, e: any) => {
                                if (e) {
                                    const selected = e as Options;

                                    const _location = (_masterCollection?.mstLocation || []).filter(a => a.id?.toLocaleString() == selected.value);
                                    if (_location.length > 0) {
                                        resetField("dataFrom.locationName");

                                        var dataFrom = getValues().dataFrom;
                                        var updateDataFrom = {
                                            ...dataFrom,
                                            locationName: selected.label || "",
                                            siteInformationId: _location[0].id || undefined,
                                            siteNetworkId: _location[0].siteNetworkId || undefined,
                                            siteNetworkName: _location[0].siteNetworkName,
                                            siteNetworkSeq: _location[0].siteNetworkSeq,
                                            address: _location[0].address,
                                            wan1Provider: _location[0].wan1Provider,
                                            wan1Cid: _location[0].wan1Cid,
                                            wan1Speed: _location[0].wan1Speed,
                                            wan1AsNumber: _location[0].wan1AsNumber,
                                            wan1IpWan1Pe: _location[0].wan1IpWan1Pe,
                                            wan1IpWan1Ce: _location[0].wan1IpWan1Ce,
                                            wan1Subnet: _location[0].wan1Subnet,
                                            wan2Provider: _location[0].wan2Provider,
                                            wan2Cid: _location[0].wan2Cid,
                                            wan2Speed: _location[0].wan2Speed,
                                            wan2AsNumber: _location[0].wan2AsNumber,
                                            wan2IpWan1Pe: _location[0].wan2IpWan1Pe,
                                            wan2IpWan1Ce: _location[0].wan2IpWan1Ce,
                                            wan2Subnet: _location[0].wan2Subnet,
                                            internetCid: _location[0].internetCid,
                                            internetSpeed: _location[0].internetSpeed,
                                            internetAsNumber: _location[0].internetAsNumber,
                                            internetWanIpAddress: _location[0].internetWanIpAddress,
                                            internetSubnet: _location[0].internetSubnet,
                                            cellularSim: _location[0].cellularSim,
                                            cellularAr109: _location[0].cellularAr109,
                                        }
                                        setValue("dataFrom", updateDataFrom);

                                        getSiteNetworkBySiteNetworkId && getSiteNetworkBySiteNetworkId(updateDataFrom.siteInformationId || 0);
                                    }
                                }
                            }}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dataFrom.siteNetworkName"
                    label={"ภาคผนวก"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
                <InputFieldComponent
                    name="dataFrom.siteNetworkSeq"
                    label={"ลำดับ"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
            </div>
            <div className="grid grid-cols-1 gap-x-5">
                <InputFieldComponent
                    name="dataFrom.address"
                    label={"ที่อยู่"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="md:w-4/12"
                    inputClassName="md:w-8/12"
                    renderControl={field => (
                        <Input
                            {...field}
                            className="w-full"
                            size="large"
                            disabled={true}
                        />
                    )}
                ></InputFieldComponent>
            </div>
        </>
    );
};
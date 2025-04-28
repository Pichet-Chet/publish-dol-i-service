import { useForm, UseFormReturn } from "react-hook-form";
import { useState } from 'react';
import SectionComponent from "@/app/components/containers/section";
import { useSiteScheduleState } from "@/app/_store/site-schedule";
import { Table, Pagination, Modal, Button } from 'antd';
import type { TableProps, PaginationProps } from 'antd';
import Link from "next/link";
import _ from "lodash";
import dayjs from "dayjs";


export default function DataListComponent({
    methods = useForm<SiteScheduleListForm>(),
    getDataFromAPI,
    isRoleDOL
}: {
    methods?: UseFormReturn<SiteScheduleListForm>;
    getDataFromAPI: () => void;
    isRoleDOL: boolean;
}) {
    const columns: TableProps<SiteScheduleList>['columns'] = [
        {
            title: 'ชื่อสถานที่',
            dataIndex: 'location',
            key: 'location',
            render(value, record, index) {
                return <Link
                    href={`/siteinfo/${record.id}`}
                    className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                >
                    {record.location}
                </Link>
            },
            sorter: true
            // sorter: (a, b) => (a.location || "").localeCompare((b.location || "")),
        },
        {
            title: 'จังหวัด',
            dataIndex: 'province',
            key: 'province',
            render(value, record, index) {
                return record.province;
            },
            sorter: true
            // sorter: (a, b) => (a.province || "").localeCompare((b.province || "")),
        },
        {
            title: 'หมวดหมู่',
            dataIndex: 'networkName',
            key: 'networkName',
            render(value, record, index) {
                return record.networkName;
            },
            sorter: true
            // sorter: (a, b) => (a.networkName || "").localeCompare((b.networkName || "")),
        },
        {
            title: 'ลำดับ',
            dataIndex: 'seq',
            key: 'seq',
            render(value, record, index) {
                return record.seq;
            },
            sorter: true
            // sorter: (a, b) => (a.seq || 0) - (b.seq || 0),
        },
        {
            title: 'Files',
            dataIndex: 'siteInformation.siteNetworkSeq',
            key: 'siteInformation.siteNetworkSeq',
            render(value, record, index) {
                const filesCount = record.files?.length || 0;
                if (!isRoleDOL) {
                    if (filesCount > 0) {
                        return <span
                            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                            style={{ cursor: 'pointer' }}
                            onClick={() => showModal(record.files || [])}
                        >
                            {filesCount}
                        </span>
                    } else {
                        return filesCount;
                    }
                } else {
                    return filesCount;
                }
            },
            // sorter: true
            // sorter: (a, b) => (a.files?.length || 0) - (b.files?.length || 0),
        },
        {
            title: 'WAN1 (วงจรหลัก)',
            dataIndex: 'assignWan1StatusName',
            key: 'assignWan1StatusName',
            render(value, record, index) {
                var textColor = "";
                if (record.assignWan1Status == 2) {
                    textColor = "text-dashboard-red";
                } else if (record.assignWan1Status == 3) {
                    textColor = "text-dashboard-yellow";
                } else if (record.assignWan1Status == 4) {
                    textColor = "text-dashboard-green";
                }
                return <label className={textColor}>{record.assignWan1StatusName}</label>
            },
            sorter: true
            // sorter: (a, b) => (a.assignWan1StatusName || "").localeCompare((b.assignWan1StatusName || "")),
        },
        {
            title: 'WAN2 (วงจรรอง)',
            dataIndex: 'assignWan2StatusName',
            key: 'assignWan2StatusName',
            render(value, record, index) {
                var textColor = "";
                if (record.assignWan2Status == 2) {
                    textColor = "text-dashboard-red";
                } else if (record.assignWan2Status == 3) {
                    textColor = "text-dashboard-yellow";
                } else if (record.assignWan2Status == 4) {
                    textColor = "text-dashboard-green";
                }
                return <label className={textColor}>{record.assignWan2StatusName}</label>
            },
            sorter: true
            // sorter: (a, b) => (a.assignWan2StatusName || "").localeCompare((b.assignWan2StatusName || "")),
        },
        {
            title: 'วงจร Internet',
            dataIndex: 'assignInternetStatusName',
            key: 'assignInternetStatusName',
            render(value, record, index) {
                var textColor = "";
                if (record.assignInternetStatus == 2) {
                    textColor = "text-dashboard-red";
                } else if (record.assignInternetStatus == 3) {
                    textColor = "text-dashboard-yellow";
                } else if (record.assignInternetStatus == 4) {
                    textColor = "text-dashboard-green";
                }
                return <label className={textColor}>{record.assignInternetStatusName}</label>
            },
            sorter: true
            // sorter: (a, b) => (a.assignInternetStatusName || "").localeCompare((b.assignInternetStatusName || "")),
        },
        {
            title: 'Cellular 20Mbps',
            dataIndex: 'assignCellularStatusName',
            key: 'assignCellularStatusName',
            render(value, record, index) {
                var textColor = "";
                if (record.assignCellularStatus == 2) {
                    textColor = "text-dashboard-red";
                } else if (record.assignCellularStatus == 3) {
                    textColor = "text-dashboard-yellow";
                } else if (record.assignCellularStatus == 4) {
                    textColor = "text-dashboard-green";
                }
                return <label className={textColor}>{record.assignCellularStatusName}</label>
            },
            sorter: true
            // sorter: (a, b) => (a.assignCellularStatusName || "").localeCompare((b.assignCellularStatusName || "")),
        },
        {
            title: 'ติดตั้งอุปกรณ์',
            dataIndex: 'assignInstallDeviceStatusName',
            key: 'assignInstallDeviceStatusName',
            render(value, record, index) {
                var textColor = "";
                if (record.assignInstallDeviceStatus == 2) {
                    textColor = "text-dashboard-red";
                } else if (record.assignInstallDeviceStatus == 3) {
                    textColor = "text-dashboard-yellow";
                } else if (record.assignInstallDeviceStatus == 4) {
                    textColor = "text-dashboard-green";
                }
                return <label className={textColor}>{record.assignInstallDeviceStatusName}</label>
            },
            sorter: true
            // sorter: (a, b) => (a.assignInstallDeviceStatusName || "").localeCompare((b.assignInstallDeviceStatusName || "")),
        },
        {
            title: 'Status',
            dataIndex: 'statusName',
            key: 'statusName',
            render(value, record, index) {
                var textColor = "";
                if (record.statusId == 2) {
                    textColor = "text-dashboard-red";
                } else if (record.statusId == 3) {
                    textColor = "text-dashboard-yellow";
                } else if (record.statusId == 4) {
                    textColor = "text-dashboard-green";
                }
                return <label className={textColor}>{record.statusName}</label>
            },
            sorter: true
            // sorter: (a, b) => (a.statusName || "").localeCompare((b.statusName || "")),
        }
    ];
    const columnsFiles: TableProps<SiteScheduleFileList>['columns'] = [
        {
            title: 'Files',
            dataIndex: 'fileName',
            key: 'fileName',
            render(value, record, index) {
                if (record.filePath && record.filePath != "") {
                    return <a href={record.filePath} target="_blank"
                        className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                        style={{ cursor: 'pointer' }}
                    // onClick={() => showModal(record)}
                    >
                        {value}
                    </a>
                } else {
                    return value;
                }
            },
            // sorter: (a, b) => { return a.mode.localeCompare(b.mode) },
        },
        {
            title: 'File size',
            dataIndex: 'fileSize',
            key: 'fileSize',
            render(value, record, index) {
                return `${record.fileSize} ${record.fileSizeUnit}`
            },
            // sorter: (a, b) => { return a.mode.localeCompare(b.mode) },
        }
    ];

    const isLoadingTable = useSiteScheduleState((state) => state.isLoadingTable);
    const { getValues, setValue } = methods;
    const _dataList = getValues().dataList || [];

    const handleTableChange: TableProps['onChange'] = (pagination, filters, sorter) => {
        setValue("sortName", _.get(sorter, ['field']));
        const sortOrder = (_.get(sorter, ['order']) || "");
        setValue("sortType", sortOrder == "descend" ? "desc" : sortOrder == "ascend" ? "asc" : "");
        getDataFromAPI();
    };
    const onChange: PaginationProps['onChange'] = (current, pageSize) => {
        setValue("pageNumber", current);
        setValue("pageSize", pageSize);
        getDataFromAPI();
    };

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [fileList, setFileList] = useState<SiteScheduleFileList[]>([]);
    const showModal = (data: SiteScheduleFileList[]) => {
        setFileList(data);
        setIsModalOpen(true);
    };
    const handleCancel = () => {
        setIsModalOpen(false);
    };

    return (
        <>
            <div className="grid grid-cols-1 gap-x-5">
                <SectionComponent
                    title=""
                    iconName=""
                    bodyClass=""
                    // titleStyle={{ styles: { header: { backgroundColor: 'rgb(99 102 241)', color: '#fff', border: 0 } } }}
                    isCustomTitle={true}
                >
                    {
                        <>
                            <Table
                                columns={columns}
                                dataSource={_dataList}
                                pagination={false}
                                loading={isLoadingTable}
                                onChange={handleTableChange}
                            />
                            {
                                _dataList.length > 0 &&
                                <div className="mt-3 float-end">
                                    <Pagination
                                        showSizeChanger={false}
                                        onChange={onChange}
                                        defaultCurrent={getValues().pageNumber}
                                        total={getValues().effectRow}
                                        pageSize={getValues().pageSize}
                                        disabled={isLoadingTable}
                                    />
                                </div>
                            }
                        </>
                    }
                </SectionComponent>
                <Modal
                    open={isModalOpen}
                    onCancel={handleCancel}
                    footer={[]}
                >
                    <Table
                        columns={columnsFiles}
                        dataSource={fileList}
                    />
                </Modal>
            </div>
        </>
    );
};
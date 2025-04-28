import { useForm, UseFormReturn } from "react-hook-form";
import { useState, useContext } from 'react';
import AlertContext from "@/app/providers/alertContext";
import SectionComponent from "@/app/components/containers/section";
import { useOnsiteOverviewState } from "@/app/_store/onsite-overview";
import { useAttachmentStore } from "@/app/_store/attachment";
import { Table, Pagination, Modal, Tooltip } from 'antd';
import type { TableProps, PaginationProps } from 'antd';
import _ from "lodash";
import Link from "next/link";
import { DownloadOutlined } from '@ant-design/icons';

export default function DataListComponent({
    methods = useForm<OnsiteOverviewForm>(),
    getDataFromAPI,
}: {
    methods?: UseFormReturn<OnsiteOverviewForm>;
    getDataFromAPI: () => void;
}) {
    const alertCtx = useContext(AlertContext);
    const columns: TableProps<OnsiteOverviewList>['columns'] = [
        {
            title: 'Team',
            dataIndex: 'team',
            key: 'team',
            render(value, record, index) {
                return <div className="flex flex-row gap-x-3">
                    <div>
                        {value}
                    </div>
                    <div>
                        <Tooltip title="ดาวน์โหลดไฟล์">
                            <DownloadOutlined
                                style={{ cursor: "pointer" }}
                                onClick={async () => {
                                    setIsLoadingTable(true);
                                    await generateFileByUser(record.userId || "").then(res => {
                                        if (res.status == 200) {
                                            const blobFile = new Blob([res.data], { type: 'application/zip' });
                                            const url = window.URL.createObjectURL(blobFile);
                                            const link = document.createElement('a');
                                            link.href = url;
                                            link.download = `รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร-${value}.zip`;
                                            document.body.appendChild(link);
                                            link.click();
                                            link?.parentNode?.removeChild(link);

                                            // console.log("res.data", res.data);

                                            // // สร้าง URL ชั่วคราวสำหรับไฟล์ที่ได้รับ
                                            // const url = URL.createObjectURL(new Blob([res.data]));
                                            // console.log("url", url);

                                            // // สร้างองค์ประกอบ <a> แบบซ่อน
                                            // const link = document.createElement('a');
                                            // link.href = url;
                                            // link.setAttribute('download', `รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร-${value}.zip`); // กำหนดชื่อไฟล์ที่ต้องการ
                                            // document.body.appendChild(link);

                                            // // เรียกใช้เมธอด click() เพื่อเริ่มการดาวน์โหลด
                                            // link.click();

                                            // // ล้าง URL ชั่วคราว
                                            // URL.revokeObjectURL(url);
                                            // link.remove();
                                        } else {
                                            alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                                                okText: "ตกลง",
                                                okButtonProps: {
                                                    className: "bg-dashboard-indigo text-white"
                                                }
                                            });
                                        }
                                        setIsLoadingTable(false);
                                    });
                                }}
                            />
                        </Tooltip>
                    </div>
                </div>
            },
            sorter: (a, b) => { return (a.team || "").localeCompare((b.team || "")) },
        },
        {
            title: 'Pending',
            dataIndex: 'jobOnsitePendings',
            key: 'jobOnsitePendings',
            render(value, record, index) {
                return <span
                    className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                    style={{ cursor: 'pointer' }}
                    onClick={() => showModal(record, "jobOnsitePendings")}
                >
                    {record.jobOnsitePendings?.length || 0}
                </span>
            },
            sorter: (a, b) => { return (a.jobOnsitePendings?.length || 0) - (b.jobOnsitePendings?.length || 0) },
        },
        {
            title: 'On process',
            dataIndex: 'jobOnsiteOnprocess',
            key: 'jobOnsiteOnprocess',
            render(value, record, index) {
                return <span
                    className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                    style={{ cursor: 'pointer' }}
                    onClick={() => showModal(record, "jobOnsiteOnprocess")}
                >
                    {record.jobOnsiteOnprocess?.length || 0}
                </span>
            },
            sorter: (a, b) => { return (a.jobOnsiteOnprocess?.length || 0) - (b.jobOnsiteOnprocess?.length || 0) },
        },
        {
            title: 'Complete',
            dataIndex: 'jobOnsiteSuccesses',
            key: 'jobOnsiteSuccesses',
            render(value, record, index) {
                return <span
                    className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                    style={{ cursor: 'pointer' }}
                    onClick={() => showModal(record, "jobOnsiteSuccesses")}
                >
                    {record.jobOnsiteSuccesses?.length || 0}
                </span>
            },
            sorter: (a, b) => { return (a.jobOnsiteSuccesses?.length || 0) - (b.jobOnsiteSuccesses?.length || 0) },
        },
        {
            title: 'Complete%',
            dataIndex: 'percentComplete',
            key: 'percentComplete',
            render(value, record, index) {
                return record.percentComplete || 0;
            },
            sorter: (a, b) => { return (a.percentComplete || 0) - (b.percentComplete || 0) },
        }
    ];
    const columnsDetail: TableProps<JobOnsiteList>['columns'] = [
        {
            title: 'ชื่อสถานที่',
            dataIndex: 'location',
            key: 'location',
            render(value, record, index) {
                // return value;
                return <Link target="_blank"
                    href={`/siteinfo/${record.id}`}
                    className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
                >
                    {value}
                </Link>
            },
            sorter: (a, b) => { return (a.location || "").localeCompare((b.location || "")) },
        },
        {
            title: 'จังหวัด',
            dataIndex: 'province',
            key: 'province',
            render(value, record, index) {
                return value;
            },
            sorter: (a, b) => { return (a.province || "").localeCompare((b.province || "")) },
        },
        {
            title: 'หมวดหมู่',
            dataIndex: 'category',
            key: 'category',
            render(value, record, index) {
                return value;
            },
            sorter: (a, b) => { return (a.category || "").localeCompare((b.category || "")) },
        }
    ];
    const isLoadingTable = useOnsiteOverviewState((state) => state.isLoadingTable);
    const setIsLoadingTable = useOnsiteOverviewState((state) => state.setIsLoadingTable);
    const generateFileByUser = useOnsiteOverviewState((state) => state.generateFileByUser);
    const { downloadBlobFile } = useAttachmentStore((state) => state);
    const { getValues, setValue } = methods;
    const _dataList = getValues().dataList || [];

    const onChange: PaginationProps['onChange'] = (current, pageSize) => {
        setValue("pageNumber", current);
        setValue("pageSize", pageSize);
        getDataFromAPI();
    };

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [dataDetails, setDataDetails] = useState<JobOnsiteList[]>([]);
    const showModal = (data: OnsiteOverviewList, type: string) => {
        const _dataDetail = _.get(data, [type]);
        setDataDetails(_dataDetail);
        // console.log("_dataDetail", _dataDetail);
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
                    isCustomTitle={true}
                    iconName=""
                    bodyClass="min-h-fit"
                    titleStyle={{
                        styles: {
                            body: {
                                minHeight: 'calc(100vh - 260px)'
                            }
                        }
                    }}
                >
                    {
                        <>
                            <Table
                                columns={columns}
                                dataSource={_dataList}
                                // pagination={false}
                                loading={isLoadingTable}
                            />
                            {/* {
                                _dataList.length > 0 &&
                                <div className="mt-3 float-end">
                                    <Pagination
                                        showSizeChanger={false}
                                        onChange={onChange}
                                        defaultCurrent={getValues()?.pageNumber}
                                        total={getValues()?.effectRow}
                                        pageSize={getValues()?.pageSize}
                                        disabled={isLoadingTable}
                                    />
                                </div>
                            } */}
                        </>
                    }
                </SectionComponent>
                <Modal
                    title=""
                    open={isModalOpen}
                    onCancel={handleCancel}
                    width={1000}
                    footer={[]}
                >
                    <Table
                        columns={columnsDetail}
                        dataSource={dataDetails || []}
                        // pagination={true}
                        loading={isLoadingTable}
                    />
                </Modal>
            </div>
        </>
    );
};
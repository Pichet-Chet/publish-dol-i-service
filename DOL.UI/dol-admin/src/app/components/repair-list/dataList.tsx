import { useForm, UseFormReturn } from "react-hook-form";
import SectionComponent from "@/app/components/containers/section";
import { useRepairListState } from "@/app/_store/repair-list";
import { Table, Pagination } from "antd";
import type { TableProps, PaginationProps } from "antd";
import Link from "next/link";
import _ from "lodash";
import dayjs from "dayjs";

const columns: TableProps<TrnJobRepairList>["columns"] = [
  {
    title: "Job No.",
    dataIndex: "id",
    key: "id",
    render(value, record, index) {
      return (
        <Link
          href={`/repair/${value}`}
          className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
        >
          {value}
        </Link>
      );
    },
    sorter: true,
    // sorter: (a, b) => a.id! - b.id!,
    // sorter: (a, b) => { return a.id!.localeCompare(b.id!) },
  },
  {
    title: "เลขที่เอกสาร",
    dataIndex: "documentRequest",
    key: "documentRequest",
    render(value, record, index) {
      return record.documentRequest;
    },
    sorter: true,
    // sorter: (a, b) => a.siteInformation?.siteNetworkName!.localeCompare(b.siteInformation?.siteNetworkName!),
  },
  {
    title: "ชื่อสถานที่",
    dataIndex: "locationName",
    key: "locationName",
    render(value, record, index) {
      return (
        <Link
          target="_blank"
          href={`/siteinfo/${record.siteInformation?.id}`}
          className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
        >
          {record.siteInformation?.locationName}
        </Link>
      );
    },
    sorter: true,
    // sorter: (a, b) => a.siteInformation?.siteNetworkName!.localeCompare(b.siteInformation?.siteNetworkName!),
  },
  {
    title: "จังหวัด",
    dataIndex: "provinceName",
    key: "provinceName",
    render(value, record, index) {
      return record.siteInformation?.provinceName;
    },
    sorter: true,
    // sorter: (a, b) => { return a.povide.localeCompare(b.povide) },
  },
  {
    title: "หมวดหมู่",
    dataIndex: "siteNetworkName",
    key: "siteNetworkName",
    render(value, record, index) {
      return record.siteNetwork?.name;
    },
    sorter: true,
    // sorter: (a, b) => { return a.mode.localeCompare(b.mode) },
  },
  {
    title: "ลำดับ",
    dataIndex: "siteNetworkSeq",
    key: "siteNetworkSeq",
    render(value, record, index) {
      return record.siteInformation?.siteNetworkSeq;
    },
    sorter: true,
    // sorter: (a, b) => { return a.mode.localeCompare(b.mode) },
  },
  {
    title: "วงจร",
    dataIndex: "typeRepairData",
    key: "typeRepairData",
    render(value, record, index) {
      return record.typeRepairData;
    },
    sorter: true,
    // sorter: (a, b) => { return a.mode.localeCompare(b.mode) },
  },
  {
    title: "วันที่แจ้ง",
    dataIndex: "jobCreatedDate",
    key: "jobCreatedDate",
    render(value, record, index) {
      return !_.isEmpty(record.jobCreatedDate)
        ? dayjs(record.jobCreatedDate).format("DD/MM/YYYY HH:mm")
        : "-";
    },
    sorter: true,
    // sorter: (a, b) => { return a.dateaction.localeCompare(b.dateaction) },
  },
  {
    title: "เวลาคงเหลือ",
    dataIndex: "remainingTime",
    key: "remainingTime",
    render(value, record, index) {
      var textColor = "";
      if (
        record.remainingTime == "00:00" ||
        record.remainingTime?.includes("-")
      ) {
        textColor = "text-dashboard-red";
      }
      return <label className={textColor}>{record.remainingTime}</label>;
    },
    sorter: true,
    // sorter: (a, b) => { return a.totle.localeCompare(b.totle) },
  },
  {
    title: "สถานะ",
    dataIndex: "sysStatusId",
    key: "sysStatusId",
    render(value, record, index) {
      var textColor = "";
      if (record.sysStatus?.id == 5) {
        textColor = "text-dashboard-red";
      } else if (record.sysStatus?.id == 6) {
        textColor = "text-dashboard-yellow";
      } else if (record.sysStatus?.id == 7) {
        textColor = "text-dashboard-green";
      }
      return <label className={textColor}>{record.sysStatus?.nameTh}</label>;
    },
    sorter: true,
    // sorter: (a, b) => { return a.totle.localeCompare(b.totle) },
  },
];

export default function DataListComponent({
  methods = useForm<RepairListForm>(),
  getDataFromAPI,
}: {
  methods?: UseFormReturn<RepairListForm>;
  getDataFromAPI: () => void;
}) {
  const isLoadingTable = useRepairListState((state) => state.isLoadingTable);
  const { getValues, setValue } = methods;
  const _dataList = getValues().dataList || [];

  const handleTableChange: TableProps["onChange"] = (
    pagination,
    filters,
    sorter
  ) => {
    setValue("sortName", _.get(sorter, ["field"]));
    const sortOrder = _.get(sorter, ["order"]) || "";
    setValue(
      "sortType",
      sortOrder == "descend" ? "desc" : sortOrder == "ascend" ? "asc" : ""
    );
    getDataFromAPI();
  };
  const onChange: PaginationProps["onChange"] = (current, pageSize) => {
    setValue("pageNumber", current);
    setValue("pageSize", pageSize);
    getDataFromAPI();
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
              {_dataList.length > 0 && (
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
              )}
            </>
          }
        </SectionComponent>
      </div>
    </>
  );
}

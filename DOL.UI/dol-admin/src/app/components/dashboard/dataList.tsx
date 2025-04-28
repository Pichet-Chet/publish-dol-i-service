import { useForm, UseFormReturn } from "react-hook-form";
import SectionComponent from "@/app/components/containers/section";
import { useDashboardState } from "@/app/_store/dashboard";
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
    // sorter: (a, b) => { return a.jobno.localeCompare(b.jobno) },
  },
  {
    title: "ชื่อสถานที่",
    dataIndex: "siteInformation.locationName",
    key: "siteInformation.locationName",
    render(value, record, index) {
      return record.siteInformation?.locationName;
    },
    // sorter: (a, b) => { return a.name.localeCompare(b.name) },
  },
  {
    title: "จังหวัด",
    dataIndex: "siteInformation.provinceName",
    key: "siteInformation.provinceName",
    render(value, record, index) {
      return record.siteInformation?.provinceName;
    },
    // sorter: (a, b) => { return a.povide.localeCompare(b.povide) },
  },
  {
    title: "หมวดหมู่",
    dataIndex: "siteNetwork.name",
    key: "siteNetwork.name",
    render(value, record, index) {
      return record.siteNetwork?.name;
    },
    // sorter: (a, b) => { return a.mode.localeCompare(b.mode) },
  },
  {
    title: "วงจร",
    dataIndex: "typeRepairData",
    key: "typeRepairData",
    render(value, record, index) {
      return record.typeRepairData;
    },
    // sorter: (a, b) => { return a.mode.localeCompare(b.mode) },
  },
  {
    title: "วันที่แจ้ง",
    dataIndex: "jobCreatedDate",
    key: "jobCreatedDate",
    render(value, record, index) {
      return !_.isEmpty(record.jobCreatedDate)
        ? dayjs(record.jobCreatedDate).format("DD/MM/YYYY")
        : "-";
    },
    // sorter: (a, b) => { return a.dateaction.localeCompare(b.dateaction) },
  },
  {
    title: "คงเหลือ",
    dataIndex: "remainingTime",
    key: "remainingTime",
    render(value, record, index) {
      return record.remainingTime;
    },
    // sorter: (a, b) => { return a.totle.localeCompare(b.totle) },
  },
];

export default function DataListComponent({
  methods = useForm<DashboardAllForm>(),
  getDataAcceptFromAPI,
  getDataOnProcessFromAPI,
}: {
  methods?: UseFormReturn<DashboardAllForm>;
  getDataAcceptFromAPI: () => void;
  getDataOnProcessFromAPI: () => void;
}) {
  const isLoadingTableAccept = useDashboardState(
    (state) => state.isLoadingTableAccept
  );
  const isLoadingTableOnProcess = useDashboardState(
    (state) => state.isLoadingTableOnProcess
  );
  const { getValues, setValue } = methods;
  const _dashboardAccept = getValues().dashboardAccept?.dataList || [];
  const _dashboardOnProcess = getValues().dashboardOnProcess?.dataList || [];

  const onChangeAccept: PaginationProps["onChange"] = (current, pageSize) => {
    setValue("dashboardAccept.pageNumber", current);
    setValue("dashboardAccept.pageSize", pageSize);
    getDataAcceptFromAPI();
  };
  const onChangeOnProcess: PaginationProps["onChange"] = (
    current,
    pageSize
  ) => {
    setValue("dashboardOnProcess.pageNumber", current);
    setValue("dashboardOnProcess.pageSize", pageSize);
    getDataOnProcessFromAPI();
  };

  return (
    <>
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-5">
        <div className="grid grid-cols-1">
          <SectionComponent
            title={"รับแจ้งแล้ว - รอดำเนินการ"}
            iconName=""
            bodyClass="min-h-fit"
            titleStyle={{
              styles: {
                header: {
                  backgroundColor: "rgb(217, 36, 80)",
                  color: "#fff",
                  border: 0,
                },
                body: {
                  minHeight: "calc(100vh - 320px)",
                },
              },
            }}
          >
            {
              <>
                <Table
                  columns={columns}
                  dataSource={_dashboardAccept}
                  pagination={false}
                  loading={isLoadingTableAccept}
                />
                {_dashboardAccept.length > 0 && (
                  <div className="mt-3 float-end">
                    <Pagination
                      showSizeChanger={false}
                      onChange={onChangeAccept}
                      defaultCurrent={getValues()?.dashboardAccept?.pageNumber}
                      total={getValues()?.dashboardAccept?.effectRow}
                      pageSize={getValues()?.dashboardAccept?.pageSize}
                      disabled={isLoadingTableAccept}
                    />
                  </div>
                )}
              </>
            }
          </SectionComponent>
        </div>
        <div className="grid grid-cols-1">
          <SectionComponent
            title={"ระหว่างดำเนินการ"}
            iconName=""
            bodyClass=""
            titleStyle={{
              styles: {
                header: {
                  backgroundColor: "rgb(234 179 8)",
                  color: "#fff",
                  border: 0,
                },
              },
            }}
          >
            {
              <>
                <Table
                  columns={columns}
                  dataSource={_dashboardOnProcess}
                  pagination={false}
                  loading={isLoadingTableOnProcess}
                />
                {_dashboardOnProcess.length > 0 && (
                  <div className="mt-3 float-end">
                    <Pagination
                      showSizeChanger={false}
                      onChange={onChangeOnProcess}
                      defaultCurrent={
                        getValues()?.dashboardOnProcess?.pageNumber
                      }
                      total={getValues()?.dashboardOnProcess?.effectRow}
                      pageSize={getValues()?.dashboardOnProcess?.pageSize}
                      disabled={isLoadingTableOnProcess}
                    />
                  </div>
                )}
              </>
            }
          </SectionComponent>
        </div>
      </div>
    </>
  );
}

import { useForm, UseFormReturn } from "react-hook-form";
import { useContext } from "react";
import AlertContext from "@/app/providers/alertContext";
import SectionComponent from "@/app/components/containers/section";
import { useReportState } from "@/app/_store/report";
import { Table, Pagination } from "antd";
import type { TableProps, PaginationProps } from "antd";
import Link from "next/link";
import _ from "lodash";
import dayjs from "dayjs";
import timezone from "dayjs/plugin/timezone";
import customParseFormat from "dayjs/plugin/customParseFormat";
dayjs.extend(timezone);
dayjs.extend(customParseFormat);

export default function DataListComponent({
  methods = useForm<ReportForm>(),
  permission,
  getDataFromAPI,
}: {
  methods?: UseFormReturn<ReportForm>;
  permission?: any;
  getDataFromAPI: () => void;
}) {
  const columns: TableProps<ReportsList>["columns"] = [
    {
      title: "เดือน",
      dataIndex: "monthName",
      key: "monthName",
      width: 150,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "";
              var isAdmin = permission == "Admin" ? true : false;
              var monthName =
                month == 1
                  ? "มกราคม"
                  : month == 2
                  ? "กุมภาพันธ์"
                  : month == 3
                  ? "มีนาคม"
                  : month == 4
                  ? "เมษายน"
                  : month == 5
                  ? "พฤษภาคม"
                  : month == 6
                  ? "มิถุนายน"
                  : month == 7
                  ? "กรกฎาคม"
                  : month == 8
                  ? "สิงหาคม"
                  : month == 9
                  ? "กันยายน"
                  : month == 10
                  ? "ตุลาคม"
                  : month == 11
                  ? "พฤศจิกายน"
                  : month == 12
                  ? "ธันวาคม"
                  : "";

              await generateExportMonthPdf(year, month).then((res) => {
                if (res.status == 200) {
                  const link = document.createElement("a");
                  link.href = res.data.data;
                  link.download = `รายงานแจ้งซ่อมประจำเดือน${monthName}.pdf`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.monthName}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "DC1",
      dataIndex: "dc1",
      key: "dc1",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "DC1";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.dc1}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "DC2",
      dataIndex: "dc2",
      key: "dc2",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "DC2";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.dc2}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "อื่นๆ",
      dataIndex: "other",
      key: "other",
      width: 100,
      render(value, record, index) {
        return record.other;
      },
      // sorter: true
    },
    {
      title: "ผนวก 1",
      dataIndex: "siteNetwork1",
      key: "siteNetwork1",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "SITE1";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.siteNetwork1}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "ผนวก 2",
      dataIndex: "siteNetwork2",
      key: "siteNetwork2",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "SITE2";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.siteNetwork2}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "ผนวก 3",
      dataIndex: "siteNetwork3",
      key: "siteNetwork3",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "SITE3";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.siteNetwork3}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "ผนวก 4",
      dataIndex: "siteNetwork4",
      key: "siteNetwork4",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "SITE4";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.siteNetwork4}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "ALL",
      dataIndex: "all",
      key: "all",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "ALL";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.all}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "UIH",
      dataIndex: "uih",
      key: "uih",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "UIH";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.uih}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "AWN",
      dataIndex: "awn",
      key: "awn",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "AWN";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.awn}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "CAT",
      dataIndex: "cat",
      key: "cat",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "CAT";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.cat}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "INTERLINK",
      dataIndex: "interlink",
      key: "interlink",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "INTERLINK";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.interlink}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "SYMPHONY",
      dataIndex: "symphony",
      key: "symphony",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "SYMPHONY";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.symphony}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "JINET",
      dataIndex: "jinet",
      key: "jinet",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "JINET";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.jinet}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "4+",
      dataIndex: "hr4",
      key: "hr4",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "4HR";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.hr4}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "5+",
      dataIndex: "hr5",
      key: "hr5",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "5HR";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.hr5}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "15+",
      dataIndex: "hr15",
      key: "hr15",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "15HR";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.hr15}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "24+",
      dataIndex: "hr24",
      key: "hr24",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "24HR";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.hr24}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "30+",
      dataIndex: "hr30",
      key: "hr30",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "30HR";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.hr30}
          </span>
        );
      },
      // sorter: true
    },
    {
      title: "2 LINK",
      dataIndex: "link2",
      key: "link2",
      width: 100,
      render(value, record, index) {
        return (
          <span
            className="text-blue-400 hover:text-blue-700 underline hover:underline underline-offset-1 hover:underline-offset-1"
            style={{ cursor: "pointer" }}
            onClick={async () => {
              setIsLoadingTable(true);

              var year = record.year;
              var month = record.month;
              var type = "2LINK";
              var isAdmin = permission == "Admin" ? true : false;
              const dateString = new Date();
              const dateTime = dayjs.tz(
                dateString,
                "DD/MM/YYYY, HH:mm",
                "Asia/Bangkok"
              );

              await generateExport(year, month, type, isAdmin).then((res) => {
                if (res.status == 200) {
                  const blobFile = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  });
                  const url = window.URL.createObjectURL(blobFile);
                  const link = document.createElement("a");
                  link.href = url;
                  link.download = `File-${type}-${dateTime.format(
                    "YYYY_MM_DD HH_mm_ss"
                  )}.xlsx`;
                  document.body.appendChild(link);
                  link.click();
                  link?.parentNode?.removeChild(link);
                } else {
                  alertCtx.error("ผิดพลาด", `ไม่สามารถ Export ไฟล์ได้`, {
                    okText: "ตกลง",
                    okButtonProps: {
                      className: "bg-dashboard-indigo text-white",
                    },
                  });
                }
                setIsLoadingTable(false);
              });
            }}
          >
            {record.link2}
          </span>
        );
      },
      // sorter: true
    },
  ];
  const alertCtx = useContext(AlertContext);
  const isLoadingTable = useReportState((state) => state.isLoadingTable);
  const setIsLoadingTable = useReportState((state) => state.setIsLoadingTable);
  const generateExport = useReportState((state) => state.generateExport);
  const generateExportMonthPdf = useReportState(
    (state) => state.generateExportMonthPdf
  );
  const { getValues, setValue } = methods;
  const _dataList = getValues().dataList || [];

  const handleTableChange: TableProps["onChange"] = (
    pagination,
    filters,
    sorter
  ) => {
    // setValue("sortName", _.get(sorter, ['field']));
    const sortOrder = _.get(sorter, ["order"]) || "";
    // setValue("sortType", sortOrder == "descend" ? "desc" : sortOrder == "ascend" ? "asc" : "");
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
                scroll={{
                  x: 2000,
                }}
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

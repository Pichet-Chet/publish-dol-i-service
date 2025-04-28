"use client"

import React from 'react';
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import AlertContext from "@/app/providers/alertContext";
import { useEffect, useState, useContext } from "react";
import { useRouter, usePathname, useParams } from "next/navigation";
import { useSession } from "next-auth/react";
import _ from "lodash";
import Link from "next/link";
import { PlusOutlined } from '@ant-design/icons';
import { message, Button, Input, Upload, Modal } from 'antd';
import type { GetProp, UploadFile, UploadProps } from 'antd';
import LoadingSection from "../../../components/loading/loadingSection";
import SectionComponent from "@/app/components/containers/section";
import InputFieldComponent, { InputMode } from "../../../components/containers/inputField";

import Can from '../../../components/rule/Can';
import AccessDenied from '../../../components/utils/403';

import { useSiteInfoState } from "@/app/_store/siteinfo";

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0];

const getBase64 = (file: FileType): Promise<string> =>
    new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result as string);
        reader.onerror = (error) => reject(error);
    });
const beforeUpload = (file: FileType) => {
    const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
    if (!isJpgOrPng) {
        message.error('You can only upload JPG/PNG file!');
    }
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) {
        message.error('Image must smaller than 2MB!');
    }
    return isJpgOrPng && isLt2M;
};
const uploadButton = (
    <button style={{ border: 0, background: 'none' }} type="button">
        <PlusOutlined />
        <div style={{ marginTop: 8 }}>Upload</div>
    </button>
);
const dummyRequest = (e: any) => {
    e.onSuccess("ok");
};

const SiteInfo = ({ params }: { params: any | undefined }) => {
    const { data: session } = useSession();
    const alertCtx = useContext(AlertContext);
    const router = useRouter();
    const pathParams = useParams();
    const pathname = usePathname();

    const isLoading = useSiteInfoState((state) => state.isLoading);
    const setIsLoading = useSiteInfoState((state) => state.setIsLoading);
    const getData = useSiteInfoState((state) => state.getData);
    const updateSiteInfo = useSiteInfoState((state) => state.updateSiteInfo);
    const updateImageSiteInfo = useSiteInfoState((state) => state.updateImageSiteInfo);
    const updateImages = useSiteInfoState((state) => state.updateImages);
    const deleteImageSiteInfo = useSiteInfoState((state) => state.deleteImageSiteInfo);
    const generatePdf = useSiteInfoState((state) => state.generatePdf);
    const generateOnsitePdf = useSiteInfoState((state) => state.generateOnsitePdf);


    const methods = useForm<TrnSiteInformation>({
        mode: "onChange",
        defaultValues: undefined,
    });
    const { formState: { errors }, getValues, watch, reset, setValue, trigger } = methods;

    const [requestId, setRequestId] = useState<string>("");
    const [previewOpen, setPreviewOpen] = useState(false);
    const [previewImage, setPreviewImage] = useState('');
    const [previewTitle, setPreviewTitle] = useState('');
    const [fileList1, setFileList1] = useState<UploadFile[]>([]);
    const [fileList2, setFileList2] = useState<UploadFile[]>([]);
    const [fileList3, setFileList3] = useState<UploadFile[]>([]);
    const [fileList4, setFileList4] = useState<UploadFile[]>([]);
    const [fileList5, setFileList5] = useState<UploadFile[]>([]);
    const [fileList6, setFileList6] = useState<UploadFile[]>([]);
    const [fileList7, setFileList7] = useState<UploadFile[]>([]);
    const [fileList8, setFileList8] = useState<UploadFile[]>([]);
    const [fileList9, setFileList9] = useState<UploadFile[]>([]);
    const [fileList10, setFileList10] = useState<UploadFile[]>([]);
    const [fileList11, setFileList11] = useState<UploadFile[]>([]);
    // const [fileList12, setFileList12] = useState<UploadFile[]>([]);
    // const [fileList13, setFileList13] = useState<UploadFile[]>([]);
    // const [fileList14, setFileList14] = useState<UploadFile[]>([]);
    // const [fileList15, setFileList15] = useState<UploadFile[]>([]);
    // const [fileList16, setFileList16] = useState<UploadFile[]>([]);
    // const [fileList17, setFileList17] = useState<UploadFile[]>([]);
    // const [fileList18, setFileList18] = useState<UploadFile[]>([]);
    // const [fileList19, setFileList19] = useState<UploadFile[]>([]);
    // const [fileList20, setFileList20] = useState<UploadFile[]>([]);
    // const [fileList21, setFileList21] = useState<UploadFile[]>([]);
    // const [fileList22, setFileList22] = useState<UploadFile[]>([]);
    const [fileList23, setFileList23] = useState<UploadFile[]>([]);
    const [fileList24, setFileList24] = useState<UploadFile[]>([]);
    // const [fileList25, setFileList25] = useState<UploadFile[]>([]);
    // const [fileList26, setFileList26] = useState<UploadFile[]>([]);
    // const [fileList27, setFileList27] = useState<UploadFile[]>([]);
    // const [fileList28, setFileList28] = useState<UploadFile[]>([]);
    const [fileList29, setFileList29] = useState<UploadFile[]>([]);
    const [fileList30, setFileList30] = useState<UploadFile[]>([]);
    const [fileList31, setFileList31] = useState<UploadFile[]>([]);
    const [fileList32, setFileList32] = useState<UploadFile[]>([]);
    const [fileList33, setFileList33] = useState<UploadFile[]>([]);
    const [fileList34, setFileList34] = useState<UploadFile[]>([]);
    const [fileList35, setFileList35] = useState<UploadFile[]>([]);
    const [fileList36, setFileList36] = useState<UploadFile[]>([]);
    const [fileList37, setFileList37] = useState<UploadFile[]>([]);
    const [fileList38, setFileList38] = useState<UploadFile[]>([]);
    const [fileList39, setFileList39] = useState<UploadFile[]>([]);
    const [fileList40, setFileList40] = useState<UploadFile[]>([]);
    const [fileList41, setFileList41] = useState<UploadFile[]>([]);
    const [fileList42, setFileList42] = useState<UploadFile[]>([]);
    const [fileList43, setFileList43] = useState<UploadFile[]>([]);
    const [fileList44, setFileList44] = useState<UploadFile[]>([]);
    const [fileList45, setFileList45] = useState<UploadFile[]>([]);
    const [fileList46, setFileList46] = useState<UploadFile[]>([]);
    const [fileList47, setFileList47] = useState<UploadFile[]>([]);
    const [fileList48, setFileList48] = useState<UploadFile[]>([]);
    const [fileList49, setFileList49] = useState<UploadFile[]>([]);
    const [fileList50, setFileList50] = useState<UploadFile[]>([]);
    const [fileList51, setFileList51] = useState<UploadFile[]>([]);
    const [fileList52, setFileList52] = useState<UploadFile[]>([]);
    const [fileList53, setFileList53] = useState<UploadFile[]>([]);
    const [fileList54, setFileList54] = useState<UploadFile[]>([]);
    const [fileList55, setFileList55] = useState<UploadFile[]>([]);
    const [fileList56, setFileList56] = useState<UploadFile[]>([]);
    const [fileList57, setFileList57] = useState<UploadFile[]>([]);
    const [fileList58, setFileList58] = useState<UploadFile[]>([]);
    const [fileList59, setFileList59] = useState<UploadFile[]>([]);
    const [fileList60, setFileList60] = useState<UploadFile[]>([]);
    const [fileList61, setFileList61] = useState<UploadFile[]>([]);
    const [fileList62, setFileList62] = useState<UploadFile[]>([]);
    const [fileList63, setFileList63] = useState<UploadFile[]>([]);
    const [fileList64, setFileList64] = useState<UploadFile[]>([]);
    const [fileList65, setFileList65] = useState<UploadFile[]>([]);
    const [fileList66, setFileList66] = useState<UploadFile[]>([]);
    const [fileList67, setFileList67] = useState<UploadFile[]>([]);
    const [fileList99, setFileList99] = useState<UploadFile[]>([]);
    const [fileListApprove, setFileListApprove] = useState<UploadFile[]>([]);
    const [isRoleDOL, setIsRoleDOL] = useState<boolean>(false);
    const [isRender, setIsRender] = useState<boolean>(false);

    useEffect(() => {
        setIsLoading(true);

        var requestId: string = "";
        if (_.isArray(params?.id)) {
            requestId = params?.id[0];
            setRequestId(requestId);
        }
        setIsRoleDOL(["DOL"].includes(_.get(session?.user, ['userGroup'])));

        getDataSiteInfo(requestId).then((data) => {
            setIsLoading(false);
        }).catch(error => {
            console.log("#Error", error);
        });
    }, [requestId, isRender]);

    const getDataSiteInfo = async (requestId: string) => {
        if (requestId && requestId != null) {
            await getData(requestId).then((data) => {
                setFileList1(data.image1 && data.image1 != "" ? [{
                    uid: data.image1 || "",
                    name: "รูปหน้าสำนักงาน",
                    status: 'done',
                    url: data.image1 || "",
                }] : []);
                setFileList2(data.image2 && data.image2 != "" ? [{
                    uid: data.image2 || "",
                    name: "ภาพตู้ RACK Network ที่สำนักงาน",
                    status: 'done',
                    url: data.image2 || "",
                }] : []);
                setFileList3(data.image3 && data.image3 != "" ? [{
                    uid: data.image3 || "",
                    name: "CPE Switch วงจรหลัก",
                    status: 'done',
                    url: data.image3 || "",
                }] : []);
                setFileList4(data.image4 && data.image4 != "" ? [{
                    uid: data.image4 || "",
                    name: "CPE Switch วงจรรอง",
                    status: 'done',
                    url: data.image4 || "",
                }] : []);
                setFileList5(data.image5 && data.image5 != "" ? [{
                    uid: data.image5 || "",
                    name: "Firewall - 1",
                    status: 'done',
                    url: data.image5 || "",
                }] : []);
                setFileList6(data.image6 && data.image6 != "" ? [{
                    uid: data.image6 || "",
                    name: "Firewall - 2",
                    status: 'done',
                    url: data.image6 || "",
                }] : []);
                setFileList7(data.image7 && data.image7 != "" ? [{
                    uid: data.image7 || "",
                    name: "Router - 1",
                    status: 'done',
                    url: data.image7 || "",
                }] : []);
                setFileList8(data.image8 && data.image8 != "" ? [{
                    uid: data.image8 || "",
                    name: "Router - 2",
                    status: 'done',
                    url: data.image8 || "",
                }] : []);
                setFileList9(data.image9 && data.image9 != "" ? [{
                    uid: data.image9 || "",
                    name: "WiFi 1 ชุด",
                    status: 'done',
                    url: data.image9 || "",
                }] : []);
                setFileList10(data.image10 && data.image10 != "" ? [{
                    uid: data.image10 || "",
                    name: "Router 4G 1 ชุด",
                    status: 'done',
                    url: data.image10 || "",
                }] : []);
                setFileList11(data.image11 && data.image11 != "" ? [{
                    uid: data.image11 || "",
                    name: "AR109",
                    status: 'done',
                    url: data.image11 || "",
                }] : []);
                // setFileList12(data.image12 && data.image12 != "" ? [{
                //     uid: data.image12 || "",
                //     name: "Ping www.dol.go.th -n 30",
                //     status: 'done',
                //     url: data.image12 || "",
                // }] : []);
                // setFileList13(data.image13 && data.image13 != "" ? [{
                //     uid: data.image13 || "",
                //     name: "Ping Test 2",
                //     status: 'done',
                //     url: data.image13 || "",
                // }] : []);
                // setFileList14(data.image14 && data.image14 != "" ? [{
                //     uid: data.image14 || "",
                //     name: "Ping Test 3",
                //     status: 'done',
                //     url: data.image14 || "",
                // }] : []);
                // setFileList15(data.image15 && data.image15 != "" ? [{
                //     uid: data.image15 || "",
                //     name: "Ping Test 4",
                //     status: 'done',
                //     url: data.image15 || "",
                // }] : []);
                // setFileList16(data.image16 && data.image16 != "" ? [{
                //     uid: data.image16 || "",
                //     name: "เข้าเว็บไซต์ Internet กรมที่ดิน",
                //     status: 'done',
                //     url: data.image16 || "",
                // }] : []);
                // setFileList17(data.image17 && data.image17 != "" ? [{
                //     uid: data.image17 || "",
                //     name: "เข้า FTP กรมที่ดิน",
                //     status: 'done',
                //     url: data.image17 || "",
                // }] : []);
                // setFileList18(data.image18 && data.image18 != "" ? [{
                //     uid: data.image18 || "",
                //     name: "เข้าระบบผู้รับมอบอำนาจ",
                //     status: 'done',
                //     url: data.image18 || "",
                // }] : []);
                // setFileList19(data.image19 && data.image19 != "" ? [{
                //     uid: data.image19 || "",
                //     name: "เข้าระบบ MIS",
                //     status: 'done',
                //     url: data.image19 || "",
                // }] : []);
                // setFileList20(data.image20 && data.image20 != "" ? [{
                //     uid: data.image20 || "",
                //     name: "เข้าระบบควบคุมการจัดเก็บหลักฐาน",
                //     status: 'done',
                //     url: data.image20 || "",
                // }] : []);
                // setFileList21(data.image21 && data.image21 != "" ? [{
                //     uid: data.image21 || "",
                //     name: "เข้าโปรแกรมบริการกรมที่ดิน",
                //     status: 'done',
                //     url: data.image21 || "",
                // }] : []);
                // setFileList22(data.image22 && data.image22 != "" ? [{
                //     uid: data.image22 || "",
                //     name: "ระบบพิสูจน์ตัวตนในการใช้งานเครือข่าย",
                //     status: 'done',
                //     url: data.image22 || "",
                // }] : []);
                setFileList23(data.image23 && data.image23 != "" ? [{
                    uid: data.image23 || "",
                    name: "WAN1 (วงจรหลัก)",
                    status: 'done',
                    url: data.image23 || "",
                }] : []);
                setFileList24(data.image24 && data.image24 != "" ? [{
                    uid: data.image24 || "",
                    name: "WAN2 (วงจรรอง)",
                    status: 'done',
                    url: data.image24 || "",
                }] : []);
                setFileList29(data.image29 && data.image29 != "" ? [{
                    uid: data.image29 || "",
                    name: "ทดสอบ การใช้งาน Wifi : Network Connection",
                    status: 'done',
                    url: data.image29 || "",
                }] : []);
                setFileList30(data.image30 && data.image30 != "" ? [{
                    uid: data.image30 || "",
                    name: "ทดสอบ Authentication",
                    status: 'done',
                    url: data.image30 || "",
                }] : []);
                setFileList31(data.image31 && data.image31 != "" ? [{
                    uid: data.image31 || "",
                    name: "ภาพ ping www.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)",
                    status: 'done',
                    url: data.image31 || "",
                }] : []);
                setFileList32(data.image32 && data.image32 != "" ? [{
                    uid: data.image32 || "",
                    name: "ภาพ tracert www.dol.go.th (วงจรหลัก)",
                    status: 'done',
                    url: data.image32 || "",
                }] : []);
                setFileList33(data.image33 && data.image33 != "" ? [{
                    uid: data.image33 || "",
                    name: "ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรหลัก)",
                    status: 'done',
                    url: data.image33 || "",
                }] : []);
                setFileList34(data.image34 && data.image34 != "" ? [{
                    uid: data.image34 || "",
                    name: "ภาพ ping ilands.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)",
                    status: 'done',
                    url: data.image34 || "",
                }] : []);
                setFileList35(data.image35 && data.image35 != "" ? [{
                    uid: data.image35 || "",
                    name: "ภาพ tracert ilands.dol.go.th (วงจรหลัก)",
                    status: 'done',
                    url: data.image35 || "",
                }] : []);
                setFileList36(data.image36 && data.image36 != "" ? [{
                    uid: data.image36 || "",
                    name: "ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรหลัก)",
                    status: 'done',
                    url: data.image36 || "",
                }] : []);
                setFileList37(data.image37 && data.image37 != "" ? [{
                    uid: data.image37 || "",
                    name: "ภาพ ping 10.200.30.247 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)",
                    status: 'done',
                    url: data.image37 || "",
                }] : []);
                setFileList38(data.image38 && data.image38 != "" ? [{
                    uid: data.image38 || "",
                    name: "ภาพ tracert 10.200.30.247 (วงจรหลัก)",
                    status: 'done',
                    url: data.image38 || "",
                }] : []);
                setFileList39(data.image39 && data.image39 != "" ? [{
                    uid: data.image39 || "",
                    name: "ภาพ ping 8.8.8.8 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)",
                    status: 'done',
                    url: data.image39 || "",
                }] : []);
                setFileList40(data.image40 && data.image40 != "" ? [{
                    uid: data.image40 || "",
                    name: "ภาพ tracert 8.8.8.8 (วงจรหลัก)",
                    status: 'done',
                    url: data.image40 || "",
                }] : []);
                setFileList41(data.image41 && data.image41 != "" ? [{
                    uid: data.image41 || "",
                    name: "ภาพ ping www.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)",
                    status: 'done',
                    url: data.image41 || "",
                }] : []);
                setFileList42(data.image42 && data.image42 != "" ? [{
                    uid: data.image42 || "",
                    name: "ภาพ tracert www.dol.go.th (วงจรรอง)",
                    status: 'done',
                    url: data.image42 || "",
                }] : []);
                setFileList43(data.image43 && data.image43 != "" ? [{
                    uid: data.image43 || "",
                    name: "ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรรอง)",
                    status: 'done',
                    url: data.image43 || "",
                }] : []);
                setFileList44(data.image44 && data.image44 != "" ? [{
                    uid: data.image44 || "",
                    name: "ภาพ ping ilands.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)",
                    status: 'done',
                    url: data.image44 || "",
                }] : []);
                setFileList45(data.image45 && data.image45 != "" ? [{
                    uid: data.image45 || "",
                    name: "ภาพ tracert ilands.dol.go.th (วงจรรอง)",
                    status: 'done',
                    url: data.image45 || "",
                }] : []);
                setFileList46(data.image46 && data.image46 != "" ? [{
                    uid: data.image46 || "",
                    name: "ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรรอง)",
                    status: 'done',
                    url: data.image46 || "",
                }] : []);
                setFileList47(data.image47 && data.image47 != "" ? [{
                    uid: data.image47 || "",
                    name: "ภาพ ping 10.200.30.247 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)",
                    status: 'done',
                    url: data.image47 || "",
                }] : []);
                setFileList48(data.image48 && data.image48 != "" ? [{
                    uid: data.image48 || "",
                    name: "ภาพ tracert 10.200.30.247 (วงจรรอง)",
                    status: 'done',
                    url: data.image48 || "",
                }] : []);
                setFileList49(data.image49 && data.image49 != "" ? [{
                    uid: data.image49 || "",
                    name: "ภาพ ping 8.8.8.8 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)",
                    status: 'done',
                    url: data.image49 || "",
                }] : []);
                setFileList50(data.image50 && data.image50 != "" ? [{
                    uid: data.image50 || "",
                    name: "ภาพ tracert 8.8.8.8 (วงจรรอง)",
                    status: 'done',
                    url: data.image50 || "",
                }] : []);
                setFileList51(data.image51 && data.image51 != "" ? [{
                    uid: data.image51 || "",
                    name: "ภาพการทดสอบรับ IP Address",
                    status: 'done',
                    url: data.image51 || "",
                }] : []);
                setFileList52(data.image52 && data.image52 != "" ? [{
                    uid: data.image52 || "",
                    name: "วงจร Internet",
                    status: 'done',
                    url: data.image52 || "",
                }] : []);
                setFileList53(data.image53 && data.image53 != "" ? [{
                    uid: data.image53 || "",
                    name: "วงจร 4G",
                    status: 'done',
                    url: data.image53 || "",
                }] : []);
                setFileList54(data.image54 && data.image54 != "" ? [{
                    uid: data.image54 || "",
                    name: "ภาพสถานะวงจรที่จะทดสอบ Nagios",
                    status: 'done',
                    url: data.image54 || "",
                }] : []);
                setFileList55(data.image55 && data.image55 != "" ? [{
                    uid: data.image55 || "",
                    name: "ภาพเริ่มทดสอบวงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร",
                    status: 'done',
                    url: data.image55 || "",
                }] : []);
                setFileList56(data.image56 && data.image56 != "" ? [{
                    uid: data.image56 || "",
                    name: "ภาพ ping 8.8.8.8 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)",
                    status: 'done',
                    url: data.image56 || "",
                }] : []);
                setFileList57(data.image57 && data.image57 != "" ? [{
                    uid: data.image57 || "",
                    name: "ภาพ tracert 8.8.8.8 (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)",
                    status: 'done',
                    url: data.image57 || "",
                }] : []);
                setFileList58(data.image58 && data.image58 != "" ? [{
                    uid: data.image58 || "",
                    name: "ภาพเริ่มทดสอบวงจรหลัก",
                    status: 'done',
                    url: data.image58 || "",
                }] : []);
                setFileList59(data.image59 && data.image59 != "" ? [{
                    uid: data.image59 || "",
                    name: "วงจรหลัก Tunnel Destination DC1",
                    status: 'done',
                    url: data.image59 || "",
                }] : []);
                setFileList60(data.image60 && data.image60 != "" ? [{
                    uid: data.image60 || "",
                    name: "วงจรหลัก Tunnel Destination DC2",
                    status: 'done',
                    url: data.image60 || "",
                }] : []);
                setFileList61(data.image61 && data.image61 != "" ? [{
                    uid: data.image61 || "",
                    name: "ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)",
                    status: 'done',
                    url: data.image61 || "",
                }] : []);
                setFileList62(data.image62 && data.image62 != "" ? [{
                    uid: data.image62 || "",
                    name: "ภาพ tracert 10.100.30.247 (วงจรหลัก)",
                    status: 'done',
                    url: data.image62 || "",
                }] : []);
                setFileList63(data.image63 && data.image63 != "" ? [{
                    uid: data.image63 || "",
                    name: "ภาพเริ่มทดสอบวงจรสำรอง",
                    status: 'done',
                    url: data.image63 || "",
                }] : []);
                setFileList64(data.image64 && data.image64 != "" ? [{
                    uid: data.image64 || "",
                    name: "วงจรสำรอง Tunnel Destination DC1",
                    status: 'done',
                    url: data.image64 || "",
                }] : []);
                setFileList65(data.image65 && data.image65 != "" ? [{
                    uid: data.image65 || "",
                    name: "วงจรสำรอง Tunnel Destination DC2",
                    status: 'done',
                    url: data.image65 || "",
                }] : []);
                setFileList66(data.image66 && data.image66 != "" ? [{
                    uid: data.image66 || "",
                    name: "ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสำรอง)",
                    status: 'done',
                    url: data.image66 || "",
                }] : []);
                setFileList67(data.image67 && data.image67 != "" ? [{
                    uid: data.image67 || "",
                    name: "ภาพ tracert 10.100.30.247 (วงจรสำรอง)",
                    status: 'done',
                    url: data.image67 || "",
                }] : []);
                setFileList99([]);
                setFileListApprove(data.fileApproveName && data.fileApproveName != "" ? [{
                    uid: data.fileApproveName || "",
                    name: "เอกสารรับมอบการติดตั้ง",
                    status: 'done',
                    url: data.fileApproveName || "",
                }] : []);
                reset(data);
            }).catch(error => {
                console.log("#Error", error);
            });
        }
    }

    const handleCancel = () => setPreviewOpen(false);
    const handlePreview = async (file: UploadFile) => {
        if (!file.url && !file.preview) {
            file.preview = await getBase64(file.originFileObj as FileType);
        }

        setPreviewImage(file.url || (file.preview as string));
        setPreviewOpen(true);
        setPreviewTitle(file.name || file.url!.substring(file.url!.lastIndexOf('/') + 1));
    };

    // const handleDownload = (url: string, name: string) => {
    //     fetch(url).then((response) => response.blob())
    //         .then((blob) => {
    //             const url = window.URL.createObjectURL(new Blob([blob]));
    //             const link = document.createElement("a");
    //             link.href = url;
    //             link.download = `รายงานผลการติดตั้ง และทดสอบเครือข่ายสื่อสาร โครงการเช่าใช้บริการเครือข่ายการสื่อสารกรมที่ดิน ${name}`;
    //             document.body.appendChild(link);

    //             link.click();

    //             document.body.removeChild(link);
    //             window.URL.revokeObjectURL(url);
    //         })
    //         .catch((error) => {
    //             console.error("Error fetching the file:", error);
    //         });
    // };

    const handleChange1: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList1(info.fileList);
                onUpdateImage(info.fileList, "image1");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList1(info.fileList);
                        onDeleteImage("image1");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList1(info.fileList);
            }
        }
    };
    const handleChange2: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList2(info.fileList);
                onUpdateImage(info.fileList, "image2");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList2(info.fileList);
                        onDeleteImage("image2");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList2(info.fileList);
            }
        }
    };
    const handleChange3: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList3(info.fileList);
                onUpdateImage(info.fileList, "image3");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList3(info.fileList);
                        onDeleteImage("image3");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList3(info.fileList);
            }
        }
    };
    const handleChange4: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList4(info.fileList);
                onUpdateImage(info.fileList, "image4");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList4(info.fileList);
                        onDeleteImage("image4");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList4(info.fileList);
            }
        }
    };
    const handleChange5: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList5(info.fileList);
                onUpdateImage(info.fileList, "image5");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList5(info.fileList);
                        onDeleteImage("image5");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList5(info.fileList);
            }
        }
    };
    const handleChange6: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList6(info.fileList);
                onUpdateImage(info.fileList, "image6");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList6(info.fileList);
                        onDeleteImage("image6");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList6(info.fileList);
            }
        }
    };
    const handleChange7: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList7(info.fileList);
                onUpdateImage(info.fileList, "image7");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList7(info.fileList);
                        onDeleteImage("image7");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList7(info.fileList);
            }
        }
    };
    const handleChange8: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList8(info.fileList);
                onUpdateImage(info.fileList, "image8");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList8(info.fileList);
                        onDeleteImage("image8");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList8(info.fileList);
            }
        }
    };
    const handleChange9: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList9(info.fileList);
                onUpdateImage(info.fileList, "image9");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList9(info.fileList);
                        onDeleteImage("image9");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList9(info.fileList);
            }
        }
    };
    const handleChange10: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList10(info.fileList);
                onUpdateImage(info.fileList, "image10");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList10(info.fileList);
                        onDeleteImage("image10");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList10(info.fileList);
            }
        }
    };
    const handleChange11: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList11(info.fileList);
                onUpdateImage(info.fileList, "image11");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList11(info.fileList);
                        onDeleteImage("image11");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList11(info.fileList);
            }
        }
    };
    // const handleChange12: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList12(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image12");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image12");
    //         }
    //     }
    // };
    // const handleChange13: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList13(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image13");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image13");
    //         }
    //     }
    // };
    // const handleChange14: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList14(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image14");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image14");
    //         }
    //     }
    // };
    // const handleChange15: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList15(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image15");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image15");
    //         }
    //     }
    // };
    // const handleChange16: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList16(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image16");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image16");
    //         }
    //     }
    // };
    // const handleChange17: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList17(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image17");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image17");
    //         }
    //     }
    // };
    // const handleChange18: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList18(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image18");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image18");
    //         }
    //     }
    // };
    // const handleChange19: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList19(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image19");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image19");
    //         }
    //     }
    // };
    // const handleChange20: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList20(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image20");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image20");
    //         }
    //     }
    // };
    // const handleChange21: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList21(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image21");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image21");
    //         }
    //     }
    // };
    // const handleChange22: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList22(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image22");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image22");
    //         }
    //     }
    // };
    const handleChange23: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList23(info.fileList);
                onUpdateImage(info.fileList, "image23");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList23(info.fileList);
                        onDeleteImage("image23");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList23(info.fileList);
            }
        }
    };
    const handleChange24: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList24(info.fileList);
                onUpdateImage(info.fileList, "image24");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList24(info.fileList);
                        onDeleteImage("image24");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList24(info.fileList);
            }
        }
    };
    // const handleChange25: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList25(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image25");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image25");
    //         }
    //     }
    // };
    // const handleChange26: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList26(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image26");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image26");
    //         }
    //     }
    // };
    // const handleChange27: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList27(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image27");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image27");
    //         }
    //     }
    // };
    // const handleChange28: UploadProps['onChange'] = (info) => {
    //     if (info.file.status) {
    //         setFileList28(info.fileList);
    //         if (info.file.status == "done") {
    //             onUpdateImage(info.fileList, "image28");
    //         } else if (info.file.status == "removed") {
    //             onDeleteImage("image28");
    //         }
    //     }
    // };
    const handleChange29: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList29(info.fileList);
                onUpdateImage(info.fileList, "image29");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList29(info.fileList);
                        onDeleteImage("image29");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList29(info.fileList);
            }
        }
    };
    const handleChange30: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList30(info.fileList);
                onUpdateImage(info.fileList, "image30");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList30(info.fileList);
                        onDeleteImage("image30");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList30(info.fileList);
            }
        }
    };
    const handleChange31: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList31(info.fileList);
                onUpdateImage(info.fileList, "image31");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList31(info.fileList);
                        onDeleteImage("image31");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList31(info.fileList);
            }
        }
    };
    const handleChange32: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList32(info.fileList);
                onUpdateImage(info.fileList, "image32");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList32(info.fileList);
                        onDeleteImage("image32");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList32(info.fileList);
            }
        }
    };
    const handleChange33: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList33(info.fileList);
                onUpdateImage(info.fileList, "image33");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList33(info.fileList);
                        onDeleteImage("image33");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList33(info.fileList);
            }
        }
    };
    const handleChange34: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList34(info.fileList);
                onUpdateImage(info.fileList, "image34");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList34(info.fileList);
                        onDeleteImage("image34");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList34(info.fileList);
            }
        }
    };
    const handleChange35: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList35(info.fileList);
                onUpdateImage(info.fileList, "image35");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList35(info.fileList);
                        onDeleteImage("image35");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList35(info.fileList);
            }
        }
    };
    const handleChange36: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList36(info.fileList);
                onUpdateImage(info.fileList, "image36");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList36(info.fileList);
                        onDeleteImage("image36");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList36(info.fileList);
            }
        }
    };
    const handleChange37: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList37(info.fileList);
                onUpdateImage(info.fileList, "image37");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList37(info.fileList);
                        onDeleteImage("image37");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList37(info.fileList);
            }
        }
    };
    const handleChange38: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList38(info.fileList);
                onUpdateImage(info.fileList, "image38");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList38(info.fileList);
                        onDeleteImage("image38");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList38(info.fileList);
            }
        }
    };
    const handleChange39: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList39(info.fileList);
                onUpdateImage(info.fileList, "image39");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList39(info.fileList);
                        onDeleteImage("image39");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList39(info.fileList);
            }
        }
    };
    const handleChange40: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList40(info.fileList);
                onUpdateImage(info.fileList, "image40");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList40(info.fileList);
                        onDeleteImage("image40");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList40(info.fileList);
            }
        }
    };
    const handleChange41: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList41(info.fileList);
                onUpdateImage(info.fileList, "image41");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList41(info.fileList);
                        onDeleteImage("image41");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList41(info.fileList);
            }
        }
    };
    const handleChange42: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList42(info.fileList);
                onUpdateImage(info.fileList, "image42");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList42(info.fileList);
                        onDeleteImage("image42");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList42(info.fileList);
            }
        }
    };
    const handleChange43: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList43(info.fileList);
                onUpdateImage(info.fileList, "image43");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList43(info.fileList);
                        onDeleteImage("image43");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList43(info.fileList);
            }
        }
    };
    const handleChange44: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList44(info.fileList);
                onUpdateImage(info.fileList, "image44");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList44(info.fileList);
                        onDeleteImage("image44");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList44(info.fileList);
            }
        }
    };
    const handleChange45: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList45(info.fileList);
                onUpdateImage(info.fileList, "image45");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList45(info.fileList);
                        onDeleteImage("image45");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList45(info.fileList);
            }
        }
    };
    const handleChange46: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList46(info.fileList);
                onUpdateImage(info.fileList, "image46");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList46(info.fileList);
                        onDeleteImage("image46");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList46(info.fileList);
            }
        }
    };
    const handleChange47: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList47(info.fileList);
                onUpdateImage(info.fileList, "image47");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList47(info.fileList);
                        onDeleteImage("image47");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList47(info.fileList);
            }
        }
    };
    const handleChange48: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList48(info.fileList);
                onUpdateImage(info.fileList, "image48");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList48(info.fileList);
                        onDeleteImage("image48");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList48(info.fileList);
            }
        }
    };
    const handleChange49: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList49(info.fileList);
                onUpdateImage(info.fileList, "image49");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList49(info.fileList);
                        onDeleteImage("image49");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList49(info.fileList);
            }
        }
    };
    const handleChange50: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList50(info.fileList);
                onUpdateImage(info.fileList, "image50");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList50(info.fileList);
                        onDeleteImage("image50");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList50(info.fileList);
            }
        }
    };
    const handleChange51: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList51(info.fileList);
                onUpdateImage(info.fileList, "image51");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList51(info.fileList);
                        onDeleteImage("image51");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList51(info.fileList);
            }
        }
    };
    const handleChange52: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList52(info.fileList);
                onUpdateImage(info.fileList, "image52");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList52(info.fileList);
                        onDeleteImage("image52");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList52(info.fileList);
            }
        }
    };
    const handleChange53: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList53(info.fileList);
                onUpdateImage(info.fileList, "image53");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList53(info.fileList);
                        onDeleteImage("image53");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList53(info.fileList);
            }
        }
    };
    const handleChange54: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList54(info.fileList);
                onUpdateImage(info.fileList, "image54");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList54(info.fileList);
                        onDeleteImage("image54");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList54(info.fileList);
            }
        }
    };
    const handleChange55: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList55(info.fileList);
                onUpdateImage(info.fileList, "image55");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList55(info.fileList);
                        onDeleteImage("image55");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList55(info.fileList);
            }
        }
    };
    const handleChange56: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList56(info.fileList);
                onUpdateImage(info.fileList, "image56");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList56(info.fileList);
                        onDeleteImage("image56");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList56(info.fileList);
            }
        }
    };
    const handleChange57: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList57(info.fileList);
                onUpdateImage(info.fileList, "image57");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList57(info.fileList);
                        onDeleteImage("image57");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList57(info.fileList);
            }
        }
    };
    const handleChange58: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList58(info.fileList);
                onUpdateImage(info.fileList, "image58");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList58(info.fileList);
                        onDeleteImage("image58");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList58(info.fileList);
            }
        }
    };
    const handleChange59: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList59(info.fileList);
                onUpdateImage(info.fileList, "image59");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList59(info.fileList);
                        onDeleteImage("image59");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList59(info.fileList);
            }
        }
    };
    const handleChange60: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList60(info.fileList);
                onUpdateImage(info.fileList, "image60");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList60(info.fileList);
                        onDeleteImage("image60");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList60(info.fileList);
            }
        }
    };
    const handleChange61: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList61(info.fileList);
                onUpdateImage(info.fileList, "image61");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList61(info.fileList);
                        onDeleteImage("image61");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList61(info.fileList);
            }
        }
    };
    const handleChange62: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList62(info.fileList);
                onUpdateImage(info.fileList, "image62");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList62(info.fileList);
                        onDeleteImage("image62");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList62(info.fileList);
            }
        }
    };
    const handleChange63: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList63(info.fileList);
                onUpdateImage(info.fileList, "image63");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList63(info.fileList);
                        onDeleteImage("image63");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList63(info.fileList);
            }
        }
    };
    const handleChange64: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList64(info.fileList);
                onUpdateImage(info.fileList, "image64");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList64(info.fileList);
                        onDeleteImage("image64");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList64(info.fileList);
            }
        }
    };
    const handleChange65: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList65(info.fileList);
                onUpdateImage(info.fileList, "image65");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList65(info.fileList);
                        onDeleteImage("image65");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList65(info.fileList);
            }
        }
    };
    const handleChange66: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList66(info.fileList);
                onUpdateImage(info.fileList, "image66");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList66(info.fileList);
                        onDeleteImage("image66");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList66(info.fileList);
            }
        }
    };
    const handleChange67: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileList67(info.fileList);
                onUpdateImage(info.fileList, "image67");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList67(info.fileList);
                        onDeleteImage("image67");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList67(info.fileList);
            }
        }
    };
    const handleChange99: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            var fileList = info.fileList.filter(a => a.type === 'image/jpeg' || a.type === 'image/png');
            if (info.file.status == "uploading") {
                setFileList99(fileList);
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileList99(fileList);
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileList99(fileList);
            }
        }
    };
    const handleChangeApprove: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            if (info.file.status == "uploading") {
                setFileListApprove(info.fileList);
                onUpdateImage(info.fileList, "approve");
            } else if (info.file.status == "removed") {
                alertCtx.confirm("ยืนยันการลบ", "คถณต้องการลบรูปภาพนี้?", {
                    onOk: async () => {
                        setFileListApprove(info.fileList);
                        onDeleteImage("approve");
                    },
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            } else if (info.file.status == "done") {
                setFileListApprove(info.fileList);
            }
        }
    };

    const onUpdateImage = async (fileList: UploadFile[], key: string) => {
        if (fileList.length > 0) {
            await updateImageSiteInfo(requestId, key, _.get(session?.user, ['username']) || "", fileList[0].originFileObj as File)
                .then(async (res) => {
                    if (res.data.status) {
                        // console.log("a", _.get(res.data.data, [key]));

                    } else {
                        alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                            okText: "ตกลง",
                            okButtonProps: {
                                className: "bg-dashboard-indigo text-white"
                            }
                        });
                    }
                }).catch(error => {
                    console.log("error", error);
                    alertCtx.error("ผิดพลาด", `${error.data.message}`, {
                        okText: "ตกลง",
                        okButtonProps: {
                            className: "bg-dashboard-indigo text-white"
                        }
                    });
                });
        }
    }
    const onUpdateMutipleImage = async (fileList: UploadFile[]) => {
        if (fileList.length > 0) {
            var files: File[] = [];
            fileList.map(ct => {
                const originFile = ct.originFileObj as File;
                if (originFile) {
                    files.push(originFile);
                }
            });
            await updateImages(requestId, _.get(session?.user, ['username']) || "", files)
                .then(async (res) => {
                    console.log("res", res);

                    if (res.data.status) {
                        alertCtx.success("สำเร็จ", "อัพโหลดรูปภาพสำเร็จ", {
                            okText: "ตกลง",
                            okButtonProps: {
                                className: "bg-dashboard-indigo text-white"
                            },
                            onOk: () => {
                                setIsRender(!isRender);
                            },
                        });
                    } else {
                        alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                            okText: "ตกลง",
                            okButtonProps: {
                                className: "bg-dashboard-indigo text-white"
                            }
                        });
                    }
                }).catch(error => {
                    console.log("error", error);
                    alertCtx.error("ผิดพลาด", `${error.data.message}`, {
                        okText: "ตกลง",
                        okButtonProps: {
                            className: "bg-dashboard-indigo text-white"
                        }
                    });
                });
        }
    }
    const onDeleteImage = async (key: string) => {
        await deleteImageSiteInfo(requestId, key, _.get(session?.user, ['username']) || "")
            .then(async (res) => {
                if (res.data.status) {
                } else {
                    alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                        okText: "ตกลง",
                        okButtonProps: {
                            className: "bg-dashboard-indigo text-white"
                        }
                    });
                }
            }).catch(error => {
                console.log("error", error);
                alertCtx.error("ผิดพลาด", `${error.data.message}`, {
                    okText: "ตกลง",
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
            });
    }
    const onSubmit: SubmitHandler<TrnSiteInformation> = async (data) => {
        setIsLoading(true);
        await updateSiteInfo(data).then(async (res) => {
            // console.log("res =>", res);
            // console.log("res => data", res.data);

            if (res.data.status) {
                alertCtx.success("สำเร็จ", "บันทึกรายการสำเร็จ", {
                    okText: "ตกลง",
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    },
                    onOk: () => {
                        router.push(`/siteinfo/${requestId}`);
                    },
                });
            } else {
                alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                    okText: "ตกลง",
                    okButtonProps: {
                        className: "bg-dashboard-indigo text-white"
                    }
                });
                setIsLoading(false);
            }
        }).catch(error => {
            console.log("error", error);
            alertCtx.error("ผิดพลาด", `${error.data.message}`, {
                okText: "ตกลง",
                okButtonProps: {
                    className: "bg-dashboard-indigo text-white"
                }
            });
            setIsLoading(false);
        });
        setIsLoading(false);
    };

    return (
        <Can
            rules={["Admin", "Staff", "Helpdesk", "DOL"]}
            perform={_.get(session?.user, ['userGroup'])}
            yes={() => (
                <FormProvider {...methods}>
                    <div className="grid grid-rows-none grid-flow-col">
                        <div className="grid-cols-1">
                            <SectionComponent
                                title={
                                    <div className="grid gap-y-8 pt-2">
                                        <div className="flex flex-row items-start items-center justify-between font-semibold text-secondary">
                                            <div>
                                                <h2 className="text-2xl font-normal text-on-base">{getValues().locationName}</h2>
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
                                        <>
                                            <div className="grid grid-cols-1 sm:grid-cols-3 gap-x-5">
                                                <InputFieldComponent
                                                    name="siteNetworkName"
                                                    label={"ภาคผนวก"}
                                                    // require={true}
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
                                                    name="siteNetworkSeq"
                                                    label={"ลำดับ"}
                                                    // require={true}
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
                                                    name="locationName"
                                                    label={"ชื่อสถานที่"}
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
                                                    name="address"
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
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                                <InputFieldComponent
                                                    name="staffOrganize"
                                                    label={"เจ้าหน้าที่หน่วยงาน"}
                                                    // require={true}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="telephoneNumber"
                                                    label={"เบอร์ติดต่อ"}
                                                    // require={true}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="latitude"
                                                    label={"Latitude"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="longitude"
                                                    label={"Longtitude"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                        </>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"ข้อมูลทีมติดตั้งวงจร / ทีมติดตั้งอุปกรณ์"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <>
                                            <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-5">
                                                <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-5">
                                                    <InputFieldComponent
                                                        name="teamInstallContactName"
                                                        label={"ทีมติดตั้ง"}
                                                        mode={InputMode.secondary}
                                                        labelAlignClassName="text-left"
                                                        labelClassName="md:w-4/12"
                                                        inputClassName="md:w-8/12"
                                                        renderControl={field => (
                                                            <Input
                                                                {...field}
                                                                className="w-full"
                                                                size="large"
                                                                disabled={isRoleDOL}
                                                            />
                                                        )}
                                                    ></InputFieldComponent>
                                                    <InputFieldComponent
                                                        name="teamInstallContactTel"
                                                        label={"เบอร์ติดต่อทีมติดตั้ง"}
                                                        mode={InputMode.secondary}
                                                        labelAlignClassName="text-left"
                                                        labelClassName="md:w-4/12"
                                                        inputClassName="md:w-8/12"
                                                        renderControl={field => (
                                                            <Input
                                                                {...field}
                                                                className="w-full"
                                                                size="large"
                                                                disabled={isRoleDOL}
                                                            />
                                                        )}
                                                    ></InputFieldComponent>
                                                </div>
                                            </div>
                                        </>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"WAN1 (วงจรหลัก)"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <>
                                            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                                <InputFieldComponent
                                                    name="wan1Provider"
                                                    label={"Provider"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan1Cid"
                                                    label={"CID"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan1Speed"
                                                    label={"Speed (Mbps)"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                                <InputFieldComponent
                                                    name="wan1AsNumber"
                                                    label={"AS Number"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan1IpWan1Pe"
                                                    label={"IP WAN1 PE"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan1IpWan1Ce"
                                                    label={"IP WAN1 CE"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan1Subnet"
                                                    label={"WAN1 Subnet"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                        </>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"WAN2 (วงจรรอง)"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <>
                                            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                                <InputFieldComponent
                                                    name="wan2Provider"
                                                    label={"Provider"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan2Cid"
                                                    label={"CID"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan2Speed"
                                                    label={"Speed (Mbps)"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                                <InputFieldComponent
                                                    name="wan2AsNumber"
                                                    label={"AS Number"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan2IpWan1Pe"
                                                    label={"IP WAN2 PE"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan2IpWan1Ce"
                                                    label={"IP WAN2 CE"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="wan2Subnet"
                                                    label={"WAN2 Subnet"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                        </>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"วงจร Internet"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <>
                                            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                                <InputFieldComponent
                                                    name="internetCid"
                                                    label={"CID"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="internetSpeed"
                                                    label={"Speed (Mbps)"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                                <InputFieldComponent
                                                    name="internetAsNumber"
                                                    label={"AS Number"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="internetWanIpAddress"
                                                    label={"WAN IP Address"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                                <InputFieldComponent
                                                    name="internetSubnet"
                                                    label={"Subnet"}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="md:w-4/12"
                                                    inputClassName="md:w-8/12"
                                                    renderControl={field => (
                                                        <Input
                                                            {...field}
                                                            className="w-full"
                                                            size="large"
                                                            disabled={isRoleDOL}
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </div>
                                        </>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"Cellular 20Mbps"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                            <InputFieldComponent
                                                name="cellularSim"
                                                label={"SIM"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="cellularAr109"
                                                label={"AR109"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"IP Lan"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                            <InputFieldComponent
                                                name="ipLanGateway"
                                                label={"IP Gateway"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="ipLanSubnet"
                                                label={"Subnet"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"รายการอุปกรณ์"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                            <InputFieldComponent
                                                name="equipmentCpeSwitchMain"
                                                label={"CPE Switch วงจรหลัก"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentSnCpeSwitchMain"
                                                label={"S/N CPE Switch วงจรหลัก"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentCpeSwitchSecondary"
                                                label={"CPE Switch วงจรรอง"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentSnCpeSwitchSecondary"
                                                label={"S/N CPE Switch วงจรรอง"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentFirewall2Set"
                                                label={"Firewall 2 ชุด"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentFirewall1Sn"
                                                label={"Firewall - 1 S/N"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentFirewall2Sn"
                                                label={"Firewall - 2 S/N"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentRouter2Set"
                                                label={"Router 2 ชุด"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentRouter1Sn"
                                                label={"Router - 1 S/N"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentRouter2Sn"
                                                label={"Router - 2 S/N"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentWifi1Set"
                                                label={"WiFi 1 ชุด"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentWifiSn"
                                                label={"Wifi S/N"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentRouter4gSet"
                                                label={"Router 4G 1 ชุด"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name="equipmentRouter4gSn"
                                                label={"Router 4G S/N"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Input
                                                        {...field}
                                                        className="w-full"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"แนบรูปภาพ"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                            <InputFieldComponent
                                                name=""
                                                label={"รูปหน้าสำนักงาน"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        // action={}
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList1}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange1}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList1.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพตู้ RACK Network ที่สำนักงาน"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList2}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange2}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList2.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"ภาพอุปกรณ์ที่ติดตั้ง"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                            <InputFieldComponent
                                                name=""
                                                label={"CPE Switch วงจรหลัก"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList3}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange3}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList3.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"CPE Switch วงจรรอง"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList4}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange4}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList4.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"Firewall - 1"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList5}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange5}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList5.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"Firewall - 2"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList6}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange6}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList6.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"Router - 1"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList7}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange7}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList7.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"Router - 2"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList8}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange8}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList8.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"WiFi 1 ชุด"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList9}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange9}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList9.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"Router 4G 1 ชุด"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList10}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange10}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList10.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"AR109"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList11}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange11}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList11.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพสถานะวงจรที่จะทดสอบ Nagios"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList54}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange54}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList54.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพเริ่มทดสอบวงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList55}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange55}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList55.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping 8.8.8.8 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList56}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange56}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList56.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert 8.8.8.8 (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList57}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange57}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList57.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                            <SectionComponent
                                title={"ภาพผลการทดสอบตามแบบทดสอบการติดตั้งอุปกรณ์"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-5">
                                            {/* <InputFieldComponent
                                                name=""
                                                label={"Ping www.dol.go.th -n 30"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList12}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange12}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList12.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent> */}
                                            {/* <InputFieldComponent
                                                name=""
                                                label={"Ping Test 2"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList13}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange13}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList13.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"Ping Test 3"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList14}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange14}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList14.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"Ping Test 4"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList15}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange15}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList15.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent> */}
                                            {/* <InputFieldComponent
                                                name=""
                                                label={"เข้าเว็บไซต์ Internet กรมที่ดิน"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList16}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange16}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList16.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent> */}
                                            {/* <InputFieldComponent
                                                name=""
                                                label={"เข้า FTP กรมที่ดิน"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList17}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange17}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList17.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"เข้าระบบผู้รับมอบอำนาจ"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList18}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange18}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList18.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"เข้าระบบ MIS"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList19}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange19}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList19.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"เข้าระบบควบคุมการจัดเก็บหลักฐาน"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList20}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange20}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList20.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"เข้าโปรแกรมบริการกรมที่ดิน"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList21}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange21}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList21.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent> */}
                                            {/* <InputFieldComponent
                                                name=""
                                                label={"ระบบพิสูจน์ตัวตนในการใช้งานเครือข่าย"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList22}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange22}
                                                        beforeUpload={beforeUpload}
                                                    >
                                                        {fileList22.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent> */}
                                            <InputFieldComponent
                                                name=""
                                                label={"ทดสอบ การใช้งาน Wifi : Network Connection"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList29}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange29}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList29.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ทดสอบ Authentication"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList30}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange30}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList30.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping www.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList31}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange31}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList31.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert www.dol.go.th (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList32}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange32}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList32.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList33}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange33}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList33.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping ilands.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList34}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange34}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList34.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert ilands.dol.go.th (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList35}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange35}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList35.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList36}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange36}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList36.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping 10.200.30.247 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList37}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange37}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList37.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert 10.200.30.247 (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList38}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange38}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList38.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping 8.8.8.8 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList39}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange39}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList39.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert 8.8.8.8 (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList40}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange40}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList40.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping www.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList41}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange41}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList41.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert www.dol.go.th (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList42}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange42}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList42.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList43}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange43}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList43.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping ilands.dol.go.th -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList44}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange44}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList44.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert ilands.dol.go.th (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList45}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange45}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList45.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList46}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange46}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList46.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping 10.200.30.247 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList47}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange47}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList47.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert 10.200.30.247 (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList48}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange48}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList48.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping 8.8.8.8 -t 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList49}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange49}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList49.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert 8.8.8.8 (วงจรรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList50}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange50}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList50.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพการทดสอบรับ IP Address"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList51}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange51}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList51.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"เอกสารรับมอบการติดตั้ง"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileListApprove}
                                                        onPreview={handlePreview}
                                                        onChange={handleChangeApprove}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileListApprove.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพเริ่มทดสอบวงจรหลัก"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList58}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange58}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList58.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"วงจรหลัก Tunnel Destination DC1"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList59}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange59}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList59.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"วงจรหลัก Tunnel Destination DC2"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList60}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange60}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList60.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList61}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange61}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList61.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert 10.100.30.247 (วงจรหลัก)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList62}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange62}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList62.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพเริ่มทดสอบวงจรสำรอง"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList63}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange63}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList63.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"วงจรสำรอง Tunnel Destination DC1"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList64}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange64}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList64.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"วงจรสำรอง Tunnel Destination DC2"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList65}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange65}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList65.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสำรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList66}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange66}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList66.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                            <InputFieldComponent
                                                name=""
                                                label={"ภาพ tracert 10.100.30.247 (วงจรสำรอง)"}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="md:w-4/12"
                                                inputClassName="md:w-8/12"
                                                renderControl={field => (
                                                    <Upload
                                                        customRequest={(e) => dummyRequest(e)}
                                                        listType="picture-card"
                                                        fileList={fileList67}
                                                        onPreview={handlePreview}
                                                        onChange={handleChange67}
                                                        beforeUpload={beforeUpload}
                                                        disabled={isRoleDOL}
                                                    >
                                                        {fileList67.length >= 1 ? null : uploadButton}
                                                    </Upload>
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>

                            <SectionComponent
                                title={
                                    <>
                                        <span>แนบรูปภาพเพิ่มเติม</span>
                                        <Button
                                            className="bg-dashboard-indigo text-white ml-2"
                                            size="small"
                                            disabled={isRoleDOL}
                                            onClick={async () => {
                                                onUpdateMutipleImage(fileList99);
                                            }}
                                        >
                                            อัปโหลดรูปภาพ
                                        </Button>
                                    </>
                                }
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <div className="grid grid-cols-1 gap-x-5">
                                            <Upload
                                                customRequest={(e) => dummyRequest(e)}
                                                listType="picture-card"
                                                fileList={fileList99}
                                                onPreview={handlePreview}
                                                onChange={handleChange99}
                                                beforeUpload={beforeUpload}
                                                disabled={isRoleDOL}
                                                multiple={true}
                                            >
                                                {uploadButton}
                                            </Upload>
                                        </div>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>

                            <SectionComponent
                                title={"ผลการทดสอบความเร็วสัญญาณเครือข่าย"}
                                iconName=""
                                bodyClass=""
                            >
                                {
                                    !isLoading ?
                                        <>
                                            <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-5 mb-5">
                                                <div className="grid grid-cols-1 sm:grid-cols-4">
                                                    <div className="grid grid-cols-1">
                                                        <InputFieldComponent
                                                            name=""
                                                            label={"WAN1 (วงจรหลัก)"}
                                                            mode={InputMode.secondary}
                                                            labelAlignClassName="text-left"
                                                            labelClassName="md:w-4/12"
                                                            inputClassName="md:w-8/12"
                                                            renderControl={field => (
                                                                <Upload
                                                                    customRequest={(e) => dummyRequest(e)}
                                                                    listType="picture-card"
                                                                    fileList={fileList23}
                                                                    onPreview={handlePreview}
                                                                    onChange={handleChange23}
                                                                    beforeUpload={beforeUpload}
                                                                    disabled={isRoleDOL}
                                                                >
                                                                    {fileList23.length >= 1 ? null : uploadButton}
                                                                </Upload>
                                                            )}
                                                        ></InputFieldComponent>
                                                    </div>
                                                    <div className="col-span-3 gap-x-5">
                                                        <InputFieldComponent
                                                            name="wan1SpeedTestUpload"
                                                            label={"WAN1 Upload"}
                                                            mode={InputMode.secondary}
                                                            labelAlignClassName="text-left"
                                                            labelClassName="md:w-4/12"
                                                            inputClassName="md:w-8/12"
                                                            renderControl={field => (
                                                                <Input
                                                                    {...field}
                                                                    className="w-full"
                                                                    size="large"
                                                                    disabled={isRoleDOL}
                                                                />
                                                            )}
                                                        ></InputFieldComponent>
                                                        <InputFieldComponent
                                                            name="wan1SpeedTestDownload"
                                                            label={"WAN1 Download"}
                                                            mode={InputMode.secondary}
                                                            labelAlignClassName="text-left"
                                                            labelClassName="md:w-4/12"
                                                            inputClassName="md:w-8/12"
                                                            renderControl={field => (
                                                                <Input
                                                                    {...field}
                                                                    className="w-full"
                                                                    size="large"
                                                                    disabled={isRoleDOL}
                                                                />
                                                            )}
                                                        ></InputFieldComponent>
                                                    </div>
                                                </div>
                                                <div className="grid grid-cols-1 sm:grid-cols-4">
                                                    <div className="grid grid-cols-1">
                                                        <InputFieldComponent
                                                            name=""
                                                            label={"WAN2 (วงจรรอง)"}
                                                            mode={InputMode.secondary}
                                                            labelAlignClassName="text-left"
                                                            labelClassName="md:w-4/12"
                                                            inputClassName="md:w-8/12"
                                                            renderControl={field => (
                                                                <Upload
                                                                    customRequest={(e) => dummyRequest(e)}
                                                                    listType="picture-card"
                                                                    fileList={fileList24}
                                                                    onPreview={handlePreview}
                                                                    onChange={handleChange24}
                                                                    beforeUpload={beforeUpload}
                                                                    disabled={isRoleDOL}
                                                                >
                                                                    {fileList24.length >= 1 ? null : uploadButton}
                                                                </Upload>
                                                            )}
                                                        ></InputFieldComponent>
                                                    </div>
                                                    <div className="col-span-3 gap-x-5">
                                                        <InputFieldComponent
                                                            name="wan2SpeedTestUpload"
                                                            label={"WAN2 Upload"}
                                                            mode={InputMode.secondary}
                                                            labelAlignClassName="text-left"
                                                            labelClassName="md:w-4/12"
                                                            inputClassName="md:w-8/12"
                                                            renderControl={field => (
                                                                <Input
                                                                    {...field}
                                                                    className="w-full"
                                                                    size="large"
                                                                    disabled={isRoleDOL}
                                                                />
                                                            )}
                                                        ></InputFieldComponent>
                                                        <InputFieldComponent
                                                            name="wan2SpeedTestDownload"
                                                            label={"WAN2 Download"}
                                                            mode={InputMode.secondary}
                                                            labelAlignClassName="text-left"
                                                            labelClassName="md:w-4/12"
                                                            inputClassName="md:w-8/12"
                                                            renderControl={field => (
                                                                <Input
                                                                    {...field}
                                                                    className="w-full"
                                                                    size="large"
                                                                    disabled={isRoleDOL}
                                                                />
                                                            )}
                                                        ></InputFieldComponent>
                                                    </div>
                                                </div>
                                                {
                                                    getValues().siteNetwork?.id == 2 &&
                                                    <>
                                                        <div className="grid grid-cols-1 sm:grid-cols-4">
                                                            <div className="grid grid-cols-1">
                                                                <InputFieldComponent
                                                                    name=""
                                                                    label={"วงจร Internet"}
                                                                    mode={InputMode.secondary}
                                                                    labelAlignClassName="text-left"
                                                                    labelClassName="md:w-4/12"
                                                                    inputClassName="md:w-8/12"
                                                                    renderControl={field => (
                                                                        <Upload
                                                                            customRequest={(e) => dummyRequest(e)}
                                                                            listType="picture-card"
                                                                            fileList={fileList52}
                                                                            onPreview={handlePreview}
                                                                            onChange={handleChange52}
                                                                            beforeUpload={beforeUpload}
                                                                            disabled={isRoleDOL}
                                                                        >
                                                                            {fileList52.length >= 1 ? null : uploadButton}
                                                                        </Upload>
                                                                    )}
                                                                ></InputFieldComponent>
                                                            </div>
                                                            <div className="col-span-3 gap-x-5">
                                                                <InputFieldComponent
                                                                    name="circuitInternet100mbUpload"
                                                                    label={"วงจร Internet Upload"}
                                                                    mode={InputMode.secondary}
                                                                    labelAlignClassName="text-left"
                                                                    labelClassName="md:w-4/12"
                                                                    inputClassName="md:w-8/12"
                                                                    renderControl={field => (
                                                                        <Input
                                                                            {...field}
                                                                            className="w-full"
                                                                            size="large"
                                                                            disabled={isRoleDOL}
                                                                        />
                                                                    )}
                                                                ></InputFieldComponent>
                                                                <InputFieldComponent
                                                                    name="circuitInternet100mbDownload"
                                                                    label={"วงจร Internet Download"}
                                                                    mode={InputMode.secondary}
                                                                    labelAlignClassName="text-left"
                                                                    labelClassName="md:w-4/12"
                                                                    inputClassName="md:w-8/12"
                                                                    renderControl={field => (
                                                                        <Input
                                                                            {...field}
                                                                            className="w-full"
                                                                            size="large"
                                                                            disabled={isRoleDOL}
                                                                        />
                                                                    )}
                                                                ></InputFieldComponent>
                                                            </div>
                                                        </div>
                                                        <div className="grid grid-cols-1 sm:grid-cols-4">
                                                            <div className="grid grid-cols-1">
                                                                <InputFieldComponent
                                                                    name=""
                                                                    label={"วงจร 4G"}
                                                                    mode={InputMode.secondary}
                                                                    labelAlignClassName="text-left"
                                                                    labelClassName="md:w-4/12"
                                                                    inputClassName="md:w-8/12"
                                                                    renderControl={field => (
                                                                        <Upload
                                                                            customRequest={(e) => dummyRequest(e)}
                                                                            listType="picture-card"
                                                                            fileList={fileList53}
                                                                            onPreview={handlePreview}
                                                                            onChange={handleChange53}
                                                                            beforeUpload={beforeUpload}
                                                                            disabled={isRoleDOL}
                                                                        >
                                                                            {fileList53.length >= 1 ? null : uploadButton}
                                                                        </Upload>
                                                                    )}
                                                                ></InputFieldComponent>
                                                            </div>
                                                            <div className="col-span-3 gap-x-5">
                                                                <InputFieldComponent
                                                                    name="circuit4g20mbUpload"
                                                                    label={"วงจร 4G Upload"}
                                                                    mode={InputMode.secondary}
                                                                    labelAlignClassName="text-left"
                                                                    labelClassName="md:w-4/12"
                                                                    inputClassName="md:w-8/12"
                                                                    renderControl={field => (
                                                                        <Input
                                                                            {...field}
                                                                            className="w-full"
                                                                            size="large"
                                                                            disabled={isRoleDOL}
                                                                        />
                                                                    )}
                                                                ></InputFieldComponent>
                                                                <InputFieldComponent
                                                                    name="circuit4g20mbDownload"
                                                                    label={"วงจร 4G Download"}
                                                                    mode={InputMode.secondary}
                                                                    labelAlignClassName="text-left"
                                                                    labelClassName="md:w-4/12"
                                                                    inputClassName="md:w-8/12"
                                                                    renderControl={field => (
                                                                        <Input
                                                                            {...field}
                                                                            className="w-full"
                                                                            size="large"
                                                                            disabled={isRoleDOL}
                                                                        />
                                                                    )}
                                                                ></InputFieldComponent>
                                                            </div>
                                                        </div>
                                                    </>
                                                }
                                            </div>
                                            <div className="grid grid-cols-1 gap-x-2 justify-center mt-6" style={{ display: 'flex' }}>
                                                {
                                                    !isRoleDOL &&
                                                    <Button
                                                        className="bg-dashboard-indigo text-white"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                        onClick={async () => {
                                                            if (!isRoleDOL) {
                                                                setIsLoading(true);
                                                                await generatePdf(requestId).then(async (res) => {
                                                                    if (res.status == 200) {
                                                                        const blobFile = new Blob([res.data], { type: 'image/jpeg' });
                                                                        const url = window.URL.createObjectURL(blobFile);
                                                                        const link = document.createElement('a');
                                                                        link.href = url;
                                                                        link.download = `รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร-${getValues().locationName}.jpeg`;
                                                                        link.target = "_blank";
                                                                        document.body.appendChild(link);
                                                                        link.click();
                                                                        link?.parentNode?.removeChild(link);
                                                                    } else {
                                                                        alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                                                                            okText: "ตกลง",
                                                                            okButtonProps: {
                                                                                className: "bg-dashboard-indigo text-white"
                                                                            }
                                                                        });
                                                                    }
                                                                    setIsLoading(false);
                                                                });
                                                            }
                                                        }}
                                                    >
                                                        แบบฟอร์มทีมติดตั้ง
                                                    </Button>
                                                }
                                                {
                                                    !isRoleDOL &&
                                                    <Button
                                                        className="bg-dashboard-indigo text-white"
                                                        size="large"
                                                        disabled={getValues().siteNetworkId == 1 || getValues().sysStatusId != 4 || isRoleDOL}
                                                        onClick={async () => {
                                                            if (!isRoleDOL) {
                                                                setIsLoading(true);
                                                                await generateOnsitePdf(requestId).then(async (res) => {
                                                                    if (res.status == 200) {
                                                                        const link = document.createElement('a');
                                                                        link.href = res.data.data;
                                                                        link.download = `รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร${getValues().locationName}.pdf`;
                                                                        link.target = "_blank";
                                                                        document.body.appendChild(link);
                                                                        link.click();
                                                                        link?.parentNode?.removeChild(link);
                                                                    } else {
                                                                        alertCtx.error("ผิดพลาด", `${res.data.message}`, {
                                                                            okText: "ตกลง",
                                                                            okButtonProps: {
                                                                                className: "bg-dashboard-indigo text-white"
                                                                            }
                                                                        });
                                                                    }
                                                                    setIsLoading(false);
                                                                });
                                                            }
                                                        }}
                                                    >
                                                        Print รายงานรูปภาพการติดตั้ง
                                                    </Button>
                                                }
                                                {
                                                    !isRoleDOL &&
                                                    <Button
                                                        className="bg-dashboard-indigo text-white"
                                                        size="large"
                                                        disabled={isRoleDOL}
                                                        onClick={() => {
                                                            if (!isRoleDOL) {
                                                                trigger().then(res => {
                                                                    if (res) {
                                                                        onSubmit(getValues());
                                                                    }
                                                                });
                                                            }
                                                        }}
                                                    >
                                                        Update
                                                    </Button>
                                                }
                                                {
                                                    !isRoleDOL &&
                                                    <Link href="/site-schedule">
                                                        <Button
                                                            className="bg-dashboard-indigo text-white"
                                                            size="large"
                                                        >
                                                            Cancel
                                                        </Button>
                                                    </Link>
                                                }
                                            </div>
                                        </>
                                        : <LoadingSection></LoadingSection>
                                }
                            </SectionComponent>
                        </div>
                    </div>
                    <Modal open={previewOpen} title={previewTitle} footer={null} onCancel={handleCancel} width={1200}>
                        <img style={{ width: '100%' }} src={previewImage} />
                    </Modal>

                    <JsonViewerComponent data={watch()}></JsonViewerComponent>
                    <JsonViewerComponent data={errors}></JsonViewerComponent>
                </FormProvider >
            )}
            no={() => <AccessDenied></AccessDenied>}
        />
    );
};

export default SiteInfo;
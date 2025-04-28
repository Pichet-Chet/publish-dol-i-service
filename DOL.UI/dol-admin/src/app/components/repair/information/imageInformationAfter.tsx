import { useState } from "react";
import { useForm, UseFormReturn } from "react-hook-form";
import InputFieldComponent, { InputMode } from "../../containers/inputField";

import { PlusOutlined } from '@ant-design/icons';
import { message, Modal, Upload, Image } from 'antd';
import type { GetProp, UploadFile, UploadProps } from 'antd';
import { watch } from "fs";

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

const dummyRequest = (e: any) => {
    setTimeout(() => {
        e.onSuccess("ok");
    }, 0);
};

export default function ImageAfterComponent({
    methods = useForm<RepairForm>(),
    jobCreateUpdate,
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    jobCreateUpdate: (data?: DataFrom | undefined) => Promise<any>;
    isRoleDOL: boolean;
}) {
    const { getValues, setValue, resetField, watch } = methods;
    const _dataFrom = getValues().dataFrom;

    const [previewOpen, setPreviewOpen] = useState(false);
    const [previewImage, setPreviewImage] = useState('');
    const [previewTitle, setPreviewTitle] = useState('');

    const [fileList1, setFileList1] = useState<UploadFile[]>(_dataFrom?.jobImage1 && _dataFrom.jobImage1 != "" ? [{
        uid: _dataFrom?.jobImage1 || "",
        name: _dataFrom?.jobImage1 || "",
        status: 'done',
        url: _dataFrom?.jobImage1 || "",
    }] : []);
    const [fileList2, setFileList2] = useState<UploadFile[]>(_dataFrom?.jobImage2 && _dataFrom.jobImage2 != "" ? [{
        uid: _dataFrom?.jobImage2 || "",
        name: _dataFrom?.jobImage2 || "",
        status: 'done',
        url: _dataFrom?.jobImage2 || "",
    }] : []);
    const [fileList3, setFileList3] = useState<UploadFile[]>(_dataFrom?.jobImage3 && _dataFrom.jobImage3 != "" ? [{
        uid: _dataFrom?.jobImage3 || "",
        name: _dataFrom?.jobImage3 || "",
        status: 'done',
        url: _dataFrom?.jobImage3 || "",
    }] : []);
    const [fileList4, setFileList4] = useState<UploadFile[]>(_dataFrom?.jobImage4 && _dataFrom.jobImage4 != "" ? [{
        uid: _dataFrom?.jobImage4 || "",
        name: _dataFrom?.jobImage4 || "",
        status: 'done',
        url: _dataFrom?.jobImage4 || "",
    }] : []);

    const handleCancel = () => setPreviewOpen(false);

    const handlePreview = async (file: UploadFile) => {
        if (!file.url && !file.preview) {
            file.preview = await getBase64(file.originFileObj as FileType);
        }

        setPreviewImage(file.url || (file.preview as string));
        setPreviewOpen(true);
        setPreviewTitle(file.name || file.url!.substring(file.url!.lastIndexOf('/') + 1));
    };

    const handleChange1: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            setFileList1(info.fileList);
            if (info.fileList.length > 0) {
                setValue("dataFrom.fileUpload1", info.file.originFileObj);
            } else {
                setValue("dataFrom.fileUpload1", undefined);
            }
            if (info.file.status == "done" || info.file.status == "error") {
                const _dataFrom = getValues().dataFrom;
                jobCreateUpdate(_dataFrom);
            }
        }
    };
    const handleChange2: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            setFileList2(info.fileList);
            if (info.fileList.length > 0) {
                setValue("dataFrom.fileUpload2", info.file.originFileObj);
            } else {
                setValue("dataFrom.fileUpload2", undefined);
            }
            if (info.file.status == "done" || info.file.status == "error") {
                const _dataFrom = getValues().dataFrom;
                jobCreateUpdate(_dataFrom);
            }
        }
    };
    const handleChange3: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            setFileList3(info.fileList);
            if (info.fileList.length > 0) {
                setValue("dataFrom.fileUpload3", info.file.originFileObj);
            } else {
                setValue("dataFrom.fileUpload3", undefined);
            }
            if (info.file.status == "done" || info.file.status == "error") {
                const _dataFrom = getValues().dataFrom;
                jobCreateUpdate(_dataFrom);
            }
        }
    };
    const handleChange4: UploadProps['onChange'] = (info) => {
        if (info.file.status) {
            setFileList4(info.fileList);
            if (info.fileList.length > 0) {
                setValue("dataFrom.fileUpload4", info.file.originFileObj);
            } else {
                setValue("dataFrom.fileUpload4", undefined);
            }
            if (info.file.status == "done" || info.file.status == "error") {
                const _dataFrom = getValues().dataFrom;
                jobCreateUpdate(_dataFrom);
            }
        }
    };

    const uploadButton = (
        <button style={{ border: 0, background: 'none' }} type="button">
            <PlusOutlined />
            <div style={{ marginTop: 8 }}>Upload</div>
        </button>
    );

    return (
        <>
            {
                <div className="grid grid-cols-4 gap-x-5">
                    <Upload
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

                </div>
            }

            <Modal open={previewOpen} title={previewTitle} footer={null} onCancel={handleCancel}>
                <img style={{ width: '100%' }} src={previewImage} />
            </Modal>
        </>
    );
};
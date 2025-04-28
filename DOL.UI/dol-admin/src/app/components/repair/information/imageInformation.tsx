import { useState } from "react";
import { useForm, UseFormReturn } from "react-hook-form";
import InputFieldComponent, { InputMode } from "../../containers/inputField";

import { PlusOutlined } from '@ant-design/icons';
import { message, Modal, Upload, Image } from 'antd';
import type { GetProp, UploadFile, UploadProps } from 'antd';

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

export default function ImageComponent({
    methods = useForm<RepairForm>(),
    isRoleDOL
}: {
    methods?: UseFormReturn<RepairForm>;
    isRoleDOL: boolean;
}) {
    const { getValues, setValue, resetField } = methods;
    const _dataFrom = getValues().dataFrom;
    const img_error = "data:image/png;base64,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";

    const [previewOpen, setPreviewOpen] = useState(false);
    const [previewImage, setPreviewImage] = useState('');
    const [previewTitle, setPreviewTitle] = useState('');

    const [fileList1, setFileList1] = useState<UploadFile[]>([]);
    const [fileList2, setFileList2] = useState<UploadFile[]>([]);
    const [fileList3, setFileList3] = useState<UploadFile[]>([]);
    const [fileList4, setFileList4] = useState<UploadFile[]>([]);

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
                _dataFrom?.id ?
                    <div className="grid grid-cols-4 gap-x-5">
                        <Image
                            className="w-3/12"
                            src={_dataFrom.jobImage1 || "error"}
                            fallback={img_error}
                        />
                        <Image
                            className="w-3/12"
                            src={_dataFrom.jobImage2 || "error"}
                            fallback={img_error}
                        />
                        <Image
                            className="w-3/12"
                            src={_dataFrom.jobImage3 || "error"}
                            fallback={img_error}
                        />
                        <Image
                            className="w-3/12"
                            src={_dataFrom.jobImage4 || "error"}
                            fallback={img_error}
                        />
                    </div>
                    :
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
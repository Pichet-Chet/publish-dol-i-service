"use client";
import React from "react";
import { ButtonProps, Modal, ModalProps } from "antd";

export type AlertProviderProps = {
    info: (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => void;
    success: (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => void;
    error: (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => void;
    warning: (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => void;
    confirm: (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => void;
};

const AlertContext = React.createContext<AlertProviderProps>({
    info: (title, message, modalProps) => { },
    success: (title, message, modalProps) => { },
    error: (title, message, modalProps) => { },
    warning: (title, message, modalProps) => { },
    confirm: (title, message, modalProps) => { },
});

const AlertProvider = (props: {
    children:
    | string
    | number
    | boolean
    | React.ReactElement<any, string | React.JSXElementConstructor<any>>
    | Iterable<React.ReactNode>
    | React.ReactPortal
    | null
    | undefined;
}) => {
    const okButtonProps: ButtonProps = { className: "!bg-success !border-success hover:!bg-success !text-on-success" };
    const cancelButtonProps: ButtonProps = { type: "text", className: "!text-success hover:!text-success" };

    const info = (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => {
        Modal.info({
            title: title,
            content: message,
            okButtonProps: okButtonProps,
            cancelButtonProps: cancelButtonProps,
            ...modalProps,
        });
    };
    const success = (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => {
        Modal.success({
            title: title,
            content: message,
            okButtonProps: okButtonProps,
            cancelButtonProps: cancelButtonProps,
            ...modalProps,
        });
    };
    const error = (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => {
        Modal.error({
            title: title,
            content: message,
            okButtonProps: okButtonProps,
            cancelButtonProps: cancelButtonProps,
            ...modalProps,
        });
    };
    const warning = (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => {
        Modal.warning({
            title: title,
            content: message,
            okButtonProps: okButtonProps,
            cancelButtonProps: cancelButtonProps,
            ...modalProps,
        });
    };
    const confirm = (title: string | React.ReactNode, message: string | React.ReactNode, modalProps: ModalProps) => {
        Modal.confirm({
            title: title,
            content: message,
            okButtonProps: okButtonProps,
            cancelButtonProps: cancelButtonProps,
            ...modalProps,
        });
    };

    return <AlertContext.Provider value={{ info, success, error, warning, confirm }}>{props.children}</AlertContext.Provider>;
};

export { AlertProvider };
export default AlertContext;

"use client"

import React, { useMemo, useState } from 'react';
import { signIn, useSession } from 'next-auth/react'
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { Button, Form, Input, notification } from 'antd';
import AlertContext from "@/app/providers/alertContext";
import { useContext } from "react";
import { useRouter, useSearchParams } from 'next/navigation';

import type { NotificationArgsProps } from 'antd';

type NotificationPlacement = NotificationArgsProps['placement'];
const Context = React.createContext({ name: 'Default' });

const LoginPage = () => {
    const router = useRouter();
    const searchParams = useSearchParams();
    const alertCtx = useContext(AlertContext);
    const [api, contextHolder] = notification.useNotification();
    const { data: session } = useSession();
    const [loading, setLoading] = useState(false);

    const openNotificationSuccess = (placement: NotificationPlacement) => {
        api.success({
            message: `สำเร็จ`,
            description: <Context.Consumer>{({ name }) => `เข้าสู่ระบบสำเร็จ`}</Context.Consumer>,
            placement,
        });
    };
    const openNotificationError = (placement: NotificationPlacement, error: string) => {
        api.error({
            message: `ผิดพลาด`,
            description: <Context.Consumer>{({ name }) => error}</Context.Consumer>,
            placement,
        });
    };
    const contextValue = useMemo(() => ({ name: '' }), []);



    const onFinish = async (username: string, password: string) => {
        setLoading(true);
        const result = await signIn('credentials', {
            username,
            password,
            redirect: false,
        });

        if (result?.error) {
            openNotificationError('topRight', result.error);
        }
        // } else {
        // router.push(searchParams.get('callbackUrl') || '/');
        // }
        setLoading(false);
        // signIn('credentials', {
        //     username: username,
        //     password: password,
        //     redirect: true,
        //     callbackUrl: searchParams.get('callbackUrl') || '/'
        // }).then((result) => {
        //     console.log("signIn > result", result);
        //     // if (result?.ok) {
        //     //     // alertCtx.success("สำเร็จ", `เข้าสู่ระบบสำเร็จ`, {
        //     //     //     okText: "ตกลง",
        //     //     //     okButtonProps: {
        //     //     //         className: "bg-indigo-500 text-white"
        //     //     //     }
        //     //     // });
        //     //     openNotificationSuccess('topRight');
        //     //     router.push('/');
        //     // } else {
        //     //     openNotificationError('topRight');
        //     //     // alertCtx.error("ล้มเหลว", `ชื่อผู้ใช้หรือรหัสผ่าน ไม่ถูกต้อง`, {
        //     //     //     okText: "ตกลง",
        //     //     //     okButtonProps: {
        //     //     //         className: "bg-indigo-500 text-white"
        //     //     //     }
        //     //     // });
        //     // }
        // });
    };

    if (session) {
        router.push(searchParams.get('callbackUrl') || '/');
        return null;
    }

    return (
        <Context.Provider value={contextValue}>
            {contextHolder}
            <div className="flex justify-center items-center h-screen bg-gradient-to-r from-[#667DE9] to-[#764CA3]">
                <div className="bg-white p-8 rounded-lg shadow-md">
                    {/* <div className="flex justify-center mb-6">
                        <img
                            src="/images/logo-login.png"
                            alt="Login Icon"
                            className="w-30 h-30 rounded-full"
                        />
                    </div> */}
                    <Form
                        name="normal_login"
                        initialValues={{ remember: true }}
                        onFinish={(values) => onFinish(values.username, values.password)}
                        className="max-w-sm"
                    >
                        <Form.Item
                            name="username"
                            rules={[{ required: true, message: 'Please input your Username!' }]}
                        >
                            <Input
                                prefix={<UserOutlined className="site-form-item-icon" />}
                                placeholder="Username"
                                value={"ping"}
                                className="py-2 px-4 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-purple-500"
                                disabled={loading}
                            />
                        </Form.Item>
                        <Form.Item
                            name="password"
                            rules={[{ required: true, message: 'Please input your Password!' }]}
                        >
                            <Input.Password
                                prefix={<LockOutlined className="site-form-item-icon" />}
                                placeholder="Password"
                                value={"12345"}
                                className="py-2 px-4 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-purple-500"
                                disabled={loading}
                            />
                        </Form.Item>
                        <Form.Item>
                            <Button
                                type="primary"
                                htmlType="submit"
                                className="w-full bg-purple-500 hover:bg-purple-600 text-white py-2 rounded-md"
                                disabled={loading}
                            >
                                Login
                            </Button>
                        </Form.Item>
                    </Form>
                </div>
            </div>
        </Context.Provider>
    );
};

export default LoginPage;
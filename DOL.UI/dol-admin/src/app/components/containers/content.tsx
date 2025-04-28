"use client";

import { Fragment, useEffect } from "react";
import { ConfigProvider, Layout, Menu } from "antd";
import { MenuInfo } from 'rc-menu/lib/interface';
import Link from "next/link";
import { usePathname, useParams } from "next/navigation";
import { useNavigations } from "@/app/_store/navigation";
import NextProgressBar from "../utils/nextProgressBar";
import { LogoutOutlined } from '@ant-design/icons';
import { signOut } from "next-auth/react";
import { useSession } from 'next-auth/react'
import _ from 'lodash';

const { Header, Content, Sider } = Layout;

const AppContent = ({ children }: { children: React.ReactNode }) => {
    const keyActive = useNavigations(state => state.keyActive);
    const setKeyActive = useNavigations(state => state.setKeyActive);
    const getPageName = useNavigations(state => state.getPageName);
    const pathname = usePathname() || "";
    const params = useParams();
    const handleClick = (e: MenuInfo) => {
        setKeyActive(e.key);
    };
    const { data: session, status } = useSession();
    const pathNames = pathname.split('/').filter(path => path);
    useEffect(() => {


        if (params?.id) {
            getPageName(pathname).then((pageName) => {
                document.title = `${pageName} ${typeof params.id === "string" ? params.id : params.id[0]}`;
            });
        } else {
            getPageName(pathname).then((pageName) => {
                document.title = pageName;
            });
        }

        if (pathname == "/" || pathname == "/dashboard") {
            setKeyActive("1");
        } else if (pathname == "/user-mgmt") {
            setKeyActive("2");
        } else if (pathname == "/onsite-overview") {
            setKeyActive("3");
        } else if (pathname == "/site-schedule" || pathname.includes("/siteinfo/")) {
            setKeyActive("4");
        } else if (pathname == "/repair-list" || pathname.includes("/repair/")) {
            setKeyActive("5");
            // } else if (pathname == "/internet-list") {
            //     setKeyActive("6");
        } else if (pathname == "/report") {
            setKeyActive("7");
        }
    }, [pathname]);

    return (
        <Fragment>
            <ConfigProvider
                theme={{
                    token: {
                        fontFamily: "Noto Sans Thai"
                    }
                }}>
                <Layout>
                    {
                        status == "loading" || pathname.includes("/api/auth/") || !session ?
                            <>
                                <Layout style={{ minHeight: '100vh' }}>
                                    <NextProgressBar
                                        height="3px"
                                        color="linear-gradient(to right, #22577a 0%, #35a3a5 100%)"
                                        options={{ showSpinner: false }}
                                        shallowRouting
                                        appDirectory
                                    />
                                    {children}
                                </Layout>
                            </>
                            :
                            <>
                                <Header style={{
                                    background: "#ffff",
                                    display: 'flex',
                                    alignItems: 'center',
                                    // boxShadow: "5px 8px 24px 5px rgba(208, 216, 243, 0.6)"
                                }} >
                                    <div style={{ textAlign: 'start', width: '50%' }}>
                                        <Link href="/">
                                            <img
                                                alt="Logo"
                                                className="w-28"
                                                src="/images/logo.png"
                                            />
                                        </Link>
                                    </div>
                                    <div style={{ textAlign: 'end', width: '50%' }}>
                                        <button
                                            onClick={() => { signOut() }}
                                            type="button"
                                            className="rounded-full hover:text-teal-400"
                                        >
                                            <div className="flex">
                                                <span><LogoutOutlined className="mr-2" />Log Out</span>
                                            </div>
                                        </button>
                                    </div>
                                </Header>
                                <Layout>
                                    <Sider width={250} style={{ background: "#ffff" }}>
                                        <Menu
                                            defaultSelectedKeys={["1"]}
                                            onClick={(e) => handleClick(e)}
                                            selectedKeys={[keyActive]}
                                            selectable={true}
                                            mode="inline"
                                        >
                                            <Menu.Item key="1">
                                                <Link href="/dashboard">
                                                    <span className="nav-text">Dashboard</span>
                                                </Link>
                                            </Menu.Item>
                                            {
                                                ["Admin"].includes(_.get(session?.user, ['userGroup'])) &&
                                                <Menu.Item key="2">
                                                    <Link href="/user-mgmt">
                                                        <span className="nav-text">User Mgmt</span>
                                                    </Link>
                                                </Menu.Item>
                                            }
                                            {
                                                ["Admin", "Staff"].includes(_.get(session?.user, ['userGroup'])) &&
                                                <Menu.Item key="3">
                                                    <Link href="/onsite-overview">
                                                        <span className="nav-text">Onsite Overview</span>
                                                    </Link>
                                                </Menu.Item>
                                            }
                                            {
                                                ["Admin", "Staff", "Helpdesk", "DOL"].includes(_.get(session?.user, ['userGroup'])) &&
                                                <Menu.Item key="4">
                                                    <Link href="/site-schedule">
                                                        <span className="nav-text">Site Schedule</span>
                                                    </Link>
                                                </Menu.Item>
                                            }
                                            {
                                                ["Admin", "Staff", "Helpdesk", "DOL"].includes(_.get(session?.user, ['userGroup'])) &&
                                                <Menu.Item key="5">
                                                    <Link href="/repair-list">
                                                        <span className="nav-text">รายการแจ้งซ่อม</span>
                                                    </Link>
                                                </Menu.Item>
                                            }
                                            {/* {
                                                ["Admin"].includes(_.get(session?.user, ['userGroup'])) &&
                                                <Menu.Item key="6">
                                                    <Link href="/internet-list">
                                                        <span className="nav-text">ข้อมูลการให้บริการและใช้งานอินเตอร์เน็ต</span>
                                                    </Link>
                                                </Menu.Item>
                                            } */}
                                            {
                                                ["Admin", "Staff", "Helpdesk"].includes(_.get(session?.user, ['userGroup'])) &&
                                                <Menu.Item key="7">
                                                    <Link href="/report">
                                                        <span className="nav-text">รายงานการแจ้งซ่อม</span>
                                                    </Link>
                                                </Menu.Item>
                                            }
                                        </Menu>
                                    </Sider>
                                    <Layout style={{ minHeight: 'calc(100vh - 64px' }}>
                                        <Content
                                            style={{
                                                padding: 24,
                                                margin: 0,
                                                minHeight: 280,
                                            }}
                                        >
                                            <NextProgressBar
                                                height="3px"
                                                color="linear-gradient(to right, #22577a 0%, #35a3a5 100%)"
                                                options={{ showSpinner: false }}
                                                shallowRouting
                                                appDirectory
                                            />
                                            {children}
                                        </Content>
                                    </Layout>
                                </Layout>
                            </>
                    }
                </Layout>
            </ConfigProvider>
        </Fragment>
    );
};

export default AppContent;

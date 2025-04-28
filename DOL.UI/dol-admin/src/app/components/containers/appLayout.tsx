"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
// import SplashScreen from "../loading/SplashScreen";

const AppLayout = ({ children }: { children: React.ReactNode }) => {
    const pathname = usePathname();
    const isHome = pathname == "/";
    const [loading, setLoading] = useState(isHome);
    if (pathname == "/") {
    }
    useEffect(() => {
        if (loading) return;
    }, [loading]);

    return (
        <div>
            {/* {loading ? <></> : children} */}
            {children}
        </div>
    );
};
export default AppLayout;

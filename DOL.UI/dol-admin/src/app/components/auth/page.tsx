"use client";

import { signOut, useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import { usePathname, useParams } from "next/navigation";

interface Props {
    children?: React.ReactNode;
}

export default function AuthClient({ children }: Props) {
    const { data: session, status } = useSession();
    const [isMount, setMount] = useState(false);
    const params = useParams();
    const pathname = usePathname() || "";

    useEffect(() => {
        if (session?.error === "RefreshAccessTokenError") {
            signOut();
        }
        setMount(true);
    }, [session]);

    return isMount && (status === "authenticated" || pathname.includes("/api/auth/")) && children;
}
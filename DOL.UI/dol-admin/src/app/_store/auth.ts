import { create } from "zustand";
import { Session } from "next-auth";
import { signOut as nextAuthSignOut } from "next-auth/react";
// import { API_IDENTITY_URL } from "../_lib/axios";

type AuthStore = {
    session: Session;
    setSession: (session: Session) => void;
    // getSignOutUrl: () => string;
};
export const useAuthStore = create<AuthStore>()((set, get) => ({
    session: {} as Session,
    setSession: async (session: Session) => {
        set({ session: session });
    },
    // getSignOutUrl: (): string => {
    //     const { user } = get().session;
    //     nextAuthSignOut({
    //         redirect: false,
    //     }).then();
    //     {
    //         const endSessionParams = new URLSearchParams({
    //             id_token_hint: user.id_token,
    //             post_logout_redirect_uri: window.location.origin,
    //         }).toString();

    //         return `${API_IDENTITY_URL}/connect/endsession?${endSessionParams}`;
    //     }
    // },
}));

import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";
import { AbstractIntlMessages } from "next-intl";

interface LocaleState {
    messages: AbstractIntlMessages | undefined;
    locale: string;
    timezone: string;
    getUserLocal: () => Promise<string>;
    saveUserLocal: (e: any) => Promise<string>;
}

export const useLocaleStore = create<LocaleState>((set, get) => ({
    messages: undefined,
    locale: "th",
    timezone: "Asia/Bangkok",
    getUserLocal: async () => {
        const locale = "th";
        const timezone = "Asia/Bangkok";
        set({ locale: locale, timezone: timezone });
        return locale;
    },
    saveUserLocal: async (e) => {
        const locale = "th";
        const timezone = "Asia/Bangkok";
        set({ locale: locale, timezone: timezone });
        return locale;
    },
}));

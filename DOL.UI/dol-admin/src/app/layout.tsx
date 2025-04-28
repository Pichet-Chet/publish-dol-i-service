// "use client";

import "./styles/style.scss";
import { Noto_Sans_Thai } from "next/font/google";
import Head from 'next/head';
// import { SessionProvider } from "next-auth/react";
// import { NextThemeProvider } from "./providers/close_themeProvider";
import NextAuthSessionProvider from "./providers/sessionProvider";
import { AlertProvider } from "./providers/alertContext";
// import UserDataContextProvider from "./providers/close_userDataContextProvider";

import AuthClient from "./components/auth/page";

import AppContent from "./components/containers/content";
// import AppLayout from "./components/containers/appLayout";

const noto = Noto_Sans_Thai({ subsets: ["thai"] });

export default function RootLayout({ children }: { children: React.ReactNode }) {
  const locale = "th";

  return (
    <NextAuthSessionProvider>
      <Head>
        <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Noto+Sans+Thai:wght@100&display=swap" />
      </Head>
      <html lang={locale} className={`${noto.className}`} suppressHydrationWarning>
        <body>
          {/* <AppLayout> */}
          {/* <NextThemeProvider> */}
          {/* <NextAuthSessionProvider> */}
          <AuthClient>
            {/* <UserDataContextProvider> */}
            <AlertProvider>
              <AppContent>
                {children}
              </AppContent>
            </AlertProvider>
            {/* </UserDataContextProvider> */}
          </AuthClient>
          {/* </NextAuthSessionProvider> */}
          {/* </NextThemeProvider> */}
          {/* </AppLayout> */}
        </body>
      </html>
    </NextAuthSessionProvider>
  );
}

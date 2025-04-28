import NextAuth from "next-auth";

declare module "next-auth" {
    /**
     * Returned by `useSession`, `getSession` and received as a prop on the `SessionProvider` React Context
     */
    interface Session {
        user: {
            id_token: string;
            access_token: string;
            refresh_token: string;
            name: string;
            email: string;
            image: string;
            employeeId: string;
            userName: string;
            active: boolean;
        };

        error?: "RefreshAccessTokenError";
    }
}

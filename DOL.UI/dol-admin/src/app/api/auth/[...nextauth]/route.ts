import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
import axios, { API_APP_URL } from "@/app/_lib/axios";

const handler = NextAuth({
  // secret: process.env.NEXTAUTH_SECRET,
  providers: [
    CredentialsProvider({
      name: "Credentials",
      credentials: {
        username: { label: "Username", type: "text" },
        password: { label: "Password", type: "password" },
      },
      async authorize(credentials) {
        const response = await axios.post(`${API_APP_URL}/api/Authentication`, {
          userName: credentials?.username,
          password: credentials?.password,
        });
        if (response.data.status) {
          return response.data.data;
        } else {
          throw new Error(response.data.message);
        }
        // await axios
        //   .get(
        //     `${API_APP_URL}/api/Authentication?UserName=${credentials?.username}&Password=${credentials?.password}`
        //   )
        //   .then((res) => {
        //     if (res.data.status) {
        //       return res.data.data.json();
        //     } else {
        //       throw new Error(res.data.message);
        //     }
        //   })
        //   .catch((errorData: any) => {
        //     throw new Error(errorData.message);
        //   });

        // const response = await fetch(
        //   `${process.env.NEXT_PUBLIC_APP_HOST}/api/Authentication?UserName=${credentials?.username}&Password=${credentials?.password}`,
        //   {
        //     method: "GET",
        //     headers: {
        //       "Content-Type": "application/json",
        //     },
        //   }
        // );
        // if (response.ok) {
        //   const data_response = await response.json();
        //   if (data_response.status) {
        //     return data_response.data;
        //   } else {
        //     throw new Error(data_response.message);
        //   }
        // } else {
        //   const errorData = await response.json();
        //   throw new Error(errorData.message);
        // }
      },
    }),
    //...add more providers here
  ],
  callbacks: {
    async session({ session, token, user }) {
      session.user = token as any;
      session.error = token.error as any;
      return session;
    },
    async jwt({ token, user, account, session }) {
      return {
        ...token,
        ...user,
      };
    },
  },
  pages: {
    signIn: "/signin",
  },
});
export { handler as GET, handler as POST };

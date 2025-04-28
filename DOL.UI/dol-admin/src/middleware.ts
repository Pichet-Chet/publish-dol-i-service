export const config = {
  matcher: [
    "/((?!api|_next/static|_next/image|favicon.ico|api/auth/signout|images|.well-known/acme-challenge).*)",
  ],
};

import { withAuth } from "next-auth/middleware";
import { NextResponse } from "next/server";

// หน้าที่ต้องการตรวจสอบ session
// const protectedRoutes = ["/", "/dashboard"];

export default withAuth(
  function middleware(req) {
    const session = req.nextauth;

    // ถ้าไม่มี session และไม่ใช่หน้า login ให้ redirect ไปหน้า login
    if (!session && !req.nextUrl.pathname.startsWith("/login")) {
      // const loginUrl = new URL("/login", req.url);
      // loginUrl.searchParams.set("callbackUrl", req.url);
      return NextResponse.redirect(new URL("/login", req.url));
    }

    // ถ้ามี session และเป็นหน้า login ให้ redirect ไปหน้าแรก
    if (session && req.nextUrl.pathname.startsWith("/login")) {
      return NextResponse.redirect(new URL("/", req.url));
    }

    // console.log("session", session.token);

    // // ตรวจสอบสิทธิ์การเข้าถึงหน้า /user-mgmt
    // if (session && req.nextUrl.pathname.startsWith("/user-mgmt")) {
    //   const { role } = session.user;
    //   if (role !== "admin") {
    //     return NextResponse.redirect(new URL("/404", req.url));
    //   }
    // }

    // ถ้าไม่ตรงเงื่อนไขข้างต้น ให้ไปหน้าปัจจุบันตามปกติ
    return NextResponse.next();

    // console.log("middleware => req", req.url);

    // const isProtectedRoute = protectedRoutes.some((route) =>
    //   req.nextUrl.pathname.startsWith(route)
    // );

    // console.log("middleware => isProtectedRoute", isProtectedRoute);
    // const session = req.nextauth.token;

    // // ถ้ายังไม่ได้ login
    // if (!session) {
    //   const url = req.nextUrl.clone();
    //   url.pathname = "/login";
    //   url.search = `?callbackUrl=${req.nextUrl.pathname}`;
    //   // console.log("middleware => url", url);
    //   return NextResponse.redirect(url);
    // }

    // ถ้ามี session อยู่แล้ว ให้ redirect ไปหน้าหลัก
    // if (!session) {
    //   return NextResponse.redirect(new URL("/", req.url));
    // }

    // ถ้าไม่มี session ให้ไปหน้า /login ตามปกติ
    // return NextResponse.next();
  },
  {
    callbacks: {
      authorized({ req, token }) {
        return !!token;
      },
    },
  }
);

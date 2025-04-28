"use client";

// import NextNProgress from "next-nprogress-bar";
import { AppProgressBar as ProgressBar } from 'next-nprogress-bar';

export default function NextProgressBar(props: any) {
    return <ProgressBar {...props} />;
}

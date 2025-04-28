"use client"

import React from 'react';
import { FormProvider, useForm } from "react-hook-form";
import { useEffect } from "react";
import { useSession } from "next-auth/react";
import _ from 'lodash';

import LoadingSection from "../../components/loading/loadingSection";

import Can from '../../components/rule/Can';
import AccessDenied from '../../components/utils/403';
import NotFound from '../../components/utils/404';

import JsonViewerComponent from "@/app/components/fields/jsonViewerComponent";

const InternetReports = () => {
    const { data: session } = useSession();

    return (
        <Can
            rules={["Admin"]}
            perform={_.get(session?.user, ['userGroup'])}
            yes={() => (
                <NotFound></NotFound>
            )}
            no={() => <AccessDenied></AccessDenied>}
        />
    );
};

export default InternetReports;
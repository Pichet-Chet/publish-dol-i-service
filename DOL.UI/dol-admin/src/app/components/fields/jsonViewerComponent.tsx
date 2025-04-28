"use client";

import { FC } from "react";
import {
    JsonView,
    darkStyles,
    defaultStyles,
    Props,
} from "react-json-view-lite";
import "react-json-view-lite/dist/index.css";

type IFormInputProps = {
    data?: any;
} & Props;

const JsonViewerComponent: FC<IFormInputProps> = ({ data, ...otherProps }) => {
    const env = process.env.NODE_ENV;

    return (
        env == "development" && (
            <JsonView
                data={data}
                // shouldInitiallyExpand={(level) => true}
                style={defaultStyles}
                {...otherProps}
            />
        )
    );
};

export default JsonViewerComponent;

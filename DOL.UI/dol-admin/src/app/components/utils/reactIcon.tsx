"use client";

import { cloneElement, useEffect, useState } from "react";
import { useIconStore } from "@/app/_store/icon";
import { DynamicIconProps } from "@/app/_types/icon";

const ReactIcon = (props: DynamicIconProps): JSX.Element => {
    const iconStore = useIconStore((state) => state);
    const [LoadedIcon, setLoadedIcon] = useState<JSX.Element>(
        iconStore.iconLoading(props)
    );
    useEffect(() => {
        setLoadedIcon(iconStore.getIcon(props));
    }, []);
    return LoadedIcon && cloneElement(LoadedIcon, props);
};

export default ReactIcon;

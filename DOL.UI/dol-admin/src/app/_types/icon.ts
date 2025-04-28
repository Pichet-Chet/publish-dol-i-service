import { IconBaseProps } from "react-icons/lib";

export interface CacheIcon {
    [key: string]: any;
}
export interface DynamicIconProps extends IconBaseProps {
    lib?: string;
    name: string;
}

"use client";
import { HelperText, Label, Tooltip } from "flowbite-react";
import { FC } from "react";
import { Controller, ControllerRenderProps, useFormContext } from "react-hook-form";
import _ from "lodash";
import ReactIcon from "../utils/reactIcon";

type IFormInputProps = {
    name: string;
    validationName?: string;
    label: string | undefined;
    renderControl: (field: ControllerRenderProps) => React.ReactNode;
    mode?: InputMode;
    layoutClassName?: string | undefined;
    layoutErrorClassName?: string | undefined;
    labelAlignClassName?: string | undefined;
    labelClassName?: string | undefined;
    secondaryLabelClassName?: string | undefined;
    inputClassName?: string | undefined;
    require?: boolean;
    tooltip?: string;
};

export enum InputMode {
    inline,
    secondary,
    nostyle,
}

const InputFieldComponent: FC<IFormInputProps> = ({
    renderControl,
    mode = InputMode.inline,
    name,
    validationName,
    label,
    labelAlignClassName = "md:text-right",
    layoutClassName = "md:flex md:items-baseline",
    layoutErrorClassName = "md:flex md:items-baseline mb-3",
    labelClassName = "md:w-1/3",
    inputClassName = "md:w-2/3",
    secondaryLabelClassName = "",
    require = false,
    tooltip,
}) => {
    const {
        control,
        formState: { errors },
    } = useFormContext();
    const _validationName = validationName ? validationName : name;
    const _errors = _.get(errors, _validationName, undefined);
    return (
        <Controller
            control={control}
            name={name}
            defaultValue=""
            render={({ field }) =>
                mode === InputMode.inline ? (
                    <>
                        <div className={layoutClassName}>
                            {label && (
                                <div className={labelClassName}>
                                    <div className="flex flex-row">
                                        <Label htmlFor={name} className={`text-sm font-normal block mb-1 md:mb-0 ${labelAlignClassName}`}>
                                            {`${label} `}
                                            {require && <span className="text-red-500">*</span>}
                                        </Label>
                                        {tooltip && (
                                            <Tooltip content={tooltip}>
                                                <ReactIcon
                                                    name="HiOutlineExclamationCircle"
                                                    lib="hi2"
                                                    className="h-5 w-5 text-accent heroicon-sw-2 cursor-pointer pl-1"
                                                ></ReactIcon>
                                            </Tooltip>
                                        )}
                                    </div>
                                </div>
                            )}
                            <div className={inputClassName}>{renderControl(field)}</div>
                        </div>
                        <div className={`${layoutErrorClassName}`}>
                            {label && <div className={labelClassName}></div>}
                            <div className={inputClassName}>
                                {_errors && (
                                    <HelperText className="block" color={!!_errors ? "failure" : ""}>
                                        {_errors ? _errors?.message?.toString() : ""}
                                    </HelperText>
                                )}
                            </div>
                        </div>
                    </>
                ) : mode === InputMode.secondary ? (
                    <div className="mb-3">
                        <div className="flex flex-row">
                            <Label htmlFor={name} value={label} className={`block text-sm font-normal mb-1 ${secondaryLabelClassName}`} />
                            {require && <span className="pl-1 text-red-500">*</span>}
                        </div>
                        {renderControl(field)}
                        {_errors && (
                            <HelperText className="block" color={!!_errors ? "failure" : ""}>
                                {_errors ? _errors?.message?.toString() : ""}
                            </HelperText>
                        )}
                    </div>
                ) : <>{renderControl(field)}</>
            }
        />
    );
};

export default InputFieldComponent;

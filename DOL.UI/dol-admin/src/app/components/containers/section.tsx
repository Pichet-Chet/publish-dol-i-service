import { Suspense } from "react";
import { useTranslations } from "next-intl";
import LoadingSection from "../loading/loadingSection";
import ReactIcon from "../utils/reactIcon";
import { Card, CardProps } from "antd";
// import TextIconComponent from "../fields/TextIconComponent";

export default function SectionComponent({
    title,
    children,
    iconName,
    iconlib,
    isCustomTitle = false,
    bodyClass = "mt-4",
    titleStyle = undefined,
}: {
    title: string | React.ReactNode;
    children: React.ReactNode;
    iconName?: string | undefined;
    iconlib?: string | undefined;
    isCustomTitle?: boolean;
    bodyClass?: string;
    titleStyle?: CardProps;
}) {
    return (
        <Card
            // type="inner"
            styles={titleStyle && titleStyle.styles}
            // headStyle={{ backgroundColor: 'rgba(255, 255, 255, 0.4)', border: 0 }}
            title={
                isCustomTitle ? (
                    title
                ) : (
                    <div className="flex flex-col-2 md:flex-row items-start md:items-center justify-between">
                        <div className="flex flex-row font-semibold text-secondary gap-x-3">
                            {iconName && iconName != "" ? (
                                <div className="self-center">
                                    <ReactIcon
                                        name={iconName}
                                        lib={iconlib}
                                        className="w-6 h-6"
                                    />
                                </div>
                            ) : (
                                <></>
                            )}
                            <div>
                                <h2 className="text-lg font-normal text-on-base">{title}</h2>
                            </div>
                        </div>
                    </div>
                )
            }
            bordered={false}
            className="mb-3"
        >
            <div className={bodyClass}>
                <Suspense fallback={<LoadingSection></LoadingSection>}>
                    {children}
                </Suspense>
            </div>
        </Card>
    );
}

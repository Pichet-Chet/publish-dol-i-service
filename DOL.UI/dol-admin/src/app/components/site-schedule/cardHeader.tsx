import { useForm, UseFormReturn } from "react-hook-form";
import { Card } from 'antd';

export default function CardHeaderComponent({
    methods = useForm<SiteScheduleListForm>()
}: {
    methods?: UseFormReturn<SiteScheduleListForm>;
}) {
    const { getValues } = methods;
    const _data = getValues()?.dataList || [];
    var _siteScheduleList: SiteScheduleList = {};
    if (_data.length > 0) {
        _siteScheduleList = _data[0] || {};
    }

    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-3 gap-x-5">
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-dashboard-red">
                        <label className="text-gray-400">Pending</label>
                        <p className="text-4xl font-semibold">
                            {_siteScheduleList.sumJobOnSite?.jobPending || 0}
                            <span className="text-2xl font-semibold">{`(${_siteScheduleList.sumJobOnSite?.jobPendingPercent || 0}%)`}</span>
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-yellow-400">
                        <label className="text-gray-400">On process</label>
                        <p className="text-4xl font-semibold">
                            {_siteScheduleList.sumJobOnSite?.jobOnProcess || 0}
                            <span className="text-2xl font-semibold">{`(${_siteScheduleList.sumJobOnSite?.jobOnProcessPercent || 0}%)`}</span>
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-green-500">
                        <label className="text-gray-400">Complete</label>
                        <p className="text-4xl font-semibold">
                            {_siteScheduleList.sumJobOnSite?.jobComplete || 0}
                            <span className="text-2xl font-semibold">{`(${_siteScheduleList.sumJobOnSite?.jobCompletePercent || 0}%)`}</span>
                        </p>
                    </Card>
                </div>
            </div>
        </>
    );
};
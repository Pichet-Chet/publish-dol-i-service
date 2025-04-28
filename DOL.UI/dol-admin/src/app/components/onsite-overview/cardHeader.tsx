import { useForm, UseFormReturn } from "react-hook-form";
import { Card } from 'antd';

export default function CardHeaderComponent({
    methods = useForm<OnsiteOverviewForm>()
}: {
    methods?: UseFormReturn<OnsiteOverviewForm>;
}) {
    const { getValues } = methods;
    const _data = getValues()?.dataList || [];
    var _onsiteOverviewList: OnsiteOverviewList = {};
    if (_data.length > 0) {
        _onsiteOverviewList = _data[0] || {};
    }

    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-3 gap-x-5">
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-dashboard-red">
                        <label className="text-gray-400">Pending</label>
                        <p className="text-4xl font-semibold">
                            {_onsiteOverviewList.sumCard?.cardPendingsCount || 0}
                            <span className="text-2xl font-semibold">{`(${_onsiteOverviewList.sumCard?.cardPendingsPercent || 0}%)`}</span>
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-yellow-400">
                        <label className="text-gray-400">On process</label>
                        <p className="text-4xl font-semibold">
                            {_onsiteOverviewList.sumCard?.cardOnprocessCount || 0}
                            <span className="text-2xl font-semibold">{`(${_onsiteOverviewList.sumCard?.cardOnprocessPercent || 0}%)`}</span>
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-green-500">
                        <label className="text-gray-400">Complete</label>
                        <p className="text-4xl font-semibold">
                            {_onsiteOverviewList.sumCard?.cardSuccessesCount || 0}
                            <span className="text-2xl font-semibold">{`(${_onsiteOverviewList.sumCard?.cardSuccessesPercent || 0}%)`}</span>
                        </p>
                    </Card>
                </div>
            </div>
        </>
    );
};
import { useForm, UseFormReturn } from "react-hook-form";
import { Card } from 'antd';

export default function CardHeaderComponent({
    methods = useForm<DashboardAllForm>()
}: {
    methods?: UseFormReturn<DashboardAllForm>;
}) {
    const { getValues } = methods;
    const _dashboardAccept = getValues().dashboardAccept?.dataList || [];
    const _dashboardOnProcess = getValues().dashboardOnProcess?.dataList || [];
    const _dashboardOnSuccess = getValues().dashboardOnSuccess?.dataList || [];
    var _trnSumJobRepair: TrnSumJobRepair = {};
    if (_dashboardAccept.length > 0) {
        _trnSumJobRepair = _dashboardAccept[0].sumJobRepair || {};
    } else if (_dashboardOnProcess.length > 0) {
        _trnSumJobRepair = _dashboardOnProcess[0].sumJobRepair || {};
    } else if (_dashboardOnSuccess.length > 0) {
        _trnSumJobRepair = _dashboardOnSuccess[0].sumJobRepair || {};
    }

    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-3 gap-x-5">
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-dashboard-red">
                        <label className="text-gray-400">จำนวนรับแจ้งแล้ว - รอดำเนินการ วันนี้</label>
                        <p className="text-4xl font-semibold">
                            {_trnSumJobRepair.acceptJobToday || 0}
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-yellow-400">
                        <label className="text-gray-400">จำนวนระหว่างรอดำเนินการ วันนี้</label>
                        <p className="text-4xl font-semibold">
                            {_trnSumJobRepair.onProcessJobToday || 0}
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-green-500">
                        <label className="text-gray-400">จำนวนดำเนินการเสร็จแล้ว วันนี้</label>
                        <p className="text-4xl font-semibold">
                            {_trnSumJobRepair.succesJobToday || 0}
                        </p>
                    </Card>
                </div>
            </div>
        </>
    );
};
import { useForm, UseFormReturn } from "react-hook-form";
import { Card } from 'antd';

export default function CardHeaderComponent({
    methods = useForm<RepairListForm>()
}: {
    methods?: UseFormReturn<RepairListForm>;
}) {
    const { getValues } = methods;
    const _dataList = getValues().dataList || [];
    var _trnSumJobRepair: TrnSumJobRepair = {};
    if (_dataList.length > 0) {
        _trnSumJobRepair = _dataList[0].sumJobRepair || {};
    }

    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-4 gap-x-2">
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-dashboard-red">
                        <label className="text-gray-400">รับแจ้งแล้ว รอดำเนินการ</label>
                        <p className="flex items-baseline text-4xl font-semibold gap-2">
                            {_trnSumJobRepair.acceptJobAll || 0}
                            {/* <span className="text-2xl font-semibold">{`(${_trnSumJobRepair.acceptJobPercent || 0}%)`}</span> */}
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-dashboard-yellow">
                        <label className="text-gray-400">ระหว่างรอดำเนินการ</label>
                        <p className="flex items-baseline text-4xl font-semibold gap-2">
                            {_trnSumJobRepair.onProcessJobAll || 0}
                            {/* <span className="text-2xl font-semibold">{`(${_trnSumJobRepair.onpProcessJobPercent || 0}%)`}</span> */}
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-dashboard-green">
                        <label className="text-gray-400">ดำเนินการเสร็จแล้ว</label>
                        <p className="flex items-baseline text-4xl font-semibold gap-2">
                            {_trnSumJobRepair.succesJobAll || 0}
                            {/* <span className="text-2xl font-semibold">{`(${_trnSumJobRepair.succesJobPercent || 0}%)`}</span> */}
                        </p>
                    </Card>
                </div>
                <div className="grid grid-cols-1">
                    <Card className="border-4 border-b-dashboard-indigo">
                        <label className="text-gray-400">เกินระยะเวลา SLA ที่ยังแก้ไขไม่เสร็จ</label>
                        <p className="flex items-baseline text-4xl font-semibold gap-2">
                            {_trnSumJobRepair.outOfSlaAll || 0}
                            {/* <span className="text-2xl font-semibold">{`(${_trnSumJobRepair.outOfSlaPercent || 0}%)`}</span> */}
                        </p>
                    </Card>
                </div>
            </div>
        </>
    );
};
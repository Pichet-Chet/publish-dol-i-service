using System;
using System.Collections.Generic;

namespace DOL.API.Models;

public partial class SiteInformation
{
    public int Id { get; set; }

    public int? SiteNetworkId { get; set; }

    public string? SiteNetworkName { get; set; }

    public int? SiteNetworkSeq { get; set; }

    public string? ProvinceName { get; set; }

    public string? LocationName { get; set; }

    public string? Address { get; set; }

    public string? StaffOrganize { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? InstallWan1Team { get; set; }

    public string? InstallWan1Telephone { get; set; }

    public string? InstallWan2Team { get; set; }

    public string? InstallWan2Telephone { get; set; }

    public string? InstallInternetTeam { get; set; }

    public string? InstallInternetTelephone { get; set; }

    public string? InstallCellularTeam { get; set; }

    public string? InstallCellularTelephone { get; set; }

    public string? InstallDeviceTeam { get; set; }

    public string? InstallDeviceTelephone { get; set; }

    public string? Wan1Provider { get; set; }

    public string? Wan1Cid { get; set; }

    public string? Wan1Speed { get; set; }

    public string? Wan1AsNumber { get; set; }

    public string? Wan1IpWan1Pe { get; set; }

    public string? Wan1IpWan1Ce { get; set; }

    public string? Wan1Subnet { get; set; }

    public string? Wan2Provider { get; set; }

    public string? Wan2Cid { get; set; }

    public string? Wan2Speed { get; set; }

    public string? Wan2AsNumber { get; set; }

    public string? Wan2IpWan1Pe { get; set; }

    public string? Wan2IpWan1Ce { get; set; }

    public string? Wan2Subnet { get; set; }

    public string? InternetCid { get; set; }

    public string? InternetSpeed { get; set; }

    public string? InternetAsNumber { get; set; }

    public string? InternetWanIpAddress { get; set; }

    public string? InternetSubnet { get; set; }

    public string? CellularSim { get; set; }

    public string? CellularAr109 { get; set; }

    public string? IpLanGateway { get; set; }

    public string? IpLanSubnet { get; set; }

    public string? EquipmentCpeSwitchMain { get; set; }

    public string? EquipmentSnCpeSwitchMain { get; set; }

    public string? EquipmentCpeSwitchSecondary { get; set; }

    public string? EquipmentSnCpeSwitchSecondary { get; set; }

    public string? EquipmentFirewall2Set { get; set; }

    public string? EquipmentFirewall1Sn { get; set; }

    public string? EquipmentFirewall2Sn { get; set; }

    public string? EquipmentRouter2Set { get; set; }

    public string? EquipmentRouter1Sn { get; set; }

    public string? EquipmentRouter2Sn { get; set; }

    public string? EquipmentWifi1Set { get; set; }

    public string? EquipmentWifiSn { get; set; }

    public string? EquipmentRouter4gSet { get; set; }

    public string? EquipmentRouter4gSn { get; set; }

    /// <summary>
    /// รูปหน้าสำนักงาน
    /// </summary>
    public string? Image1 { get; set; }

    /// <summary>
    /// Rack ที่สำนักงาน
    /// </summary>
    public string? Image2 { get; set; }

    /// <summary>
    /// CPE switch วงจรหลัก
    /// </summary>
    public string? Image3 { get; set; }

    /// <summary>
    /// CPE switch วงจรรอง
    /// </summary>
    public string? Image4 { get; set; }

    /// <summary>
    /// Firewall1
    /// </summary>
    public string? Image5 { get; set; }

    /// <summary>
    /// Firewall2
    /// </summary>
    public string? Image6 { get; set; }

    /// <summary>
    /// Router1
    /// </summary>
    public string? Image7 { get; set; }

    /// <summary>
    /// Router2
    /// </summary>
    public string? Image8 { get; set; }

    /// <summary>
    /// Wifi 1 ชุด
    /// </summary>
    public string? Image9 { get; set; }

    /// <summary>
    /// Router 4G 1 ชุด
    /// </summary>
    public string? Image10 { get; set; }

    /// <summary>
    /// AR109
    /// </summary>
    public string? Image11 { get; set; }

    /// <summary>
    /// Ping Test 1
    /// </summary>
    public string? Image12 { get; set; }

    /// <summary>
    /// Ping Test 2
    /// </summary>
    public string? Image13 { get; set; }

    /// <summary>
    /// Ping Test 3
    /// </summary>
    public string? Image14 { get; set; }

    /// <summary>
    /// Ping Test 4
    /// </summary>
    public string? Image15 { get; set; }

    /// <summary>
    /// เข้าเว็บไซต์อินเตอร์เน็ตกรมที่ดิน
    /// </summary>
    public string? Image16 { get; set; }

    /// <summary>
    /// เข้า FTP กรมที่ดิน
    /// </summary>
    public string? Image17 { get; set; }

    /// <summary>
    /// เข้าระบบผู้รับมอบอำนาจ
    /// </summary>
    public string? Image18 { get; set; }

    /// <summary>
    /// เข้าระบบ MIS
    /// </summary>
    public string? Image19 { get; set; }

    /// <summary>
    /// เข้าระบบควบคุมการจัดเก็บหลักฐาน
    /// </summary>
    public string? Image20 { get; set; }

    /// <summary>
    /// เข้าโปรแกรมบริการกรมที่ดิน
    /// </summary>
    public string? Image21 { get; set; }

    /// <summary>
    /// ระบบพิสูจน์ตัวตนในการใช้งานเครือข่าย
    /// </summary>
    public string? Image22 { get; set; }

    /// <summary>
    /// ผลการทดสอบความสัญญาณเครือข่าย WAN 1 วงจรหลัก
    /// </summary>
    public string? Image23 { get; set; }

    /// <summary>
    /// ผลการทดสอบความสัญญาณเครือข่าย WAN 2 วงจรรอง
    /// </summary>
    public string? Image24 { get; set; }

    /// <summary>
    /// ไม่ได้ใช้
    /// </summary>
    public string? Image25 { get; set; }

    /// <summary>
    /// ไม่ได้ใช้
    /// </summary>
    public string? Image26 { get; set; }

    /// <summary>
    /// ไม่ได้ใช้
    /// </summary>
    public string? Image27 { get; set; }

    public string? Image28 { get; set; }

    public DateTime CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public bool IsActive { get; set; }

    public double? MainMaxHour { get; set; }

    public double? MainMonthlyServiceFee { get; set; }

    public double? MainFineRateHourPercent { get; set; }

    public double? MainFineRateHourAmount { get; set; }

    public double? MainFineRateMinimum { get; set; }

    public double? SecondaryMaxHour { get; set; }

    public double? SecondaryMonthlyServiceFee { get; set; }

    public double? SecondaryFineRateHourPercent { get; set; }

    public double? SecondaryFineRateHourAmount { get; set; }

    public double? SecondaryFineRateMinimum { get; set; }

    public double? InternetMaxHour { get; set; }

    public double? InternetMonthlyServiceFee { get; set; }

    public double? InternetFineRateHourPercent { get; set; }

    public double? InternetFineRateHourAmount { get; set; }

    public double? InternetFineRateMinimum { get; set; }

    public double? CorpnetMaxHour { get; set; }

    public double? CorpnetMonthlyServiceFee { get; set; }

    public double? CorpnetFineRateHourPercent { get; set; }

    public double? CorpnetFineRateHourAmount { get; set; }

    public double? CorpnetFineRateMinimum { get; set; }

    public double? CellularMaxHour { get; set; }

    public double? CellularMonthlyServiceFee { get; set; }

    public double? CellularFineRateHourPercent { get; set; }

    public double? CellularFineRateHourAmount { get; set; }

    public double? CellularFineRateMinimum { get; set; }

    public double? SumFineRatePercent { get; set; }

    public double? SumPrincipalAmount { get; set; }

    public double? SumDate { get; set; }

    public double? SumAmount { get; set; }

    public string? FileAccept1 { get; set; }

    public string? FileAccept1By { get; set; }

    public DateTime? FileAccept1Date { get; set; }

    public string? FileAccept2 { get; set; }

    public string? FileAccept2By { get; set; }

    public DateTime? FileAccept2Date { get; set; }

    public string? FileAccept3 { get; set; }

    public string? FileAccept3By { get; set; }

    public DateTime? FileAccept3Date { get; set; }

    public int? SysStatusId { get; set; }

    public string? TeamInstallContactName { get; set; }

    public string? TeamInstallContactTel { get; set; }

    public string? FileApproveName { get; set; }

    public string? FileApproveSize { get; set; }

    public string? FileApproveUnitSize { get; set; }

    public int? SysUserId { get; set; }

    public string? Wan1SpeedTestDownload { get; set; }

    public string? Wan1SpeedTestUpload { get; set; }

    public string? Wan2SpeedTestDownload { get; set; }

    public string? Wan2SpeedTestUpload { get; set; }

    public string? Guid { get; set; }

    /// <summary>
    /// ทดสอบ การใช้งาน Wifi : Network Connection
    /// </summary>
    public string? Image29 { get; set; }

    /// <summary>
    /// ทดสอบ Authentication
    /// </summary>
    public string? Image30 { get; set; }

    /// <summary>
    /// ภาพ ping www.dol.go.th -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรหลัก)
    /// </summary>
    public string? Image31 { get; set; }

    /// <summary>
    /// ภาพ tracert www.dol.go.th (วงจรหลัก)
    /// </summary>
    public string? Image32 { get; set; }

    /// <summary>
    /// ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรหลัก)
    /// </summary>
    public string? Image33 { get; set; }

    /// <summary>
    /// ภาพ ping ilands.dol.go.th -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรหลัก)
    /// </summary>
    public string? Image34 { get; set; }

    /// <summary>
    /// ภาพ tracert ilands.dol.go.th (วงจรหลัก)
    /// </summary>
    public string? Image35 { get; set; }

    /// <summary>
    /// ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรหลัก)
    /// </summary>
    public string? Image36 { get; set; }

    /// <summary>
    /// ภาพ ping 10.200.30.247 -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรหลัก)
    /// </summary>
    public string? Image37 { get; set; }

    /// <summary>
    /// ภาพ tracert 10.200.30.247 (วงจรหลัก)
    /// </summary>
    public string? Image38 { get; set; }

    /// <summary>
    /// ภาพ ping 8.8.8.8 -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรหลัก)
    /// </summary>
    public string? Image39 { get; set; }

    /// <summary>
    /// ภาพ tracert 8.8.8.8 (วงจรหลัก)
    /// </summary>
    public string? Image40 { get; set; }

    /// <summary>
    /// ภาพ ping www.dol.go.th -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรรอง)
    /// </summary>
    public string? Image41 { get; set; }

    /// <summary>
    /// ภาพ tracert www.dol.go.th (วงจรรอง)
    /// </summary>
    public string? Image42 { get; set; }

    /// <summary>
    /// ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรรอง)
    /// </summary>
    public string? Image43 { get; set; }

    /// <summary>
    /// ภาพ ping ilands.dol.go.th -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรรอง)
    /// </summary>
    public string? Image44 { get; set; }

    /// <summary>
    /// ภาพ tracert ilands.dol.go.th (วงจรรอง)
    /// </summary>
    public string? Image45 { get; set; }

    /// <summary>
    /// ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรรอง)
    /// </summary>
    public string? Image46 { get; set; }

    /// <summary>
    /// ภาพ ping 10.200.30.247 -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรรอง)
    /// </summary>
    public string? Image47 { get; set; }

    /// <summary>
    /// ภาพ tracert 10.200.30.247 (วงจรรอง)
    /// </summary>
    public string? Image48 { get; set; }

    /// <summary>
    /// ภาพ ping 8.8.8.8 -n 30 (response time &lt;100 ms, ต้องไม่มี Time Out) (วงจรรอง)
    /// </summary>
    public string? Image49 { get; set; }

    /// <summary>
    /// ภาพ tracert 8.8.8.8 (วงจรรอง)
    /// </summary>
    public string? Image50 { get; set; }

    /// <summary>
    /// ภาพการทดสอบรับ IP Address
    /// </summary>
    public string? Image51 { get; set; }

    /// <summary>
    /// วงจร Internet (100Mbps) โดยปลดสาย  WAN1 วงจรหลัก และ WAN2 วงจรรอง
    /// </summary>
    public string? Image52 { get; set; }

    /// <summary>
    /// วงจร 4G (20Mbps) โดยปลดสาย  WAN1 วงจรหลัก, WAN2 วงจรรอง และ วงจร Internet
    /// </summary>
    public string? Image53 { get; set; }

    public string? CircuitInternet100mbDownload { get; set; }

    public string? CircuitInternet100mbUpload { get; set; }

    public string? Circuit4g20mbDownload { get; set; }

    public string? Circuit4g20mbUpload { get; set; }

    /// <summary>
    /// ภาพสถานะวงจรที่จะทดสอบ Nagios
    /// </summary>
    public string? Image54 { get; set; }

    /// <summary>
    /// ภาพเริ่มทดสอบวงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร
    /// </summary>
    public string? Image55 { get; set; }

    /// <summary>
    /// ภาพ ping 8.8.8.8 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)
    /// </summary>
    public string? Image56 { get; set; }

    /// <summary>
    /// ภาพ tracert 8.8.8.8 (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)
    /// </summary>
    public string? Image57 { get; set; }

    /// <summary>
    /// ภาพเริ่มทดสอบวงจรหลัก.jpg
    /// </summary>
    public string? Image58 { get; set; }

    /// <summary>
    /// วงจรหลัก Tunnel Destination DC1.jpg
    /// </summary>
    public string? Image59 { get; set; }

    /// <summary>
    /// วงจรหลัก Tunnel Destination DC2
    /// </summary>
    public string? Image60 { get; set; }

    /// <summary>
    /// ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)
    /// </summary>
    public string? Image61 { get; set; }

    /// <summary>
    /// ภาพ tracert 10.100.30.247 (วงจรหลัก)
    /// </summary>
    public string? Image62 { get; set; }

    /// <summary>
    /// ภาพเริ่มทดสอบวงจรสำรอง
    /// </summary>
    public string? Image63 { get; set; }

    /// <summary>
    /// วงจรสำรอง Tunnel Destination DC1
    /// </summary>
    public string? Image64 { get; set; }

    /// <summary>
    /// วงจรสำรอง Tunnel Destination DC2
    /// </summary>
    public string? Image65 { get; set; }

    /// <summary>
    /// ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสำรอง)
    /// </summary>
    public string? Image66 { get; set; }

    /// <summary>
    /// ภาพ tracert 10.100.30.247 (วงจรสำรอง)
    /// </summary>
    public string? Image67 { get; set; }
}

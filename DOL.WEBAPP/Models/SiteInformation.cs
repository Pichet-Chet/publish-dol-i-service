using System;
using System.Collections.Generic;

namespace DOL.WEBAPP.Models;

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

    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }

    public string? Image5 { get; set; }

    public string? Image6 { get; set; }

    public string? Image7 { get; set; }

    public string? Image8 { get; set; }

    public string? Image9 { get; set; }

    public string? Image10 { get; set; }

    public string? Image11 { get; set; }

    public string? Image12 { get; set; }

    public string? Image13 { get; set; }

    public string? Image14 { get; set; }

    public string? Image15 { get; set; }

    public string? Image16 { get; set; }

    public string? Image17 { get; set; }

    public string? Image18 { get; set; }

    public string? Image19 { get; set; }

    public string? Image20 { get; set; }

    public string? Image21 { get; set; }

    public string? Image22 { get; set; }

    public string? Image23 { get; set; }

    public string? Image24 { get; set; }

    public string? Image25 { get; set; }

    public string? Image26 { get; set; }

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

    public string? Image29 { get; set; }

    public string? Image30 { get; set; }

    public string? Image31 { get; set; }

    public string? Image32 { get; set; }

    public string? Image33 { get; set; }

    public string? Image34 { get; set; }

    public string? Image35 { get; set; }

    public string? Image36 { get; set; }

    public string? Image37 { get; set; }

    public string? Image38 { get; set; }

    public string? Image39 { get; set; }

    public string? Image40 { get; set; }

    public string? Image41 { get; set; }

    public string? Image42 { get; set; }

    public string? Image43 { get; set; }

    public string? Image44 { get; set; }

    public string? Image45 { get; set; }

    public string? Image46 { get; set; }

    public string? Image47 { get; set; }

    public string? Image48 { get; set; }

    public string? Image49 { get; set; }

    public string? Image50 { get; set; }

    public string? Image51 { get; set; }

    public string? Image52 { get; set; }

    public string? Image53 { get; set; }

    public string? CircuitInternet100mbDownload { get; set; }

    public string? CircuitInternet100mbUpload { get; set; }

    public string? Circuit4g20mbDownload { get; set; }

    public string? Circuit4g20mbUpload { get; set; }
}

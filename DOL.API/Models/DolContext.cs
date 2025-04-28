using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DOL.API.Models;

public partial class DolContext : DbContext
{
    public DolContext()
    {
    }

    public DolContext(DbContextOptions<DolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CaseOfFixed> CaseOfFixeds { get; set; }

    public virtual DbSet<CaseOfIssue> CaseOfIssues { get; set; }

    public virtual DbSet<CaseOfIssueSub> CaseOfIssueSubs { get; set; }

    public virtual DbSet<JobOnsite> JobOnsites { get; set; }

    public virtual DbSet<JobRepair> JobRepairs { get; set; }

    public virtual DbSet<SiteInformation> SiteInformations { get; set; }

    public virtual DbSet<SiteLocation> SiteLocations { get; set; }

    public virtual DbSet<SiteNetwork> SiteNetworks { get; set; }

    public virtual DbSet<SysStatus> SysStatuses { get; set; }

    public virtual DbSet<SysUser> SysUsers { get; set; }

    public virtual DbSet<WatchdogLog> WatchdogLogs { get; set; }

    public virtual DbSet<WatchdogWatchexceptionlog> WatchdogWatchexceptionlogs { get; set; }

    public virtual DbSet<WatchdogWatchlog> WatchdogWatchlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        var connectionString = configuration.GetConnectionString("ConnectionStr");

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<CaseOfFixed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("case_of_fixed_pkey");

            entity.ToTable("case_of_fixed");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<CaseOfIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("case_of_issue_pkey");

            entity.ToTable("case_of_issue");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<CaseOfIssueSub>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("case_of_issue_sub_pkey");

            entity.ToTable("case_of_issue_sub");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseFix)
                .HasMaxLength(100)
                .HasColumnName("case_fix");
            entity.Property(e => e.CaseOfIssueId).HasColumnName("case_of_issue_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<JobOnsite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("job_onsite_pk");

            entity.ToTable("job_onsite");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcceptBy)
                .HasMaxLength(100)
                .HasColumnName("accept_by");
            entity.Property(e => e.AcceptDate).HasColumnName("accept_date");
            entity.Property(e => e.AcceptPosition)
                .HasMaxLength(300)
                .HasColumnName("accept_position");
            entity.Property(e => e.AcceptSign).HasColumnName("accept_sign");
            entity.Property(e => e.AssignDate).HasColumnName("assign_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(50)
                .HasColumnName("document_no");
            entity.Property(e => e.SiteInformationId).HasColumnName("site_information_id");
            entity.Property(e => e.SysStatusId).HasColumnName("sys_status_id");
            entity.Property(e => e.SysUserId).HasColumnName("sys_user_id");
            entity.Property(e => e.TeamInstallComment)
                .HasMaxLength(500)
                .HasColumnName("team_install_comment");
            entity.Property(e => e.TeamInstallContactName)
                .HasMaxLength(300)
                .HasColumnName("team_install_contact_name");
            entity.Property(e => e.TeamInstallContactTel)
                .HasMaxLength(30)
                .HasColumnName("team_install_contact_tel");
            entity.Property(e => e.TeamInstallDate).HasColumnName("team_install_date");
            entity.Property(e => e.TypeOnsiteValue)
                .HasMaxLength(50)
                .HasColumnName("type_onsite_value");
        });

        modelBuilder.Entity<JobRepair>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("job_repair_pk");

            entity.ToTable("job_repair");

            entity.HasIndex(e => e.DocumentNo, "job_repair_document_no_idx");

            entity.HasIndex(e => e.JobCreatedDate, "job_repair_job_created_date_idx");

            entity.HasIndex(e => e.SiteInformationId, "job_repair_site_information_id_idx");

            entity.HasIndex(e => e.SiteNetworkId, "job_repair_site_network_id_idx");

            entity.HasIndex(e => e.SysStatusId, "job_repair_sys_status_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseOfFixId).HasColumnName("case_of_fix_id");
            entity.Property(e => e.CaseOfIssueId).HasColumnName("case_of_issue_id");
            entity.Property(e => e.CaseOfIssueSubId).HasColumnName("case_of_issue_sub_id");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(36)
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentRequest)
                .HasMaxLength(20)
                .HasColumnName("document_request");
            entity.Property(e => e.JobAcceptBy)
                .HasMaxLength(100)
                .HasColumnName("job_accept_by");
            entity.Property(e => e.JobAcceptDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("job_accept_date");
            entity.Property(e => e.JobCompleteBy)
                .HasMaxLength(100)
                .HasColumnName("job_complete_by");
            entity.Property(e => e.JobCompleteDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("job_complete_date");
            entity.Property(e => e.JobContactName)
                .HasMaxLength(100)
                .HasColumnName("job_contact_name");
            entity.Property(e => e.JobContactTel)
                .HasMaxLength(100)
                .HasColumnName("job_contact_tel");
            entity.Property(e => e.JobCreatedBy)
                .HasMaxLength(100)
                .HasColumnName("job_created_by");
            entity.Property(e => e.JobCreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("job_created_date");
            entity.Property(e => e.JobDescription)
                .HasMaxLength(500)
                .HasColumnName("job_description");
            entity.Property(e => e.JobFixedComment)
                .HasMaxLength(500)
                .HasColumnName("job_fixed_comment");
            entity.Property(e => e.JobFixedContactName)
                .HasMaxLength(100)
                .HasColumnName("job_fixed_contact_name");
            entity.Property(e => e.JobFixedContactTel)
                .HasMaxLength(100)
                .HasColumnName("job_fixed_contact_tel");
            entity.Property(e => e.JobFixedDescription)
                .HasMaxLength(500)
                .HasColumnName("job_fixed_description");
            entity.Property(e => e.JobImage1).HasColumnName("job_image1");
            entity.Property(e => e.JobImage2).HasColumnName("job_image2");
            entity.Property(e => e.JobImage3).HasColumnName("job_image3");
            entity.Property(e => e.JobImage4).HasColumnName("job_image4");
            entity.Property(e => e.JobProcessBy)
                .HasMaxLength(100)
                .HasColumnName("job_process_by");
            entity.Property(e => e.JobProcessDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("job_process_date");
            entity.Property(e => e.JobSenderContactName)
                .HasMaxLength(100)
                .HasColumnName("job_sender_contact_name");
            entity.Property(e => e.JobSenderContactTel)
                .HasMaxLength(100)
                .HasColumnName("job_sender_contact_tel");
            entity.Property(e => e.JobSenderRemark)
                .HasMaxLength(500)
                .HasColumnName("job_sender_remark");
            entity.Property(e => e.SiteInformationId).HasColumnName("site_information_id");
            entity.Property(e => e.SiteNetworkId).HasColumnName("site_network_id");
            entity.Property(e => e.SysStatusId).HasColumnName("sys_status_id");
            entity.Property(e => e.TypeRepairData)
                .HasMaxLength(100)
                .HasColumnName("type_repair_data");
            entity.Property(e => e.TypeRepairValue)
                .HasMaxLength(100)
                .HasColumnName("type_repair_value");
        });

        modelBuilder.Entity<SiteInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("site_information_pk");

            entity.ToTable("site_information");

            entity.HasIndex(e => e.LocationName, "site_information_location_name_idx");

            entity.HasIndex(e => e.ProvinceName, "site_information_province_name_idx");

            entity.HasIndex(e => e.SiteNetworkId, "site_information_site_network_id_idx");

            entity.HasIndex(e => e.SysUserId, "site_information_sys_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.CellularAr109)
                .HasMaxLength(100)
                .HasColumnName("cellular_ar109");
            entity.Property(e => e.CellularFineRateHourAmount).HasColumnName("cellular_fine_rate_hour_amount");
            entity.Property(e => e.CellularFineRateHourPercent).HasColumnName("cellular_fine_rate_hour_percent");
            entity.Property(e => e.CellularFineRateMinimum).HasColumnName("cellular_fine_rate_minimum");
            entity.Property(e => e.CellularMaxHour).HasColumnName("cellular_max_hour");
            entity.Property(e => e.CellularMonthlyServiceFee).HasColumnName("cellular_monthly_service_fee");
            entity.Property(e => e.CellularSim)
                .HasMaxLength(100)
                .HasColumnName("cellular_sim");
            entity.Property(e => e.Circuit4g20mbDownload)
                .HasColumnType("character varying")
                .HasColumnName("circuit_4g_20mb_download");
            entity.Property(e => e.Circuit4g20mbUpload)
                .HasColumnType("character varying")
                .HasColumnName("circuit_4g_20mb_upload");
            entity.Property(e => e.CircuitInternet100mbDownload)
                .HasColumnType("character varying")
                .HasColumnName("circuit_internet_100mb_download");
            entity.Property(e => e.CircuitInternet100mbUpload)
                .HasColumnType("character varying")
                .HasColumnName("circuit_internet_100mb_upload");
            entity.Property(e => e.CorpnetFineRateHourAmount).HasColumnName("corpnet_fine_rate_hour_amount");
            entity.Property(e => e.CorpnetFineRateHourPercent).HasColumnName("corpnet_fine_rate_hour_percent");
            entity.Property(e => e.CorpnetFineRateMinimum).HasColumnName("corpnet_fine_rate_minimum");
            entity.Property(e => e.CorpnetMaxHour).HasColumnName("corpnet_max_hour");
            entity.Property(e => e.CorpnetMonthlyServiceFee).HasColumnName("corpnet_monthly_service_fee");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.EquipmentCpeSwitchMain)
                .HasMaxLength(100)
                .HasColumnName("equipment_cpe_switch_main");
            entity.Property(e => e.EquipmentCpeSwitchSecondary)
                .HasMaxLength(100)
                .HasColumnName("equipment_cpe_switch_secondary");
            entity.Property(e => e.EquipmentFirewall1Sn)
                .HasMaxLength(100)
                .HasColumnName("equipment_firewall1_sn");
            entity.Property(e => e.EquipmentFirewall2Set)
                .HasMaxLength(100)
                .HasColumnName("equipment_firewall2_set");
            entity.Property(e => e.EquipmentFirewall2Sn)
                .HasMaxLength(100)
                .HasColumnName("equipment_firewall2_sn");
            entity.Property(e => e.EquipmentRouter1Sn)
                .HasMaxLength(100)
                .HasColumnName("equipment_router1_sn");
            entity.Property(e => e.EquipmentRouter2Set)
                .HasMaxLength(100)
                .HasColumnName("equipment_router2_set");
            entity.Property(e => e.EquipmentRouter2Sn)
                .HasMaxLength(100)
                .HasColumnName("equipment_router2_sn");
            entity.Property(e => e.EquipmentRouter4gSet)
                .HasMaxLength(100)
                .HasColumnName("equipment_router4g_set");
            entity.Property(e => e.EquipmentRouter4gSn)
                .HasMaxLength(100)
                .HasColumnName("equipment_router4g_sn");
            entity.Property(e => e.EquipmentSnCpeSwitchMain)
                .HasMaxLength(100)
                .HasColumnName("equipment_sn_cpe_switch_main");
            entity.Property(e => e.EquipmentSnCpeSwitchSecondary)
                .HasMaxLength(100)
                .HasColumnName("equipment_sn_cpe_switch_secondary");
            entity.Property(e => e.EquipmentWifi1Set)
                .HasMaxLength(100)
                .HasColumnName("equipment_wifi1_set");
            entity.Property(e => e.EquipmentWifiSn)
                .HasMaxLength(100)
                .HasColumnName("equipment_wifi_sn");
            entity.Property(e => e.FileAccept1).HasColumnName("file_accept1");
            entity.Property(e => e.FileAccept1By)
                .HasMaxLength(50)
                .HasColumnName("file_accept1_by");
            entity.Property(e => e.FileAccept1Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("file_accept1_date");
            entity.Property(e => e.FileAccept2).HasColumnName("file_accept2");
            entity.Property(e => e.FileAccept2By)
                .HasMaxLength(50)
                .HasColumnName("file_accept2_by");
            entity.Property(e => e.FileAccept2Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("file_accept2_date");
            entity.Property(e => e.FileAccept3).HasColumnName("file_accept3");
            entity.Property(e => e.FileAccept3By)
                .HasMaxLength(50)
                .HasColumnName("file_accept3_by");
            entity.Property(e => e.FileAccept3Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("file_accept3_date");
            entity.Property(e => e.FileApproveName).HasColumnName("file_approve_name");
            entity.Property(e => e.FileApproveSize)
                .HasMaxLength(50)
                .HasColumnName("file_approve_size");
            entity.Property(e => e.FileApproveUnitSize)
                .HasMaxLength(10)
                .HasColumnName("file_approve_unit_size");
            entity.Property(e => e.Guid)
                .HasColumnType("character varying")
                .HasColumnName("guid");
            entity.Property(e => e.Image1)
                .HasComment("รูปหน้าสำนักงาน")
                .HasColumnName("image1");
            entity.Property(e => e.Image10)
                .HasComment("Router 4G 1 ชุด")
                .HasColumnName("image10");
            entity.Property(e => e.Image11)
                .HasComment("AR109")
                .HasColumnName("image11");
            entity.Property(e => e.Image12)
                .HasComment("Ping Test 1")
                .HasColumnName("image12");
            entity.Property(e => e.Image13)
                .HasComment("Ping Test 2")
                .HasColumnName("image13");
            entity.Property(e => e.Image14)
                .HasComment("Ping Test 3")
                .HasColumnName("image14");
            entity.Property(e => e.Image15)
                .HasComment("Ping Test 4")
                .HasColumnName("image15");
            entity.Property(e => e.Image16)
                .HasComment("เข้าเว็บไซต์อินเตอร์เน็ตกรมที่ดิน")
                .HasColumnName("image16");
            entity.Property(e => e.Image17)
                .HasComment("เข้า FTP กรมที่ดิน")
                .HasColumnName("image17");
            entity.Property(e => e.Image18)
                .HasComment("เข้าระบบผู้รับมอบอำนาจ")
                .HasColumnName("image18");
            entity.Property(e => e.Image19)
                .HasComment("เข้าระบบ MIS")
                .HasColumnName("image19");
            entity.Property(e => e.Image2)
                .HasComment("Rack ที่สำนักงาน")
                .HasColumnName("image2");
            entity.Property(e => e.Image20)
                .HasComment("เข้าระบบควบคุมการจัดเก็บหลักฐาน")
                .HasColumnName("image20");
            entity.Property(e => e.Image21)
                .HasComment("เข้าโปรแกรมบริการกรมที่ดิน")
                .HasColumnName("image21");
            entity.Property(e => e.Image22)
                .HasComment("ระบบพิสูจน์ตัวตนในการใช้งานเครือข่าย")
                .HasColumnName("image22");
            entity.Property(e => e.Image23)
                .HasComment("ผลการทดสอบความสัญญาณเครือข่าย WAN 1 วงจรหลัก")
                .HasColumnName("image23");
            entity.Property(e => e.Image24)
                .HasComment("ผลการทดสอบความสัญญาณเครือข่าย WAN 2 วงจรรอง")
                .HasColumnName("image24");
            entity.Property(e => e.Image25)
                .HasComment("ไม่ได้ใช้")
                .HasColumnName("image25");
            entity.Property(e => e.Image26)
                .HasComment("ไม่ได้ใช้")
                .HasColumnName("image26");
            entity.Property(e => e.Image27)
                .HasComment("ไม่ได้ใช้")
                .HasColumnName("image27");
            entity.Property(e => e.Image28).HasColumnName("image28");
            entity.Property(e => e.Image29)
                .HasComment("ทดสอบ การใช้งาน Wifi : Network Connection")
                .HasColumnName("image29");
            entity.Property(e => e.Image3)
                .HasComment("CPE switch วงจรหลัก")
                .HasColumnName("image3");
            entity.Property(e => e.Image30)
                .HasComment("ทดสอบ Authentication")
                .HasColumnName("image30");
            entity.Property(e => e.Image31)
                .HasComment("ภาพ ping www.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)")
                .HasColumnName("image31");
            entity.Property(e => e.Image32)
                .HasComment("ภาพ tracert www.dol.go.th (วงจรหลัก)")
                .HasColumnName("image32");
            entity.Property(e => e.Image33)
                .HasComment("ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรหลัก)")
                .HasColumnName("image33");
            entity.Property(e => e.Image34)
                .HasComment("ภาพ ping ilands.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)")
                .HasColumnName("image34");
            entity.Property(e => e.Image35)
                .HasComment("ภาพ tracert ilands.dol.go.th (วงจรหลัก)")
                .HasColumnName("image35");
            entity.Property(e => e.Image36)
                .HasComment("ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรหลัก)")
                .HasColumnName("image36");
            entity.Property(e => e.Image37)
                .HasComment("ภาพ ping 10.200.30.247 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)")
                .HasColumnName("image37");
            entity.Property(e => e.Image38)
                .HasComment("ภาพ tracert 10.200.30.247 (วงจรหลัก)")
                .HasColumnName("image38");
            entity.Property(e => e.Image39)
                .HasComment("ภาพ ping 8.8.8.8 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)")
                .HasColumnName("image39");
            entity.Property(e => e.Image4)
                .HasComment("CPE switch วงจรรอง")
                .HasColumnName("image4");
            entity.Property(e => e.Image40)
                .HasComment("ภาพ tracert 8.8.8.8 (วงจรหลัก)")
                .HasColumnName("image40");
            entity.Property(e => e.Image41)
                .HasComment("ภาพ ping www.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)")
                .HasColumnName("image41");
            entity.Property(e => e.Image42)
                .HasComment("ภาพ tracert www.dol.go.th (วงจรรอง)")
                .HasColumnName("image42");
            entity.Property(e => e.Image43)
                .HasComment("ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรรอง)")
                .HasColumnName("image43");
            entity.Property(e => e.Image44)
                .HasComment("ภาพ ping ilands.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)")
                .HasColumnName("image44");
            entity.Property(e => e.Image45)
                .HasComment("ภาพ tracert ilands.dol.go.th (วงจรรอง)")
                .HasColumnName("image45");
            entity.Property(e => e.Image46)
                .HasComment("ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรรอง)")
                .HasColumnName("image46");
            entity.Property(e => e.Image47)
                .HasComment("ภาพ ping 10.200.30.247 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)")
                .HasColumnName("image47");
            entity.Property(e => e.Image48)
                .HasComment("ภาพ tracert 10.200.30.247 (วงจรรอง)")
                .HasColumnName("image48");
            entity.Property(e => e.Image49)
                .HasComment("ภาพ ping 8.8.8.8 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)")
                .HasColumnName("image49");
            entity.Property(e => e.Image5)
                .HasComment("Firewall1")
                .HasColumnName("image5");
            entity.Property(e => e.Image50)
                .HasComment("ภาพ tracert 8.8.8.8 (วงจรรอง)")
                .HasColumnName("image50");
            entity.Property(e => e.Image51)
                .HasComment("ภาพการทดสอบรับ IP Address")
                .HasColumnName("image51");
            entity.Property(e => e.Image52)
                .HasComment("วงจร Internet (100Mbps) โดยปลดสาย  WAN1 วงจรหลัก และ WAN2 วงจรรอง")
                .HasColumnName("image52");
            entity.Property(e => e.Image53)
                .HasComment("วงจร 4G (20Mbps) โดยปลดสาย  WAN1 วงจรหลัก, WAN2 วงจรรอง และ วงจร Internet")
                .HasColumnName("image53");
            entity.Property(e => e.Image54)
                .HasComment("ภาพสถานะวงจรที่จะทดสอบ Nagios")
                .HasColumnName("image54");
            entity.Property(e => e.Image55)
                .HasComment("ภาพเริ่มทดสอบวงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร")
                .HasColumnName("image55");
            entity.Property(e => e.Image56)
                .HasComment("ภาพ ping 8.8.8.8 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)")
                .HasColumnName("image56");
            entity.Property(e => e.Image57)
                .HasComment("ภาพ tracert 8.8.8.8 (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)")
                .HasColumnName("image57");
            entity.Property(e => e.Image58)
                .HasComment("ภาพเริ่มทดสอบวงจรหลัก.jpg")
                .HasColumnName("image58");
            entity.Property(e => e.Image59)
                .HasComment("วงจรหลัก Tunnel Destination DC1.jpg")
                .HasColumnName("image59");
            entity.Property(e => e.Image6)
                .HasComment("Firewall2")
                .HasColumnName("image6");
            entity.Property(e => e.Image60)
                .HasComment("วงจรหลัก Tunnel Destination DC2")
                .HasColumnName("image60");
            entity.Property(e => e.Image61)
                .HasComment("ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)")
                .HasColumnName("image61");
            entity.Property(e => e.Image62)
                .HasComment("ภาพ tracert 10.100.30.247 (วงจรหลัก)")
                .HasColumnName("image62");
            entity.Property(e => e.Image63)
                .HasComment("ภาพเริ่มทดสอบวงจรสำรอง")
                .HasColumnName("image63");
            entity.Property(e => e.Image64)
                .HasComment("วงจรสำรอง Tunnel Destination DC1")
                .HasColumnName("image64");
            entity.Property(e => e.Image65)
                .HasComment("วงจรสำรอง Tunnel Destination DC2")
                .HasColumnName("image65");
            entity.Property(e => e.Image66)
                .HasComment("ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสำรอง)")
                .HasColumnName("image66");
            entity.Property(e => e.Image67)
                .HasComment("ภาพ tracert 10.100.30.247 (วงจรสำรอง)")
                .HasColumnName("image67");
            entity.Property(e => e.Image7)
                .HasComment("Router1")
                .HasColumnName("image7");
            entity.Property(e => e.Image8)
                .HasComment("Router2")
                .HasColumnName("image8");
            entity.Property(e => e.Image9)
                .HasComment("Wifi 1 ชุด")
                .HasColumnName("image9");
            entity.Property(e => e.InstallCellularTeam)
                .HasMaxLength(100)
                .HasColumnName("install_cellular_team");
            entity.Property(e => e.InstallCellularTelephone)
                .HasMaxLength(100)
                .HasColumnName("install_cellular_telephone");
            entity.Property(e => e.InstallDeviceTeam)
                .HasMaxLength(100)
                .HasColumnName("install_device_team");
            entity.Property(e => e.InstallDeviceTelephone)
                .HasMaxLength(100)
                .HasColumnName("install_device_telephone");
            entity.Property(e => e.InstallInternetTeam)
                .HasMaxLength(100)
                .HasColumnName("install_internet_team");
            entity.Property(e => e.InstallInternetTelephone)
                .HasMaxLength(100)
                .HasColumnName("install_internet_telephone");
            entity.Property(e => e.InstallWan1Team)
                .HasMaxLength(100)
                .HasColumnName("install_wan1_team");
            entity.Property(e => e.InstallWan1Telephone)
                .HasMaxLength(100)
                .HasColumnName("install_wan1_telephone");
            entity.Property(e => e.InstallWan2Team)
                .HasMaxLength(100)
                .HasColumnName("install_wan2_team");
            entity.Property(e => e.InstallWan2Telephone)
                .HasMaxLength(100)
                .HasColumnName("install_wan2_telephone");
            entity.Property(e => e.InternetAsNumber)
                .HasMaxLength(100)
                .HasColumnName("internet_as_number");
            entity.Property(e => e.InternetCid)
                .HasMaxLength(100)
                .HasColumnName("internet_cid");
            entity.Property(e => e.InternetFineRateHourAmount).HasColumnName("internet_fine_rate_hour_amount");
            entity.Property(e => e.InternetFineRateHourPercent).HasColumnName("internet_fine_rate_hour_percent");
            entity.Property(e => e.InternetFineRateMinimum).HasColumnName("internet_fine_rate_minimum");
            entity.Property(e => e.InternetMaxHour).HasColumnName("internet_max_hour");
            entity.Property(e => e.InternetMonthlyServiceFee).HasColumnName("internet_monthly_service_fee");
            entity.Property(e => e.InternetSpeed)
                .HasMaxLength(100)
                .HasColumnName("internet_speed");
            entity.Property(e => e.InternetSubnet)
                .HasMaxLength(100)
                .HasColumnName("internet_subnet");
            entity.Property(e => e.InternetWanIpAddress)
                .HasMaxLength(100)
                .HasColumnName("internet_wan_ip_address");
            entity.Property(e => e.IpLanGateway)
                .HasMaxLength(100)
                .HasColumnName("ip_lan_gateway");
            entity.Property(e => e.IpLanSubnet)
                .HasMaxLength(100)
                .HasColumnName("ip_lan_subnet");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Latitude)
                .HasMaxLength(30)
                .HasColumnName("latitude");
            entity.Property(e => e.LocationName)
                .HasMaxLength(300)
                .HasColumnName("location_name");
            entity.Property(e => e.Longitude)
                .HasMaxLength(30)
                .HasColumnName("longitude");
            entity.Property(e => e.MainFineRateHourAmount).HasColumnName("main_fine_rate_hour_amount");
            entity.Property(e => e.MainFineRateHourPercent).HasColumnName("main_fine_rate_hour_percent");
            entity.Property(e => e.MainFineRateMinimum).HasColumnName("main_fine_rate_minimum");
            entity.Property(e => e.MainMaxHour).HasColumnName("main_max_hour");
            entity.Property(e => e.MainMonthlyServiceFee).HasColumnName("main_monthly_service_fee");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(300)
                .HasColumnName("province_name");
            entity.Property(e => e.SecondaryFineRateHourAmount).HasColumnName("secondary_fine_rate_hour_amount");
            entity.Property(e => e.SecondaryFineRateHourPercent).HasColumnName("secondary_fine_rate_hour_percent");
            entity.Property(e => e.SecondaryFineRateMinimum).HasColumnName("secondary_fine_rate_minimum");
            entity.Property(e => e.SecondaryMaxHour).HasColumnName("secondary_max_hour");
            entity.Property(e => e.SecondaryMonthlyServiceFee).HasColumnName("secondary_monthly_service_fee");
            entity.Property(e => e.SiteNetworkId).HasColumnName("site_network_id");
            entity.Property(e => e.SiteNetworkName)
                .HasMaxLength(300)
                .HasColumnName("site_network_name");
            entity.Property(e => e.SiteNetworkSeq).HasColumnName("site_network_seq");
            entity.Property(e => e.StaffOrganize)
                .HasMaxLength(300)
                .HasColumnName("staff_organize");
            entity.Property(e => e.SumAmount).HasColumnName("sum_amount");
            entity.Property(e => e.SumDate).HasColumnName("sum_date");
            entity.Property(e => e.SumFineRatePercent).HasColumnName("sum_fine_rate_percent");
            entity.Property(e => e.SumPrincipalAmount).HasColumnName("sum_principal_amount");
            entity.Property(e => e.SysStatusId)
                .HasDefaultValue(1)
                .HasColumnName("sys_status_id");
            entity.Property(e => e.SysUserId).HasColumnName("sys_user_id");
            entity.Property(e => e.TeamInstallContactName)
                .HasMaxLength(100)
                .HasColumnName("team_install_contact_name");
            entity.Property(e => e.TeamInstallContactTel)
                .HasMaxLength(30)
                .HasColumnName("team_install_contact_tel");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(50)
                .HasColumnName("telephone_number");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.Wan1AsNumber)
                .HasMaxLength(100)
                .HasColumnName("wan1_as_number");
            entity.Property(e => e.Wan1Cid)
                .HasMaxLength(100)
                .HasColumnName("wan1_cid");
            entity.Property(e => e.Wan1IpWan1Ce)
                .HasMaxLength(100)
                .HasColumnName("wan1_ip_wan1_ce");
            entity.Property(e => e.Wan1IpWan1Pe)
                .HasMaxLength(100)
                .HasColumnName("wan1_ip_wan1_pe");
            entity.Property(e => e.Wan1Provider)
                .HasMaxLength(100)
                .HasColumnName("wan1_provider");
            entity.Property(e => e.Wan1Speed)
                .HasMaxLength(100)
                .HasColumnName("wan1_speed");
            entity.Property(e => e.Wan1SpeedTestDownload)
                .HasColumnType("character varying")
                .HasColumnName("wan1_speed_test_download");
            entity.Property(e => e.Wan1SpeedTestUpload)
                .HasColumnType("character varying")
                .HasColumnName("wan1_speed_test_upload");
            entity.Property(e => e.Wan1Subnet)
                .HasMaxLength(100)
                .HasColumnName("wan1_subnet");
            entity.Property(e => e.Wan2AsNumber)
                .HasMaxLength(100)
                .HasColumnName("wan2_as_number");
            entity.Property(e => e.Wan2Cid)
                .HasMaxLength(100)
                .HasColumnName("wan2_cid");
            entity.Property(e => e.Wan2IpWan1Ce)
                .HasMaxLength(100)
                .HasColumnName("wan2_ip_wan1_ce");
            entity.Property(e => e.Wan2IpWan1Pe)
                .HasMaxLength(100)
                .HasColumnName("wan2_ip_wan1_pe");
            entity.Property(e => e.Wan2Provider)
                .HasMaxLength(100)
                .HasColumnName("wan2_provider");
            entity.Property(e => e.Wan2Speed)
                .HasMaxLength(100)
                .HasColumnName("wan2_speed");
            entity.Property(e => e.Wan2SpeedTestDownload)
                .HasColumnType("character varying")
                .HasColumnName("wan2_speed_test_download");
            entity.Property(e => e.Wan2SpeedTestUpload)
                .HasColumnType("character varying")
                .HasColumnName("wan2_speed_test_upload");
            entity.Property(e => e.Wan2Subnet)
                .HasMaxLength(100)
                .HasColumnName("wan2_subnet");
        });

        modelBuilder.Entity<SiteLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("site_location_pkey");

            entity.ToTable("site_location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LocationName)
                .HasMaxLength(300)
                .HasColumnName("location_name");
            entity.Property(e => e.ProviceName)
                .HasMaxLength(300)
                .HasColumnName("provice_name");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SiteNetwork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("site_network_pk");

            entity.ToTable("site_network");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.JobCellular).HasColumnName("job_cellular");
            entity.Property(e => e.JobCorpnet).HasColumnName("job_corpnet");
            entity.Property(e => e.JobDevice).HasColumnName("job_device");
            entity.Property(e => e.JobInternet).HasColumnName("job_internet");
            entity.Property(e => e.JobWan1).HasColumnName("job_wan1");
            entity.Property(e => e.JobWan2).HasColumnName("job_wan2");
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .HasColumnName("name");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("site_information_stauts_pk");

            entity.ToTable("sys_status");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('site_information_stauts_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(300)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(300)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sys_user_pk");

            entity.ToTable("sys_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .HasColumnName("password");
            entity.Property(e => e.TemplateConfig)
                .HasColumnType("character varying")
                .HasColumnName("template_config");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserGroup).HasColumnName("user_group");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<WatchdogLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("watchdog_logs_pkey");

            entity.ToTable("watchdog_logs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Callingfrom)
                .HasColumnType("character varying")
                .HasColumnName("callingfrom");
            entity.Property(e => e.Callingmethod)
                .HasMaxLength(100)
                .HasColumnName("callingmethod");
            entity.Property(e => e.Eventid)
                .HasMaxLength(100)
                .HasColumnName("eventid");
            entity.Property(e => e.Linenumber).HasColumnName("linenumber");
            entity.Property(e => e.Loglevel)
                .HasMaxLength(30)
                .HasColumnName("loglevel");
            entity.Property(e => e.Message)
                .HasColumnType("character varying")
                .HasColumnName("message");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
        });

        modelBuilder.Entity<WatchdogWatchexceptionlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("watchdog_watchexceptionlog_pkey");

            entity.ToTable("watchdog_watchexceptionlog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Encounteredat).HasColumnName("encounteredat");
            entity.Property(e => e.Message)
                .HasColumnType("character varying")
                .HasColumnName("message");
            entity.Property(e => e.Method)
                .HasMaxLength(30)
                .HasColumnName("method");
            entity.Property(e => e.Path)
                .HasColumnType("character varying")
                .HasColumnName("path");
            entity.Property(e => e.Querystring)
                .HasColumnType("character varying")
                .HasColumnName("querystring");
            entity.Property(e => e.Requestbody)
                .HasColumnType("character varying")
                .HasColumnName("requestbody");
            entity.Property(e => e.Source)
                .HasColumnType("character varying")
                .HasColumnName("source");
            entity.Property(e => e.Stacktrace)
                .HasColumnType("character varying")
                .HasColumnName("stacktrace");
            entity.Property(e => e.Typeof)
                .HasColumnType("character varying")
                .HasColumnName("typeof");
        });

        modelBuilder.Entity<WatchdogWatchlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("watchdog_watchlog_pkey");

            entity.ToTable("watchdog_watchlog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Host)
                .HasColumnType("character varying")
                .HasColumnName("host");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(30)
                .HasColumnName("ipaddress");
            entity.Property(e => e.Method)
                .HasMaxLength(30)
                .HasColumnName("method");
            entity.Property(e => e.Path)
                .HasColumnType("character varying")
                .HasColumnName("path");
            entity.Property(e => e.Querystring)
                .HasColumnType("character varying")
                .HasColumnName("querystring");
            entity.Property(e => e.Requestbody)
                .HasColumnType("character varying")
                .HasColumnName("requestbody");
            entity.Property(e => e.Requestheaders)
                .HasColumnType("character varying")
                .HasColumnName("requestheaders");
            entity.Property(e => e.Responsebody)
                .HasColumnType("character varying")
                .HasColumnName("responsebody");
            entity.Property(e => e.Responseheaders)
                .HasColumnType("character varying")
                .HasColumnName("responseheaders");
            entity.Property(e => e.Responsestatus).HasColumnName("responsestatus");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Timespent)
                .HasColumnType("character varying")
                .HasColumnName("timespent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

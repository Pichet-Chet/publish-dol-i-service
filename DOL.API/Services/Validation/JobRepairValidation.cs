using System;
using System.Reflection;
using DOL.API.Models;
using DOL.API.Models.Response;

namespace DOL.API.Services.Validation
{
    public class JobRepairValidation
    {
        #region Not null zone

        public static Response Id(int Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (Input != null)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response SiteNetworkId(int? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (Input != null)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response SiteInformationId(int? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (Input != null)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobDescription(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobContactName(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobContactTel(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobSenderRemark(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobSenderContactName(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobFixedDescription(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobFixedComment(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobFixedContactName(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobFixedContactTel(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobSenderContactTel(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response SysStatusId(int? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (Input != null)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobCreatedBy(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(Input))
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response JobCreatedDate(DateTime? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (Input != null)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        public static Response DocumentRequestId(string? Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (Input != null)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        #endregion

        #region Max Leng Zone

        public static Response DocumentNoLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 36;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response DocumentRequestLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 36;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobDescriptionLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 500;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobContactNameLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobContactTelLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobSenderContactNameLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobSenderContactTelLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobSenderRemarkLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 500;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }


        public static Response JobFixedDescriptionLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 500;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }


        public static Response JobFixedCommentLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 500;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobFixedContactNameLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobFixedContactTelLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan1ProviderLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan1CidLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan1SpeedLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan1AsNumberLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan1IpWan1PeLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan1IpWan1CeLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan1SubnetLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan2ProviderLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan2CidLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan2SpeedLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan2AsNumberLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan2IpWan1PeLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan2IpWan1CeLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response Wan2SubnetLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response InternetCidLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response InternetSpeedLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response InternetAsNumberLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response InternetWanIpAddressLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response InternetSubnetLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response CellularSimLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response CellularAr109Lenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobCreatedByLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobAcceptByLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobProcessByLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }

        public static Response JobCompleteByLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 100;

            if (Lenght <= MaxLenght)
            {
                resp.status = true;
            }
            else
            {
                resp.status = false;
                resp.message = $"กรุณาระบุข้อมูล {textBeforeColon} ไม่ให้เกิน '" + MaxLenght + "' ตัวอักษร";
            }

            return resp;
        }


        #endregion

        #region Master Zone

        public static Response SiteNetworkIdMaster(int? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SiteNetworks.Where(x => x.Id == Input).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response SiteInformationMaster(int? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SiteInformations.Where(x => x.Id == Input).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response SysStatusMaster(int? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SysStatuses.Where(x => x.Id == Input).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response JobCreatedByMaster(string? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SysUsers.Where(x => x.Username.ToLower().Contains(Input)).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response JobAcceptByMaster(string? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SysUsers.Where(x => x.Username.ToLower().Contains(Input)).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response JobProcessByMaster(string? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SysUsers.Where(x => x.Username.ToLower().Contains(Input)).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response JobCompleteByMaster(string? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SysUsers.Where(x => x.Username.ToLower().Contains(Input)).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response CaseOfIssueIdMaster(int? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.CaseOfIssues.Where(x => x.Id == Input).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        public static Response CaseOfFixIdMaster(int? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.CaseOfFixeds.Where(x => x.Id == Input).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"ไม่พบรายการข้อมูล {textBeforeColon} กรุณาตรวจสอบก่อนทำรายการใหม่อีกครั้ง";
            }

            return resp;
        }

        #endregion

    }
}


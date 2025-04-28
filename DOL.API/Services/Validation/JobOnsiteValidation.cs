using System;
using System.Reflection;
using DocumentFormat.OpenXml.Wordprocessing;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Response;

namespace DOL.API.Services.Validation
{
    public class JobOnsiteValidation
    {
        #region Not Null

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

        public static Response SiteInformationId(int Input)
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

        public static Response SysUserId(int Input)
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

        public static Response SysStatusId(int Input)
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

        public static Response AssignDate(DateTime Input)
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

        public static Response TypeOnsiteValue(string Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            if (Input != null)
            {
                resp.status = true;

                switch (Input)
                {
                    case Constants.jobWan1:
                        resp.status = true;
                        break;
                    case Constants.jobWan2:
                        resp.status = true;
                        break;
                    case Constants.jobInternet:
                        resp.status = true;
                        break;
                    case Constants.jobCorpnet:
                        resp.status = true;
                        break;
                    case Constants.jobCellular:
                        resp.status = true;
                        break;
                    case Constants.jobEquipment:
                        resp.status = true;
                        break;
                    default:
                        resp.status = false;
                        resp.message = $"ประเภทงานไม่ถูกต้อง กรุณาตรวจสอบข้อมูลใหม่อีกครั้งขอบคุณค่ะ.";
                        break;
                }
            }
            else
            {
                resp.status = false;
                resp.message = $"The [{methodName}] must not be null or empty.";
            }

            return resp;
        }

        #endregion


        #region Max Lenght

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

        public static Response TypeOnsiteValueLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 50;

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

        public static Response TeamInstallContactNameLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 300;

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

        public static Response TeamInstallContactTelLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 30;

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

        public static Response TeamInstallCommentLenght(int Lenght)
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

        public static Response AcceptByLenght(int Lenght)
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

        public static Response AcceptPositionLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 300;

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


        #region Master

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

        public static Response SysUserMaster(int? Input, DolContext context)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Master");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            if (Input != null)
            {
                var findMasterValue = context.SysUsers.Where(x => x.Id == Input).Any();

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


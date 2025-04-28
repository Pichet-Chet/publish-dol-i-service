using System;
using System.Reflection;
using DOL.API.Models.Response;

namespace DOL.API.Services.Validation
{
	public class SiteNetworkValidation
	{
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

        public static Response Name(string Input)
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

        public static Response JobWan1(bool Input)
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

        public static Response JobWan2(bool Input)
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

        public static Response JobInternet(bool Input)
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

        public static Response JobCorpnet(bool Input)
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

        public static Response JobCellular(bool Input)
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

        public static Response JobDevice(bool Input)
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

        public static Response CreateDate(DateTime Input)
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

        public static Response CreateBy(string Input)
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

        public static Response UpdateDate(DateTime Input)
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

        public static Response UpdateBy(string Input)
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

        public static Response IsActive(bool Input)
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


        #region Max Lenght

        public static Response NameLenght(int Lenght)
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

        public static Response CreateByLenght(int Lenght)
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

        public static Response UpdateByLenght(int Lenght)
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

        #endregion
    }

}


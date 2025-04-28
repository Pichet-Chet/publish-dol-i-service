using System;
using System.Reflection;
using DOL.API.Models.Response;

namespace DOL.API.Services.Validation
{
	public class SiteLocationValidation
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

        public static Response ProviceName(string Input)
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

        public static Response LocationName(string Input)
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



        public static Response ProviceNameLenght(int Lenght)
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

        public static Response LocationNameLenght(int Lenght)
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
    }
}


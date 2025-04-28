using System;
using System.Reflection;
using DOL.API.Models;
using DOL.API.Models.Response;
using static System.Net.Mime.MediaTypeNames;

namespace DOL.API.Services.Validation
{
	public class SysUserValidation
	{
        #region Not null

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

        public static Response Username(string Input)
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

        public static Response Password(string Input)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;
            string lenght = "8";

            int uppercaseCount = CountUppercase(Input);
            int lowercaseCount = CountLowercase(Input);

            if (!string.IsNullOrEmpty(Input))
            {
                if (Input.Length >= 8 && Input.Length <= 32)
                {
                    if (uppercaseCount >= 1)
                    {
                        resp.status = true;
                    }
                    else
                    {
                        resp.status = false;
                        resp.message = $"a minimum of 1 upper case letter [A-Z]";
                    }


                    if (lowercaseCount >= 1)
                    {
                        resp.status = true;
                    }
                    else
                    {
                        resp.status = false;
                        resp.message = $"a minimum of 1 lower case letter [a-z]";
                    }
                }
                else
                {
                    resp.status = false;
                    resp.message = $"Your {methodName} must be a combination of alphanumeric characters of 8-32 characters.";
                }


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

        public static Response UserGroup(string Input)
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
        #endregion


        #region Max Lenght

        public static Response UsernameLenght(int Lenght)
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


        public static Response PasswordLenght(int Lenght)
        {
            Response resp = new Response();

            string methodName = MethodBase.GetCurrentMethod().Name;

            int indexOfColon = methodName.IndexOf("Lenght");

            string textBeforeColon = methodName.Substring(0, indexOfColon);

            int MaxLenght = 32;

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

        public static Response NameLenght(int Lenght)
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

        #region Master


        public static Response UserGroupMaster(string Input, DolContext context)
        {
            Response resp = new Response();

            List<string> roles = new List<string>();

            roles.Add("Admin");
            roles.Add("Staff");
            roles.Add("Onsite Team");
            roles.Add("Helpdesk");

            if (Input != null)
            {
                var findMasterValue = roles.Where(x => x == Input).Any();

                if (findMasterValue)
                {
                    resp.status = true;
                }
                else
                {
                    resp.status = false;
                    resp.message = "ไม่พบรายการข้อมูล UserGroup ภายในระบบ กรุณาตรวจสอบข้อมูลใหม่อีกครั้ง [ Admin , Staff , Onsite Team , Helpdesk ]";
                }
            }
            else
            {
                resp.status = false;
                resp.message = "ไม่พบรายการข้อมูล UserGroup ภายในระบบ กรุณาตรวจสอบข้อมูลใหม่อีกครั้ง [ Admin , Staff , Onsite Team , Helpdesk ]";
            }

            return resp;
        }


        #endregion



        static int CountUppercase(string text)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsUpper(c))
                {
                    count++;
                }
            }
            return count;
        }

        static int CountLowercase(string text)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsLower(c))
                {
                    count++;
                }
            }
            return count;
        }
    }
}


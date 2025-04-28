using System;
using DOL.API.Models.Response;
using System.Globalization;
using System.Reflection;
using System.Text;
using DOL.API.Models.Constants;
using System.Security.Cryptography;
using System.Data;
using ClosedXML.Excel;

namespace DOL.API.Extension.Helper
{
	public class AppHelper
	{
        #region | string Column Name |
        public static string CreateBy = "CreateBy";
        public static string CreateDate = "CreateDate";
        #endregion

        public AppHelper()
		{
		}

        public static DateTime GetDateTimeNow()
        {
            DateTime dt = DateTime.Now;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            return dt;
        }

        public static string GetYearTh()
        {
            string result = "";

            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            DateTime dt = DateTime.Now;

            ThaiBuddhistCalendar thaiCalendar = new ThaiBuddhistCalendar();

            result = Convert.ToString(thaiCalendar.GetYear(dt));

            return result;
        }
        public static string GetYearUs()
        {
            DateTime dt = DateTime.Now;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-Us");

            string result = "";

            result = Convert.ToString(dt.Year);

            return result;
        }

        public static string GetMonth()
        {
            string result = "";

            result = DateTime.Now.ToString("MM");

            return result;
        }
        public static string GetDay()
        {
            string result = "";

            result = DateTime.Now.ToString("dd");

            return result;
        }

        public static double ConvertToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public static string ConvertMonthNumberToMonthThai(string param)
        {
            string result = string.Empty;

            switch (param)
            {
                case "01":
                    result = "เดือนมกราคม";
                    break;
                case "02":
                    result = "เดือนกุมภาพันธ์";
                    break;
                case "03":
                    result = "เดือนมีนาคม";
                    break;
                case "04":
                    result = "เดือนเมษายน";
                    break;
                case "05":
                    result = "เดือนพฤษภาคม";
                    break;
                case "06":
                    result = "เดือนมิถุนายน";
                    break;
                case "07":
                    result = "เดือนกรกฏาคม";
                    break;
                case "08":
                    result = "เดือนสิงหาคม";
                    break;
                case "09":
                    result = "เดือนกันยายน";
                    break;
                case "10":
                    result = "เดือนตุลาคม";
                    break;
                case "11":
                    result = "เดือนพฤศจิกายน";
                    break;
                case "12":
                    result = "เดือนธันวาคม";
                    break;
            }

            return result;
        }

        public static string ConvertMonthNumberToMonthThaiShort(string param)
        {
            string result = string.Empty;

            switch (param)
            {
                case "01":
                    result = "มกราคม";
                    break;
                case "02":
                    result = "กุมภาพันธ์";
                    break;
                case "03":
                    result = "มีนาคม";
                    break;
                case "04":
                    result = "เมษายน";
                    break;
                case "05":
                    result = "พฤษภาคม";
                    break;
                case "06":
                    result = "มิถุนายน";
                    break;
                case "07":
                    result = "กรกฏาคม";
                    break;
                case "08":
                    result = "สิงหาคม";
                    break;
                case "09":
                    result = "กันยายน";
                    break;
                case "10":
                    result = "ตุลาคม";
                    break;
                case "11":
                    result = "พฤศจิกายน";
                    break;
                case "12":
                    result = "ธันวาคม";
                    break;
            }

            return result;
        }

        public static string ConvertMonthNumberToShortMonthThai(string param)
        {
            string result = string.Empty;

            switch (param)
            {
                case "01":
                    result = "ม.ค.";
                    break;
                case "02":
                    result = "ก.พ.";
                    break;
                case "03":
                    result = "มี.ค.";
                    break;
                case "04":
                    result = "เม.ย.";
                    break;
                case "05":
                    result = "พ.ค.";
                    break;
                case "06":
                    result = "มิ.ย.";
                    break;
                case "07":
                    result = "ก.ค.";
                    break;
                case "08":
                    result = "ส.ค.";
                    break;
                case "09":
                    result = "ก.ย.";
                    break;
                case "10":
                    result = "ต.ค.";
                    break;
                case "11":
                    result = "พ.ย.";
                    break;
                case "12":
                    result = "ธ.ค.";
                    break;
            }

            return result;
        }

        public static string ConvertMonthNumberToMonthEnglish(string param)
        {
            string result = string.Empty;

            switch (param)
            {
                case "01":
                    result = "Jan";
                    break;
                case "02":
                    result = "Feb";
                    break;
                case "03":
                    result = "Mar";
                    break;
                case "04":
                    result = "Apr";
                    break;
                case "05":
                    result = "May";
                    break;
                case "06":
                    result = "Jun";
                    break;
                case "07":
                    result = "Jul";
                    break;
                case "08":
                    result = "Aug";
                    break;
                case "09":
                    result = "Sep";
                    break;
                case "10":
                    result = "Oct";
                    break;
                case "11":
                    result = "Nov";
                    break;
                case "12":
                    result = "Dec";
                    break;
            }

            return result;
        }

        public static string ConvertArabicToThai(string param)
        {
            string result = string.Empty;

            string[] strArabicNumber = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            string[] strThaiNumber = { "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙" };

            if (!string.IsNullOrEmpty(param))
            {
                var getArrayString = param.ToArray();

                var getArrayList = getArrayString.Select(c => c.ToString()).ToList();

                foreach (var item in getArrayList)
                {
                    for (int i = 0; i < strArabicNumber.Length; i++)
                    {
                        if (item == strArabicNumber[i])
                        {
                            result += strThaiNumber[i];
                        }
                        else
                        {
                            if (!strArabicNumber.Contains(item))
                            {
                                result += item;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                result = Constants.invalidDataFormat;
            }

            return result;
        }
        public static string ConvertThaiToArabic(string param)
        {
            string result = string.Empty;

            string[] strArabicNumber = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            string[] strThaiNumber = { "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙" };

            if (!string.IsNullOrEmpty(param))
            {
                var getArrayString = param.ToArray();

                var getArrayList = getArrayString.Select(c => c.ToString()).ToList();

                foreach (var item in getArrayList)
                {
                    for (int i = 0; i < strThaiNumber.Length; i++)
                    {
                        if (item == strThaiNumber[i])
                        {
                            result += strArabicNumber[i];
                        }
                        else
                        {
                            if (!strThaiNumber.Contains(item))
                            {
                                result += item;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                result = Constants.invalidDataFormat;
            }

            return result;
        }
        public static string ConvertPriceToThaiWording(string price)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(price))
            {
                string bahtTxt, n;
                double amount;
                try { amount = Convert.ToDouble(price); }
                catch { amount = 0; }
                bahtTxt = amount.ToString("####.00");
                string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                string[] temp = bahtTxt.Split('.');
                string intVal = temp[0];
                string decVal = temp[1];
                if (Convert.ToDouble(bahtTxt) == 0)
                    result = "ศูนย์บาทถ้วน";
                else
                {
                    for (int i = 0; i < intVal.Length; i++)
                    {
                        n = intVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == (intVal.Length - 1)) && (n == "1"))
                                result += "เอ็ด";
                            else if ((i == (intVal.Length - 2)) && (n == "2"))
                                result += "ยี่";
                            else if ((i == (intVal.Length - 2)) && (n == "1"))
                                result += "";
                            else
                                result += num[Convert.ToInt32(n)];
                            result += rank[(intVal.Length - i) - 1];
                        }
                    }
                    result += "บาท";
                    if (decVal == "00")
                        result += "ถ้วน";
                    else
                    {
                        for (int i = 0; i < decVal.Length; i++)
                        {
                            n = decVal.Substring(i, 1);
                            if (n != "0")
                            {
                                if ((i == decVal.Length - 1) && (n == "1"))
                                    result += "เอ็ด";
                                else if ((i == (decVal.Length - 2)) && (n == "2"))
                                    result += "ยี่";
                                else if ((i == (decVal.Length - 2)) && (n == "1"))
                                    result += "";
                                else
                                    result += num[Convert.ToInt32(n)];
                                result += rank[(decVal.Length - i) - 1];
                            }
                        }
                        result += "สตางค์";
                    }
                }
            }
            else
            {
                result = Constants.invalidDataFormat;
            }


            return result;
        }

        public static string FirstCharToLowerCase(string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        public static string HashPassword(string password)
        {
            StringBuilder builder = new StringBuilder();

            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash from the password bytes
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the hashed bytes to a string representation

                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
            }

            return builder.ToString();
        }

        public static string GetQueryString<T>(T param)
        {
            string result = "";

            List<string> strings = new List<string>();

            Type type = param.GetType();

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.GetValue(param) != null)
                {
                    var values = property.Name + "=" + property.GetValue(param);

                    strings.Add(values);
                }
            }

            result = string.Join("&", strings);

            return result;
        }

        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            // Hash the entered password and compare it with the stored hashed password
            string enteredPasswordHash = HashPassword(enteredPassword);

            return enteredPasswordHash == hashedPassword;
        }


        public static DataTable ReadExcelFlie(string filePath)
        {
            //Create a new DataTable.
            DataTable dt = new DataTable();

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                int MaxCell = 0;
                foreach (IXLRow row in workSheet.Rows())
                {
                    var rowCellCount = row.Cells().Count();
                    if (rowCellCount > MaxCell)
                    {
                        MaxCell = rowCellCount;
                    }
                }
                for (int i = 1; i <= MaxCell; i++)
                {
                    dt.Columns.Add("column" + i);
                }
                //Loop through the Worksheet rows.
                bool firstRow = false;
                foreach (IXLRow row in workSheet.Rows())
                {

                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {

                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
            }
            return dt;
        }

        public static object GetResponseController(Response? response)
        {
            if (response?.httpCode == Constants.httpCode200)
            {
                return new
                {
                    response?.status,
                    response?.statusCode,
                    response?.type,
                    response?.message,
                    response?.responseTime,
                    response?.pageNumber,
                    response?.pageSize,
                    response?.effectRow,
                    response?.data

                };
            }
            else if (response?.httpCode == Constants.httpCode500)
            {
                return new
                {
                    response?.status,
                    response?.statusCode,
                    response?.type,
                    response?.message,
                    response?.responseTime,
                    response?.exception
                };
            }
            else
            {
                return new
                {
                    response?.status,
                    response?.statusCode,
                    response?.responseTime,
                    response?.type,
                    response?.message
                };
            }
        }

        public static void TransferData_ClassA_to_ClassB<A, B>(A TempleteA, ref B TempleteB, List<string> lst_NotTransferColumn = null)
        {
            try
            {
                if (lst_NotTransferColumn == null) lst_NotTransferColumn = new List<string>();
                foreach (PropertyInfo item in TempleteB.GetType().GetProperties())
                {
                    //string Value = string.Empty;
                    if (!lst_NotTransferColumn.Contains(item.Name)) //check not transfer data column
                    {
                        PropertyInfo PropA = TempleteA.GetType().GetProperty(item.Name);
                        if (PropA != null)
                        {
                            object tmp = PropA.GetValue(TempleteA, BindingFlags.GetProperty, null, null, null);
                            if (item.CanWrite)
                            {
                                item.SetValue(TempleteB, tmp, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_ClasName.Error(ex.Message);
            }
        }

        public static DateTime GetDateTimeByGMT(int gmt)
        {
            DateTimeOffset currentTimeUtc = DateTimeOffset.UtcNow;

            int gmtOffset = gmt;

            TimeSpan offset = TimeSpan.FromHours(gmtOffset);

            DateTimeOffset currentTimeGmt = currentTimeUtc.ToOffset(offset);

            return currentTimeGmt.DateTime;
        }
    }
}


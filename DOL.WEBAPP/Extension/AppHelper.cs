using System;
using System.Reflection;

namespace DOL.WEBAPP.Extension
{
	public static class AppHelper
	{
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
    }


}


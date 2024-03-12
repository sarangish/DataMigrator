using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;



namespace WindowsServiceDemo
{
    class DataAccess
    {
        public void Logging(string str, string sType)
        {
            try
            {
                string file = AppDomain.CurrentDomain.BaseDirectory + "/LOGS/" + sType + DateTime.Now.ToString("ddMMyy") + ".LOG";
                File.AppendAllText(file, DateTime.Now.ToLongTimeString() + "***" + str + Environment.NewLine);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

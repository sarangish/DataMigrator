﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace WindowsServiceDemo
{
    class UpdateRecord
    {
        int timedelay = 0;
        Thread thread;
        ThreadStart threadstart;
        public static Boolean IsRunning = false;
        Connectioncls con = new Connectioncls();
        DataAccess log = new DataAccess();
        public void checkStatus()
        {
            try
            {
                log.Logging("check status started at " + DateTime.Now,"T");
                timedelay = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]);

                threadstart = new ThreadStart(processData);
                thread = new Thread(threadstart);
                thread.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void processData()
        {
            try
            {
                while (IsRunning)
                {
                    log.Logging("process data started at " + DateTime.Now, "T");
                    fetchAndUpdateData();
                    Thread.Sleep(timedelay);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void fetchAndUpdateData() //select data - insert and update it. -----------------------
        {


            try
            {
                log.Logging("fetchAndUpdateData at " + DateTime.Now, "T");
                string StatementType = "Select";
                DataTable Dt = selectData(StatementType);

                string id = Dt.Rows[0][0].ToString();
                string name = Dt.Rows[0][1].ToString();
                string status = Dt.Rows[0][4].ToString();

                string StatementType1 = "insert";
                DataTable Dt1 = insertData(StatementType1); 

                string StatementType2 = "Update";
                DataTable Dt2 = updateData(StatementType2);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public DataTable selectData(string StatementType)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [sp_fetchupdate] '  ', '  ', '  ',' ', ' ','" + StatementType + "'";
            log.Logging("selectData at " + sql, "T");
            dt = con.ExecuteGetDataTable(sql);
            return dt;

        }

        public DataTable insertData(string StatementType1)
        { 

                string StatementType = "Select";
                DataTable Dt = selectData(StatementType);
                DataTable Dt1 = new DataTable();
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    string id = Dt.Rows[i][0].ToString();
                    string name = Dt.Rows[i][1].ToString();
                    string status = Dt.Rows[i][4].ToString();


                    string sql = "EXEC [sp_fetchupdate] '" + id + "' ,'" + name + "' , '','' ,'" + status + "','" + StatementType1 + "'";

                    Dt1 = con.ExecuteGetDataTable(sql);
                    
                }
                return Dt1;

        }

        public DataTable updateData(string StatementType2)
        {

            
            DataTable Dt2 = new DataTable();

            string sql = "EXEC [sp_fetchupdate] '', '', '', '', '', '" + StatementType2 + "'";

            Dt2 = con.ExecuteGetDataTable(sql);

            return Dt2;

        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace DoAn01.Home.Schedule
{
    internal class Schedule
    {
        MY_DB mydb = new MY_DB();
        private string id;
        private string patientid;
        private string dentistid;
        private DateTime date;
        private string tinhtrang;
        private string ca;

        public Schedule()
        { }
        public string Id { get; set; }
        public string PatientID { get; set; }
        public string DentistID { get; set; }
        public DateTime Date { get; set; }
        public string Tinhtrang { get; set; }
        public string Ca { get; set; }

        public bool insertSchedule(string id, string dentistID,string patientid,DateTime date,string tinhtrang,string ca)
        {

            SqlCommand command = new SqlCommand("INSERT INTO schedule (id,dentistid, patientid, ngaykham, tinhtrang, ca) " +
                                                "VALUES (@id,@dentistid, @patientid, @date, @tinhtrang, @ca)", mydb.getConnection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@dentistid", dentistID);
            command.Parameters.AddWithValue("@patientid", patientid);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@tinhtrang", tinhtrang);
            command.Parameters.AddWithValue("@ca", ca);

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }
        

      
        public Schedule getSchedule(string id,string ca,string date)
        {
            Schedule schedule = new Schedule();
            SqlCommand cmd = new SqlCommand("SELECT * FROM schedule WHERE dentistid = @id and ca = @ca and ngaykham = @date", mydb.getConnection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ca",ca);
            cmd.Parameters.AddWithValue("@date", date);
            SqlDataReader reader = null;

            try
            {
                mydb.openConnection();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    schedule.Id = reader["id"].ToString();
                    schedule.PatientID = reader["patientid"].ToString();
                    schedule.DentistID = reader["dentistid"].ToString();
/*                    schedule.Date = Convert.ToDateTime(reader["date"]);*/
                    schedule.Tinhtrang = reader["tinhtrang"].ToString();
                    schedule.Ca = reader["ca"].ToString();

                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                mydb.closeConnection();
            }

            return schedule;
        }



    }
}
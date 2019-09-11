﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class Pesticide_Buy_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Pesticide_Buy ";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetListByMember(int MemberKey, string YEAR, string MONTH)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     COUNT(DatetimeBuy) AS Count, DatetimeBuy
                            FROM         dbo.PUL_Pesticide_Buy
                            WHERE     (MemberKey = @MemberKey) AND (MONTH(DatetimeBuy) = @Month) AND (YEAR(DatetimeBuy) = @Year)
                            GROUP BY DatetimeBuy";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@YEAR", SqlDbType.NVarChar).Value = YEAR;
                zCommand.Parameters.Add("@MONTH", SqlDbType.NVarChar).Value = MONTH;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetListByMemberDay(int MemberKey, DateTime DatetimeBuy)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Pesticide_Buy.PesticideBuyKey, dbo.PUL_Pesticide_Buy.PesticideKey, dbo.PUL_Pesticides.Trade_Name, dbo.PUL_Pesticide_Buy.IsActive
                            FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                                                  dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey
                            WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.DatetimeBuy = @DatetimeBuy)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DatetimeBuy", SqlDbType.DateTime).Value = DatetimeBuy;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
    }
}

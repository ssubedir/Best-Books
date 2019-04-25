﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BookStoreLIB
{
    class DALOrder
    {
        public int Proceed2Order(string xmlOrder)
        {
            SqlConnection cn = new SqlConnection(
                Properties.Settings.Default.connectionString);
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "down_PlaceOrder";
                SqlParameter inParameter = new SqlParameter();
                inParameter.ParameterName = "@xmlOrder";
                inParameter.Value = xmlOrder;
                inParameter.DbType = DbType.String;
                inParameter.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(inParameter);
                SqlParameter ReturnParameter = new SqlParameter();
                ReturnParameter.ParameterName = "@OrderID";
                ReturnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(ReturnParameter);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return (int)cmd.Parameters["@OrderID"].Value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.script.db
{
    public class DBManager
    {
        public static MySqlConnection mysql;

        public static bool Connect(string db,string ip,int port,string user,string pw)
        {
            mysql = new MySqlConnection();
            string s = string.Format("Database={0};Data Source = {1}; port={2}; User Id={3}; Password={4}", db, ip, port, user, pw);
            mysql.ConnectionString = s;
            try
            {
                mysql.Open();
                Console.WriteLine("[数据库] conn success");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[数据库] conn failed , {0}", e.Message); ;
                return false;
            }
        }
    }
}

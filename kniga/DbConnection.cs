﻿using System.Windows;

namespace kniga
{
    internal class DbConnection
    {
        MySqlConnection _connection;

        public void Config()
        {
            // пример строки подключения: "userid=student;password=student;database=1125_new_2025;server=192.168.200.13;characterset=utf8mb4";
            // конфигурация берется из файла / из окошка / из настроек / или статично
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.UserID = "student";
            sb.Password = "student";
            sb.Server = "192.168.200.13";
            sb.Database = "1125_new_2025";
            sb.CharacterSet = "utf8mb4";

            // инициализация объекта для подключения к субд
            _connection = new MySqlConnection(sb.ToString());
        }

        public bool OpenConnection()
        {
            if (_connection == null)
                Config();
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        internal void CloseConnection()
        {
            if (_connection == null)
                return;

            try
            {
                _connection.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        internal MySqlCommand CreateCommand(string sql)
        {
            return new MySqlCommand(sql, _connection);
        }


        static DbConnection dbConnection;
        private DbConnection() { }
        public static DbConnection GetDbConnection()
        {
            if (dbConnection == null)
                dbConnection = new DbConnection();
            return dbConnection;
        }


    }
}

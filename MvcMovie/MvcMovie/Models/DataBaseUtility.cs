﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Windows;

namespace MvcMovie.Models
{
    public class DataBaseUtility
    {
        private static string escape(string oldstr)
        {
            return oldstr.Replace("'", "\"");
        }

        private static SqlConnection GetConnector()
        {
            var conn = new SqlConnection();
            try
            {
                string currentpath = "";
                currentpath = currentpath + AppDomain.CurrentDomain.BaseDirectory.ToString();
                conn.ConnectionString = "Data Source = (LocalDb)\\MSSQLLocalDB; AttachDbFilename = \"" + currentpath.Replace("\\","\\") + "App_Data\\Movies.mdf\"; Integrated Security = True";
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }
       }

        private static void CloseConnector(SqlConnection conn)
        {
            if (conn == null)
                return;

            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static bool ExeSqlNoRes(string sql)
        {
            var conn = GetConnector();
            if (conn == null)
                return false;

            try
            {
                var command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                CloseConnector(conn);
                return true;
            }
            catch (Exception ex)
            {
                CloseConnector(conn);
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static SqlDataReader ExeSqlWithRes(SqlConnection conn,string sql)
        {
            try
            {
                var command = conn.CreateCommand();
                command.CommandText = sql;
                var sqlreader = command.ExecuteReader();
                if (sqlreader.HasRows)
                {
                    return sqlreader;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static bool AddMovie(string title,string releasedate,string genre,float price,string rate)
        {
            string sqlstr = "insert into Movies(Title,ReleaseDate,Genre,Price,Rate) values('<title>','<releasedate>','<genre>',<price>,'<rate>')";
            sqlstr = sqlstr.Replace("<title>", escape(title))
                .Replace("<releasedate>", escape(releasedate))
                .Replace("<genre>", escape(genre))
                .Replace("<price>", price.ToString())
                .Replace("<rate>", escape(rate));
            return DataBaseUtility.ExeSqlNoRes(sqlstr);
        }

        public static bool UpdateMovie(long id, string title, string releasedate, string genre, float price, string rate)
        {
            string sqlstr = "Update Movies Set Title = '<title>',ReleaseDate = '<releasedate>',Genre = '<genre>',price = <price>,rate = '<rate>' where Id = <id>";
            sqlstr = sqlstr.Replace("<id>", id.ToString())
                .Replace("<title>", escape(title))
                .Replace("<releasedate>", escape(releasedate))
                .Replace("<genre>", escape(genre))
                .Replace("<price>", price.ToString())
                .Replace("<rate>", escape(rate));
            return DataBaseUtility.ExeSqlNoRes(sqlstr);
        }

        public static bool DeleteMovie(long id)
        {
            string sqlstr = "Delete from Movies where Id =" + id.ToString();
            return DataBaseUtility.ExeSqlNoRes(sqlstr);
        }

        public static List<Movie> GetMovies(string sql)
        {
            var mvlist = new List<Movie>();
            var conn = GetConnector();
            if (conn == null)
                return new List<Movie>();
            var reader = ExeSqlWithRes(conn, sql);
            if (reader == null)
                return new List<Movie>();
            try
            {
                while (reader.Read())
                {
                    mvlist.Add(new Movie(reader.GetInt64(0), reader.GetString(1), reader.GetDateTime(2).ToString(),reader.GetString(3), (float)reader.GetDouble(4), reader.GetString(5)));
                }
                reader.Close();
                CloseConnector(conn);
                return mvlist;
            }
            catch (Exception ex)
            {
                mvlist.Clear();
                reader.Close();
                CloseConnector(conn);
                System.Windows.MessageBox.Show(ex.ToString());
                return new List<Movie>();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devart.Data;
using Devart.Data.SQLite;
using Devart.Data.SQLite.Linq;
using MainContext;
using System.IO;
using System.Diagnostics;

namespace UpgradeScript
{
    class Program
    {
        static void Main(string[] args)
        {
            OzelliklerTablosu();
        }

        private static void OzelliklerTablosu()
        {
            if (File.Exists("db.db"))
            {
                #region Tablo yoksa oluştur

                SQLiteConnection conn = new SQLiteConnection();
                conn.DataSource = "db.db";
                SQLiteCommand command = new SQLiteCommand(@"Select COUNT(*) From sqlite_master where type='table' and name = 'Ozellikler'", conn);
                conn.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                int COUNT = -1;
                while (reader.Read())
                {
                    COUNT = reader.GetInt32(0);
                }

                if (COUNT == 0)
                {
                    command.CommandText = @"CREATE TABLE [Ozellikler] (
  [ID] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT, 
  [Ozellik] NVARCHAR(100) NOT NULL ON CONFLICT FAIL, 
  [Deger] NVARCHAR(100) NOT NULL ON CONFLICT FAIL, 
  [SonIslemTarihi] DATETIME);
CREATE UNIQUE INDEX [ozellik] ON [Ozellikler] ([Ozellik]);";
                    command.ExecuteNonQuery();
                }
                conn.Close();

                #endregion Tablo yoksa oluştur


                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
                builder.DataSource="db.db";
                MainDataContext ctxdb = new MainDataContext(builder.ToString());
                builder.DataSource = "cdb.db";
                MainDataContext ctxcdb = new MainDataContext(builder.ToString());

                #region Ana tabloya eklenen kayıtları yükle

                IEnumerable<Ozellikler> ozelliklerYeni = ctxcdb.Ozelliklers.Select(o => o);


                foreach (Ozellikler item in ozelliklerYeni)
                {
                    if (ctxdb.Ozelliklers.Count(o => o.ID == item.ID) < 1)
                    {
                        ctxdb.Ozelliklers.InsertOnSubmit(item);
                    }
                }

                ctxdb.SubmitChanges();


                #endregion Ana tabloya eklenen kayıtları yükle

                #region Ana tablodan silinen özellikleri sil

                IEnumerable<Ozellikler> silinecekOzellikler = ctxdb.Ozelliklers.Select(o => o);

                foreach (Ozellikler item in silinecekOzellikler)
                {
                    if (ozelliklerYeni.Count(o => o.ID == item.ID) == 0)
                    {
                        ctxdb.Ozelliklers.DeleteOnSubmit(item);
                    }
                }
                ctxdb.SubmitChanges();


                #endregion Ana tablodan silinen özellikleri sil


            }

            else
            {
                if (File.Exists("cdb.db"))
                {
                    File.Copy("cdb.db", "db.db");
                }
            }

            Process.Start("http:\\saitorhan.wordpress.com");

        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void antalFilmKategori (string kategori)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = "Context Connection=true";

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        conn.Open();
        cmd.CommandText = "SELECT COUNT (Kategorier.KategoriID) AS AntalFilmer FROM Kategorier JOIN Filmtitlar ON Kategorier.KategoriID=Filmtitlar.KategoriID WHERE Kategorier.Kategorinamn = @kategori";

        SqlParameter paramDatum = new SqlParameter();

        paramDatum.Value = kategori;
        paramDatum.Direction = ParameterDirection.Input;
        paramDatum.DbType = DbType.DateTime;
        paramDatum.ParameterName = "@kategori";

        cmd.Parameters.Add(paramDatum);
        SqlDataReader sqldr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(sqldr);

        sqldr.Close();
        conn.Close();
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SqlVideouthyrning(DateTime datum)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = "Context Connection=true";

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        conn.Open();
        cmd.CommandText = "SELECT Filmtitlar.Filmnamn, Uthyrningar.Uthyrningsdatum, Uthyrningar.Återlämningsdag, Medlemmar.Förnamn, Medlemmar.Efternamn, Filmkopior.FilmkopiaID FROM Medlemmar RIGHT OUTER JOIN Uthyrningar on Uthyrningar.MedlemsID = Medlemmar.MedlemsID JOIN Filmkopior on Uthyrningar.UthyrningsID = Filmkopior.UthyrningsID JOIN Filmtitlar on Filmkopior.FilmtitelID = Filmtitlar.FilmtitelID where Uthyrningar.Återlämningsdag <= @datum";

        SqlParameter paramDatum = new SqlParameter();

        paramDatum.Value = datum;
        paramDatum.Direction = ParameterDirection.Input;
        paramDatum.DbType = DbType.DateTime;
        paramDatum.ParameterName = "@datum";

        cmd.Parameters.Add(paramDatum);
        SqlDataReader sqldr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(sqldr);

        sqldr.Close();
        conn.Close();
           


    }
}

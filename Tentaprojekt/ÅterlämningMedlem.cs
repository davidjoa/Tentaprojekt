using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ÅterlämningMedlem ()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = "Context Connection=true";

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        conn.Open();
        cmd.CommandText = "SELECT SUM(Uthyrningar.Hyrkostnad) AS Totalkostnad FROM Uthyrningar JOIN Medlemmar ON Uthyrningar.MedlemsID=Medlemmar.MedlemsID WHERE Medlemmar.Förnamn = 'Tina' AND Medlemmar.Efternamn = 'Danielsson' SELECT COUNT(Uthyrningar.Uthyrningsdatum)AS AntalFilmer FROM Uthyrningar JOIN Medlemmar ON Uthyrningar.MedlemsID = Medlemmar.MedlemsID WHERE Medlemmar.Förnamn = 'Tina' AND Medlemmar.Efternamn = 'Danielsson' SELECT Medlemmar.Förnamn, Medlemmar.Efternamn, Uthyrningar.Uthyrningsdatum, Uthyrningar.Återlämningsdag, Uthyrningar.Hyrkostnad FROM Medlemmar JOIN Uthyrningar ON Uthyrningar.MedlemsID = Medlemmar.MedlemsID JOIN Filmkopior ON Uthyrningar.UthyrningsID = Filmkopior.UthyrningsID JOIN Filmtitlar ON Filmkopior.FilmtitelID = Filmtitlar.FilmtitelID WHERE Medlemmar.Förnamn = 'Tina' AND Medlemmar.Efternamn = 'Danielsson'";
       
        SqlDataReader sqldr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(sqldr);

        sqldr.Close();
        conn.Close();


    }
}

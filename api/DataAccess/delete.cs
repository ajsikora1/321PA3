using api;
using api.Interfaces;
using api.Models;
using MySql.Data.MySqlClient;
using System;

namespace api.DataAccess
{
    public class DeleteSong : IDeleteSong
    {
        public static void DropSongTable()
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"DROP TABLE IF EXISTS songs";

            using var cmd = new MySqlCommand(stm, con);

            cmd.ExecuteNonQuery();
        }

        public void DeleteSongById(string id)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"DELETE FROM songs WHERE id = @id";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        void IDeleteSong.DeleteSong(string id)
        {
            throw new NotImplementedException();
        }
    }
}

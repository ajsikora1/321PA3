using api;
using api.interfaces;
using api.Interfaces;
using api.Models;
using MySql.Data.MySqlClient;
using System;

namespace api.DataAccess
{
    public class SaveSong : ISaveSong
    {
        public static void CreateSongTable()
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"CREATE TABLE songs(id VARCHAR(255) PRIMARY KEY, title TEXT, artist TEXT, date_added DATETIME, favorited BOOLEAN, deleted BOOLEAN)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.ExecuteNonQuery();
        }
        public void CreateSong(Songs mySong)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"INSERT INTO songs(id, title, artist, date_added, favorited, deleted) VALUES(@id, @title, @artist, @date_added, @favorited, @deleted)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@id", mySong.SongId);
            cmd.Parameters.AddWithValue("@title", mySong.Title);
            cmd.Parameters.AddWithValue("@artist", mySong.Artist);
            cmd.Parameters.AddWithValue("@date_added", mySong.DateAdded);
            cmd.Parameters.AddWithValue("@favorited", mySong.Favorited);
            cmd.Parameters.AddWithValue("@deleted", mySong.Deleted);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public void UpdateSong(Songs mySong)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"UPDATE songs SET title = @title, artist = @artist, favorited = @favorited, deleted = @deleted WHERE id = @id";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@id", mySong.SongId);
            cmd.Parameters.AddWithValue("@title", mySong.Title);
            cmd.Parameters.AddWithValue("@artist", mySong.Artist);
            cmd.Parameters.AddWithValue("@favorited", mySong.Favorited);
            cmd.Parameters.AddWithValue("@deleted", mySong.Deleted);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        // void ISaveSong.SaveSong(Songs mySong)
        // {
        //     // SaveSong(mySong);
        // }

        public void InitializeDatabase()
        {
            string sql = @"USE l2znj4xjg2pekeiv;
                DROP TABLE IF EXISTS books;

                CREATE TABLE IF NOT EXISTS songs (
                    id VARCHAR(255) PRIMARY KEY,
                    title TEXT NOT NULL,
                    artist TEXT NOT NULL,
                    date_added DATETIME NOT NULL,
                    favorited BOOLEAN NOT NULL DEFAULT 0,
                    deleted BOOLEAN NOT NULL DEFAULT 0
                );

                SELECT * FROM l2znj4xjg2pekeiv.songs;";

            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        public Songs GetSongById(string id)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = "SELECT * FROM songs WHERE id = @id";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Songs song = new Songs
                {
                    SongId = reader.GetString("id"),
                    Title = reader.GetString("title"),
                    Artist = reader.GetString("artist"),
                    DateAdded = reader.GetDateTime("date_added"),
                    Favorited = reader.GetBoolean("favorited"),
                    Deleted = reader.GetBoolean("deleted")
                };
                return song;
            }
            else
            {
                return null;
            }
        }
    }
}
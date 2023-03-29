using api;
using api.DataAccess;
using api.Interfaces;
using api.Models;
using MySql.Data.MySqlClient;
using System;

namespace api.Handler{
    public class SongHandler{
        public static List<Songs> AllSongs = new List<Songs>();

        public SongHandler(){

        }

        public List<Songs> GetAllSongs(){
            return AllSongs;
        }

        // public void AddSongs(Songs newSongs){
        //     SaveSong saveSong = new SaveSong();
        //     saveSong.CreateSong(newSongs);
        //     AllSongs.Add(newSongs);
        // }

        public void PrintAll(){
            foreach(Songs songs in AllSongs){
                System.Console.WriteLine(songs.ToString());
            }
        }

        public void EditSong(string id, Songs editSong)
        {
            int index = AllSongs.FindIndex(s => s.SongId == id);
            SaveSong saveSong = new SaveSong();
            saveSong.UpdateSong(editSong);
            AllSongs[index] = editSong;
        }

        public void DeleteSong(string id)
        {
            int index = AllSongs.FindIndex(s => s.SongId == id);
            AllSongs.RemoveAt(index);
            DeleteSong deleteSong = new DeleteSong();
            deleteSong.DeleteSongById(id);
        }
    }
}
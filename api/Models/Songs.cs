using api.DataAccess;
using api.Interfaces;
using api.Models;

namespace api.Models
{
    public class Songs
    {
        public string SongId {get; set;}

        public string Title {get; set;}

        public string Artist {get; set;}

        public DateTime DateAdded {get; set;}
        public bool Favorited {get; set;}
        public bool Deleted {get; set;}

        // public ISaveSong Save {get; set;}

         public Songs()
        {
            SongId = Guid.NewGuid().ToString();
            DateAdded = DateTime.Now;
            // Save = new SaveSong();
        }

        public override string ToString(){
            return $" {Title} {Artist} {DateAdded} {Favorited} {Deleted}";
        }

        internal void Add(Songs song)
        {
            throw new NotImplementedException();
        }

    }
}
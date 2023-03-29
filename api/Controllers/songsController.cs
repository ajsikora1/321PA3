using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Handler;
using api.DataAccess;
using api.Interfaces;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using api.interfaces;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        // GET: api/Song
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Songs> Get()
        {
            SongHandler mySongHandler = new SongHandler();
            return mySongHandler.GetAllSongs();

            // Database db = new Database();
            // db.Get();
            // return mySongs;
        }

        // GET: api/Song/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(string id)
        // {
        //     System.Console.WriteLine("III ID");
        //     return "value";
        // }

        // GET: api/Song/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            SaveSong mySavedSong = new SaveSong();
            Songs song = mySavedSong.GetSongById(id);
            Console.WriteLine("Fetched Song: " + JsonConvert.SerializeObject(song)); // Add this line

            if (song != null)
            {
                return Ok(song);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Song
        [EnableCors("OpenPolicy")]
        [HttpPost]  //CREATE
        public void Post([FromBody] Songs value)
        {
            ISaveSong mySongHandler = new SaveSong();
            mySongHandler.CreateSong(value);
        }

        // PUT: api/Song/5
        [HttpPut("{id}")]   //UPDATE
        public void Put(string id, [FromBody] Songs value)
        {
            SongHandler mySongHandler = new SongHandler();
            mySongHandler.EditSong(id, value);
        }

        // DELETE: api/Song/5
        [HttpDelete("{id}")]    //DELETE
        public void Delete(string id)
        {
            SongHandler mySongHandler = new SongHandler();
            mySongHandler.DeleteSong(id);
        }
    }
}

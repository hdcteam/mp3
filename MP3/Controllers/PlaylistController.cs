using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP3.Models;

namespace MP3.Controllers
{
    public class PlaylistController : BaseController
    {
        private Entities db = new Entities();
        //
        // GET: /Playlist/{songId}/{playlistId}

        public ActionResult Add(int songId, int playlistId)
        {
            Playlists playlist = db.Playlists.Find(playlistId);
            if (db.Songs.Find(songId) == null || CurrentUser == null || playlist == null || playlist.UserId != CurrentUser.UserId || playlist.SongsInPlaylist.Where(s => s.SongId == songId).Count() > 0)
                return HttpNotFound();

            int next = playlist.SongsInPlaylist.Count > 0 ? playlist.SongsInPlaylist.Max(p => p.Order) + 1 : 1;
            SongPlaylist songPlaylist = new SongPlaylist { SongId = songId, PlaylistId = playlistId, Order = next };
            db.SongPlaylist.Add(songPlaylist);
            db.SaveChanges();

            db.Entry(songPlaylist).Reference(s => s.SongInfo).Load();
            db.Entry(songPlaylist).Reference(s => s.Playlist).Load();

            return View(songPlaylist);
        }

    }
}

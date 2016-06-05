using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Local;


namespace SpotifyLyrics
{
    class SpotifyManager
    {
        private SpotifyLocalAPI spotify;
        private ILyrics lyrics;
        public static bool IsSpotifyRunning => SpotifyLocalAPI.IsSpotifyRunning();

        public SpotifyManager()
        {
            spotify = new SpotifyLocalAPI();

            if (IsSpotifyRunning)
            {
                bool hasConnected = spotify.Connect();
                if (!hasConnected)
                    throw new SpotifyCantConnectException();
            }
            else
            {
                throw new SpotifyNotRunningException();
            }

            spotify.ListenForEvents = true;
            lyrics = new LyricWiki();
        }

        public void OnTrackChangeSubscribe(EventHandler<TrackChangeEventArgs> del)
        {
            spotify.OnTrackChange += del;
            del.Invoke(null, null); // Get the current lyrics right away
        }

        public async Task<string> GetCurrentLyrics()
        {
            if (spotify.GetStatus().Track.IsAd())
                return "Ad is playing";

            string trackName = spotify.GetStatus().Track.TrackResource.Name.Replace(' ', '_');
            string artistName = spotify.GetStatus().Track.ArtistResource.Name.Replace(' ', '_');

            return await lyrics.GetLyrics(trackName, artistName);
        }

       
    }
}

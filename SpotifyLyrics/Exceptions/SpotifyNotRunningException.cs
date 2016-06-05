using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLyrics
{
    class SpotifyNotRunningException : Exception
    {
        public SpotifyNotRunningException() : this("Spotify is currently not running")
        {

        }

        public SpotifyNotRunningException(string message) : base(message)
        {

        }

        public SpotifyNotRunningException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

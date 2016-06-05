using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLyrics
{
    class SpotifyCantConnectException : Exception
    {
        public SpotifyCantConnectException() : this("Could not connect to spotify")
        {
        }

        public SpotifyCantConnectException(string message) : base(message)
        {

        }

        public SpotifyCantConnectException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

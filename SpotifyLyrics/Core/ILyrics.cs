using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLyrics
{
    interface ILyrics
    {
        Task<string> GetLyrics(string trackName, string artist);
    }
}

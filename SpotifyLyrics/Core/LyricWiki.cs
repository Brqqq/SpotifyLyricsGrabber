using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;

namespace SpotifyLyrics
{
    class LyricWiki : ILyrics
    {
        private HttpClient http;
        private static readonly string BASE_URL = @"http://lyrics.wikia.com/wiki";

        public LyricWiki()
        {
            http = new HttpClient();
        }
        public async Task<string> GetLyrics(string trackName, string artist)
        {
            string url = getUrl(trackName, artist);
            string html = await getHtmlAtUrl(url);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlNode lyricsNode = getLyricsNode(htmlDocument);

            string result = extractTextFromLyricsNode(lyricsNode);

            result = formatLyrics(result);

            return string.IsNullOrWhiteSpace(result) ? "No lyrics found" : result;
        }

        private async Task<string> getHtmlAtUrl(string url)
        {
            HttpResponseMessage response = await http.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        private HtmlNode getLyricsNode(HtmlDocument document)
        {
            return document.DocumentNode.Descendants("div").Where(
                d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lyricbox")
            ).FirstOrDefault();
        }

        private string extractTextFromLyricsNode(HtmlNode lyricsNode)
        {
            StringBuilder sb = new StringBuilder();
            if (lyricsNode != null)
            {
                foreach (var node in lyricsNode.ChildNodes)
                {
                    sb.Append(node.InnerText);
                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        private string formatLyrics(string result)
        {
            result = WebUtility.HtmlDecode(result);
            result = result.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);

            result = Regex.Replace(result, @"<!--.*?-->", string.Empty, RegexOptions.Singleline); // Remove the HTML comment at the end of the page

            return result;
        }

        private string getUrl(string trackName, string artist)
        {
            return $"{BASE_URL}/{artist}:{trackName}";
        }
    }
}

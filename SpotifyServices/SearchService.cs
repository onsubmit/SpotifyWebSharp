using SpotifyWebApi.SpotifyResponses.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SpotifyWebApi.SpotifyServices
{
    /// <summary>
    /// Search service
    /// https://developer.spotify.com/technologies/web-api/search/
    /// </summary>
    public class SearchService : SpotifyService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchService()
        {
            this.BaseUrl = "http://ws.spotify.com/search/1/";
        }

        /// <summary>
        /// Searches for the given artist
        /// </summary>
        /// <param name="artist">Artist query e.g. "Foo Fighters"</param>
        /// <param name="page">The page of the result set to return; defaults to 1</param>
        /// <returns>Artists matching the query</returns>
        public Artists SearchArtists(string artist, int? page = null)
        {
            return Search<Artists>(artist, page);
        }

        /// <summary>
        /// Searches for the given album
        /// </summary>
        /// <param name="album">Album query e.g. "One By One"</param>
        /// <param name="page">The page of the result set to return; defaults to 1</param>
        /// <returns>Albums matching the query</returns>
        public Albums SearchAlbums(string album, int? page = null)
        {
            return Search<Albums>(album, page);
        }

        /// <summary>
        /// Searches for the given track
        /// </summary>
        /// <param name="track">Track query e.g. "All My Life"</param>
        /// <param name="page">The page of the result set to return; defaults to 1</param>
        /// <returns>Tracks matching the query</returns>
        public Tracks SearchTracks(string track, int? page = null)
        {
            return Search<Tracks>(track, page);
        }

        /// <summary>
        /// Generic Search method
        /// </summary>
        /// <typeparam name="T">Type of search to perform</typeparam>
        /// <param name="query">Query</param>
        /// <param name="page">The page of the result set to return; defaults to 1</param>
        /// <returns>Track, Artist, or Album -- based on T</returns>
        private T Search<T>(string query, int? page = null) where T : class
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Query required.");
            }

            // Determine endpoint
            string endpoint;
            Type t = typeof(T);
            if (t == typeof(Albums))
            {
                endpoint = "album";
            }
            else if (t == typeof(Tracks))
            {
                endpoint = "track";

            }
            else if (t == typeof(Artists))
            {
                endpoint = "artist";
            }
            else
            {
                throw new NotSupportedException(string.Format("Type {0} not supported.", t));
            }

            string uri = this.BaseUrl + string.Format("{0}?q={1}", endpoint, query);

            if (page != null)
            {
                // Append page parameter
                uri += string.Format("&page={0}", page);
            }

            // Get response
            XDocument doc = GetResponse(uri);

            // Deserialize response to type T
            return Deserialize<T>(doc);
        }
    }
}

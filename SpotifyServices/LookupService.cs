using SpotifyWebSharp.SpotifyResponses.Lookup;
using System;
using System.Web;
using System.Xml.Linq;

namespace SpotifyWebSharp.SpotifyServices
{
    /// <summary>
    /// Lookup service
    /// https://developer.spotify.com/technologies/web-api/lookup/
    /// </summary>
    public class LookupService : SpotifyService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LookupService()
        {
            this.BaseUrl = "http://ws.spotify.com/lookup/1/";
        }

        /// <summary>
        /// Artist extras
        /// </summary>
        public enum ArtistExtras
        {
            /// <summary>
            /// None: do not supply an 'extras' parameter
            /// </summary>
            None,

            /// <summary>
            /// Album: returns basic information about all the albums the artist is featured in
            /// </summary>
            Album,

            /// <summary>
            /// AlbumDetail: returns detailed information about all the albums the artist is featured in
            /// </summary>
            AlbumDetail
        }

        /// <summary>
        /// Album extras
        /// </summary>
        public enum AlbumExtras
        {
            /// <summary>
            /// None: do not supply an 'extras' parameter
            /// </summary>
            None,

            /// <summary>
            /// Track: returns basic information about all tracks in the album
            /// </summary>
            Track,

            /// <summary>
            /// TrackDetail: returns detailed information about all tracks in the album
            /// </summary>
            TrackDetail
        }

        /// <summary>
        /// Looks up the given artist
        /// </summary>
        /// <param name="artist">Artist e.g. "4YrKBkKSVeqDamzBPWVnSJ"</param>
        /// <param name="extras">Artist extras</param>
        /// <returns>Artist</returns>
        public Artist LookupArtist(string artist, ArtistExtras extras = ArtistExtras.None)
        {
            return Lookup<Artist>(query: artist, directHref: false, extras: extras == ArtistExtras.None ? null : extras.ToString());
        }

        /// <summary>
        /// Looks up the given album
        /// </summary>
        /// <param name="album">Album e.g. "6G9fHYDCoyEErUkHrFYfs4"</param>
        /// <param name="extras">Album extras</param>
        /// <returns>Album</returns>
        public Album LookupAlbum(string album, AlbumExtras extras = AlbumExtras.None)
        {
            return Lookup<Album>(query: album, directHref: false, extras: extras == AlbumExtras.None ? null : extras.ToString());
        }

        /// <summary>
        /// Looks up the given track
        /// </summary>
        /// <param name="track">Track e.g. "6NmXV4o6bmp704aPGyTVVG"</param>
        /// <returns>Track</returns>
        public Track LookupTrack(string track)
        {
            return Lookup<Track>(track);
        }

        /// <summary>
        /// Looks up an artist from its href
        /// </summary>
        /// <param name="artistHref">Artist href</param>
        /// <param name="artistExtras">Artist extras</param>
        /// <returns>Artist</returns>
        public Artist LookupByHref(string artistHref, ArtistExtras artistExtras)
        {
            return Lookup<Artist>(query: artistHref, directHref: true, extras: artistExtras == ArtistExtras.None ? null : artistExtras.ToString());
        }

        /// <summary>
        /// Looks up an album from its href
        /// </summary>
        /// <param name="albumHref">Album href</param>
        /// <param name="albumExtras">Album extras</param>
        /// <returns>Album</returns>
        public Album LookupByHref(string albumHref, AlbumExtras albumExtras)
        {
            return Lookup<Album>(query: albumHref, directHref: true, extras: albumExtras == AlbumExtras.None ? null : albumExtras.ToString());
        }

        /// <summary>
        /// Looks up a track from its href
        /// </summary>
        /// <param name="trackHref">Track href</param>
        /// <returns>Artist</returns>
        public Track LookupByHref(string trackHref)
        {
            return Lookup<Track>(query: trackHref, directHref: true, extras: null);
        }

        /// <summary>
        /// Generic Lookup method
        /// </summary>
        /// <typeparam name="T">Type of lookup to perform</typeparam>
        /// <param name="query">Query</param>
        /// <param name="directHref">Indicates if a href was supplied</param>
        /// <param name="extras">Extras value, null to not append the extras parameter</param>
        /// <returns>Track, Artist, or Album -- based on T</returns>
        private T Lookup<T>(string query, bool directHref = false, string extras = null) where T : class
        {
            string uri;
            if (directHref)
            {
                // A href was supplied
                if (string.IsNullOrEmpty(query))
                {
                    throw new ArgumentException("href required.");
                }

                uri = this.BaseUrl + string.Format("?uri={0}", query);
            }
            else
            {
                // No href supplied, determine the query
                Type t = typeof(T);
                if (string.IsNullOrEmpty(query))
                {
                    throw new ArgumentException(string.Format("{0} required.", t.Name));
                }

                uri = this.BaseUrl + string.Format("?uri=spotify:{0}:{1}", t.Name.ToLower(), HttpUtility.UrlEncode(query));
            }

            if (extras != null)
            {
                // Append extras parameter
                uri += string.Format("&extras={0}", extras.ToLower());
            }

            // Get response
            XDocument doc = GetResponse(uri);

            // Deserialize response to type T
            return Deserialize<T>(doc);
        }
    }
}

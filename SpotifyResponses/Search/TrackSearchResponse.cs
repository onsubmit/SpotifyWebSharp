using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyResponses.Search
{
    /// <summary>
    /// Deserialized object corresponding to track search results
    /// https://developer.spotify.com/technologies/web-api/search/
    /// </summary>
    [XmlRootAttribute("tracks", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class Tracks : Response
    {
        /// <summary>
        /// Track list
        /// </summary>
        [XmlElement("track", Namespace = "http://www.spotify.com/ns/music/1")]
        public List<SearchTrack> TrackList { get; set; }

        /// <summary>
        /// Track list indexer by integer
        /// </summary>
        /// <param name="index">Index of track to return</param>
        /// <returns>Track corresponding to the given index in the list, null if not found</returns>
        public SearchTrack this[int index]
        {
            get
            {
                if (index < 0 || index >= this.TrackList.Count)
                {
                    return null;
                }

                return this.TrackList[index];
            }
        }

        /// <summary>
        /// Track list indexer by name
        /// </summary>
        /// <param name="name">Name of track to return</param>
        /// <returns>Track with the given name, null if not found</returns>
        public SearchTrack this[string name]
        {
            get
            {
                var query = from track in this.TrackList
                            where track.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                            select track;

                if (query.Count() > 0)
                {
                    return query.First();
                }
                else
                {
                    return null;
                }
            }
        }
    }

    /// <summary>
    /// Deserialized object corresponding to a single track
    /// </summary>
    [Serializable]
    public class SearchTrack : Lookup.Response.BaseTrack
    {
        /// <summary>
        /// Href
        /// </summary>
        [XmlAttribute("href")]
        public string Href { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("id")]
        public TrackId Id { get; set; }

        /// <summary>
        /// Album
        /// </summary>
        [XmlElement("album")]
        public SearchTrackAlbum Album { get; set; }
    }

    /// <summary>
    /// Deserialized object corresponding to a single track album
    /// </summary>
    [Serializable]
    public class SearchTrackAlbum : Lookup.Response.BaseAlbum
    {
        /// <summary>
        /// Href
        /// </summary>
        [XmlAttribute("href")]
        public string Href { get; set; }

        /// <summary>
        /// Released year
        /// </summary>
        [XmlElement("released")]
        public int? Released { get; set; }
    }

    /// <summary>
    /// Deserialized object corresponding to a single track ID
    /// </summary>
    [Serializable]
    public class TrackId
    {
        /// <summary>
        /// Value
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}

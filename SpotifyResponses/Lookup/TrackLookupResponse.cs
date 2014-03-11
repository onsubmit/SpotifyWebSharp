using System;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyResponses.Lookup
{
    [XmlRootAttribute("track", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class Track : Response.BaseTrack
    {
        [XmlElement("album")]
        public Response.BaseAlbum Album { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("id")]
        public TrackId Id { get; set; }

        /// <summary>
        /// Href
        /// Not actually retrieved when using the Lookup service but I'm adding the property so it can be set dynamically
        /// </summary>
        public string Href { get; set; }
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

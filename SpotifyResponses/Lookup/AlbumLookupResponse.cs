using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyResponses.Lookup
{
    [XmlRootAttribute("album", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class Album : ArtistAlbum
    {
        [XmlArray("tracks")]
        [XmlArrayItem("track")]
        public List<ArtistTrack> Tracks { get; set; }
    }

    [Serializable]
    public class ArtistTrack : Response.BaseTrack
    {
        [XmlAttribute("href")]
        public string Href { get; set; }
    }

    [Serializable]
    public class ArtistId
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}

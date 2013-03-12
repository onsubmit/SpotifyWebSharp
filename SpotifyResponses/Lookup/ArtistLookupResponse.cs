using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpotifyWebApi.SpotifyResponses.Lookup
{
    [XmlRootAttribute("artist", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class BaseRootArtist : Response.BaseArtist
    {

    }

    [XmlRootAttribute("artist", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class Artist
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("albums")]
        [XmlArrayItem("album")]
        public List<ArtistAlbum> Albums { get; set; }
    }

    [Serializable]
    public class ArtistAlbum : Response.BaseAlbum
    {
        [XmlAttribute("href")]
        public string Href { get; set; }

        [XmlElement("id")]
        public AlbumId Id { get; set; }

        [XmlElement("released")]
        public int? Released { get; set; }

        [XmlElement("artist")]
        public BaseRootArtist Artist { get; set; }
    }

    [Serializable]
    public class AlbumId
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    [Serializable]
    public class Availability
    {
        [XmlElement("territories")]
        public string Territories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyResponses.Lookup
{
    public class Response
    {
        [Serializable]
        public class BaseArtist
        {
            [XmlElement("name")]
            public string Name { get; set; }

            [XmlAttribute("href")]
            public string Href { get; set; }
        }

        [Serializable]
        public class BaseAlbum
        {
            [XmlElement("name")]
            public string Name { get; set; }

            [XmlElement("availability")]
            public Availability Availability { get; set; }
        }

        [Serializable]
        public class BaseTrack
        {
            [XmlElement("name")]
            public string Name { get; set; }

            [XmlElement("artist")]
            public Response.BaseArtist Artist { get; set; }

            [XmlElement("available")]
            public bool? Available { get; set; }

            [XmlElement("disc-number")]
            public int? DiscNumber { get; set; }

            [XmlElement("track-number")]
            public int? TrackNumber { get; set; }

            [XmlElement("length")]
            public double? Length { get; set; }

            [XmlElement("popularity")]
            public double? Popularity { get; set; }
        }
    }
}

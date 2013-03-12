using System.Xml.Serialization;

namespace SpotifyWebApi.SpotifyResponses.Lookup
{
    [XmlRootAttribute("track", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class Track : Response.BaseTrack
    {
        [XmlElement("album")]
        public Response.BaseAlbum Album { get; set; }
    }
}

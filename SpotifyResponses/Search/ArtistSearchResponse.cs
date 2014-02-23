using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyResponses.Search
{
    /// <summary>
    /// Deserialized object corresponding to artist search results
    /// https://developer.spotify.com/technologies/web-api/search/
    /// </summary>
    [XmlType(Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    [XmlRootAttribute("artists", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class Artists : Response
    {
        /// <summary>
        /// Artist list
        /// </summary>
        [XmlElement("artist", Namespace = "http://www.spotify.com/ns/music/1")]
        public List<SearchArtist> ArtistList { get; set; }

        /// <summary>
        /// Artist list indexer by integer
        /// </summary>
        /// <param name="index">Index of artist to return</param>
        /// <returns>Artist corresponding to the given index in the list, null if not found</returns>
        public SearchArtist this[int index]
        {
            get
            {
                if (index < 0 || index >= this.ArtistList.Count)
                {
                    return null;
                }

                return this.ArtistList[index];
            }
        }

        /// <summary>
        /// Artist list indexer by name
        /// </summary>
        /// <param name="name">Name of artist to return</param>
        /// <returns>Artist with the given name, null if not found</returns>
        public SearchArtist this[string name]
        {
            get
            {
                var query = from artist in this.ArtistList
                            where artist.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                            select artist;

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
    /// Deserialized object corresponding to a single artist
    /// </summary>
    [Serializable]
    public class SearchArtist : Lookup.Response.BaseArtist
    {
        /// <summary>
        /// Popularity
        /// </summary>
        [XmlElement("popularity")]
        public double? Popularity { get; set; }
    }
}

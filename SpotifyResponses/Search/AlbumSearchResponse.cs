using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyResponses.Search
{
    /// <summary>
    /// Deserialized object corresponding to album search results
    /// https://developer.spotify.com/technologies/web-api/search/
    /// </summary>
    [XmlType(Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    [XmlRootAttribute("albums", Namespace = "http://www.spotify.com/ns/music/1", IsNullable = false)]
    public class Albums : Response
    {
        /// <summary>
        /// Album list
        /// </summary>
        [XmlElement("album", Namespace = "http://www.spotify.com/ns/music/1")]
        public List<SearchAlbum> AlbumList { get; set; }

        /// <summary>
        /// Album list indexer by integer
        /// </summary>
        /// <param name="index">Index of album to return</param>
        /// <returns>Album corresponding to the given index in the list, null if not found</returns>
        public SearchAlbum this[int index]
        {
            get
            {
                if (index < 0 || index >= this.AlbumList.Count)
                {
                    return null;
                }

                return this.AlbumList[index];
            }
        }

        /// <summary>
        /// Album list indexer by name
        /// </summary>
        /// <param name="name">Name of album to return</param>
        /// <returns>Album with the given name, null if not found</returns>
        public SearchAlbum this[string name]
        {
            get
            {
                var query = from album in this.AlbumList
                            where album.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                            select album;

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
    /// Deserialized object corresponding to a single album
    /// </summary>
    [Serializable]
    public class SearchAlbum : Lookup.Response.BaseAlbum
    {
        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("id")]
        public Lookup.AlbumId Id { get; set; }

        /// <summary>
        /// Artist
        /// </summary>
        [XmlElement("artist")]
        public Lookup.BaseRootArtist Artist { get; set; }

        /// <summary>
        /// Popularity
        /// </summary>
        [XmlElement("popularity")]
        public double? Popularity { get; set; }
    }
}

using System;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyResponses.Search
{
    /// <summary>
    /// Base deserialized object corresponding to search results
    /// https://developer.spotify.com/technologies/web-api/search/
    /// </summary>
    [XmlType(Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    public class Response
    {
        /// <summary>
        /// Query
        /// </summary>
        [XmlElement("Query")]
        public Query Query { get; set; }

        /// <summary>
        /// Total number of results
        /// </summary>
        [XmlElement("totalResults")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Start index
        /// </summary>
        [XmlElement("startIndex")]
        public int StartIndex { get; set; }

        /// <summary>
        /// Items per page
        /// </summary>
        [XmlElement("itemsPerPage")]
        public int ItemsPerPage { get; set; }
    }

    /// <summary>
    /// Deserialized object corresponding to the query response in the search results
    /// </summary>
    [Serializable]
    public class Query
    {
        /// <summary>
        /// Role
        /// </summary>
        [XmlAttribute("role")]
        public string Role { get; set; }

        /// <summary>
        /// Start page
        /// </summary>
        [XmlAttribute("startPage")]
        public string StartPage { get; set; }

        /// <summary>
        /// Search terms
        /// </summary>
        [XmlAttribute("searchTerms")]
        public string SearchTerms { get; set; }
    }
}

﻿using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SpotifyWebSharp.SpotifyServices
{
    /// <summary>
    /// Base service
    /// </summary>
    public class SpotifyService
    {
        /// <summary>
        /// Service base URL
        /// </summary>
        protected string BaseUrl { get; set; }

        /// <summary>
        /// Web request helper
        /// </summary>
        protected WebRequestHelper WebRequestHelper { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SpotifyService()
        {
            WebRequestHelper = new WebRequestHelper();
        }

        /// <summary>
        /// Handles the given HTTP status code
        /// </summary>
        /// <param name="code">HTTP status code</param>
        protected void HandleResponseStatusCode(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.OK :
                    break;
                case HttpStatusCode.NotModified :
                    break;
                case HttpStatusCode.BadRequest :
                    throw new Exception("400 BadRequest: The request was not understood. Used for example when a required parameter was omitted.");
                case HttpStatusCode.Forbidden :
                    throw new Exception("403 Forbidden: The rate limiting has kicked in.");
                case HttpStatusCode.NotFound :
                    throw new Exception("404 NotFound: The requested resource was not found. Also used if a format is requested using the url and the format isn’t available.");
                case HttpStatusCode.NotAcceptable:
                    throw new Exception("406 NotAcceptable: The requested format isn’t available.");
                case HttpStatusCode.InternalServerError:
                    throw new Exception("500 InternalServerError: The server encountered an unexpected problem. Should not happen.");
                case HttpStatusCode.ServiceUnavailable:
                    throw new Exception("503 ServiceUnavailable: The API is temporarily unavailable.");
            }
        }

        /// <summary>
        /// Gets the response from the REST interface
        /// </summary>
        /// <param name="uri">Uri to which to send the request</param>
        /// <returns>XML document</returns>
        protected XDocument GetResponse(string uri)
        {
            HttpWebResponse response;
            string responseString = this.WebRequestHelper.GetResponse(uri, out response);

            HandleResponseStatusCode(response.StatusCode);

            XDocument doc = null;
            if (!string.IsNullOrEmpty(responseString))
            {
                doc = XDocument.Parse(responseString);
            }

            return doc;
        }

        /// <summary>
        /// Deserialize the XML response
        /// </summary>
        /// <typeparam name="T">Output type</typeparam>
        /// <param name="doc">XML response</param>
        /// <returns>Track, Artist, or Album -- based on T</returns>
        protected T Deserialize<T>(XDocument doc) where T : class
        {
            T obj = null;

            if (doc != null)
            {
                using (XmlReader reader = doc.CreateReader())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    obj = serializer.Deserialize(reader) as T;
                }
            }

            return obj;
        }

        /// <summary>
        /// Serializes an object to XML string
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>XML representation of the object</returns>
        public static string Serialize<T>(T obj) where T : class
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Serializes an object to XML file
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <param name="path">XML file to write</param>
        public static void Serialize<T>(T obj, string path) where T : class
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(streamWriter, obj);
            }
        }

        /// <summary>
        /// Deserializes an XML string to an object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="xmlString">XML representation of object</param>
        /// <returns>Deserialized object</returns>
        public static T Deserialize<T>(string xmlString) where T : class
        {
            T obj = null;

            if (!string.IsNullOrEmpty(xmlString))
            {
                using (StringReader stringReader = new StringReader(xmlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    obj = serializer.Deserialize(stringReader) as T;
                }
            }

            return obj;
        }
    }
}

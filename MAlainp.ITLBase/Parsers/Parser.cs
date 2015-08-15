//  Parser.cs - Base class for other parsers for 
//  the http://laguna.snit.mx/ page.

//  Author: Alain Peralta <malain.pera@gmail.com>

//  Copyright(C) 2015 Alain Peralta

//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or(at your option) any later version.

//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
//  Lesser General Public License for more details.

//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
//  USA

using MAlainp.ITLBase.Extensors;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MAlainp.ITLBase.Parsers
{
    /// <summary>
    /// Base class for other parsers for the http://laguna.snit.mx/ page.
    /// This class prepares a web request (POST) for the given URL with the given params.
    /// </summary>
    /// <remarks>
    /// This class does not contains any fields which stores the HTML response the server returns. Inherited classes
    /// should retain the HTML response in some field.
    /// </remarks>
    public abstract class Parser
    {
        string url;
        string studentId;
        string password;
        readonly string data;
        readonly HttpWebRequest request;

        /// <summary>
        /// Gets a value indicating whether the requested URL is available.
        /// </summary>
        /// <value><c>true</c> if the URL is available; otherwise, <c>false</c>.</value>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Parsers.Parser"/> class
        /// with the given URL, Student ID and Password.
        /// </summary>
        /// <param name="url">The URL for doing the post request.</param>
        /// <param name="studentId">The Student identifier aka 'Numero de control'.</param>
        /// <param name="password">The Student Password.</param>
        protected Parser(string url, string studentId, string password)
        {
            this.url = url;
            this.studentId = studentId;
            this.password = password;
            data = "cCONTROL=" + studentId + "&cCONTRASENA=" + password;

            request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
        }

        /// <summary>
        /// Gets the request stream for write the data to the webpage.
        /// </summary>
        /// <returns>The request stream.</returns>
        Task<Stream> GetRequestStream()
        {
            return request.GetRequestStreamAsync();
        }

        /// <summary>
        /// Gets the response of the webpage.
        /// </summary>
        /// <returns>The response.</returns>
        Task<WebResponse> GetResponse()
        {
            return request.GetResponseAsync();
        }

        /// <summary>
        /// Post data to the web page and returns a string which contains the full HTML response.
        /// </summary>
        public async Task<string> Post()
        {
            using (var w = new StreamWriter(await GetRequestStream()))
            {
                w.Write(data);
                w.Flush();
                w.Dispose();
            }
            var r = await GetResponse();
            IsAvailable = (r as HttpWebResponse).StatusCode == HttpStatusCode.OK;
            using (var sr = new StreamReader(r.GetResponseStream()))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public abstract Task<bool> ParseHTMLAsync();
    }
}

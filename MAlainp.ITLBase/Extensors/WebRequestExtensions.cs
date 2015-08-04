//  WebRequestExtensions.cs - Adds a couple of extension methods to the WebRequest class.

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

using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MAlainp.ITLBase.Extensors
{
    /// <summary>
	/// This static class adds a couple of extension methods to the WebRequest class for working asynchronously with 
	/// some stuff.
	/// </summary>
	public static class WebRequestExtensions
    {
        public static Task<WebResponse> GetResponseAsync(this WebRequest request)
        {
            return Task.Factory.StartNew<WebResponse>(() => {
                var t = Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse,
                    request.EndGetResponse,
                    null);
                t.Wait();

                return t.Result;
            });
        }

        public static Task<Stream> GetRequestStreamAsync(this WebRequest request)
        {
            return Task.Factory.StartNew<Stream>(() => {
                var t = Task.Factory.FromAsync<Stream>(
                    request.BeginGetRequestStream,
                    request.EndGetRequestStream,
                    null);
                t.Wait();
                return t.Result;
            });
        }
    }
}

//  StringExtensions.cs - Adds a couple of extension methods to the String class.

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

namespace MAlainp.ITLBase.Extensors
{
    /// <summary>
	///This static class adds extension methods to the String class for working with some messed stuff 
	/// of ITL web page.
	/// </summary>
	public static class StringExtensions
    {
        public static string ReplaceHTMLAccents(this string s)
        {
            return s.Replace("&aacute;", "á")
                    .Replace("&eacute;", "é")
                    .Replace("&iacute;", "í")
                    .Replace("&oacute;", "ó")
                    .Replace("&uacute;", "ú")
                    .Replace("&Aacute;", "Á")
                    .Replace("&Eacute;", "É")
                    .Replace("&Iacute;", "Í")
                    .Replace("&Oacute;", "Ó")
                    .Replace("&Uacute;", "Ú");
        }

        public static string ReplaceSquareBracketsToAccents(this string s)
        {
            if (s.Contains("[A"))
            {
                s = s.Replace("[A", "Á");
            }
            if (s.Contains("[E"))
            {
                s = s.Replace("[E", "É");
            }
            if (s.Contains("[I"))
            {
                s = s.Replace("[I", "Í");
            }
            if (s.Contains("[O"))
            {
                s = s.Replace("[O", "Ó");
            }
            if (s.Contains("[U"))
            {
                s = s.Replace("[U", "Ú");
            }
            return s;
        }
    }
}

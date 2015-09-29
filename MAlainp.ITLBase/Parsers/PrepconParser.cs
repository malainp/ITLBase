//  PrepconParser.cs - Parses the html of 
//  http://www.itlalaguna.edu.mx/servicios/escolares/estatus_alumno/prepcon.asp.

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

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAlainp.ITLBase.Parsers
{
    /// <summary>
    /// Prepcon parser parses the html of http://www.itlalaguna.edu.mx/servicios/escolares/estatus_alumno/prepcon.asp.
    /// It extracts the name of the student if available identified with the pair Id-Password 
    /// </summary>
    public class PrepconParser : Parser
    {
        const string CHAR = "�";
        string html;

        /// <summary>
        /// Gets the name of the student.
        /// </summary>
        /// <value>The name of the student.</value>
        public string StudentName { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Parsers.PrepconParser"/> class.
        /// </summary>
        /// <param name="url">The URL for doing the post request.</param>
        /// <param name="studentId">The Student identifier aka 'Numero de control'.</param>
        /// <param name="password">The Student Password.</param>
        public PrepconParser(string url, string studentId, string password)
            : base(url, studentId, password)
        {
        }

        /// <summary>
        /// Parses the HTML and extracts the student name from the page body.
        /// </summary>
        /// <returns><c>true</c>, if the student name was parsed, <c>false</c> otherwise.</returns>
        public override async Task<bool> ParseHTMLAsync()
        {
            html = await Post();

            var regexName = new Regex("Alumno :[@A-Z" + CHAR + " ]*");
            Match m = regexName.Match(html);
            if (m.Success)
            {
                StudentName = m.Value.Replace(CHAR, "Ñ").Replace("@", "").Replace("Alumno :", "");
                return true;
            }
            return false;
        }
    }
}

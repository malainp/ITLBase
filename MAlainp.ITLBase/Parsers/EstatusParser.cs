//  EstatusParser.cs - Parses the html of 
//  http://www.itlalaguna.edu.mx/servicios/escolares/estatus_alumno/estatuscbb.asp.

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

using MAlainp.ITLBase.Items;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAlainp.ITLBase.Parsers
{
    public class EstatusParser : Parser
    {
        const string studentIdRegex = "[0-9]{8}";
        const string studentName = "face=\"Arial\">[@A-Z�\\? ]+&nbsp;";
        const string studentStatus = "<font size=\"2\" face=\"Arial\">[A-Za-z ]+</font>";
        const string studentCarrer = "<font size=\"2\" face=\"Arial\">&nbsp;[A-Z ]+";
        const string studentSemester = "<font size=\"2\" face=\"Arial\">&nbsp;[0-9]+";

        string html;

        /// <summary>
        /// Gets an object with the student's information
        /// </summary>
        public Student Student { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="MAlainp.ITLBase.Parsers.EstatusParser"/> class.
        /// </summary>
        /// <param name="url">The URL for doing the post request.</param>
        /// <param name="studentId">The Student identifier aka 'Numero de control'.</param>
        /// <param name="password">The Student Password.</param>
        public EstatusParser(string url, string studentId, string password)
            : base(url, studentId, password)
        {

        }

        /// <summary>
        /// Parses the HTML and extracts the Student Id, Name, Status, Semester and carrer.
        /// </summary>
        /// <returns><c>true</c>, if HTML was parsed, <c>false</c> otherwise.</returns>
        public override async Task<bool> ParseHTMLAsync()
        {
            html = await Post();

            var rexSId = new Regex(studentIdRegex);
            var sId = int.Parse(rexSId.Match(html).ToString().Trim());

            var rexSName = new Regex(studentName);
            var sName = rexSName.Match(html).ToString();
            sName = sName.Remove(sName.IndexOf('&')).Substring(sName.IndexOf('>') + 1).Replace('�', 'Ñ');

            var rexStatus = new Regex(studentStatus);
            var sStatus = rexStatus.Match(html).ToString();
            sStatus = sStatus.Substring(sStatus.IndexOf('>') + 1);
            sStatus = sStatus.Remove(sStatus.IndexOf('<'));

            var rexCarrer = new Regex(studentCarrer);
            var sCarrer = rexCarrer.Match(html).ToString();
            sCarrer = sCarrer.Substring(sCarrer.IndexOf(';') + 1).Trim();

            var rexSemester = new Regex(studentSemester);
            var sSemester = rexSemester.Match(html).ToString().Trim();
            var iSemester = int.Parse(sSemester.Substring(sSemester.IndexOf(';') + 1));

            Student = new Student(sId, sName, sStatus, sCarrer, iSemester);

            return true;
        }
    }
}

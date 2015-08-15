//  KardexParser.cs - Parses the html of 
//  http://www.itlalaguna.edu.mx/servicios/escolares/estatus_alumno/kardex.asp.

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
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAlainp.ITLBase.Parsers
{
    /// <summary>
    /// Kardex parser parses the html of http://www.itlalaguna.edu.mx/servicios/escolares/estatus_alumno/kardex.asp.
    /// It extracts the whole academic history from the table in the page and hold it into a list.
    /// </summary>
    public class KardexParser : Parser
    {
        /// <summary>
        /// The course identifier regex used to extract each course Id in the table.
        /// </summary>
        const string courseIdRegex = "<center>[A-Z][0-9]{2}";

        /// <summary>
        /// The course name regex used to extract each course name in the table.
        /// </summary>
        const string courseNameRegex = "<td bgcolor='#[A-F0-9]*'><center>[A-Z\\\\. ]*</center></td>";

        /// <summary>
        /// The course info regex used to extract the info of each course in the table.
        /// </summary>
        const string courseInfoRegex = "<center>[0-9]+</center>";

        /// <summary>
        /// The course attempt regex used to extract the obtained attemp of each course in the table.
        /// </summary>
        const string courseAttemptRegex = "<td bgcolor='#[A-F0-9]*'><center></center>([A-Za-z .\\\\&aacute\\\\&eacute\\\\&iacute\\\\&oacute\\\\&uacute;]*)</td>";

        readonly List<string> ids;
        readonly List<string> names;
        readonly List<string> credits;
        readonly List<string> semesters;
        readonly List<string> grades;
        readonly List<string> attempts;

        string html;

        /// <summary>
        /// Gets a list with all the kardex courses.
        /// </summary>
        /// <value>The kardex courses.</value>
        public List<KardexCourse> KardexCourses { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Parsers.KardexParser"/> class.
        /// </summary>
        /// <param name="url">The URL for doing the post request.</param>
        /// <param name="studentId">The Student identifier aka 'Numero de control'.</param>
        /// <param name="password">The Student Password.</param>
        public KardexParser(string url, string studentId, string password)
            : base(url, studentId, password)
        {
            ids = new List<string>();
            names = new List<string>();
            credits = new List<string>();
            semesters = new List<string>();
            grades = new List<string>();
            attempts = new List<string>();
            KardexCourses = new List<KardexCourse>();
        }

        /// <summary>
        /// Parses the HTML and extracts the CourseId, Course Name, Course Credits, Course Semester, Course grade
        /// and the Course Attempt.
        /// </summary>
        /// <returns><c>true</c>, if HTML was parsed, <c>false</c> otherwise.</returns>
        public override async Task<bool> ParseHTMLAsync()
        {
            html = await Post();

            var rexIds = new Regex(courseIdRegex);
            foreach (var courseMatch in rexIds.Matches(html))
            {
                ids.Add(courseMatch.ToString().Replace("<center>", ""));
            }

            var rexNames = new Regex(courseNameRegex);
            foreach (var nameMatch in rexNames.Matches(html))
            {
                names.Add(nameMatch.ToString().Replace("</center></td>", "").Substring(30));
            }

            var rexInfo = new Regex(courseInfoRegex);
            var infoMatches = rexInfo.Matches(html);
            for (int i = 0; i < infoMatches.Count; i++)
            {
                credits.Add(infoMatches[i++].ToString().Replace("</center>", "").Replace("<center>", ""));
                semesters.Add(infoMatches[i++].ToString().Replace("</center>", "").Replace("<center>", ""));
                grades.Add(infoMatches[i].ToString().Replace("</center>", "").Replace("<center>", ""));
            }

            var rexAttemps = new Regex(courseAttemptRegex);
            foreach (var attempMatch in rexAttemps.Matches(html))
            {
                attempts.Add(attempMatch.ToString().Replace("</td>", "").Substring(39));
            }

            for (int i = 0; i < ids.Count; i++)
            {
                KardexCourses.Add(new KardexCourse(ids[i],
                    names[i],
                    int.Parse(credits[i]),
                    int.Parse(semesters[i]),
                    int.Parse(grades[i]),
                    attempts[i]));
            }

            return true;
        }
    }
}

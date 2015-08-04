//  CursandoParser.cs - Parses the html of 
//  http://www.itlalaguna.edu.mx/servicios/escolares/estatus_alumno/matcursa.asp.

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
using MAlainp.ITLBase.Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAlainp.ITLBase.Parsers
{
    /// <summary>
    /// Cursando parser parses the html of http://www.itlalaguna.edu.mx/servicios/escolares/estatus_alumno/matcursa.asp.
    /// It extracts the courses within the table in the page and holds them into a list
    /// </summary>
    public class CursandoParser : Parser
    {
        /// <summary>
        /// The group identifier regex.
        /// </summary>
        const string GroupIdRegex = "[A-Z][0-9][0-9]</font>[A-Z]";

        /// <summary>
        /// The course name regex.
        /// </summary>
        const string CourseNameRegex = "\"#000000\">[A-Z \\[]*<";

        /// <summary>
        /// The course info regex.
        /// </summary>
        const string CourseInfoRegex = "\"#000000\">(0/|[0-9]*/[0-9A-Z]*)";

        string html;
        readonly List<string> groups;
        readonly List<string> courseNames;

        /// <summary>
        /// Gets the enrolledin courses.
        /// </summary>
        /// <value>The enrolledin courses.</value>
        public List<EnrolledinCourse> EnrolledinCourses { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Parsers.CursandoParser"/> class.
        /// </summary>
        /// <param name="url">The URL for doing the post request.</param>
        /// <param name="studentId">The Student identifier aka 'Numero de control'.</param>
        /// <param name="password">The Student Password.</param>
        public CursandoParser(string url, string studentId, string password)
            : base(url, studentId, password)
        {
            groups = new List<string>();
            courseNames = new List<string>();
            EnrolledinCourses = new List<EnrolledinCourse>();
        }

        /// <summary>
        /// Do the POST request to the web page and stores the HTML response. 
        /// </summary>
        public async Task DoPost()
        {
            html = await Post();
        }

        /// <summary>
        /// Parses the HTML and extracts the CourseId, Course Name and Course Info
        /// </summary>
        /// <returns><c>true</c>, if HTML was parsed, <c>false</c> otherwise.</returns>
        public override bool ParseHTML()
        {
            var rexGroups = new Regex(GroupIdRegex);
            foreach (var groupMatch in rexGroups.Matches(html))
            {
                groups.Add(groupMatch.ToString().Replace("</font>", ""));
            }

            var rexCourses = new Regex(CourseNameRegex);
            foreach (var nameMatch in rexCourses.Matches(html))
            {
                courseNames.Add(nameMatch.ToString().Replace("\"#000000\">", "").Replace("<", ""));
            }

            var rexCourseInfo = new Regex(CourseInfoRegex);
            var infoMatches = rexCourseInfo.Matches(html);
            int i = 0;
            int item = 0;
            while (i < infoMatches.Count)
            {
                string monday, tuesday, wednesday, thursday, friday;
                monday = infoMatches[i++].ToString().Replace("\"#000000\">", "");
                tuesday = infoMatches[i++].ToString().Replace("\"#000000\">", "");
                wednesday = infoMatches[i++].ToString().Replace("\"#000000\">", "");
                thursday = infoMatches[i++].ToString().Replace("\"#000000\">", "");
                friday = infoMatches[i++].ToString().Replace("\"#000000\">", "");

                const string freeHour = "0/";
                const string noClassRoom = "NC";

                EnrolledinCourses.Add(new EnrolledinCourse(groups[item],
                    courseNames[item++].ReplaceSquareBracketsToAccents(),
                    monday == freeHour ? Hour.FreeHour : GetHour(monday.Substring(0, monday.IndexOf('/')).Length == 4 ? monday.Substring(0, 2) : monday.Substring(0, 1)),
                    monday == freeHour ? Hour.FreeHour : GetHour(monday.Substring(0, monday.IndexOf('/')).Length == 4 ? monday.Substring(2, 4) : monday.Substring(1, monday.IndexOf('/'))),
                    tuesday == freeHour ? Hour.FreeHour : GetHour(tuesday.Substring(0, tuesday.IndexOf('/')).Length == 4 ? tuesday.Substring(0, 2) : tuesday.Substring(0, 1)),
                    tuesday == freeHour ? Hour.FreeHour : GetHour(tuesday.Substring(0, tuesday.IndexOf('/')).Length == 4 ? tuesday.Substring(2, 4) : tuesday.Substring(1, tuesday.IndexOf('/'))),
                    wednesday == freeHour ? Hour.FreeHour : GetHour(wednesday.Substring(0, wednesday.IndexOf('/')).Length == 4 ? wednesday.Substring(0, 2) : wednesday.Substring(0, 1)),
                    wednesday == freeHour ? Hour.FreeHour : GetHour(wednesday.Substring(0, wednesday.IndexOf('/')).Length == 4 ? wednesday.Substring(2, 4) : wednesday.Substring(1, wednesday.IndexOf('/'))),
                    thursday == freeHour ? Hour.FreeHour : GetHour(thursday.Substring(0, thursday.IndexOf('/')).Length == 4 ? thursday.Substring(0, 2) : thursday.Substring(0, 1)),
                    thursday == freeHour ? Hour.FreeHour : GetHour(thursday.Substring(0, thursday.IndexOf('/')).Length == 4 ? thursday.Substring(2, 4) : thursday.Substring(1, thursday.IndexOf('/'))),
                    friday == freeHour ? Hour.FreeHour : GetHour(friday.Substring(0, friday.IndexOf('/')).Length == 4 ? friday.Substring(0, 2) : friday.Substring(0, 1)),
                    friday == freeHour ? Hour.FreeHour : GetHour(friday.Substring(0, friday.IndexOf('/')).Length == 4 ? friday.Substring(2, 4) : friday.Substring(1, friday.IndexOf('/'))),
                    monday == freeHour ? noClassRoom : monday.Substring(monday.IndexOf('/') + 1, monday.Length),
                    tuesday == freeHour ? noClassRoom : tuesday.Substring(tuesday.IndexOf('/') + 1, tuesday.Length),
                    wednesday == freeHour ? noClassRoom : wednesday.Substring(wednesday.IndexOf('/') + 1, wednesday.Length),
                    thursday == freeHour ? noClassRoom : thursday.Substring(thursday.IndexOf('/') + 1, thursday.Length),
                    friday == freeHour ? noClassRoom : friday.Substring(friday.IndexOf('/') + 1, friday.Length)
                ));
            }
            return true;
        }

        /// <summary>
        /// Converts an string into an <see cref="ITLBase.Items.Hour"/>.
        /// </summary>
        /// <returns>The hour.</returns>
        /// <param name="hour">A string with an hour.</param>
        static Hour GetHour(string hour)
        {
            int x = Int32.Parse(hour);
            Hour h = Hour.FreeHour;
            switch (x)
            {
                case 7:
                    h = Hour.AM_Seven;
                    break;
                case 8:
                    h = Hour.AM_Eight;
                    break;
                case 9:
                    h = Hour.AM_Nine;
                    break;
                case 10:
                    h = Hour.AM_Ten;
                    break;
                case 11:
                    h = Hour.AM_Eleven;
                    break;
                case 12:
                    h = Hour.PM_Twelve;
                    break;
                case 13:
                    h = Hour.PM_One;
                    break;
                case 14:
                    h = Hour.PM_Two;
                    break;
                case 15:
                    h = Hour.PM_Three;
                    break;
                case 16:
                    h = Hour.PM_Four;
                    break;
                case 17:
                    h = Hour.PM_Five;
                    break;
                case 18:
                    h = Hour.PM_Six;
                    break;
                case 19:
                    h = Hour.PM_Seven;
                    break;
                case 20:
                    h = Hour.PM_Eight;
                    break;
                case 21:
                    h = Hour.PM_Nine;
                    break;
                case 0:
                    h = Hour.FreeHour;
                    break;
            }
            return h;
        }
    }
}

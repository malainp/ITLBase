//  EnrolledinCourse.cs - Holds the information of one course

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
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using MAlainp.ITLBase.Extensors;

namespace MAlainp.ITLBase.Items
{
    /// <summary>
    /// Enrolledin course class holds the information of one course (Group Id, Group Name and Classrooms).
    /// </summary>
    public class EnrolledinCourse
    {
        const int Start = 0;
        const int Finish = 1;

        /// <summary>
        /// Holds the Id given by the SQLite database
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets the group Id.
        /// </summary>
        /// <value>The group.</value>
        public string Group { get; private set; }

        /// <summary>
        /// Gets the group name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the list with the course information
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CourseHour> Hours { get; set; }

        /// <summary>
        /// Initializes a new empty instance of the <see cref="ITLBase.Items.EnrolledinCourse"/> class.
        /// </summary>
        public EnrolledinCourse() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Items.EnrolledinCourse"/> class.
        /// </summary>
        /// <param name="group">Course Group Id.</param>
        /// <param name="name">Course Name.</param>
        public EnrolledinCourse(string group, string name)
        {
			Group = group;
			Name = name.Capitalize ();
            Hours = new List<CourseHour>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Items.EnrolledinCourse"/> class.
        /// </summary>
        /// <param name="group">Course Group Id.</param>
        /// <param name="name">Course Name.</param>
        /// <param name="sMonday">Course Start hour on monday.</param>
        /// <param name="fMonday">Course Finish hour on monday.</param>
        /// <param name="sTuesday">Course Start hour tuesday.</param>
        /// <param name="fTuesday">Course Finish hour on tuesday.</param>
        /// <param name="sWednesday">Course Start hour on wednesday.</param>
        /// <param name="fWednesday">Course Finish hour on wednesday.</param>
        /// <param name="sThursday">Course Start hour on thursday.</param>
        /// <param name="fThursday">Course Finish hour on thursday.</param>
        /// <param name="sFriday">Course Start hour on friday.</param>
        /// <param name="fFriday">Course Finish hour on friday.</param>
        /// <param name="crMonday">Course Classroom on monday.</param>
        /// <param name="crTuesday">Course Classroom on tuesday.</param>
        /// <param name="crWednesday">Course Classroom on wednesday.</param>
        /// <param name="crThursday">Course Classroom on thursday.</param>
        /// <param name="crFriday">Course Classroom on friday.</param>
        public EnrolledinCourse(string group, string name,
                                 Hour sMonday, Hour fMonday,
                                 Hour sTuesday, Hour fTuesday,
                                 Hour sWednesday, Hour fWednesday,
                                 Hour sThursday, Hour fThursday,
                                 Hour sFriday, Hour fFriday,
                                 string crMonday,
                                 string crTuesday,
                                 string crWednesday,
                                 string crThursday,
                                 string crFriday) : this(group, name)
        {
            Hours.Add(new CourseHour(sMonday, fMonday, crMonday, this));
            Hours.Add(new CourseHour(sTuesday, fTuesday, crTuesday, this));
            Hours.Add(new CourseHour(sWednesday, fWednesday, crWednesday, this));
            Hours.Add(new CourseHour(sThursday, fThursday, crThursday, this));
            Hours.Add(new CourseHour(sFriday, fFriday, crFriday, this));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Items.EnrolledinCourse"/> class.
        /// </summary>
        /// <param name="group">Course Group Id.</param>
        /// <param name="name">Course Name.</param>
        /// <param name="hours">Array of hours [start, finish]</param>
        /// <param name="classrooms">Array of classrooms.</param>
        public EnrolledinCourse(string group, string name, Hour[,] hours, string[] classrooms) : this(group, name)
        {
            foreach (var day in Enum.GetValues(typeof(Day)))
            {
                Hours.Add(new CourseHour(hours[(int)day, Start],hours[(int)day, Finish],classrooms[(int)day], this));
            }
        }

        /// <summary>
        /// Gets Starts the hour for a course in the specified day.
        /// </summary>
        /// <returns>The start hour.</returns>
        /// <param name="d">The day</param>
        public Hour StartHour(Day d)
        {
            return Hours[(int)d].StartHour;
        }

        /// <summary>
        /// Gets the Finish hour for a course in the specified day
        /// </summary>
        /// <returns>The finish hour.</returns>
        /// <param name="d">The day</param>
        public Hour FinishHour(Day d)
        {
            return Hours[(int)d].FinishHour;
        }

        /// <summary>
        /// Gets the classroom for a course in the specified day.
        /// </summary>
        /// <returns>The classroom</returns>
        /// <param name="d">The day</param>
        public string Classroom(Day d)
        {
            return Hours[(int)d].Classroom;
        }
    }
}

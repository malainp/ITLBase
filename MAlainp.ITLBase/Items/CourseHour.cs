//  CourseHour.cs - Holds the information of one course

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

using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace MAlainp.ITLBase.Items
{
    public class CourseHour
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

		/// <summary>
		/// Gets or sets the course identifier.
		/// </summary>
		/// <value>The course identifier.</value>
        [ForeignKey(typeof(EnrolledinCourse))]
        public int CourseId { get; set; }

		/// <summary>
		/// Gets or sets the start hour.
		/// </summary>
		/// <value>The start hour.</value>
        public Hour StartHour { get; private set; }

		/// <summary>
		/// Gets or sets the finish hour.
		/// </summary>
		/// <value>The finish hour.</value>
        public Hour FinishHour { get; private set; }

		/// <summary>
		/// Gets or sets the classroom.
		/// </summary>
		/// <value>The classroom.</value>
        public string Classroom { get; private set; }

        [ManyToOne]
        public EnrolledinCourse Course { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAlainp.ITLBase.Items.CourseHour"/> class.
        /// </summary>
        public CourseHour() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="MAlainp.ITLBase.Items.CourseHour"/> class.
		/// </summary>
		/// <param name="startHour">Start hour.</param>
		/// <param name="finishHour">Finish hour.</param>
		/// <param name="classroom">Classroom.</param>
		/// <param name="course">Course.</param>
        public CourseHour(Hour startHour, Hour finishHour, string classroom, EnrolledinCourse course)
        {
            StartHour = startHour;
            FinishHour = finishHour;
            Classroom = classroom;
        }
    }
}

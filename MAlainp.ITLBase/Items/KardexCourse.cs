//  KardexCourse.cs - Holds the information of one course into the student's Kardex.

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

namespace MAlainp.ITLBase.Items
{
    /// <summary>
    /// Kardex course holds the information of one course into the student's Kardex
    /// </summary>
    public class KardexCourse
    {
        /// <summary>
        /// Gets the course identifier.
        /// </summary>
        /// <value>The course identifier.</value>
        public string CourseId { get; private set; }

        /// <summary>
        /// Gets the name of the course.
        /// </summary>
        /// <value>The name of the course.</value>
        public string CourseName { get; private set; }

        /// <summary>
        /// Gets the credits given by the course.
        /// </summary>
        /// <value>The course credits.</value>
        public int CourseCredits { get; private set; }

        /// <summary>
        /// Gets the course semester.
        /// </summary>
        /// <value>The course semester.</value>
        public int CourseSemester { get; private set; }

        /// <summary>
        /// Gets the obtained course grade.
        /// </summary>
        /// <value>The course grade.</value>
        public int CourseGrade { get; private set; }

        /// <summary>
        /// Gets the course oportunity.
        /// </summary>
        /// <value>The course oportunity.</value>
        public string CourseOportunity { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Items.KardexCourse"/> class.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        /// <param name="courseName">Course name.</param>
        /// <param name="courseCredits">Course credits.</param>
        /// <param name="courseSemester">Course semester.</param>
        /// <param name="courseGrade">Course grade.</param>
        /// <param name="courseOportunity">Course oportunity.</param>
        public KardexCourse(string courseId, string courseName, int courseCredits,
                             int courseSemester, int courseGrade, string courseOportunity)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseCredits = courseCredits;
            CourseSemester = courseSemester;
            CourseGrade = courseGrade;
            CourseOportunity = courseOportunity.ReplaceHTMLAccents();
        }
    }
}

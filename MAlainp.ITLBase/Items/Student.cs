//  Student.cs - Holds the student's information in the url
//              /servicios/escolares/estatus_alumno/estatuscbb.asp

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
using MAlainp.ITLBase.Extensors;

namespace MAlainp.ITLBase.Items
{
    public class Student
    {
        /// <summary>
        /// Holds the student's Id.
        /// </summary>
        [PrimaryKey]
        public int Id { get; private set; }

        /// <summary>
        /// The student's name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// The student's status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; private set; }

        /// <summary>
        /// The student's carrer.
        /// </summary>
        /// <value>The carrer</value>
        public string Carrer { get; private set; }

        /// <summary>
        /// The student's semester.
        /// </summary>
        /// <value>The semester.</value>
        public int Semester { get; private set; }

        /// <summary>
        /// Initializes a new empty instance of the <see cref="ITLBase.Items.Student"/> class.
        /// </summary>
		public Student() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ITLBase.Items.Student"/> class.
        /// </summary>
        /// <param name="id">Student's Id.</param>
        /// <param name="name">Student's name.</param>
        /// <param name="status">Student's status.</param>
        /// <param name="carrer">Student's carrer.</param>
        /// <param name="semester">Student's semester.</param>
        public Student(int id, string name, string status, string carrer, int semester)
        {
            Id = id;
			Name = name.Capitalize();
			Status = status.Capitalize();
			Carrer = carrer.Capitalize();
            Semester = semester;
        }

    }
}

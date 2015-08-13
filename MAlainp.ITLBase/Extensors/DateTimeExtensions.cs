//  DateTimeExtensions.cs - Adds some extension methods to the DateTime struct

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

namespace MAlainp.ITLBase.Extensors
{
    /// <summary>
	///This static class adds extension methods to the DateTime struct
	/// </summary>
    public static class DateTimeExtensions
    {
        public static Day DayOfWeekToDay(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                default:
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                    return Day.Monday;
                case DayOfWeek.Tuesday:
                    return Day.Tuesday;
                case DayOfWeek.Wednesday:
                    return Day.Wednesday;
                case DayOfWeek.Thursday:
                    return Day.Thursday;
                case DayOfWeek.Friday:
                    return Day.Friday;     
            }
        }
    }
}

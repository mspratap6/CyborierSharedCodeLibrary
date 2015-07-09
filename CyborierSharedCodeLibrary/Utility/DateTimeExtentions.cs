/*
 * **************************************************************************
 * Developer : Pratap Singh, Manish (mpratap@Cyboriercompany.com)              *
 * Date : "29-09-2013"                                                   	*
 * Copyright  © 2013 Cyborier Systems Pvt Ltd. - All Rights Reserved   *
 * Unauthorized copying of this file, via any medium is strictly prohibited *
 * Proprietary and confidential.                                            *
 * **************************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyborier.Shared.Utility
{
    /// <summary>
    /// class to hold extension methods for the DateTime.
    /// </summary>
    public static class DateTimeExtentions
    {
        /// <summary>
        /// Get POSIX Time Stamp accurate to 1 Microseconds
        /// </summary>
        /// <param name="datetime">
        /// DateTime for which to get the TimeStamp
        /// </param>
        /// <returns>
        /// POSIX TimeStamp accurate to 1 microSeconds
        /// </returns>
        public static ulong GetPosixTimeStamp(this DateTime datetime)
        {
            DateTime epoch  = new DateTime(1970, 1, 1);
            return (ulong)(DateTime.UtcNow - epoch).Ticks / 10;
        }
    }
}

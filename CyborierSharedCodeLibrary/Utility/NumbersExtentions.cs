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
    public static class NumbersExtentions
    {
        /// <summary>
        /// Get binary String Representation of a Uint32
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string Get32BitBinary(this UInt32 state)
        {
            string binary = Convert.ToString(state, 2);
            string strprefix = "0b";
            for (int i = 0; i < 32 - binary.Length; i++)
            {
                strprefix += "0";
            }
            return strprefix + binary;
        }
    }
}

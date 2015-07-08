/*
 * **************************************************************************
 * Developer : Pratap Singh, Manish (mpratap@Cyboriercompany.com)              *
 * Date : 23/10/2012                                                        *
 * Copyright  © 2012 Cyborier Sistemas, India Pvt Ltd. - All Rights Reserved   *
 * Unauthorized copying of this file, via any medium is strictly prohibited *
 * Proprietary and confidential.                                             *
 * **************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyborier.Shared.Database.Exceptions
{
    /// <summary>
    /// DB not connected Exception
    /// </summary>
    [Serializable]
    public class DBNotConnectedException : Exception
    {
        public DBNotConnectedException() { }
        public DBNotConnectedException(string message) : base(message) { }
        public DBNotConnectedException(string message, Exception inner) : base(message, inner) { }
        protected DBNotConnectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

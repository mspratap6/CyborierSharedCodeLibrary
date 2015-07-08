using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Diagnostics;
using System.Reflection;

namespace Cyborier.Shared.ThirdParty.Log4Net
{
    /// <summary>
    /// Manage Loggers 
    /// </summary>
    public static class LogManager
    {
        #region variables
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(LogManager));
        #endregion

        #region Constractors
        /// <summary>
        /// Initiate new instace of LogManager
        /// </summary>
        static LogManager()
        {
            log.Debug("Initiating Log Manager");
            log.Debug("Log manager Initiation complete");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the Logger For the Calling Assambly Name
        /// </summary>
        /// <returns>
        /// Logger for the Log
        /// </returns>
        public static log4net.ILog Getlogger()
        {
            // get call stack
            StackTrace stackTrace = new StackTrace();
            // get the First StackFrame                        
            StackFrame stackFrame = stackTrace.GetFrame(1);
            // get calling method
            MethodBase caller = stackFrame.GetMethod();

            if (caller != null && caller.DeclaringType != null)
                return log4net.LogManager.GetLogger(caller.DeclaringType.FullName /* + caller.Name*/);
            // it was everytime displaying that .ctor ,so removed it .
            return log4net.LogManager.GetLogger(typeof(LogManager)); //TODO: what the hell. check this , its not the perfect way.
        }
        #endregion
    }
}

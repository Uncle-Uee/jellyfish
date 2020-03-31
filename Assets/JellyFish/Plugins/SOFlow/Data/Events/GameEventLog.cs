// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using SOFlow.Data.Events;

namespace SOFlow
{
    public class GameEventLog
    {
        /// <summary>
        ///     The error data.
        /// </summary>
        public List<List<GameEventErrorData>> ErrorData = new List<List<GameEventErrorData>>();

        /// <summary>
        ///     The error messages.
        /// </summary>
        public List<string> ErrorMessages = new List<string>();

        /// <summary>
        ///     Indicates whether this log produced an error.
        /// </summary>
        public List<bool> IsError = new List<bool>();

        /// <summary>
        ///     The event listener.
        /// </summary>
        public List<IEventListener> Listener;

        /// <summary>
        ///     The event log time.
        /// </summary>
        public DateTime LogTime;

        /// <summary>
        ///     The error stack traces.
        /// </summary>
        public List<string> StackTraces = new List<string>();

        public GameEventLog(List<IEventListener> listener, DateTime logTime)
        {
            Listener = listener;
            LogTime  = logTime;
        }
    }
}
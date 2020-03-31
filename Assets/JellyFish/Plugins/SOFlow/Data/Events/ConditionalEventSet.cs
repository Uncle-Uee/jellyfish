// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;

namespace SOFlow.Data.Events
{
    [Serializable]
    public class ConditionalEventSet
    {
	    /// <summary>
	    ///     The list of conditional events.
	    /// </summary>
	    public List<ConditionalEvent> Conditions = new List<ConditionalEvent>();

	    /// <summary>
	    ///     Evaluates all conditions for this condition set.
	    /// </summary>
	    /// <returns></returns>
	    public bool Evaluate()
        {
            foreach(ConditionalEvent condition in Conditions)
                if(!condition.Invoke())
                    return false;

            return true;
        }
    }
}
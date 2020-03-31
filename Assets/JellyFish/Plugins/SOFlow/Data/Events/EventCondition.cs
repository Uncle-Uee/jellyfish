// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using SOFlow.Data.Primitives;

namespace SOFlow.Data.Events
{
    [Serializable]
    public class EventCondition
    {
	    /// <summary>
	    ///     The condition to test against.
	    /// </summary>
	    public ConditionalEvent Condition = new ConditionalEvent();

	    /// <summary>
	    ///     Indicates whether this condition should be inverted.
	    /// </summary>
	    public BoolField InvertCondition = new BoolField();

	    /// <summary>
	    ///     Evaluates the event condition.
	    /// </summary>
	    /// <returns></returns>
	    public bool Evaluate()
	    {
		    bool result = Condition.Invoke();
		    
            if(InvertCondition) return !result;

            return result;
        }
    }
}
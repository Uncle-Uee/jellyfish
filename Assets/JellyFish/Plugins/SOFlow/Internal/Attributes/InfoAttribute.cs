// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using UnityEngine;

namespace SOFlow.Internal
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InfoAttribute : PropertyAttribute
    {
	    /// <summary>
	    ///     The info included in the associated field.
	    /// </summary>
	    public string Info = "";

	    /// <summary>
	    ///     Indicates whether the info is currently being viewed.
	    /// </summary>
	    public bool InfoViewed = false;

	    /// <summary>
	    ///     Attribute constructor.
	    /// </summary>
	    /// <param name="info"></param>
	    public InfoAttribute(string info)
        {
            Info = info;
        }
    }
}
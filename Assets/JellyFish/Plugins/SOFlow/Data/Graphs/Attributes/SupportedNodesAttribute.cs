// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using UnityEngine;

namespace SOFlow
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SupportedNodesAttribute : PropertyAttribute
    {
        /// <summary>
        ///     The list of supported nodes for the graph.
        /// </summary>
        public Type[] SupportedNodes;

        /// <summary>
        ///     Attribute constructor.
        /// </summary>
        /// <param name="supportedNodes"></param>
        public SupportedNodesAttribute(params Type[] supportedNodes)
        {
            SupportedNodes = supportedNodes;
        }
    }
}
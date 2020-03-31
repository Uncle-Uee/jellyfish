// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;

namespace SOFlow.Data.Graphs
{
    public abstract class TreeGraph : TraversableGraph
    {
	    /// <summary>
	    ///     The list of nodes already visited within this tree graph.
	    /// </summary>
	    public List<TraversableNode> VisitedNodes = new List<TraversableNode>();

        /// <inheritdoc />
        public override void Start()
        {
            VisitedNodes.Clear();

            base.Start();
        }

        /// <inheritdoc />
        public override void SignalNodeTriggered()
        {
            if(CurrentNode != null && !VisitedNodes.Contains(CurrentNode)) VisitedNodes.Add(CurrentNode);

            base.SignalNodeTriggered();
        }
    }
}
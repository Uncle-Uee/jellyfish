// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using SOFlow.Data.Events;
using UltEvents;
using XNode;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [SupportedNodes(typeof(BranchNode), typeof(EventNode))]
    public abstract class TraversableGraph : NodeGraph
    {
	    /// <summary>
	    ///     The current node.
	    /// </summary>
	    public TraversableNode CurrentNode;

	    /// <summary>
	    ///     The entry node for this graph.
	    /// </summary>
	    public EntryNode EntryNode;

	    /// <summary>
	    ///     Event raised when the end of this traversable graph has been reached.
	    /// </summary>
	    public GameEvent OnGraphEndReached;

	    /// <summary>
	    ///     Event raised when a node is triggered within this graph.
	    /// </summary>
	    public UltEvent OnNodeTriggered;

	    /// <summary>
	    ///     Begins traversal of this graph.
	    /// </summary>
	    public virtual void Start()
        {
            EntryNode.FirstNode.TriggerNode();
        }

	    /// <summary>
	    ///     Traverses to the next node within the graph.
	    /// </summary>
	    public void TraverseToNextNode()
        {
            if(CurrentNode != null)
                CurrentNode.TraverseToNextNode();
            else
                SignalEndReached();
        }

	    /// <summary>
	    ///     Raises a signal that a node has been triggered within the graph.
	    /// </summary>
	    public virtual void SignalNodeTriggered()
        {
            OnNodeTriggered.Invoke();
        }

	    /// <summary>
	    ///     Raises a signal that the end of the graph has been reached.
	    /// </summary>
	    public virtual void SignalEndReached()
        {
            OnGraphEndReached.Raise();
        }

        /// <inheritdoc />
        public override Node AddNode(Type type)
        {
            if(type == typeof(EntryNode))
            {
                if(EntryNode == null)
                {
                    EntryNode = (EntryNode)base.AddNode(type);

                    return EntryNode;
                }

                return null;
            }

            return base.AddNode(type);
        }
    }
}
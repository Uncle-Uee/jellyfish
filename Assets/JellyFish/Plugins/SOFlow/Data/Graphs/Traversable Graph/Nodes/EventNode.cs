// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UltEvents;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [CreateNodeMenu("SOFlow/Logic/Event")]
    public class EventNode : TraversableNode
    {
        /// <summary>
        ///     The event raised by this node.
        /// </summary>
        public UltEvent Event;

        /// <inheritdoc />
        public override void TriggerNode()
        {
            base.TriggerNode();

            Event.Invoke();

            TraverseToNextNode();
        }

        /// <inheritdoc />
        public override void TraverseToNextNode()
        {
            TraversableGraph traversableGraph = (TraversableGraph)graph;

            TraversableNode exit = GetConnectedNode("Exit");

            if(exit != null)
                exit.TriggerNode();
            else
                traversableGraph.SignalEndReached();
        }
    }
}
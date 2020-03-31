// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using XNode;
#if UNITY_EDITOR
using XNodeEditor;
using UnityEditor;

#endif

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    public abstract class TraversableNode : Node
    {
        /// <summary>
        ///     Triggers this node within the graph.
        /// </summary>
        public virtual void TriggerNode()
        {
            TraversableGraph traversableGraph = (TraversableGraph)graph;

            if(traversableGraph != null) traversableGraph.CurrentNode = this;

            traversableGraph.SignalNodeTriggered();

#if UNITY_EDITOR
            EditorWindow.GetWindow<NodeEditorWindow>("xNode", false)?.Repaint();
#endif
        }

        /// <summary>
        ///     Traverses to the next connection of this node.
        /// </summary>
        public abstract void TraverseToNextNode();

        /// <inheritdoc />
        public override object GetValue(NodePort port)
        {
            return null;
        }

        /// <summary>
        ///     Gets the connected node for the given output port.
        /// </summary>
        /// <param name="port"></param>
        public TraversableNode GetConnectedNode(string port)
        {
            NodePort defaultPort = GetOutputPort(port);
            NodePort connection  = defaultPort?.Connection;

            return (TraversableNode)connection?.node;
        }
    }
}
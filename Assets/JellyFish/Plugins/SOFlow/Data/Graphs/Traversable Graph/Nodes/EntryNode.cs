// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using XNode;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [CreateNodeMenu("")]
    public sealed class EntryNode : Node
    {
	    /// <summary>
	    ///     The first node this entry node is connected to.
	    /// </summary>
	    [Output(connectionType = ConnectionType.Override)]
        public TraversableNode FirstNode;

        /// <inheritdoc />
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);

            FirstNode = (TraversableNode)to.node;
        }

        /// <inheritdoc />
        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);

            FirstNode = null;
        }

        /// <inheritdoc />
        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
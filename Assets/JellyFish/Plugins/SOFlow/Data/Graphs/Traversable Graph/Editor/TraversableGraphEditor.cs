// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using UnityEngine;
using System;
using XNodeEditor;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [CustomNodeGraphEditor(typeof(TraversableGraph))]
    public class TraversableGraphEditor : NodeGraphEditor
    {
        /// <inheritdoc />
        public override void OnOpen()
        {
            base.OnOpen();

            if(((TraversableGraph)target).EntryNode == null) CreateNode(typeof(EntryNode), new Vector2(-104f, -40f));
        }

        /// <inheritdoc />
        public override string GetNodeMenuName(Type type)
        {
            object[] attributes = target.GetType().GetCustomAttributes(true);

            foreach(object attribute in attributes)
                if(attribute is SupportedNodesAttribute)
                {
                    SupportedNodesAttribute nodeAttribute = (SupportedNodesAttribute)attribute;

                    foreach(Type supportedNode in nodeAttribute.SupportedNodes)
                        if(supportedNode == type || supportedNode.IsSubclassOf(type))
                            return base.GetNodeMenuName(type);
                }

            return null;
        }
    }
}
#endif
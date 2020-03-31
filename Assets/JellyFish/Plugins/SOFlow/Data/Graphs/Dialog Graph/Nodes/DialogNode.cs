// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using SOFlow.Data.Primitives;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [CreateNodeMenu("SOFlow/Dialog/Text Dialog")]
    public class DialogNode : TraversableNode
    {
        /// <summary>
        ///     The list of choices for this dialog.
        /// </summary>
        [Output(dynamicPortList = true)]
        public List<string> Choices = new List<string>();

        /// <summary>
        ///     The dialog.
        /// </summary>
        public StringField Dialog;

        /// <inheritdoc />
        public override void TriggerNode()
        {
            base.TriggerNode();

            DialogGraph dialogGraph = (DialogGraph)graph;
            dialogGraph.DialogString = Dialog;
        }

        /// <inheritdoc />
        public override void TraverseToNextNode()
        {
            DialogGraph dialogGraph = (DialogGraph)graph;

            if(Choices.Count > 0)
            {
                TraversableNode choice = GetConnectedNode($"{nameof(Choices)} {dialogGraph.DialogChoice}");

                if(choice != null)
                    choice.TriggerNode();
                else
                    dialogGraph.SignalEndReached();
            }
            else
            {
                TraversableNode exit = GetConnectedNode("Exit");

                if(exit != null)
                    exit.TriggerNode();
                else
                    dialogGraph.SignalEndReached();
            }
        }
    }
}
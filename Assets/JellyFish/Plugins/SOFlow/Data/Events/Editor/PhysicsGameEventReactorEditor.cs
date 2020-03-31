// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using UnityEditor;
using SOFlow.Internal;

namespace SOFlow.Data.Events
{
	[CustomEditor(typeof(PhysicsGameEventReactor))]
	public class PhysicsGameEventReactorEditor : SOFlowCustomEditor
	{
		/// <summary>
		/// The PhysicsGameEventReactor target.
		/// </summary>
		private PhysicsGameEventReactor _target;

		private void OnEnable()
		{
			_target = (PhysicsGameEventReactor)target;
		}

		/// <inheritdoc />
		protected override void DrawCustomInspector()
		{
			base.DrawCustomInspector();

			SOFlowEditorUtilities.DrawPrimaryLayer(() =>
			                                       {
				                                       serializedObject.DrawProperty(nameof(_target
					                                                                           .ListenForCollisionEnter
				                                                                     ));

				                                       if(_target.ListenForCollisionEnter)
				                                       {
					                                       serializedObject
						                                      .DrawProperty(nameof(_target.OnCollisionEnterEvent));
				                                       }

				                                       serializedObject.DrawProperty(nameof(_target
					                                                                           .ListenForCollisionExit
				                                                                     ));

				                                       if(_target.ListenForCollisionExit)
				                                       {
					                                       serializedObject
						                                      .DrawProperty(nameof(_target.OnCollisionExitEvent));
				                                       }

				                                       serializedObject.DrawProperty(nameof(_target
					                                                                           .ListenForCollisionStay
				                                                                     ));

				                                       if(_target.ListenForCollisionStay)
				                                       {
					                                       serializedObject
						                                      .DrawProperty(nameof(_target.OnCollisionStayEvent));
				                                       }

				                                       serializedObject
					                                      .DrawProperty(nameof(_target.ListenForTriggerEnter));

				                                       if(_target.ListenForTriggerEnter)
				                                       {
					                                       serializedObject
						                                      .DrawProperty(nameof(_target.OnTriggerEnterEvent));
				                                       }

				                                       serializedObject.DrawProperty(nameof(_target.ListenForTriggerExit
				                                                                     ));

				                                       if(_target.ListenForTriggerExit)
				                                       {
					                                       serializedObject
						                                      .DrawProperty(nameof(_target.OnTriggerExitEvent));
				                                       }

				                                       serializedObject.DrawProperty(nameof(_target.ListenForTriggerStay
				                                                                     ));

				                                       if(_target.ListenForTriggerStay)
				                                       {
					                                       serializedObject
						                                      .DrawProperty(nameof(_target.OnTriggerStayEvent));
				                                       }
			                                       });
		}
	}
}
#endif
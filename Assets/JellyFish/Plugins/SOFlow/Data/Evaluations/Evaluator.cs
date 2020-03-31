// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using SOFlow.Data.Primitives;
using SOFlow.Extensions;
using UnityEngine;

namespace SOFlow.Data.Evaluations
{
    public class Evaluator : MonoBehaviour
    {
	    /// <summary>
	    ///     Enable for any single comparison to succeed this evaluator.
	    /// </summary>
	    public BoolField Any = new BoolField();

	    /// <summary>
	    ///     The comparisons to resolve by this evaluator.
	    /// </summary>
	    public List<Comparison> Comparisons = new List<Comparison>();

	    /// <summary>
	    ///     Evaluates all comparisons.
	    /// </summary>
	    /// <returns></returns>
	    public bool Evaluate()
        {
            Comparisons.ValidateListEntries();

            if(Any)
            {
                foreach(Comparison comparison in Comparisons)
                    if(comparison.Evaluate())
                        return true;

                return false;
            }

            foreach(Comparison comparison in Comparisons)
                if(!comparison.Evaluate())
                    return false;

            return true;
        }

#if UNITY_EDITOR
	    /// <summary>
	    ///     Adds an Evaluator to the scene.
	    /// </summary>
	    [UnityEditor.MenuItem("GameObject/SOFlow/Evaluations/Add Evaluator", false, 10)]
	    public static void AddComponentToScene()
	    {
		    GameObject _gameObject = new GameObject("Evaluator", typeof(Evaluator));

		    if(UnityEditor.Selection.activeTransform != null)
		    {
			    _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
		    }

		    UnityEditor.Selection.activeGameObject = _gameObject;
	    }
#endif
    }
}
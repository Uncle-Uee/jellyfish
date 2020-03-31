// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using SOFlow.Extensions;
using SOFlow.Data.Events;
using SOFlow.Data.Primitives;
using UltEvents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOFlow.Data.Evaluations
{
    [ExecuteInEditMode]
    public class Comparison : MonoBehaviour, IEventListener
    {
        /// <summary>
        ///     All types available for comparisons.
        /// </summary>
        public enum ComparisonTypes
        {
            Int    = 0,
            Float  = 1,
            String = 2,
            Bool   = 3,
            Data   = 4,
            Method = 5
        }

        /// <summary>
        ///     All operators available for data comparisons.
        /// </summary>
        public enum DataOperators
        {
            Full = 0,
            Type = 1
        }

        /// <summary>
        ///     All operators available for primitive comparisons.
        /// </summary>
        public enum PrimitiveOperators
        {
            Equal            = 0,
            NotEqual         = 1,
            LessThan         = 2,
            GreaterThan      = 3,
            LessThanEqual    = 4,
            GreaterThanEqual = 5,
            Between          = 6,
            Outside          = 7
        }

        /// <summary>
        ///     The game object reference.
        /// </summary>
        private GameObject _gameObjectReference;

        /// <summary>
        ///     The result to return after evaluation.
        /// </summary>
        private bool _result;

        /// <summary>
        ///     The type of comparison to make.
        /// </summary>
        public ComparisonTypes ComparisonType;

        /// <summary>
        ///     The data operator to use for this comparison.
        /// </summary>
        public DataOperators DataOperator;

        // The bool values.
        public BoolField FirstBool = new BoolField();

        // The data values.
        public ScriptableObject FirstData;

        // The float values.
        public FloatField FirstFloat = new FloatField();

        // The int values.
        public IntField FirstInt = new IntField();

        // The string values.
        public StringField FirstString = new StringField();

        /// <summary>
        ///     The list of action responses that occur when this comparison fails.
        /// </summary>
        public UltEvent OnComparisonFail = new UltEvent();

        /// <summary>
        ///     The list of action responses that occur when this comparison succeeds.
        /// </summary>
        public UltEvent OnComparisonSuccess = new UltEvent();

        /// <summary>
        ///     The primitive operator to use for this comparison.
        /// </summary>
        public PrimitiveOperators PrimitiveOperator;

        /// <summary>
        ///  The conditional event.
        /// </summary>
        public ConditionalEvent ConditionalEvent = new ConditionalEvent();

        /// <summary>
        ///     An override used for invoking responses during edit time.
        /// </summary>
        public bool RespondInEditor;

        public BoolField        SecondBool = new BoolField();
        public ScriptableObject SecondData;
        public FloatField       SecondFloat = new FloatField();
        public IntField         SecondInt = new IntField();
        public StringField      SecondString = new StringField();
        public FloatField       ThirdFloat = new FloatField();
        public IntField         ThirdInt = new IntField();

        /// <summary>
        ///     The event that will trigger this comparison.
        /// </summary>
        public GameEvent Trigger;

        /// <inheritdoc />
        public void OnEventRaised(GameEvent raisedEvent)
        {
            Evaluate();
        }

        /// <inheritdoc />
        public GameObject GetGameObject()
        {
            return _gameObjectReference;
        }

        /// <inheritdoc />
        public Type GetObjectType()
        {
            return GetType();
        }

        /// <inheritdoc />
        public List<GameEvent> GetEvents()
        {
            return new List<GameEvent>
                   {
                       Trigger
                   };
        }

        private void OnEnable()
        {
            _gameObjectReference = gameObject;

            if(!Application.isPlaying) return;

            Trigger?.RegisterListener(this);
        }

        private void OnDisable()
        {
            if(!Application.isPlaying) return;

            Trigger?.DeregisterListener(this);
        }

        /// <summary>
        ///     Evaluates this comparison operation.
        /// </summary>
        /// <returns></returns>
        public bool Evaluate()
        {
            _result = false;

            switch(ComparisonType)
            {
                case ComparisonTypes.Int:
                    _result = Compare(FirstInt.Value, SecondInt.Value, ThirdInt.Value);

                    break;
                case ComparisonTypes.Float:
                    _result = Compare(FirstFloat.Value, SecondFloat.Value, ThirdFloat.Value);

                    break;
                case ComparisonTypes.String:
                    _result = Compare(FirstString.Value, SecondString.Value, null);

                    break;
                case ComparisonTypes.Bool:
                    _result = Compare(FirstBool.Value, SecondBool.Value, null);

                    break;
                case ComparisonTypes.Data:
                    _result = CompareData(FirstData, SecondData);

                    break;
                case ComparisonTypes.Method:
                    _result = ConditionalEvent.Invoke();
                    
                    break;
            }

            // Only invoke the response while we are playing.
            // This avoids any issues where actions are dependent on play mode only functionality.
            if(Application.isPlaying || RespondInEditor)
            {
                if(_result)
                    OnComparisonSuccess.Invoke();
                else
                    OnComparisonFail.Invoke();
            }

            return _result;
        }

        /// <summary>
        ///     Compares the parameters according to the operation to evaluate.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <returns></returns>
        private bool Compare(IComparable first, IComparable second, IComparable third)
        {
            if(!TestComparisonParameters(first, second)) return false;

            switch(PrimitiveOperator)
            {
                case PrimitiveOperators.Equal:

                    return first.CompareTo(second) == 0;
                case PrimitiveOperators.NotEqual:

                    return first.CompareTo(second) != 0;
                case PrimitiveOperators.LessThan:

                    return first.CompareTo(second) < 0;
                case PrimitiveOperators.GreaterThan:

                    return first.CompareTo(second) > 0;
                case PrimitiveOperators.LessThanEqual:

                    return first.CompareTo(second) <= 0;
                case PrimitiveOperators.GreaterThanEqual:

                    return first.CompareTo(second) >= 0;
                case PrimitiveOperators.Between:

                    if(third == null)
                    {
                        Debug.LogWarning("[Comparison] Third operand is null.\n" +
                                         "<<Culprit>> "                          + transform.GetPath());

                        return false;
                    }

                    return first.CompareTo(second) >= 0 && first.CompareTo(third) <= 0;
                case PrimitiveOperators.Outside:

                    if(third == null)
                    {
                        Debug.LogWarning("[Comparison] Third operand is null.\n" +
                                         "<<Culprit>> "                          + transform.GetPath());

                        return false;
                    }

                    return first.CompareTo(second) < 0 || first.CompareTo(third) > 0;
            }

            return false;
        }

        /// <summary>
        ///     Compares the parameters according to the operation to evaluate.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private bool CompareData(Object first, Object second)
        {
            if(!TestComparisonParameters(first, second)) return false;

            object firstOperand  = first;
            object secondOperand = second;

            switch(DataOperator)
            {
                case DataOperators.Type:
                    firstOperand  = first.GetType();
                    secondOperand = second.GetType();

                    break;
            }

            switch(PrimitiveOperator)
            {
                case PrimitiveOperators.Equal:

                    return firstOperand == secondOperand;
                case PrimitiveOperators.NotEqual:

                    return firstOperand != secondOperand;
            }

            return false;
        }

        /// <summary>
        ///     Verifies the given parameters are available for comparing.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private bool TestComparisonParameters(object first, object second)
        {
            if(first == null)
            {
                Debug.LogWarning("[Comparison] First operand is null.\n" +
                                 "<<Culprit>> "                          + transform.GetPath());

                return false;
            }

            if(second == null)
            {
                Debug.LogWarning("[Comparison] Second operand is null.\n" +
                                 "<<Culprit>> "                           + transform.GetPath());

                return false;
            }

            return true;
        }
        
#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Comparison to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Evaluations/Add Comparison", false, 10)]
        public static void AddComponentToScene()
        {
            GameObject _gameObject = new GameObject("Comparison", typeof(Comparison));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}
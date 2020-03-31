// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using SOFlow.Utilities;
using UnityEngine;

namespace SOFlow.Data.Primitives
{
    [Serializable]
    public class Vector2Field : DataField
    {
        /// <summary>
        ///     The explicitly inferred type for the ConstantValue property.
        /// </summary>
        [HideInInspector]
        public Vector2 ConstantValueType;

        /// <summary>
        ///     The explicitly inferred type for the Variable property.
        /// </summary>
        [HideInInspector]
        public Vector2Data VariableType;

        /// <summary>
        /// Event raised when the constant value of this field changes.
        /// </summary>
        [HideInInspector]
        public Vector2Event OnConstantValueChanged = new Vector2Event();

        /// <summary>
        ///     The value of this field.
        /// </summary>
        public Vector2 Value
        {
            get
            {
                if(UseConstant) return ConstantValue;

                if(Variable == null) return ConstantValue;

                return Variable.Value;
            }
            set
            {
                if(UseConstant)
                {
                    if(!ConstantValue.Equals(value))
                    {
                        ConstantValue = value;

                        OnConstantValueChanged.Invoke(ConstantValue);
                    }
                }
                else
                {
                    if(Variable != null) Variable.Value = value;
                }
            }
        }

        /// <summary>
        ///     The volatile data for this field.
        /// </summary>
        public Vector2 ConstantValue
        {
            get => ConstantValueType;
            set => ConstantValueType = value;
        }

        /// <summary>
        ///     The non-volatile data for this field.
        /// </summary>
        public Vector2Data Variable
        {
            get => VariableType;
            set => VariableType = value;
        }

        /// <summary>
        /// Indicates whether the value changed event should be displayed.
        /// </summary>
#pragma warning disable 0414
        [SerializeField, HideInInspector]
        private bool _displayValueChangedEvent = false;
#pragma warning restore 0414

        public Vector2Field()
        {
        }

        public Vector2Field(Vector2 value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Value.x},{Value.y}";
        }

        /// <summary>
        ///     Implicit conversion to the corresponding data type.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static implicit operator Vector2(Vector2Field data)
        {
            return data.Value;
        }

        /// <inheritdoc />
        public override PrimitiveData GetVariable()
        {
            return Variable;
        }
    }
}
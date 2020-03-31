// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using SOFlow.Utilities;
using UnityEngine;

namespace SOFlow.Data.Primitives
{
    [Serializable]
    public class ColorField : DataField
    {
        /// <summary>
        ///     The explicitly inferred type for the ConstantValue property.
        /// </summary>
        [HideInInspector]
        public Color ConstantValueType;

        /// <summary>
        ///     The explicitly inferred type for the Variable property.
        /// </summary>
        [HideInInspector]
        public ColorData VariableType;

        /// <summary>
        /// Event raised when the constant value of this field changes.
        /// </summary>
        [HideInInspector]
        public ColorEvent OnConstantValueChanged = new ColorEvent();

        /// <summary>
        ///     The value of this field.
        /// </summary>
        public Color Value
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
        public Color ConstantValue
        {
            get => ConstantValueType;
            set => ConstantValueType = value;
        }

        /// <summary>
        ///     The non-volatile data for this field.
        /// </summary>
        public ColorData Variable
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

        public ColorField()
        {
        }

        public ColorField(Color value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        ///     Implicit conversion to the corresponding data type.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static implicit operator Color(ColorField data)
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
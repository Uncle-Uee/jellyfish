// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace SOFlow.Utilities
{
    /// <inheritdoc />
    public class SOFlowScriptableObject : ScriptableObject
    {
        protected bool Equals(SOFlowScriptableObject other)
        {
            return base.Equals(other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;

            if(ReferenceEquals(this, obj)) return true;

            if(obj.GetType() != GetType()) return false;

            return Equals((SOFlowScriptableObject)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(SOFlowScriptableObject first, SOFlowScriptableObject second)
        {
            try
            {
                return first?.name == second?.name;
            }
            catch
            {
                return false;
            }
        }

        public static bool operator !=(SOFlowScriptableObject first, SOFlowScriptableObject second)
        {
            return !(first == second);
        }
    }
}
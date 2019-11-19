/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Internal.Utilities
{
    /// <summary>
    /// Vector Methods that Aid with Vector Calculations.
    /// </summary>
    public static class VectorUtility
    {
        #region VECTOR METHODS

        /// <summary>
        /// Get Angle From Vector as Float.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static float GetAngleFromVectorFloat(Vector3 direction)
        {
            direction = direction.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle += angle < 0 ? 360f : 0f;

            return angle;
        }

        /// <summary>
        /// Get Angle From Vector as Integer.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static int GetAngleFromVectorInt(Vector3 direction)
        {
            return Mathf.RoundToInt(GetAngleFromVectorFloat(direction));
        }

        /// <summary>
        /// Get Angle From Vector 180.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static int GetAngleFromVector180(Vector3 direction)
        {
            direction = direction.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Mathf.RoundToInt(angle);
        }

        /// <summary>
        /// Apply Rotation To Vector using Vector Rotation.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="vectorRotation"></param>
        /// <returns></returns>
        public static Vector3 ApplyRotationToVector(Vector3 vector, Vector3 vectorRotation)
        {
            return ApplyRotationToVector(vector, GetAngleFromVectorFloat(vectorRotation));
        }

        /// <summary>
        /// Apply Rotation to Vector using Float Angle.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 ApplyRotationToVector(Vector3 vector, float angle)
        {
            return Quaternion.Euler(0, 0, angle) * vector;
        }

        #endregion
    }
}
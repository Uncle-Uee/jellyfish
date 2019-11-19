/*
 *
 * Created By: Ubaidullah Effendi-Emjedi
 * Alias: Uee
 * Modified By: 
 *
 * Last Modified: 20 June 2019
 *
 *
 * This software is released under the terms of the
 * GNU license. See https://www.gnu.org/licenses/#GPL
 * for more information.
 *
 */

using UnityEngine;

public static class Vector2Extension
{
    /// <summary>
    /// Get Angle From Vector as Float.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static float GetAngleFromVectorFloat(this Vector2 direction)
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
    public static int GetAngleFromVectorInt(this Vector2 direction)
    {
        return Mathf.RoundToInt(direction.GetAngleFromVectorFloat());
    }

    /// <summary>
    /// Get Angle From Vector 180.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static int GetAngleFromVector180(this Vector2 direction)
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
    public static Vector2 ApplyRotationToVector(this Vector2 vector, Vector2 vectorRotation)
    {
        return ApplyRotationToVector(vector, GetAngleFromVectorFloat(vectorRotation));
    }

    /// <summary>
    /// Apply Rotation to Vector using Float Angle.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Vector2 ApplyRotationToVector(this Vector2 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }
}
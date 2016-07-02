/// Tim Tryzbiak, ootii, LLC
using System;
using UnityEngine;

namespace com.ootii.Helpers
{
	/// <summary>
	/// Static functions to help us
	/// </summary>
	public class NumberHelper
	{
		/// <summary>
		/// Create a single instance that we can reuse as needed
		/// </summary>
		public static System.Random Randomizer = new System.Random();

		/// <summary>
		/// Clamps the angle (in degrees) between the min and max
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="rAngle">Angle to clamp</param>
		/// <param name="rMin">Minimum value</param>
		/// <param name="rMax">Maximum value</param>
		public static float ClampAngle(float rAngle, float rMin, float rMax)
		{
			if (rAngle < -360) { rAngle += 360; }
			if (rAngle > 360) { rAngle -= 360; }
			return Mathf.Clamp(rAngle, rMin, rMax);
		}

		/// <summary>
		/// Ensure the angle stays within the 360 range
		/// </summary>
		/// <returns>The angle cleaned up angle</returns>
		/// <param name="rAngle">The initial angle</param>
		public static float NormalizeAngle(float rAngle)
		{
			if (rAngle < -360f) { rAngle += 360f; }
			if (rAngle > 360f) { rAngle -= 360f; }
			return rAngle;
		}

		/// <summary>
		/// Gets the horizontal angle between two vectors. The calculation
		/// removes any y components before calculating the angle.
		/// </summary>
		/// <returns>The signed horizontal angle (in degrees).</returns>
		/// <param name="rFrom">Angle representing the starting vector</param>
		/// <param name="rTo">Angle representing the resulting vector</param>
		public static float GetHorizontalAngle(Vector3 rFrom, Vector3 rTo)
		{			
			float lAngle = Mathf.Atan2(Vector3.Dot(Vector3.up, Vector3.Cross(rFrom, rTo)), Vector3.Dot(rFrom, rTo));
			lAngle *= Mathf.Rad2Deg;

			return lAngle;			
		}
		
		/// <summary>
		/// Gets the vector difference between two vectors minus the
		/// height component
		/// </summary>
		/// <returns>The horizontal difference.</returns>
		/// <param name="rFrom">R from.</param>
		/// <param name="rTo">R to.</param>
		/// <param name="rResult">R result.</param>
		public static float GetHorizontalDistance(Vector3 rFrom, Vector3 rTo)
		{
			rFrom.y = 0;
			rTo.y = 0;
			return (rTo - rFrom).magnitude;
		}
		
		/// <summary>
		/// Gets the vector difference between two vectors minus the
		/// height component
		/// </summary>
		/// <returns>The horizontal difference.</returns>
		/// <param name="rFrom">R from.</param>
		/// <param name="rTo">R to.</param>
		/// <param name="rResult">R result.</param>
		public static void GetHorizontalDifference(Vector3 rFrom, Vector3 rTo, ref Vector3 rResult)
		{
			rFrom.y = 0;
			rTo.y = 0;
			rResult = rTo - rFrom;
		}

		/// <summary>
		/// Gets the quaternion that represents the normalized rotation between
		/// the two vectors (minus the height component).
		/// </summary>
		/// <param name="rFrom">Source vector</param>
		/// <param name="rTo">Destination vector</param>
		/// <param name="rResult">Rotation result</param>
		public static void GetHorizontalQuaternion(Vector3 rFrom, Vector3 rTo, ref Quaternion rResult)
		{
			rFrom.y = 0;
			rTo.y = 0;
			rResult = Quaternion.LookRotation(rTo - rFrom);
		}
	}
}


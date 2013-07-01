using UnityEngine;
using System.Collections;

public static class AOC2Math {
	
	/// <summary>
	/// Gets the square of the ground distance (y-axis ignored)
	/// between two points.
	/// </summary>
	/// <returns>
	/// The distance square
	/// </returns>
	/// <param name='ptA'>
	/// Point a.
	/// </param>
	/// <param name='ptB'>
	/// Point b.
	/// </param>
	public static float GroundDistanceSqr(Vector3 ptA, Vector3 ptB)
	{
		return Mathf.Pow(ptA.x-ptB.x,2) + Mathf.Pow (ptA.z-ptB.z,2);
	}
	
}

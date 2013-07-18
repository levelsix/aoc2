using UnityEngine;
using System.Collections;
using System;

public static class AOC2Math {
	
	const int secondsPerGem = 400;
	
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
	
	public static int GemsForTime(long time)
	{
		return (int)Mathf.Ceil((float)(time / secondsPerGem));
	}
	
    public static long UnixTimeStamp(DateTime time)
    {
        return (long) (time - new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()).TotalSeconds;
    }
}

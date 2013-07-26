using UnityEngine;
using System.Collections;
using System;
using com.lvl6.proto;

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
	
    /// <summary>
    /// Takes an amount of time and figures out how many gems to charge
    /// </summary>
    /// <returns>
    /// The number of gems
    /// </returns>
    /// <param name='time'>
    /// Time, in seconds
    /// </param>
	public static int GemsForTime(long time)
	{
		return (int)Mathf.Ceil((float)(time / secondsPerGem));
	}
	
    /// <summary>
    /// Takes a DateTime object and turns it into a timestamp that adheres to
    /// Unix Standard Time
    /// </summary>
    /// <returns>
    /// The time stamp.
    /// </returns>
    /// <param name='time'>
    /// Time.
    /// </param>
    public static long UnixTimeStamp(DateTime time)
    {
        return (long) (time - new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()).TotalSeconds;
    }
    
    /// <summary>
    /// Turns a ColorProto into a Color object
    /// </summary>
    /// <returns>
    /// The color.
    /// </returns>
    /// <param name='proto'>
    /// Proto of the color
    /// </param>
    public static Color ProtoToColor(ColorProto proto)
    {
        return new Color(proto.red, proto.green, proto.blue);
    }
    
    /// <summary>
    /// Figures the attack speed modifier out from the stat
    /// </summary>
    /// <returns>
    /// The speed mod.
    /// </returns>
    /// <param name='attackSpeed'>
    /// Attack speed character stat
    /// </param>
    public static float AttackSpeedMod(int attackSpeed)
    {
        return (100f / (100f+attackSpeed));
    }
    
    public static float ResistanceMod(int resistance)
    {
        return (100f - resistance) / 100f;  
    }
}

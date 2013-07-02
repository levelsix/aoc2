using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Since Vector3 in C# is a value type rather than
/// a reference type, AOC2Position is a wrapper that
/// allows us to manipulate Vector3's in a simplier
/// fashion
/// </summary>
public class AOC2Position{
	
	/// <summary>
	/// The position that this is a wrapper around
	/// </summary>
	public Vector3 position;
	
	//We can't modify a Vector3's values directly,
	//however these properties will allow us to 
	//get and set these values in a much cleaner manner.
	
	/// <summary>
	/// Gets or sets the x.
	/// </summary>
	/// <value>
	/// The x.
	/// </value>
	public float x
	{
		set
		{
			position = new Vector3(value, position.y, position.z);
		}
		get
		{
			return position.x;
		}
	}
	
	/// <summary>
	/// Gets or sets the y.
	/// </summary>
	/// <value>
	/// The y.
	/// </value>
	public float y
	{
		set
		{
			position = new Vector3(position.x, value, position.z);
		}
		get
		{
			return position.y;
		}
	}
	
	/// <summary>
	/// Gets or sets the z.
	/// </summary>
	/// <value>
	/// The z.
	/// </value>
	public float z
	{
		set
		{
			position = new Vector3(position.x, position.y, value);
		}
		get
		{
			return position.z;
		}
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2Position"/> class.
	/// </summary>
	/// <param name='x'>
	/// X.
	/// </param>
	/// <param name='y'>
	/// Y.
	/// </param>
	/// <param name='z'>
	/// Z.
	/// </param>
	public AOC2Position(float x, float y, float z)
	{
		position = new Vector3(x,y,z);
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2Position"/> class.
	/// </summary>
	/// <param name='pos'>
	/// Position.
	/// </param>
	public AOC2Position(Vector3 pos)
	{
		position = pos;
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2Position"/> class.
	/// </summary>
	public AOC2Position()
	{
		position = Vector3.zero;
	}
}

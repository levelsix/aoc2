using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Logic state where the unit waits for a cast time, then immediately
/// moves to its target, followed by a delay before the next check
/// for exit states.
/// </summary>
public class AOC2LogicBlinkToTarget : AOC2LogicState {
	
	/// <summary>
	/// The delay time before use; casting time
	/// </summary>
	private float delayBef;
	
	/// <summary>
	/// The delay time after use, before the user can do anything else
	/// </summary>
	private float delayAft;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicBlinkToTarget"/> class.
	/// </summary>
	/// <param name='thisUnit'>
	/// This unit.
	/// </param>
	/// <param name='delayBefore'>
	/// Delay before.
	/// </param>
	/// <param name='delayAfter'>
	/// Delay after.
	/// </param>
	public AOC2LogicBlinkToTarget(AOC2Unit thisUnit, float delayBefore, float delayAfter)
		: base(thisUnit)
	{
		delayAft = delayAfter;
		delayBef = delayBefore;
	}
	
	/// <summary>
	/// Logic this instance.
	/// Waits for casting time, then moves the user, then stalls the user for the delay afterward
	/// </summary>
	public override IEnumerator Logic ()
	{
		while (true)
		{
			canBeInterrupt = false;
			yield return new WaitForSeconds(delayBef);
			_user.aPos.position = _user.targetPos.position;
			yield return new WaitForSeconds(delayAft);
			canBeInterrupt = true;
			_complete = true;
			yield return null;
		}
	}
}

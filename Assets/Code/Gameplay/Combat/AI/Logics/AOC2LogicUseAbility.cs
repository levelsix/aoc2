using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Logic for using a character ability. Waits for cast time before
/// activating the ability.
/// </summary>
public class AOC2LogicUseAbility : AOC2LogicState {
	
	/// <summary>
	/// The unit.
	/// </summary>
	private AOC2Unit _unit;
	
	/// <summary>
	/// The ability.
	/// </summary>
	private AOC2Ability _ability;
	
	/// <summary>
	/// The enemy flag.
	/// </summary>
	private bool _isEnemy;
	
	AOC2LogicState moveLogic;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicUseAbility"/> class.
	/// </summary>
	/// <param name='unit'>
	/// Unit.
	/// </param>
	/// <param name='abil'>
	/// Ability.
	/// </param>
	/// <param name='enemy'>
	/// Enemy flag.
	/// </param>
	public AOC2LogicUseAbility(AOC2Unit unit, AOC2Ability abil, bool enemy)
		: base()
	{
		_ability = abil;
		_unit = unit;
	}
	
	/// <summary>
	/// Logic this instance.
	/// Uses the given ability, including waiting for cast time.
	/// </summary>
	protected override IEnumerator Logic ()
	{
		while(true)
		{	
			//Wait for the cast time if there is one
			if (_ability.castTime > 0)
			{
				yield return new WaitForSeconds(_ability.castTime);
			}
			
			while(!_ability.Use(_unit, _unit.aPos.position, _unit.targetPos.position))
			{
				yield return null;
			}
			
			complete = true;
			yield return null;
		}
	}
	
}

using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// HFSM for navigating within range to use an ability, and then using that ability.
/// Completes when the ability has been used.
/// </summary>
public class AOC2LogicHighStateAbility : AOC2HFSMLogic {
	
	/// <summary>
	/// Logic to move within the range of the ability
	/// </summary>
	AOC2LogicState _moveInRange;
	
	/// <summary>
	/// Logic to use the ability
	/// </summary>
	AOC2LogicState _useAbility;
	
	/// <summary>
	/// The _sprint in attack distance.
	/// </summary>
	AOC2LogicState _sprintInAttackDistance;
	
	/// <summary>
	/// The ability that the user is using.
	/// </summary>
	AOC2Ability _ability;
	
	/// <summary>
	/// Gets a value indicating whether this <see cref="AOC2LogicHighStateAbility"/> is complete.
	/// </summary>
	/// <value>
	/// <c>true</c> if the ability has been used; otherwise, <c>false</c>.
	/// </value>
	override public bool Complete{
		get
		{
			return _useAbility.Complete;
		}
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicHighStateAbility"/> class.
	/// </summary>
	/// <param name='unit'>
	/// Unit.
	/// </param>
	/// <param name='ability'>
	/// Ability.
	/// </param>
	public AOC2LogicHighStateAbility(AOC2Unit unit, AOC2Ability ability)
		: base(null, unit)
	{
		_ability = ability;
		
		//float findGridSquareDist = _ability.range + AOC2ManagerReferences.gridManager.spaceSize;
		
		
		_moveInRange = new AOC2LogicNavigateTowardTarget(_user);
		//_moveSquare = new AOC2LogicFollowPath(_unit);
		_useAbility = new AOC2LogicUseAbility(_user, _ability);
		
		
		if (ability.attackDistance < ability.range)
		{
			_sprintInAttackDistance = new AOC2LogicSprint(_user);
			
			_moveInRange.AddExit(new AOC2ExitTargetInRange(_sprintInAttackDistance, _user, ability.range));
			_sprintInAttackDistance.AddExit(new AOC2ExitTargetInRange(_useAbility, _user, ability.attackDistance));
		}
		else
		{
			_moveInRange.AddExit(new AOC2ExitTargetInRange(_useAbility, _user, _ability.attackDistance));
		}
		
		
		//_moveSquare.AddExit(new AOC2ExitTargetInRange(_useAbility, _unit, _ability.range));
		//_moveSquare.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(null, _unit, findGridSquareDist), _moveInRange));
		
		_useAbility.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(null, _user, _ability.range), _moveInRange));
		
		current = _baseState = _moveInRange;
		
	}
	
	/// <summary>
	///  Initialize this instance. Sets the debug string.
	/// </summary>
	public override void Init ()
	{
		_user.currentLogicState = "LogicHighState";
		base.Init ();
	}

}
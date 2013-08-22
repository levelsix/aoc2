using UnityEngine;
using System.Collections;

public class AOC2LogicHighStateAbility : AOC2HFSMLogic {

	AOC2LogicState _moveInRange;
	
	//AOC2LogicState _moveSquare;
	
	AOC2LogicState _useAbility;
	
	AOC2Ability _ability;
	
	override public bool Complete
	{
		get
		{
			return _useAbility.Complete;
		}
	}
	
	public AOC2LogicHighStateAbility(AOC2Unit unit, AOC2Ability ability)
		: base(null, unit)
	{
		_ability = ability;
		
		//float findGridSquareDist = _ability.range + AOC2ManagerReferences.gridManager.spaceSize;
		
		_moveInRange = new AOC2LogicNavigateTowardTarget(_user);
		//_moveSquare = new AOC2LogicFollowPath(_unit);
		_useAbility = new AOC2LogicUseAbility(_user, _ability);
		
		_moveInRange.AddExit(new AOC2ExitTargetInRange(_useAbility, _user, _ability.range));
		
		//_moveSquare.AddExit(new AOC2ExitTargetInRange(_useAbility, _unit, _ability.range));
		//_moveSquare.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(null, _unit, findGridSquareDist), _moveInRange));
		
		_useAbility.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(null, _user, _ability.range), _moveInRange));
		
		current = _baseState = _moveInRange;
		
	}
	
	public override void OnExitState ()
	{
		base.OnExitState ();
	}
	
	public override void Init ()
	{
		_user.currentLogicState = "LogicHighState";
		base.Init ();
	}
}
